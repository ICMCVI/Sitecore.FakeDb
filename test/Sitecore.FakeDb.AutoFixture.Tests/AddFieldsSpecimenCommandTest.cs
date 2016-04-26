namespace Sitecore.FakeDb.AutoFixture.Tests
{
  using FluentAssertions;
  using Ploeh.AutoFixture.Kernel;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class AddFieldsSpecimenCommandTest
  {
    [Theory, AutoData]
    public void SutIsISpecimenCommand(AddFieldsSpecimenCommand sut)
    {
      Assert.IsAssignableFrom<ISpecimenCommand>(sut);
    }

    [Theory, AutoData]
    public void SutReceivesFields(AddFieldsSpecimenCommand sut)
    {
      Assert.NotNull(sut.Fields);
    }

    [Theory, AutoData]
    public void ExecuteAddsFieldsToDbItem(AddFieldsSpecimenCommand sut, DbItem item)
    {
      sut.Execute(item, null);
      item.Fields.Should().HaveCount(3);
    }

    [Theory, AutoData]
    public void ExecuteIgnoresNotDbItems(AddFieldsSpecimenCommand sut, object specimen)
    {
      sut.Execute(specimen, null);
    }
  }
}