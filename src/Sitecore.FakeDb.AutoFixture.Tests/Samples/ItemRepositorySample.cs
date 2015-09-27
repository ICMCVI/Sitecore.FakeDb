namespace Sitecore.FakeDb.AutoFixture.Tests.Samples
{
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Kernel;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.Data.Managers;
  using Xunit;

  public class ItemRepositorySample
  {
    [Theory, DefaultAutoData]
    public void SutCreatesNewItem(Repository sut, RepositoryItem itemToCreate)
    {
      sut.Create(itemToCreate);

      Assert.NotNull(sut.Root.Children[itemToCreate.Id]);
      Assert.NotNull(sut.Root.Children[itemToCreate.Name]);
    }

    [Theory, DefaultAutoData]
    public void CreateSavesTitle(Repository sut, RepositoryItem itemToCreate, RepDbTemplate template)
    {
      sut.Create(itemToCreate);

      Assert.Equal(itemToCreate.Title, sut.Root.Database.GetItem(itemToCreate.Id)["Title"]);
    }

    [Theory, DefaultAutoData]
    public void FindReturnsRepositoryItem(Repository sut, RepDbTemplate template, DbItem item, string title)
    {
      item.Fields.Add("Title", title);

      var result = sut.Find(item.ID);

      result.Should().NotBeNull();
      result.Name.Should().Be(item.Name);
      result.Title.Should().Be(title);
    }

    public class RepDbTemplate : DbTemplate
    {
      /// <param name="templateId">
      /// The parameter name 'templateId' is important here. It allows AutoFixture to share the ID
      /// between this template, SUT and a DbItem based on this template.
      /// 
      /// Implement AddFieldsOnMatchCustomization.
      /// </param>
      public RepDbTemplate(ID templateId)
        : base(templateId)
      {
        this.Fields.Add("Title");
      }
    }

    private class DefaultAutoDataAttribute : AutoDataAttribute
    {
      public DefaultAutoDataAttribute()
      {
        this.Fixture.Customize(new AutoDbCustomization())
          .Customize(new AutoContentCustomization())
          .Customize(new FreezeOnMatchCustomization(
            typeof(ID), new ParameterSpecification(typeof(ID), "templateId")));
      }
    }
  }

  public class Repository
  {
    private readonly Item root;

    private readonly ID templateId;

    public Repository(Item root, ID templateId)
    {
      this.root = root;
      this.templateId = templateId;
    }

    public Item Root
    {
      get { return this.root; }
    }

    public ID TemplateId
    {
      get { return this.templateId; }
    }

    public void Create(RepositoryItem repositoryItem)
    {
      var item = ItemManager.CreateItem(repositoryItem.Name, this.Root, this.TemplateId, repositoryItem.Id);
      using (new EditContext(item))
      {
        item["Title"] = repositoryItem.Title;
      }
    }

    public RepositoryItem Find(ID id)
    {
      var item = this.Root.Database.GetItem(id);
      return new RepositoryItem { Id = id, Name = item.Name, Title = item["Title"] };
    }
  }

  public class RepositoryItem
  {
    public ID Id { get; set; }

    public string Name { get; set; }

    public string Title { get; set; }
  }
}