import './Dashboard.css';
import { useAuth } from '../../contexts/AuthContext';
import ProductList from './Products/ProductList/ProductList';
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
          <ProductList searchTerm={searchTerm} isAdmin={user.isAdmin} />
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
