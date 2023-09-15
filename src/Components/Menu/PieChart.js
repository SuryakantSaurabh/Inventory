import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { PieChart } from 'react-minimal-pie-chart';

function ProductQuantityPieChart() {
  const [data, setData] = useState([]);

  useEffect(() => {
    async function fetchData() {
      try {
        const response = await axios.get('https://localhost:7039/api/SalesOrderDetailApi/GetSalesOrderDetails'); // Replace with your API endpoint
        const apiData = response.data.responseData;

        // Calculate total quantity for all products
        const totalQuantity = apiData.reduce((total, item) => total + item.quantity, 0);

        // Calculate percentage of quantity for each product
        const chartData = apiData.map(item => ({
          title: `Product ${item.productID}`,
          value: (item.quantity / totalQuantity) * 100,
          color: getRandomColor(), // You can define a function to generate random colors
        }));

        setData(chartData);
      } catch (error) {
        console.error('Error fetching data:', error);
      }
    }

    fetchData();
  }, []); // The empty dependency array ensures that this effect runs once when the component mounts

  // Function to generate random colors (you can use any color generation method)
  function getRandomColor() {
    return '#' + (Math.random().toString(16) + '000000').slice(2, 8);
  }

  return (
    <div className="product-quantity-pie-chart">
        <h2>Product Stock According to Quantity</h2>
      <PieChart
        data={data}
        lineWidth={15}
        label={({ dataEntry }) => `${Math.round(dataEntry.percentage)}%`}
        labelStyle={{
          fontSize: '2px',
          fontFamily: 'sans-serif',
          padding:'0',

        }}
        radius={42}
        animate
      />
    </div>
  );
}

export default ProductQuantityPieChart;
