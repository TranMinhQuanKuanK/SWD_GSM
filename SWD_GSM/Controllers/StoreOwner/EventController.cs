﻿using BusinessLayer.Interfaces;
using BusinessLayer.Interfaces.StoreOwner;
using BusinessLayer.RequestModels;
using BusinessLayer.RequestModels.CreateModels;
using BusinessLayer.RequestModels.SearchModels;
using BusinessLayer.RequestModels.SearchModels.StoreOwner;
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

    public class EventController : BaseStoreOwnerController
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
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

