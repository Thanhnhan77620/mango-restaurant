using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mongo.Services.ProductAPI.Models.Dto;
using Mongo.Services.ProductAPI.Repository;

namespace Mongo.Services.ProductAPI.Controllers
{
    [Route("api/v1/Products ")]
    public class ProductAPIController:ControllerBase
    {
        protected ResponseDto _response;
        private IProductRepository _productRepository;

        public ProductAPIController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
            _response = new ResponseDto();
        }

        [Authorize]
        [HttpGet]
        public async Task<object> Get() {
            try
            {
                var productDtos = await _productRepository.GetProducts();
                _response.Result = productDtos;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
               var productDto = await _productRepository.GetProductById(id);
                if (productDto!=null)
                {
                    _response.Result = productDto;
                    _response.IsSuccess = true;
                }
                else
                {
                    _response.DisplayMessage = "Not found";
                    _response.IsSuccess = false;
                }
               
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [Authorize]
        [HttpPost]
        public async Task<object> Post([FromBody]ProductDto productDto)
        {
            try
            {
                _response.Result = await _productRepository.CreateUpdateProduct(productDto);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[Authorize]
        //[HttpPut]
        //public async Task<object> Put([FromBody] ProductDto productDto)
        //{
        //    try
        //    {
        //        _response.Result = await _productRepository.CreateUpdateProduct(productDto);
        //        _response.IsSuccess = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.ErrorMessages = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id}")]
        public async Task<object> Delete(int id)
        {
            try
            {
                _response.IsSuccess = await _productRepository.DeleteProduct(id);
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}
