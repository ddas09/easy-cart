import { Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const ProtectedRoute = ({ children }: { children: JSX.Element }) => {
  const { token } = useAuth();

  if (!token) {
    // Redirect to login page if not authenticated
    return <Navigate to="/login" replace />;
  }

  // If authenticated, allow access to the protected route
  return children;
};

export default ProtectedRoute;
