namespace Sitecore.FakeDb.AutoFixture
{
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Kernel;

  public class DbFieldsCustomization : ICustomization
  {
    private readonly string[] fields;

    public DbFieldsCustomization(params string[] fields)
    {
      this.fields = fields;
    }

    public void Customize(IFixture fixture)
    {
      Diagnostics.Assert.ArgumentNotNull(fixture, "fixture");

      fixture.Customizations.Add(
        new FilteringSpecimenBuilder(
          new Postprocessor(
            new MethodInvoker(new GreedyConstructorQuery()),
            new AddFieldsSpecimenCommand(this.fields)),
          new DbItemSpecification()));
    }
  }
}