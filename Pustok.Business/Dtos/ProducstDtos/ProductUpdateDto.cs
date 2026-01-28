namespace Pustok.Business.Dtos;

public class ProductUpdateDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public Guid CategoryId { get; set; } 
    public decimal Price { get; set; }
}