using System.ComponentModel.DataAnnotations;

namespace ECommerce.Shared.Dtos.BasketDtos
{
    public record BasketItemDto
    (
        int Id,
        string ProductName,
        string PictureUrl,
        [Range(1, double.MaxValue)]
        decimal Price,
        [Range(1, 100)]
        int Quantity
    );
}