namespace Sitecore.FakeDb.AutoFixture
{
  using System.Collections.Generic;
  using Ploeh.AutoFixture.Kernel;

  /// <summary>
  /// Populates <see cref="DbItem"/>'s and <see cref="DbTemplate"/>'s with specimens.
  /// </summary>
  public class AddFieldsSpecimenCommand : ISpecimenCommand
  {
    private readonly IEnumerable<string> fields;

    /// <summary>
    /// Initializes a new instance of <see cref="AddFieldsSpecimenCommand"/> with the supplied fields.
    /// </summary>
    /// <param name="fields">
    /// The fields to be added to the <see cref="DbItem"/> or <see cref="DbTemplate"/>.
    /// </param>
    public AddFieldsSpecimenCommand(params string[] fields)
    {
      this.fields = fields;
    }

    /// <summary>
    /// Gets the fields contained by the instance.
    /// </summary>
    public IEnumerable<string> Fields
    {
      get { return this.fields; }
    }

    /// <summary>
    /// Adds many fields to an item or template.
    /// </summary>
    /// <param name="specimen">The item or template to which fields should be added.</param>
    /// <param name="context">The context which can be used to resolve other specimens.</param>
    public void Execute(object specimen, ISpecimenContext context)
    {
      var item = specimen as DbItem;
      if (item == null)
      {
        return;
      }

      foreach (var field in this.Fields)
      {
        item.Add(field, null);
      }
    }
  }
}