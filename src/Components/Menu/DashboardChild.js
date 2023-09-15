import React, { useState, useEffect } from 'react';
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
import ProductQuantityPieChart from './PieChart';
import SalesRevenueGraph from './Revenue';


const DashboardChild = () => {
  const [statusData, setStatusData] = useState([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    // Fetch data from the API
    axios
      .get('https://localhost:7039/api/SalesOrderApi/GetSalesOrders')
      .then((response) => {
        const salesOrders = response.data.responseData;
        const statusCounts = countStatuses(salesOrders);
        const statusData = Object.keys(statusCounts).map((status) => ({
          x: status,
          y: statusCounts[status],
        }));
        setStatusData(statusData);
        setIsLoading(false);
      })
      .catch((error) => {
        console.error('Error fetching data:', error);
        setIsLoading(false);
      });
  }, []);

  // Count the number of each status type
  const countStatuses = (orders) => {
    const counts = {
      Pending: 0,
      Delivered: 0,
      Shipped: 0,
    };

    orders.forEach((order) => {
      counts[order.status]++;
    });

    return counts;
  };

  const legendItems = [
    { title: 'Pending', color: 'red' },
    { title: 'Delivered', color: 'blue' },
    { title: 'Shipped', color: 'green' },
  ];

  const chartStyle = {
    background: '#f0f0f0',
    padding: '20px',
    margin:'2rem auto',
    backgroundColour:'transparent',
    display:'flex',
    justifyContent:'center',
    flexDirection:'column',
    alignItems:'center'
  };

  const titleStyle = {
    color: '#333',
   
  };
 
  
  

  const barSeriesColor = '#ffcc00'; // Define the bar color

  return (
    <>
    
    <div className="bar-chart" style={chartStyle}>
      
      {isLoading ? (
        <p>Loading...</p>
      ) : (
        <>
        <h2>Order status</h2>
          <DiscreteColorLegend items={legendItems} orientation="horizontal"  style={{padding:'10px',margin:'1rem'}}/>
          <XYPlot width={500} height={300} xType="ordinal" margin={{ bottom: 40 }}>
            <VerticalGridLines />
            <HorizontalGridLines />
            <XAxis title="" />
            <YAxis title="" />
            {/* Update the color prop to set the bar color */}
            <VerticalBarSeries data={statusData} color={barSeriesColor} />
          </XYPlot>
          
        </>
      )}
      <ProductQuantityPieChart/>
      <SalesRevenueGraph/>
    </div>
    
    </>
  );
};




export default DashboardChild;