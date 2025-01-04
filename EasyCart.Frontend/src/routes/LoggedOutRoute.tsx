import { Navigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const LoggedOutRoute = ({ children }: { children: JSX.Element }) => {
  const { user } = useAuth();

  if (user) {
    return <Navigate to="/dashboard" replace />;
  }

  // Allow access to route only when user is logged out
  return children;
};


export default LoggedOutRoute;
