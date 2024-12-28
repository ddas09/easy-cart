using EasyCart.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using EasyCart.Shared.ActionFilters;
using EasyCart.ProductService.Models.Request;
using EasyCart.ProductService.Services.Contracts;

namespace EasyCart.ProductService.Controllers
{
    /// <summary>
    /// Controller responsible for managing product related operations.
    /// </summary>
    [ApiController]
    [ValidateModelState]
    [Route("api/products")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly CustomResponse _customResponse = new();
        private readonly IProductService _productService = productService;

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="request">The request containing product details.</param>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductAddRequest request)
        {
            var product = await this._productService.AddProduct(request);
            return _customResponse.Success(message: "Product added successfully.", data: product);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="request">The request containing updated product details.</param>
        [HttpPut("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromBody] ProductUpdateRequest request)
        {
            if (productId != request.Id)
            {
                return _customResponse.BadRequest(message: "Product ID in URL does not match the ID in the request.");
            }

            var product = await this._productService.UpdateProduct(request);
            return _customResponse.Success(message: "Product updated successfully.", data: product);
        }

        /// <summary>
        /// Deactivates a product.
        /// </summary>
        /// <param name="productId">The ID of the product to update.</param>
        /// <param name="request">The request containing updated product details.</param>
        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete([FromRoute] int productId, [FromBody] ProductDeleteRequest request)
        {
            if (productId != request.Id)
            {
                return _customResponse.BadRequest(message: "Product ID in URL does not match the ID in the request.");
            }

            await this._productService.DeleteProduct(request);
            return _customResponse.Success(message: "Product deleted successfully.");
        }

        /// <summary>
        /// Returns product details with the id.
        /// </summary>
        /// <param name="productId">The ID of the product.</param>
        [HttpGet("{productId}")]
        public async Task<IActionResult> Get([FromRoute] int productId)
        {
            var product = await this._productService.GetProduct(productId);
            return _customResponse.Success(data: product);
        }

        /// <summary>
        /// Returns product that matches the search criteria.
        /// </summary>
        /// <param name="searchText">Search text to filter products.</param>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string searchText)
        {
            var product = await this._productService.GetAllProducts(searchText);
            return _customResponse.Success(data: product);
        }
    }
}
