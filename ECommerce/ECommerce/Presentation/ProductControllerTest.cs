//using ECommerce.Business;
//using ECommerce.Presentation;
//using ECommerce.DataAccess;
//using FluentValidation;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using System.Threading.Tasks;
//using Xunit;
//using ECommerce.Factorymethod;

//namespace ECommerce.Tests
//{
//    public class ProductControllerTests
//    {
//        private readonly Mock<ProductService> _mockProductService;
//        private readonly Mock<ECommerceContext> _mockContext;
//        private readonly Mock<IValidator<ProductDto>> _mockProductDtoValidator;
//        private readonly Mock<IValidator<Product>> _mockProductValidator;
//        private readonly ProductController _controller;

//        public ProductControllerTests()
//        {
//            _mockProductService = new Mock<ProductService>();
//            _mockContext = new Mock<ECommerceContext>();
//            _mockProductDtoValidator = new Mock<IValidator<ProductDto>>();
//            _mockProductValidator = new Mock<IValidator<Product>>();
//            _controller = new ProductController(_mockProductService.Object, _mockContext.Object);
//        }

//        [Fact]
//        public async Task GetProductById_ReturnsOkResult_WhenProductExists()
//        {
//            Arrange
//           var productId = 1;
//            var product = new Product { Id = productId, Name = "Product1", Price = 100 };
//            _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
//                               .ReturnsAsync(product);

//            Act
//           var result = await _controller.GetProductById(productId);

//            Assert
//           var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(200, okResult.StatusCode);
//            var returnedProduct = Assert.IsType<Product>(okResult.Value);
//            Assert.Equal(productId, returnedProduct.Id);
//        }

//        [Fact]
//        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
//        {
//            Arrange
//           var productId = 1;
//            _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
//                               .ReturnsAsync((Product)null);

//            Act
//           var result = await _controller.GetProductById(productId);

//            Assert
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public async Task CreateProduct_ReturnsOkResult_WhenValidProductDto()
//        {
//            Arrange
//           var productDto = new ProductDto { Name = "Product1", Price = 100 };
//            _mockProductDtoValidator.Setup(v => v.ValidateAsync(productDto, default))
//                                    .ReturnsAsync(new FluentValidation.Results.ValidationResult());

//            var createdProduct = new Product { Id = 1, Name = productDto.Name, Price = productDto.Price };
//            _mockProductService.Setup(x => x.CreateProductAsync(productDto.Name, productDto.Price))
//                               .ReturnsAsync(createdProduct);

//            Act
//           var result = await _controller.CreateProduct(productDto, _mockProductDtoValidator.Object);

//            Assert
//           var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(200, okResult.StatusCode);
//            var returnedProduct = Assert.IsType<Product>(okResult.Value);
//            Assert.Equal(productDto.Name, returnedProduct.Name);
//        }

//        [Fact]
//        public async Task CreateProduct_ReturnsBadRequest_WhenInvalidProductDto()
//        {
//            Arrange
//           var productDto = new ProductDto { Name = "", Price = -1 };
//            var validationResult = new FluentValidation.Results.ValidationResult(
//                new[] { new FluentValidation.Results.ValidationFailure("Name", "Name is required.") }
//            );
//            _mockProductDtoValidator.Setup(v => v.ValidateAsync(productDto, default))
//                                    .ReturnsAsync(validationResult);

//            Act
//           var result = await _controller.CreateProduct(productDto, _mockProductDtoValidator.Object);

//            Assert
//           var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
//            Assert.Equal(400, badRequestResult.StatusCode);
//        }

//        [Fact]
//        public async Task UpdateProduct_ReturnsOkResult_WhenValidProduct()
//        {
//            Arrange
//           var productId = 1;
//            var product = new Product { Id = productId, Name = "Product1", Price = 100 };
//            var updatedProduct = new Product { Id = productId, Name = "UpdatedProduct", Price = 200 };

//            _mockProductValidator.Setup(v => v.ValidateAsync(updatedProduct, default))
//                                 .ReturnsAsync(new FluentValidation.Results.ValidationResult());
//            _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
//                               .ReturnsAsync(product);
//            _mockProductService.Setup(x => x.UpdateProductAsync(product))
//                               .Returns(Task.CompletedTask);

//            Act
//           var result = await _controller.UpdateProduct(productId, updatedProduct, _mockProductValidator.Object);

//            Assert
//           var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(200, okResult.StatusCode);
//        }

//        [Fact]
//        public async Task UpdateProduct_ReturnsNotFound_WhenProductDoesNotExist()
//        {
//            Arrange
//           var productId = 1;
//            var updatedProduct = new Product { Id = productId, Name = "UpdatedProduct", Price = 200 };

//            _mockProductValidator.Setup(v => v.ValidateAsync(updatedProduct, default))
//                                 .ReturnsAsync(new FluentValidation.Results.ValidationResult());
//            _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
//                               .ReturnsAsync((Product)null);

//            Act
//           var result = await _controller.UpdateProduct(productId, updatedProduct, _mockProductValidator.Object);

//            Assert
//            Assert.IsType<NotFoundResult>(result);
//        }

//        [Fact]
//        public async Task DeleteProduct_ReturnsOkResult_WhenProductExists()
//        {
//            Arrange
//           var productId = 1;
//            var product = new Product { Id = productId, Name = "Product1", Price = 100 };
//            _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
//                               .ReturnsAsync(product);
//            _mockProductService.Setup(x => x.DeleteProductAsync(productId))
//                               .Returns(Task.CompletedTask);

//            Act
//           var result = await _controller.DeleteProduct(productId);

//            Assert
//           var okResult = Assert.IsType<OkObjectResult>(result);
//            Assert.Equal(200, okResult.StatusCode);
//        }

//        [Fact]
//        public async Task DeleteProduct_ReturnsNotFound_WhenProductDoesNotExist()
//        {
//            Arrange
//           var productId = 1;
//            _mockProductService.Setup(x => x.GetProductByIdAsync(productId))
//                               .ReturnsAsync((Product)null);

//            Act
//           var result = await _controller.DeleteProduct(productId);

//            Assert
//            Assert.IsType<NotFoundResult>(result);
//        }
//    }
//}
