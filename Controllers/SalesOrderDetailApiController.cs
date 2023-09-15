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
    public class SalesOrderDetailApiController : ControllerBase
    {
        private readonly DbHelperSalesOrderDetail _db;

        public SalesOrderDetailApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelperSalesOrderDetail(eF_DataContext);
        }

        // GET api/SalesOrderDetailApi/GetSalesOrderDetails
        [HttpGet("GetSalesOrderDetails")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetSalesOrderDetails();
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

        // GET api/SalesOrderDetailApi/GetSalesOrderDetailById/5
        [HttpGet("GetSalesOrderDetailById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetSalesOrderDetailById(id);
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

        // POST api/SalesOrderDetailApi/CreateSalesOrderDetail
        [HttpPost("CreateSalesOrderDetail")]
        public IActionResult Post([FromBody] SalesOrderDetailModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.CreateSalesOrderDetail(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/SalesOrderDetailApi/UpdateSalesOrderDetail
        [HttpPut("UpdateSalesOrderDetail")]
        public IActionResult Put([FromBody] SalesOrderDetailModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.UpdateSalesOrderDetail(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/SalesOrderDetailApi/DeleteSalesOrderDetail/5
        [HttpDelete("DeleteSalesOrderDetail/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteSalesOrderDetail(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
