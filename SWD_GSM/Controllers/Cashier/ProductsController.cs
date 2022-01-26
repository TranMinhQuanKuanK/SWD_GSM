using BusinessLayer.Interfaces.Cashier;
using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.SearchModels.StoreOwner;
using Microsoft.AspNetCore.Mvc;
using SWD_GSM.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWD_GSM.Controllers.Cashier
{
    [Route(CashierRoute)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Role)]
    public class ProductsController : BaseCashierController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
           _productService = productService;
        }
        [NonAction]
    

        [HttpGet]
        public async Task<IActionResult> Get(int BrandId, [FromQuery] ProductSearchModel searchModel, [FromQuery] PagingRequestModel paging)
        {
            return null;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int BrandId, int id)
        {
            return null;
        }
       
    }
}

