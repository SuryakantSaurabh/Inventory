import React, { useState } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import styles from './Styles/Navbar.module.css';
import logo from './Assets/feature.png';

function Navbar(props) {
  const navigate = useNavigate();
  const location = useLocation();
  const [loggedIn, setLoggedIn] = useState(false); // You can set the initial state based on your login status

  const handleLogout = () => {
    // Perform any logout logic here
    setLoggedIn(false); // Update the login status

    // Redirect to the home page ("/")
    navigate('/');
  };

  // Check if the current route is "/"
  const isHomeRoute = location.pathname === '/';

  return (
    <div className={styles.navbar}>
      <div className={styles.logo}>
        <img src={logo} alt="Oiell Logo" width="80" height="80" />
        <span className={styles['company-name']}>Crude Hub Technologies</span>
      </div>

      {isHomeRoute ? (
        <div>
          {/* You can render a Login button here */}
         
        </div>
      ) : (
        <div className={styles['user-info']}>
          <button onClick={handleLogout} className={styles['logout-button']}>
            Logout
          </button>
          <div className={styles['admin-icon']}>
            <span>A</span>
          </div>
        </div>
      )}
    </div>
  );
}

export default Navbar;
