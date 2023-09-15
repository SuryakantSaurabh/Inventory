import React,{useState} from 'react';

import { Link, Routes, Route } from 'react-router-dom';

// Components for each section
import Product from './Menu/Product';
import Suppliers from './Menu/Supplier';
import Inventory from './Menu/Inventory'; 
import PurchaseOrder from './Menu/PurchaseOrder';
import SalesOrder from './Menu/Salesorder';

import styles from './Styles/Dashboard.module.css';

import DashboardChild from './Menu/DashboardChild'
import hamfinal from './Styles/hamfinal.png'
import close from './Styles/closefinal.png'

 // Import the module CSS styles

function Dashboard() {
  const [flag,setFlag]=useState(true)
  
  

  return (
    <div className={styles.dashboard}> {/* Use the module CSS class */}
      <div className={flag?styles.sidebar:styles.sidebar1}> 
      {flag?<button onClick={()=>setFlag(!flag) } className={styles.Ham}>
      X
      </button>:<button onClick={()=>setFlag(!flag)} className={styles.Ham1}>
        <img src={hamfinal} style={{width:'40px',height:'40px',}}/>
        </button>} 
       {flag?<ul className={styles.menu}> {/* Use the module CSS class */} 
        <ul className="menu" style={{flex: '1',display: 'flex', flexDirection: 'column',justifyContent: 'flex-start', listStyle:'none', padding: '15px'}}>
        <li className="menu-item" style={{margin:'0.8rem'}}>
    <Link to="analytics" style={{ textDecoration: 'none', color: 'inherit', }}>
      Dashboard
    </Link>
  </li>
  <li className="menu-item" style={{margin:'0.8rem'}}>
    <Link to="product" style={{ textDecoration: 'none', color: 'inherit', }}>
      Product
    </Link>
  </li>
  <li className="menu-item" style={{margin:'0.8rem'}}>
    <Link to="suppliers" style={{ textDecoration: 'none', color: 'inherit' ,}}>
      Suppliers
    </Link>
  </li>
  <li className="menu-item" style={{margin:'0.8rem'}}>
    <Link to="inventory" style={{ textDecoration: 'none', color: 'inherit' ,}}>
      Inventory
    </Link>
  </li>
  <li className="menu-item" style={{margin:'0.8rem'}}>
    <Link to="salesorder" style={{ textDecoration: 'none', color: 'inherit' ,}}>
      Sales Order
    </Link>
  </li>
  <li className="menu-item" style={{margin:'0.8rem'}}>
    <Link to="purchaseorder" style={{ textDecoration: 'none', color: 'inherit'}}>
      Purchase Order
    </Link>
  </li>
</ul>

        </ul>:''}
      </div>
      <div className={styles.content}> {/* Use the module CSS class */}
     
        <Routes> 
          <Route path="analytics" element={<DashboardChild/>} />
          
          <Route path="product" element={<Product />} />
          <Route path="suppliers" element={<Suppliers />} />
          <Route path="inventory" element={<Inventory />} />
          <Route path="salesorder" element={<SalesOrder />} />
          <Route path="purchaseorder" element={<PurchaseOrder />} />
        </Routes>
      </div>
    </div>
  );
}

export default Dashboard;
