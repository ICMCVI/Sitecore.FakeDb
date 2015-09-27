namespace Sitecore.FakeDb.AutoFixture.Tests
{
  using System;
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Kernel;
  using Ploeh.AutoFixture.Xunit2;
  using Sitecore.Data;
  using Xunit;

  public class AutoContentDbItemCustomizationTest
  {
    [Theory, AutoData]
    public void SutIsCustomization(AutoContentDbItemCustomization sut)
    {
      Assert.IsAssignableFrom<ICustomization>(sut);
    }

    [Theory, AutoData]
    public void CustomizeThrowsIfFeatureIsNull(AutoContentDbItemCustomization sut)
    {
      Assert.Throws<ArgumentNullException>(() => sut.Customize(null));
    }

    [Fact]
    public void SutSharesTempalteIdWithFreezeOnMatchCustomization()
    {
      var fixture = new Fixture()
        .Customize(new AutoContentDbItemCustomization())
        .Customize(new FreezeOnMatchCustomization(typeof(ID), new ParameterSpecification(typeof(ID), "templateId")));

      fixture.Create<CustomDbTemplate>().ID.Should().BeSameAs(fixture.Create<DbItem>().TemplateID);
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private class CustomDbTemplate : DbTemplate
    {
      public CustomDbTemplate(ID templateId)
        : base(templateId)
      {
      }
    }
  }
}