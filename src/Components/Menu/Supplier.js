import React, { useState, useEffect } from 'react';
import axios from 'axios';
import styles from '../Styles/Supplier.module.css'; // Import the styles from the CSS module

const Supplier = () => {
  const [suppliers, setSuppliers] = useState([]);
  const [newSupplier, setNewSupplier] = useState({
    supplierName: '',
    contactName: '',
    contactEmail: '',
    contactPhone: '',
  });
  const [searchTerm, setSearchTerm] = useState('');
  const [sortBy, setSortBy] = useState('supplierName');
  const [sortOrder, setSortOrder] = useState('asc');

  const fetchSuppliers = async () => {
    try {
      const response = await axios.get('https://localhost:7039/api/SupplierApi/GetSuppliers');
      const responseData = response.data.responseData;
      setSuppliers(responseData);
    } catch (error) {
      console.error('Error fetching suppliers:', error);
    }
  };

  useEffect(() => {
    fetchSuppliers();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewSupplier({ ...newSupplier, [name]: value });
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

  const addSupplier = async () => {
    try {
      await axios.post('https://localhost:7039/api/SupplierApi/CreateSupplier', newSupplier);
      fetchSuppliers();
      setNewSupplier({
        supplierName: '',
        contactName: '',
        contactEmail: '',
        contactPhone: '',
      });
    } catch (error) {
      console.error('Error adding supplier:', error);
    }
  };

  const deleteSupplier = async (id) => {
    try {
      await axios.delete(`https://localhost:7039/api/SupplierApi/DeleteSupplier/${id}`);
      fetchSuppliers();
    } catch (error) {
      console.error('Error deleting supplier:', error);
    }
  };

  const filteredSuppliers = suppliers.filter((supplier) =>
    supplier.supplierName.toLowerCase().includes(searchTerm.toLowerCase())
  );

  const sortedSuppliers = filteredSuppliers.sort((a, b) => {
    const multiplier = sortOrder === 'asc' ? 1 : -1;
    return multiplier * (a[sortBy].localeCompare(b[sortBy]));
  });

  return (
    <div>
      <h2 className={styles.supplierHeader}>Supplier Management</h2>
      <div>
        <input
          type="text"
          placeholder="Search Suppliers"
          value={searchTerm}
          onChange={handleSearch}
          className={styles.searchInput}
        />
      </div>
      <table className={styles.table}>
        <thead>
          <tr>
            <th onClick={() => handleSort('supplierName')}>
              Supplier Name
              {sortBy === 'supplierName' && (
                <i className={`bi bi-caret-${sortOrder === 'asc' ? 'up' : 'down'}`}></i>
              )}
            </th>
            <th onClick={() => handleSort('contactName')}>
              Contact Name
              {sortBy === 'contactName' && (
                <i className={`bi bi-caret-${sortOrder === 'asc' ? 'up' : 'down'}`}></i>
              )}
            </th>
            <th>Contact Email</th>
            <th>Contact Phone</th>
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {sortedSuppliers.map((supplier) => (
            <tr key={supplier.supplierID}>
              <td>{supplier.supplierName}</td>
              <td>{supplier.contactName}</td>
              <td>{supplier.contactEmail}</td>
              <td>{supplier.contactPhone}</td>
              <td>
                <button onClick={() => deleteSupplier(supplier.supplierID)} className={styles.deleteBtn}>
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
      <div>
        <h3 className={styles.addSupplierHeader}>Add New Supplier</h3>
        <div className={styles.addsupp}>
          <input
            type="text"
            placeholder="Supplier Name"
            name="supplierName"
            value={newSupplier.supplierName}
            onChange={handleInputChange}
            className={styles.inputField}
          />
          <input
            type="text"
            placeholder="Contact Name"
            name="contactName"
            value={newSupplier.contactName}
            onChange={handleInputChange}
            className={styles.inputField}
          />
          <input
            type="text"
            placeholder="Contact Email"
            name="contactEmail"
            value={newSupplier.contactEmail}
            onChange={handleInputChange}
            className={styles.inputField}
          />
          <input
            type="text"
            placeholder="Contact Phone"
            name="contactPhone"
            value={newSupplier.contactPhone}
            onChange={handleInputChange}
            className={styles.inputField}
          /><br/>
          <button onClick={addSupplier} className={styles.addBtn}>
            Add Supplier
          </button>
        </div>
      </div>
    </div>
  );
};

export default Supplier;
