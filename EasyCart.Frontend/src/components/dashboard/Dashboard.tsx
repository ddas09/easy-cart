import './Dashboard.css';
import { useAuth } from '../../contexts/AuthContext';
import ProductList from './Products/ProductList';
import { useState } from 'react';

const Dashboard = () => {
  const { user, logout } = useAuth();
  const [searchTerm, setSearchTerm] = useState('');

  if (!user) {
    return null;
  }

  const handleSearchChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setSearchTerm(e.target.value);
  };

  return (
    <div className="dashboard-container">
      {/* Top Navigation Bar */}
      <div className="dashboard-nav">
        <div className="nav-left">
          <input
            type="text"
            placeholder="Search products..."
            value={searchTerm}
            onChange={handleSearchChange}
            className="nav-search"
          />
        </div>
        <div className="nav-right">
          <p className="user-email">{user.email}</p>
          <button onClick={logout} className="logout-button">
            Logout
          </button>
        </div>
      </div>

      {/* Dashboard Content */}
      <div className="dashboard-content">
        <div className="product-section">
          <h2>Available Products</h2>
          <ProductList searchTerm={searchTerm} />
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
