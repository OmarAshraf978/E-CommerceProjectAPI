namespace ECommerce.Shared.Dtos.OrderDtos
{
    public record OrderItemDto
    (
        string ProductName,
        string PictureUrl,
        decimal Price,
        int Quantity
    );
}