import './Dashboard.css';
import { useAuth } from '../../contexts/AuthContext';

const Dashboard = () => {
  const { user, logout } = useAuth();

  if (!user) {
    return null;
  }

  return (
    <div className="dashboard-container">
      <div className="dashboard-header">
        <h1>Welcome to the Dashboard</h1>
        <button onClick={logout}>Logout</button>
      </div>
      <div className="dashboard-content">
        <div className="summary-card">
          <h3>Your Email</h3>
          <p>{user.email}</p>
        </div>
      </div>
    </div>
  );
};

export default Dashboard;
