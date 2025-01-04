import Login from './components/auth/login/Login';
import { AuthProvider } from './contexts/AuthContext';
import Dashboard from './components/dashboard/Dashboard';
import ProtectedRoute from './routes/ProtectedRoute';
import Register from './components/auth/register/Register';
import Notification from './components/notification/Notification';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import LoggedOutRoute from './routes/LoggedOutRoute';

const App = () => {
  return (
    <Router>
      <AuthProvider>
        <Notification />

        <Routes>
          {/* Use LoggedOutRoute for the login and register routes */}
          <Route 
            path="/login" 
            element={
              <LoggedOutRoute>
                <Login />
              </LoggedOutRoute>
            } 
          />
          <Route 
            path="/register" 
            element={
              <LoggedOutRoute>
                <Register />
              </LoggedOutRoute>
            } 
          />

          {/* Protect the Dashboard route */}
          <Route 
            path="/dashboard" 
            element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            } 
          />

          {/* Default route */}
          <Route path="/" element={<Login />} />
        </Routes>
      </AuthProvider>
    </Router>
  );
};

export default App;
