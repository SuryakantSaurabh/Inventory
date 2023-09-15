import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './SalesOrderDetail.css';

const SalesOrderDetailTable = () => {
  const [salesOrderDetails, setSalesOrderDetails] = useState([]);
  const [newSalesOrderDetail, setNewSalesOrderDetail] = useState({
    orderID: 0,
    productID: 0,
    quantity: 0,
    unitPrice: 0,
  });

  useEffect(() => {
    axios
      .get('https://localhost:7039/api/SalesOrderDetailApi/GetSalesOrderDetails')
      .then((response) => {
        const responseData = response.data.responseData;
        setSalesOrderDetails(responseData);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);

  const handleSalesOrderDetailSubmit = (e) => {
    e.preventDefault();
    axios
      .post('https://localhost:7039/api/SalesOrderDetailApi/CreateSalesOrderDetail', newSalesOrderDetail)
      .then((response) => {
        // After successful creation, set newSalesOrderDetail to initial state
        setNewSalesOrderDetail({
          orderID: 0,
          productID: 0,
          quantity: 0,
          unitPrice: 0,
        });
        // Refresh the sales order details list
        axios
          .get('https://localhost:7039/api/SalesOrderDetailApi/GetSalesOrderDetails')
          .then((response) => {
            const responseData = response.data.responseData;
            setSalesOrderDetails(responseData);
          });
      })
      .catch((error) => {
        console.error('Error creating sales order detail:', error);
      });
  };

  return (
    <div className="sales-order-detail-container">
      <h2>Sales Order Detail</h2>
      <form onSubmit={handleSalesOrderDetailSubmit}>
        <h3>Create New Sales Order Detail</h3>
        <label>
          Order ID:
          <input
            type="number"
            value={newSalesOrderDetail.orderID}
            onChange={(e) => setNewSalesOrderDetail({ ...newSalesOrderDetail, orderID: e.target.valueAsNumber })}
          />
        </label>
        <label>
          Product ID:
          <input
            type="number"
            value={newSalesOrderDetail.productID}
            onChange={(e) => setNewSalesOrderDetail({ ...newSalesOrderDetail, productID: e.target.valueAsNumber })}
          />
        </label>
        <label>
          Quantity:
          <input
            type="number"
            value={newSalesOrderDetail.quantity}
            onChange={(e) => setNewSalesOrderDetail({ ...newSalesOrderDetail, quantity: e.target.valueAsNumber })}
          />
        </label>
        <label>
          Unit Price:
          <input
            type="number"
            value={newSalesOrderDetail.unitPrice}
            onChange={(e) => setNewSalesOrderDetail({ ...newSalesOrderDetail, unitPrice: e.target.valueAsNumber })}
          />
        </label>
        <button type="submit">Create</button>
      </form>
      <table className="sales-order-detail-table">
        <thead>
          <tr>
            <th>Order Detail ID</th>
            <th>Order ID</th>
            <th>Product ID</th>
            <th>Quantity</th>
            <th>Unit Price</th>
          </tr>
        </thead>
        <tbody>
          {salesOrderDetails.map((detail) => (
            <tr key={detail.orderDetailID}>
              <td>{detail.orderDetailID}</td>
              <td>{detail.orderID}</td>
              <td>{detail.productID}</td>
              <td>{detail.quantity}</td>
              <td>{detail.unitPrice} $</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default SalesOrderDetailTable;
