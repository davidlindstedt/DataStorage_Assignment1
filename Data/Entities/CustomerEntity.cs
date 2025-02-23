namespace Data.Entities;

public class CustomerEntity
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
    public virtual ICollection<ProjectEntity> Projects { get; set; } = [];

}
