import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './Product.css'

const Product = () => {
  const [products, setProducts] = useState([]);
  const [newProduct, setNewProduct] = useState({
    productID: 0,
    productName: '',
    description: '',
    unitOfMeasure: '',
    price: 0,
  });

  const fetchProducts = async () => {
    try {
      const response = await axios.get('https://localhost:7039/api/ShoppingApi/GetProducts'); 
      const responseData = response.data.responseData; // Access the responseData property
      setProducts(responseData);
    } catch (error) {
      console.error('Error fetching products:', error);
    }
    console.log(products);
  };

  useEffect(() => {
    fetchProducts();
  }, []);

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setNewProduct({ ...newProduct, [name]: value });
  };

  const addProduct = async () => {
    try {
      await axios.post('https://localhost:7039/api/ShoppingApi/CreateProduct', newProduct);
      fetchProducts();
      setNewProduct({
        productID: 0,
        productName: '',
        description: '',
        unitOfMeasure: '',
        price: 0,
      });
    } catch (error) {
      console.error('Error adding product:', error);
    }
  };

  const deleteProduct = async (id) => {
    try {
      await axios.delete(`https://localhost:7039/api/ShoppingApi/DeleteProduct/${id}`);
      fetchProducts();
    } catch (error) {
      console.error('Error deleting product:', error);
    }
  };

  return (
    <div className="product-container">
      <div className="product-form" >
        <h2>Add a New Product</h2>
        <div style={{width:'85%'}}>
          <input
            type="text"
            placeholder="Product Name"
            name="productName"
            value={newProduct.productName}
            onChange={handleInputChange}
          />
          <input
            type="text"
            placeholder="Description"
            name="description"
            value={newProduct.description}
            onChange={handleInputChange}
          />
          <input
            type="text"
            placeholder="Unit of Measure"
            name="unitOfMeasure"
            value={newProduct.unitOfMeasure}
            onChange={handleInputChange}
          />
          <input
            type="number"
            placeholder="Price"
            name="price"
            value={newProduct.price}
            onChange={handleInputChange}
          />
          <button onClick={addProduct}>Add Product</button>
        </div>
      </div>
      <div className="product-list"> 
        {products.map((product) => (
          <div className="product-card" key={product.productID}>
            <h3>{product.productName}</h3>
            <p>{product.description}</p>
            <p>{product.unitOfMeasure}</p>
            <p>${product.price}</p>
            <button onClick={() => deleteProduct(product.productID)}>Delete</button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default Product;