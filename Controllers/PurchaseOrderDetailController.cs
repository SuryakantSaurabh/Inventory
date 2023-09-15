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
    public class PurchaseOrderDetailController : ControllerBase
    {
        private readonly DbHelperPurchaseOrderDetail _db;

        public PurchaseOrderDetailController(EF_DataContext eF_DataContext)
        {
            _db = new DbHelperPurchaseOrderDetail(eF_DataContext);
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                List<PurchaseOrderDetail> purchaseOrderDetails = _db.GetPurchaseOrderDetails();
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, purchaseOrderDetails));
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
                PurchaseOrderDetail purchaseOrderDetail = _db.GetPurchaseOrderDetailById(id);
                if (purchaseOrderDetail == null)
                {
                    return NotFound(ResponseHandler.GetAppResponse(ResponseType.NotFound, null));
                }
                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, purchaseOrderDetail));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] PurchaseOrderDetailModel model)
        {
            try
            {
                PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail
                {
                    OrderID = model.OrderID,
                    ProductID = model.ProductID,
                    Quantity = model.Quantity,
                    UnitPrice = model.UnitPrice
                };

                _db.CreatePurchaseOrderDetail(purchaseOrderDetail);

                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, purchaseOrderDetail));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PurchaseOrderDetailModel model)
        {
            try
            {
                PurchaseOrderDetail existingPurchaseOrderDetail = _db.GetPurchaseOrderDetailById(id);
                if (existingPurchaseOrderDetail == null)
                {
                    return NotFound(ResponseHandler.GetAppResponse(ResponseType.NotFound, null));
                }

                existingPurchaseOrderDetail.OrderID = model.OrderID;
                existingPurchaseOrderDetail.ProductID = model.ProductID;
                existingPurchaseOrderDetail.Quantity = model.Quantity;
                existingPurchaseOrderDetail.UnitPrice = model.UnitPrice;

                _db.UpdatePurchaseOrderDetail(existingPurchaseOrderDetail);

                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, existingPurchaseOrderDetail));
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
                PurchaseOrderDetail existingPurchaseOrderDetail = _db.GetPurchaseOrderDetailById(id);
                if (existingPurchaseOrderDetail == null)
                {
                    return NotFound(ResponseHandler.GetAppResponse(ResponseType.NotFound, null));
                }

                _db.DeletePurchaseOrderDetail(id);

                return Ok(ResponseHandler.GetAppResponse(ResponseType.Success, "Deleted Successfully"));
            }
            catch (Exception ex)
            {
                return BadRequest(ResponseHandler.GetExceptionResponse(ex));
            }
        }
    }
}
