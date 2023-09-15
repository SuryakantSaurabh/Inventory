using Microsoft.AspNetCore.Mvc;
using TestingCrud.EfCore;
using TestingCrud.Model;
using System;
using Microsoft.AspNetCore.Cors;

namespace TestingCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class InventoryApiController : ControllerBase
    {
        private readonly DbHelperInventory _db;

        public InventoryApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelperInventory(eF_DataContext);
        }

        [HttpGet("GetInventoryItems")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetInventoryItems();
                if (data.Count == 0)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("GetInventoryItemById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetInventoryItemById(id);
                if (data == null)
                {
                    type = ResponseType.NotFound;
                }
                return Ok(ResponseHandler.GetAppResponse(type, data));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost("CreateInventoryItem")]
        public IActionResult Post([FromBody] InventoryItemModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveInventoryItem(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("UpdateInventoryItem")]
        public IActionResult Put([FromBody] InventoryItemModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdateInventoryItem(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("DeleteInventoryItem/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteInventoryItem(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
