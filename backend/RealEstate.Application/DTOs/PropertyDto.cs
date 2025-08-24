using System.ComponentModel.DataAnnotations;

public class PropertyDto
{
    public string Id { get; set; }
    public string IdOwner { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Address { get; set; }

    public decimal Price { get; set; }
    public string Image { get; set; }
    public List<PropertyTraceDto> Traces { get; set; }

}