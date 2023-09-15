import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './PurchaseOrder.css'
const PurchaseOrder = () => {
  const [purchaseOrders, setPurchaseOrders] = useState([]);
  const [search, setSearch] = useState('');

  useEffect(() => {
    // Use Axios to fetch data from the API
    axios.get('https://localhost:7039/api/PurchaseOrder')
      .then((response) => {
        const responseData = response.data.responseData;
        setPurchaseOrders(responseData);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);

  // Filter purchaseOrders based on search input
  const filteredPurchaseOrders = purchaseOrders.filter((order) =>
    order.status.toLowerCase().includes(search.toLowerCase())
  );

  return (
    <div className="purchase-order-container">
      <h2>Purchase Orders</h2>
      <input
        type="text"
        placeholder="Search by Status"
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        className="purchase-order-search"
      />
      <table className="purchase-order-table">
        <thead>
          <tr>
            <th>Order ID</th>
            <th>Supplier ID</th>
            <th>Order Date</th>
            <th>Expected Arrival Date</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {filteredPurchaseOrders.map((order) => (
            <tr key={order.OrderID}>
              <td>{order.orderID}</td>
              <td>{order.supplierID}</td>
              <td>{order.orderDate}</td>
              <td>{order.expectedArrivalDate}</td>
              <td>{order.status}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default PurchaseOrder;
