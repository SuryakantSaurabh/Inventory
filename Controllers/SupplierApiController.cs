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
    public class SupplierApiController : ControllerBase
    {
        private readonly DbHelperSupplier _db;

        public SupplierApiController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelperSupplier(eF_DataContext);
        }

        // GET api/SupplierApi/GetSuppliers
        [HttpGet("GetSuppliers")]
        public IActionResult Get()
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetSuppliers();
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

        // GET api/SupplierApi/GetSupplierById/5
        [HttpGet("GetSupplierById/{id}")]
        public IActionResult Get(int id)
        {
            ResponseType type = ResponseType.Success;
            try
            {
                var data = _db.GetSupplierById(id);
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

        // POST api/SupplierApi/CreateSupplier
        [HttpPost("CreateSupplier")]
        public IActionResult Post([FromBody] SupplierModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveSupplier(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // PUT api/SupplierApi/UpdateSupplier
        [HttpPut("UpdateSupplier")]
        public IActionResult Put([FromBody] SupplierModel model)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.SaveSupplier(model);
                return Ok(ResponseHandler.GetAppResponse(type, model));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        // DELETE api/SupplierApi/DeleteSupplier/5
        [HttpDelete("DeleteSupplier/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                ResponseType type = ResponseType.Success;
                _db.DeleteSupplier(id);
                return Ok(ResponseHandler.GetAppResponse(type, "Delete Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
