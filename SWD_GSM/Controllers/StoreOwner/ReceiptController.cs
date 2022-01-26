using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.Cashier;
using BusinessLayer.Interfaces.StoreOwner;
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
    [ApiExplorerSettings(GroupName = Role)]
    public class DailyRevenueController : BaseStoreOwnerController
    {
        private readonly IDailyRevenueService _dailyRevenueService;

        public DailyRevenueController(IDailyRevenueService dailyRevenueService)
        {
            _dailyRevenueService = dailyRevenueService;
        }
        [NonAction]
    

        [HttpGet]
        public async Task<IActionResult> Get()
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

