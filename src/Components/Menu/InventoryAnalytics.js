import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { DataSet } from 'vis-data';
import { Timeline } from 'vis-timeline/standalone';
import 'vis-timeline/styles/vis-timeline-graph2d.css';
import { PieChart } from 'react-minimal-pie-chart';

const InventoryPieChart = () => {
  const [inventoryData, setInventoryData] = useState([]);

  const getRandomColor = () => {
    const letters = '0123456789ABCDEF';
    let color = '#';
    for (let i = 0; i < 6; i++) {
      color += letters[Math.floor(Math.random() * 16)];
    }
    return color;
  };

  useEffect(() => {
    // Fetch inventory data from the API
    axios
      .get('https://localhost:7039/api/InventoryApi/GetInventoryItems')
      .then((response) => {
        const responseData = response.data.responseData;
        setInventoryData(responseData);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });
  }, []);

  useEffect(() => {
    if (inventoryData.length === 0) return;

    // Create a DataSet for the inventory data
    const inventoryItems = new DataSet(inventoryData);

    // Calculate the sum of quantities for each product ID
    const aggregatedData = inventoryItems
      .get()
      .reduce((result, item) => {
        const productId = item.productID;
        const quantity = item.quantity;
        if (!result[productId]) {
          result[productId] = 0;
        }
        result[productId] += quantity;
        return result;
      }, {});

    // Convert aggregated data into an array for the pie chart
    const pieChartData = Object.keys(aggregatedData).map((productId) => ({
      title: `Product ${productId}`,
      value: aggregatedData[productId],
      color: getRandomColor(), // You can define a function to generate random colors
    }));

    return (
      <div>
        <h2>Inventory Pie Chart</h2>
        <PieChart
          data={pieChartData}
          label={({ dataEntry }) => `${dataEntry.title} (${dataEntry.value})`}
          labelStyle={{ fontSize: '5px', fill: 'white' }}
          lengthAngle={360}
          animate
        />
      </div>
    );
  }, [inventoryData]);

  return null;
};

export default InventoryPieChart;
