namespace Sitecore.FakeDb.AutoFixture.Tests
{
  using System;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Xunit2;
  using Xunit;

  public class DbFieldsCustomizationTest
  {
    [Theory, AutoData]
    public void SutIsCustomization(DbFieldsCustomization sut)
    {
      Assert.IsAssignableFrom<ICustomization>(sut);
    }

    [Theory, AutoData]
    public void CustomizeThrowsIfFeatureIsNull(DbFieldsCustomization sut)
    {
      Assert.Throws<ArgumentNullException>(() => sut.Customize(null));
    }

    [Theory, AutoData]
    public void CustomizeAddsFields(DbFieldsCustomization sut)
    {
      var fixture = new Fixture();

      sut.Customize(fixture);

      fixture.Create<DbItem>().Fields.Should().HaveCount(3);
    }
  }
}