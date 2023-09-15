import React, { useState, useEffect } from 'react';
import axios from 'axios';
import styles from '../Styles/Inventory.module.css'; // Import the styles from the CSS module

const Inventory = () => {
  const [inventoryItems, setInventoryItems] = useState([]);
  const [newInventoryItem, setNewInventoryItem] = useState({
    productID: 0,
    quantity: 0,
    location: '',
    expiryDate: new Date().toISOString(),
  });
  const [searchTerm, setSearchTerm] = useState('');
  const [sortBy, setSortBy] = useState('location');
  const [sortOrder, setSortOrder] = useState('asc');

  const fetchInventoryItems = async () => {
    try {
      const response = await axios.get('https://localhost:7039/api/InventoryApi/GetInventoryItems');
      const responseData = response.data.responseData;
      setInventoryItems(responseData);
    } catch (error) {
      console.error('Error fetching inventory items:', error);
    }
  };

  useEffect(() => {
    fetchInventoryItems();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewInventoryItem({ ...newInventoryItem, [name]: value });
  };

  const handleSearch = (e) => {
    setSearchTerm(e.target.value);
  };

  const handleSort = (field) => {
    if (field === sortBy) {
      setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
    } else {
      setSortBy(field);
      setSortOrder('asc');
    }
  };

  const addInventoryItem = async () => {
    try {
      await axios.post('https://localhost:7039/api/InventoryApi/CreateInventoryItem', newInventoryItem);
      fetchInventoryItems();
      setNewInventoryItem({
        productID: 0,
        quantity: 0,
        location: '',
        expiryDate: new Date().toISOString(),
      });
    } catch (error) {
      console.error('Error adding inventory item:', error);
    }
  };

  const deleteInventoryItem = async (id) => {
    try {
      await axios.delete(`https://localhost:7039/api/InventoryApi/DeleteInventoryItem/${id}`);
      fetchInventoryItems();
    } catch (error) {
      console.error('Error deleting inventory item:', error);
    }
  };

  const filteredInventoryItems = inventoryItems.filter((item) =>
    item.location.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const sortedInventoryItems = filteredInventoryItems.sort((a, b) => {
    const multiplier = sortOrder === 'asc' ? 1 : -1;
    return multiplier * (a[sortBy].localeCompare(b[sortBy]));
  });

  return (
    <div>
      <h2 className={styles.inventoryHeader}>Inventory Management</h2>
      <div>
        <input
          type="text"
          placeholder="Search Inventory Items"
          value={searchTerm}
          onChange={handleSearch}
          className={styles.searchInput}
        />
      </div>
      <table className={styles.table}>
        <thead>
          <tr>
            <th onClick={() => handleSort('productID')}>
              Product ID
              {sortBy === 'productID' && (
                <i className={`bi bi-caret-${sortOrder === 'asc' ? 'up' : 'down'}`}></i>
              )}
            </th>
            <th onClick={() => handleSort('quantity')}>
              Quantity
              {sortBy === 'quantity' && (
                <i className={`bi bi-caret-${sortOrder === 'asc' ? 'up' : 'down'}`}></i>
              )}
            </th>
            <th onClick={() => handleSort('location')}>
              Location
              {sortBy === 'location' && (
                <i className={`bi bi-caret-${sortOrder === 'asc' ? 'up' : 'down'}`}></i>
              )}
            </th>
            <th onClick={() => handleSort('expiryDate')}>
              Expiry Date
              {sortBy === 'expiryDate' && (
                <i className={`bi bi-caret-${sortOrder === 'asc' ? 'up' : 'down'}`}></i>
              )}
            </th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {sortedInventoryItems.map((item) => (
            <tr key={item.inventoryID}>
              <td>{item.productID}</td>
              <td>{item.quantity}</td>
              <td>{item.location}</td>
              <td>{item.expiryDate}</td>
              <td>
                <button onClick={() => deleteInventoryItem(item.inventoryID)} className={styles.deleteBtn}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div className={styles.addin}>
        <h3 className={styles.addInventoryHeader}>Add New Inventory Item</h3>
        <div>
          <input
            type="number"
            placeholder="Product ID"
            name="productID"
            value={newInventoryItem.productID}
            onChange={handleInputChange}
            className={styles.inputField}
          />
          <input
            type="number"
            placeholder="Quantity"
            name="quantity"
            value={newInventoryItem.quantity}
            onChange={handleInputChange}
            className={styles.inputField}
          />
          <input
            type="text"
            placeholder="Location"
            name="location"
            value={newInventoryItem.location}
            onChange={handleInputChange}
            className={styles.inputField}
          />
          <input
            type="datetime-local"
            name="expiryDate"
            value={newInventoryItem.expiryDate}
            onChange={handleInputChange}
            className={styles.inputField}
          />
          <button onClick={addInventoryItem} className={styles.addBtn}>
            Add Inventory Item
          </button>
        </div>
      </div>
    </div>
  );
};

export default Inventory;
