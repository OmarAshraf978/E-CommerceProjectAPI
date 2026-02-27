namespace ECommerce.Shared.Dtos.OrderDtos
{
    public record OrderAddressDto
    (
        string Street,
        string City,
        string Country,
        string FirstName,
        string LastName
    );
}