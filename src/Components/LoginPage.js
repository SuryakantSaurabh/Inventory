import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import styles from './Styles/LoginPage.module.css'; // Import the CSS module

function LoginPage() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleSubmit = (e) => {
    e.preventDefault();
    if (username === 'admin' && password === 'admin') {
      navigate('/dashboard/analytics');
    } else {
      alert('Incorrect username or password');
    }
  };

  return (
    <div className={styles['login-container']}> {/* Use [] to access class names with hyphens */}
      <div className={styles['login-box']} style={{width:"300px"}}> {/* Use [] to access class names with hyphens */}
        <h2>Login</h2>
        <form onSubmit={handleSubmit}>
          <div className={styles['form-group']}> {/* Use [] to access class names with hyphens */}
            <input
              type="text"
              placeholder="Username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required
              style={{width:'75%'}}
            />
          </div>
          <div className={styles['form-group']}> {/* Use [] to access class names with hyphens */}
            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              required
              style={{width:'75%'}}
            />
          </div>
          <div className={styles['form-group']}> {/* Use [] to access class names with hyphens */}
            <button type="submit">Login</button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default LoginPage;
