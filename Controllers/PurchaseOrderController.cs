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
    public class PurchaseOrderController : ControllerBase
    {
        private readonly DbHelperPurchaseOrder _db;

        public PurchaseOrderController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelperPurchaseOrder(eF_DataContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<PurchaseOrder> purchaseOrders = _db.GetPurchaseOrders();
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, purchaseOrders));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                PurchaseOrder purchaseOrder = _db.GetPurchaseOrderById(id);
                if (purchaseOrder == null)
                {
                    return NotFound(ResponseHandler.GetAppResponse(ResponseType.NotFound, null));
                }
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, purchaseOrder));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PurchaseOrderModel model)
        {
            try
            {
                PurchaseOrder purchaseOrder = new PurchaseOrder
                {
                    SupplierID = model.SupplierID,
                    OrderDate = model.OrderDate,
                    ExpectedArrivalDate = model.ExpectedArrivalDate,
                    Status = model.Status
                };

                _db.CreatePurchaseOrder(purchaseOrder);

                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, purchaseOrder));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PurchaseOrderModel model)
        {
            try
            {
                PurchaseOrder existingPurchaseOrder = _db.GetPurchaseOrderById(id);
                if (existingPurchaseOrder == null)
                {
                    return NotFound(ResponseHandler.GetAppResponse(ResponseType.NotFound, null));
                }

                existingPurchaseOrder.SupplierID = model.SupplierID;
                existingPurchaseOrder.OrderDate = model.OrderDate;
                existingPurchaseOrder.ExpectedArrivalDate = model.ExpectedArrivalDate;
                existingPurchaseOrder.Status = model.Status;

                _db.UpdatePurchaseOrder(existingPurchaseOrder);

                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, existingPurchaseOrder));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                PurchaseOrder existingPurchaseOrder = _db.GetPurchaseOrderById(id);
                if (existingPurchaseOrder == null)
                {
                    return NotFound(ResponseHandler.GetAppResponse(ResponseType.NotFound, null));
                }

                _db.DeletePurchaseOrder(id);

                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, "Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
