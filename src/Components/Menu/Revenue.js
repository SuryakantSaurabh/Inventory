import React, { useEffect, useState } from 'react';
import {
  XYPlot,
  VerticalBarSeries,
  XAxis,
  YAxis,
  VerticalGridLines,
  HorizontalGridLines,
  DiscreteColorLegend,
} from 'react-vis';
import axios from 'axios';

const SalesRevenueGraph = () => {
  const [data, setData] = useState([]);
  const [productNames, setProductNames] = useState({});

  useEffect(() => {
    // Fetch the revenue data
    axios
      .get('https://localhost:7039/api/SalesOrderDetailApi/GetSalesOrderDetails')
      .then((response) => {
        const fetchedData = response.data.responseData;
        const revenueByProduct = calculateRevenue(fetchedData);
        // Convert revenue data into an array of objects
        const formattedData = Object.keys(revenueByProduct).map((productId) => ({
          x: productId,
          y: revenueByProduct[productId],
        }));
        // Sort the data by revenue in descending order
        formattedData.sort((a, b) => b.y - a.y);
        setData(formattedData);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
      });

    // Fetch the product names and map them to product IDs
    axios
      .get('https://localhost:7039/api/ShoppingApi/GetProducts')
      .then((response) => {
        const productData = response.data.responseData;
        const productNameMap = {};
        productData.forEach((product) => {
          productNameMap[product.productID] = product.productName;
        });
        setProductNames(productNameMap);
      })
      .catch((error) => {
        console.error('Error fetching product names:', error);
      });
  }, []);

  const calculateRevenue = (data) => {
    const revenueByProduct = {};
    data.forEach((item) => {
      const { productID, quantity, unitPrice } = item;
      const revenue = quantity * unitPrice;
      if (!revenueByProduct[productID]) {
        revenueByProduct[productID] = revenue;
      } else {
        revenueByProduct[productID] += revenue;
      }
    });
    return revenueByProduct;
  };

  const legendItems = data.map((item) => ({
    title: ` ${productNames[item.x]} (${item.x}) `, // Map product ID to product name
    color: '#333',
  }));

  const chartStyle = {
    background: '#f0f0f0',
    padding: '20px',
    alignItems: 'center',
  };

  const titleStyle = {
    color: '#333',
    padding: '10px',
    marginBottom: '20px',
    marginLeft: '6rem',
  };

  const getColor = (d) => 'blue';

  return (
    <div className="sales-revenue-graph" style={chartStyle}>
      <h2 style={titleStyle}>Product Revenue Graph</h2>
      <DiscreteColorLegend
        items={legendItems}
        orientation="horizontal"
        style={{
          color: '#333',
          padding: '10px',
        }}
      />
      <XYPlot width={600} height={300} xType="ordinal" margin={{ bottom: 60 }}>
        <VerticalGridLines />
        <HorizontalGridLines />
        <XAxis title="" position="end" />
        <YAxis title="Revenue" />
        <VerticalBarSeries data={data} getColor={getColor} barWidth={0.5} />
      </XYPlot>
    </div>
  );
};

export default SalesRevenueGraph;
