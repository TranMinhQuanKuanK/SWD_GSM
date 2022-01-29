using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.SystemAdmin;
using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.RequestModels.SearchModels;
using BusinessLayer.RequestModels.SearchModels.StoreOwner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SWD_GSM.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWD_GSM.Controllers.SystemAdmin
{
    [Route(SystemAdminRoute)]
    [ApiController]
    [ApiExplorerSettings(GroupName = Role)]
    [Authorize(Roles = Role)]

    public class BrandController : BaseSystemAdminController
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
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

