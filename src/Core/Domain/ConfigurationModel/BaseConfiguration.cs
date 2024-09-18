using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ConfigurationModel;

public abstract class BaseConfiguration
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int ConfigurationTypeId { get; set; }
    public bool IsActive { get; set; }
}
