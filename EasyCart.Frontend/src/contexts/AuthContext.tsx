import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import StorageService from '../services/StorageService';
import { AuthResponse, UserInformation } from '../models/AuthResponse';
import { createContext, useContext, useState, useEffect, ReactNode } from 'react';

interface AuthContextType {
  user: UserInformation | null;
  token: string | null;
  login: (authResponse: AuthResponse) => void;
  logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

interface AuthProviderProps {
  children: ReactNode;
}

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const navigate = useNavigate();

  const [user, setUser] = useState<UserInformation | null>(null);
  const [token, setToken] = useState<string | null>(null);

  useEffect(() => {
    // Get the user and token from localStorage when the app initializes
    const storedToken = StorageService.getValue<string>('authToken');
    const storedUser = StorageService.getValue<UserInformation>('currentUser');

    if (storedUser && storedToken) {
      setUser(storedUser);
      setToken(storedToken);
    }
  }, []);

  const login = (authResponse: AuthResponse) => {
    setUser(authResponse.user);
    setToken(authResponse.token);

    StorageService.setValue('authToken', authResponse.token);
    StorageService.setValue('currentUser', authResponse.user);
  };

  const logout = () => {
    setUser(null);
    setToken(null);

    StorageService.removeValue('authToken');
    StorageService.removeValue('currentUser');

    // Redirect to login page and clear history to prevent navigation back
    navigate('/login', { replace: true });

    toast.success('Logged out successfully.');
  };

  return (
    <AuthContext.Provider value={{ user, token, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider');
  }
  return context;
};
