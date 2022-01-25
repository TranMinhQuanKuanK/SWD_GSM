using BusinessLayer.Interfaces;
using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.RequestModels.SearchModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD_GSM.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWD_GSM.Controllers.StoreOwner
{
    [Route(StoreOwnerRoute)]
    [ApiController]
    public class ProductsController : BaseStoreOwnerController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService service)
        {
            _productService = service;
        }
        [NonAction]
        private PagingRequestModel getDefaultPaging()
        {
            return new PagingRequestModel
            {
                PageIndex = PageConstant.DefaultPageIndex,
                PageSize = PageConstant.DefaultPageSize
            };
        }
        [NonAction]
        private PagingRequestModel checkDefaultPaging(PagingRequestModel paging)
        {
            if (paging.PageIndex <= 0) paging.PageIndex = PageConstant.DefaultPageIndex;
            if (paging.PageSize <= 0) paging.PageSize = PageConstant.DefaultPageSize;
            return paging;
        }

        [HttpGet]
        public async Task<IActionResult> Get(int BrandId, [FromQuery] ProductSearchModel searchModel, [FromQuery] PagingRequestModel paging)
        {
            try
            {
                //check storeId
                paging = checkDefaultPaging(paging);
                var products = await _productService.GetProductList(BrandId, searchModel, paging);
                return Ok(products);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int BrandId, int id)
        {
            try
            {
                var paging = getDefaultPaging();
                var product = await _productService.GetProductById(BrandId, id);
                return Ok(product);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateNewProduct(int BrandId, [FromBody] ProductCreateModel model)
        {
            try
            {
                var id = await _productService.AddProduct(BrandId, model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}

