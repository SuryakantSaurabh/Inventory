using Microsoft.AspNetCore.Mvc;
using TestingCrud.EfCore;
using TestingCrud.Model;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace TestingCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("AllowAll")]
    public class SalesOrderApiController : ControllerBase
    {
        private readonly DbHelperSalesOrder _db;

        public SalesOrderApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelperSalesOrder(eF_DataContext);
        }

        // GET api/SalesOrderApi/GetSalesOrders
        [HttpGet("GetSalesOrders")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetSalesOrders();
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

        // GET api/SalesOrderApi/GetSalesOrderById/5
        [HttpGet("GetSalesOrderById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetSalesOrderById(id);
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

        // POST api/SalesOrderApi/CreateSalesOrder
        [HttpPost("CreateSalesOrder")]
        public IActionResult Post([FromBody] SalesOrderModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.CreateSalesOrder(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/SalesOrderApi/UpdateSalesOrder
        [HttpPut("UpdateSalesOrder")]
        public IActionResult Put([FromBody] SalesOrderModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdateSalesOrder(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/SalesOrderApi/DeleteSalesOrder/5
        [HttpDelete("DeleteSalesOrder/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteSalesOrder(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
