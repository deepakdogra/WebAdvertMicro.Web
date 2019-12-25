using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertApi.Models;
using Microsoft.AspNetCore.Mvc;
using AdvertApi.Services;

namespace AdvertApi.Controllers
{
    [ApiController]
    [Route("adverts/v1")]
    public class AdvertController : ControllerBase
    {
        private readonly IAdvertStorageService _advertStorageService;
        public AdvertController(IAdvertStorageService advertStorageService)
        {
            _advertStorageService = advertStorageService;
        }

        [HttpPost]
        [Route("Create")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(AdvertResponse))]
        public async Task<IActionResult> Create(AdvertModel model)
        {
            String recordId = "";
            try
            {
                recordId = await _advertStorageService.Add(model);
            }
            catch (KeyNotFoundException ex)
            {
                return new NotFoundResult() { };
            }
            catch(Exception ex) {
                return StatusCode(500, ex.Message);
                throw;
            }

            return StatusCode(201, new AdvertResponse { Id = recordId});
        }

        [HttpPut]
        [Route("Confirm")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Confirm(ConfirmAdvertModel model)
        {
            try
            {
                await _advertStorageService.Confirm(model);
            }
            catch (KeyNotFoundException ex)
            {
                return new NotFoundResult() { };
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
                throw;
            }

            return new OkResult();  
        }
    }
}