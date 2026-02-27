using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Presentation.Attributes;
using ECommerce.Services_Abstraction.IServices;
using ECommerce.Shared;
using ECommerce.Shared.Dtos.BasketDtos;
using ECommerce.Shared.Dtos.IdentityDtos;
using ECommerce.Shared.Dtos.OrderDtos;
using ECommerce.Shared.Dtos.ProductDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Presentation.Controllers
{
    #region Controllers
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var Result = await _authenticationService.LoginAsync(loginDto);
            return HandleResult(Result);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var Result = await _authenticationService.RegisterAsync(registerDto);
            return HandleResult(Result);
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var Result = await _authenticationService.CheckEmailAsync(email);
            return Ok(Result);
        }

        [Authorize]
        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UserDto>> GetUserByEmail()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email)!;
            var result = await _authenticationService.GetUserByEmailAsync(Email);
            return HandleResult(result);
        }
    }
    public class BasketsController : ApiBaseController
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet]
        public async Task<ActionResult<BasketDto>> GetBasket(string basketId)
        {
            var basket = await _basketService.GetBasketAsync(basketId);
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basketDto)
        {
            var basket = await _basketService.CreateOrUpdateBasketAsync(basketDto);
            return Ok(basket);
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
            var result = await _basketService.DeleteBasketAsync(basketId);
            return Ok(result);
        }
    }
    public class OrderController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto, string email)
        {
            var result = await _orderService.CreateOrderAsync(orderDto, email);
            return HandleResult(result);
        }

        [HttpGet("GetAllOrders")]
        public async Task<ActionResult<List<OrderToReturnDto>>> GetAllOrders(string email)
        {
            var result = await _orderService.GetAllOrderAsync(email);
            return HandleResult(result);
        }

        [HttpGet("GetOrderById")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid id)
        {
            var result = await _orderService.GetOrderByIdAsync(id);
            return HandleResult(result);
        }

        [HttpGet("GetAllDeliveryMethods")]
        public async Task<ActionResult<List<DeliveryMethodDto>>> GetAllDeliveryMethods()
        {
            var result = await _orderService.GetAllDeliveryMethodsAsync();
            return HandleResult(result);
        }
    }
    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [Authorize]
        [HttpGet]
        [RedisCache]
        public async Task<ActionResult<PaginationResult<ProductDto>>> GetAllProduct([FromQuery] ProductQueryParams queryParams)
        {
            var products = await _productService.GetAllProductAsync(queryParams);
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProductById(int id)
        {
            var Result = await _productService.GetProductByIdAsync(id);
            return HandleResult<ProductDto>(Result);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<BrandDto>>> GetAllBrands()
        {
            var brands = await _productService.GetAllBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IEnumerable<TypeDto>>> GetAllTypes()
        {
            var types = await _productService.GetAllTypesAsync();
            return Ok(types);
        }
    }
    #endregion
}
