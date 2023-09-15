import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Salesorder.css';
import SalesOrderDetailTable from './SalesOrderDetails';

const SalesOrderTable = () => {
  const [salesOrders, setSalesOrders] = useState([]);
  const [search, setSearch] = useState('');
  const [newSalesOrder, setNewSalesOrder] = useState({
    customerName: '',
    orderDate: '',
    deliveryDate: '',
    status: '',
  });

  useEffect(() => {
    axios
      .get('https://localhost:7039/api/SalesOrderApi/GetSalesOrders')
      .then((response) => {
        const responseData = response.data.responseData;
        setSalesOrders(responseData);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);

  const handleSalesOrderSubmit = (e) => {
    e.preventDefault();

    // Format the order date and delivery date
    const formattedOrderDate = new Date(newSalesOrder.orderDate).toISOString();
    const formattedDeliveryDate = new Date(newSalesOrder.deliveryDate).toISOString();

    // Create a new sales order object with formatted dates
    const formattedSalesOrder = {
      customerName: newSalesOrder.customerName,
      orderDate: formattedOrderDate,
      deliveryDate: formattedDeliveryDate,
      status: newSalesOrder.status,
    };

    axios
      .post('https://localhost:7039/api/SalesOrderApi/CreateSalesOrder', formattedSalesOrder)
      .then((response) => {
        // After successful creation, set newSalesOrder to initial state
        setNewSalesOrder({
          customerName: '',
          orderDate: '',
          deliveryDate: '',
          status: '',
        });
        // Refresh the sales orders list
        axios
          .get('https://localhost:7039/api/SalesOrderApi/GetSalesOrders')
          .then((response) => {
            const responseData = response.data.responseData;
            setSalesOrders(responseData);
          });
      })
      .catch((error) => {
        console.error('Error creating sales order:', error);
      });
  };

  const filteredSalesOrders = salesOrders.filter((order) =>
    order.customerName.toLowerCase().includes(search.toLowerCase())
  );

  filteredSalesOrders.sort(
    (a, b) => new Date(b.OrderDate) - new Date(a.OrderDate)
  );

  return (
    <div className="sales-order-container">
      <h2>Sales Order</h2>
      <form onSubmit={handleSalesOrderSubmit}>
        <h3>Create New Sales Order</h3>
        <label>
          Customer Name:
          </label>
          <input
            type="text"
            value={newSalesOrder.customerName}
            onChange={(e) => setNewSalesOrder({ ...newSalesOrder, customerName: e.target.value })}
          />
        
        <label>
          Order Date:
          </label>
          <input
            type="date"
            value={newSalesOrder.orderDate}
            onChange={(e) => setNewSalesOrder({ ...newSalesOrder, orderDate: e.target.value })}
          />
        
        <label>
          Delivery Date:
          <input
            type="date"
            value={newSalesOrder.deliveryDate}
            onChange={(e) => setNewSalesOrder({ ...newSalesOrder, deliveryDate: e.target.value })}
          />
        </label><br />
        <label>
          Status:
          <input
            type="text"
            value={newSalesOrder.status}
            onChange={(e) => setNewSalesOrder({ ...newSalesOrder, status: e.target.value })}
          />
        </label>
        <button type="submit">Create Sales Order</button>
      </form>
      <h3>Sales Order Table</h3>
      <table className="sales-order-table">
        <thead>
          <tr>
            <th>Order ID</th>
            <th>Customer Name</th>
            <th>Order Date</th>
            <th>Delivery Date</th>
            <th>Status</th>
          </tr>
        </thead>
        <tbody>
          {filteredSalesOrders.map((order) => (
            <tr key={order.orderID}>
              <td>{order.orderID}</td>
              <td>{order.customerName}</td>
              <td>{new Date(order.orderDate).toISOString().split('T')[0]}</td>
              <td>{new Date(order.deliveryDate).toISOString().split('T')[0]}</td>
              <td>{order.status}</td>
            </tr>
          ))}
        </tbody>
      </table>

      <SalesOrderDetailTable />
    </div>
  );
};

export default SalesOrderTable;
