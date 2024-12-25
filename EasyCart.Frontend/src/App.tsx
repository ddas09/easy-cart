import Login from './components/auth/login/Login';
import { AuthProvider } from './contexts/AuthContext';
import Dashboard from './components/dashboard/Dashboard';
import ProtectedRoute from './routes/ProtectedRoute';
import Register from './components/auth/register/Register';
import Notification from './components/notification/Notification';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';

const App = () => {
  return (
    <Router>
      <AuthProvider>
        <Notification />

        <Routes>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
          
          {/* Protect the Dashboard route */}
          <Route 
            path="/dashboard" 
            element={
              <ProtectedRoute>
                <Dashboard />
              </ProtectedRoute>
            } 
          />

          <Route path="/" element={<Login />} />
        </Routes>
      </AuthProvider>
    </Router>
  );
};

export default App;
