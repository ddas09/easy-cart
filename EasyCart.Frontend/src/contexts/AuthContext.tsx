import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import StorageService from '../services/StorageService';
import { AuthResponse, UserInformation } from '../models/AuthResponse';
import { JwtTokenContainerModel } from '../models/JwtTokenContainerModel';
import { createContext, useContext, useState, useEffect, ReactNode } from 'react';

interface AuthContextType {
  user: UserInformation | null;
  tokens: JwtTokenContainerModel | null;
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
  const [tokens, setTokens] = useState<JwtTokenContainerModel | null>(null);
  const [loading, setLoading] = useState(true);  // Add loading state

  useEffect(() => {
    // Get the user and token from localStorage when the app initializes
    const tokens = StorageService.getValue<JwtTokenContainerModel>('tokens');
    const storedUser = StorageService.getValue<UserInformation>('currentUser');

    if (storedUser && tokens) {
      setUser(storedUser);
      setTokens(tokens);
    }
    
    setLoading(false);  // Set loading to false once data is loaded
  }, []);

  const login = (authResponse: AuthResponse) => {
    setUser(authResponse.user);
    setTokens(authResponse.tokenContainer);

    StorageService.setValue('currentUser', authResponse.user);
    StorageService.setValue<JwtTokenContainerModel>('tokens', authResponse.tokenContainer);

    navigate('/dashboard', { replace: true });
  };

  const logout = () => {
    setUser(null);
    setTokens(null);
    StorageService.clearStorage();
    navigate('/login', { replace: true });
    toast.success('Logged out successfully.');
  };

  if (loading) {
    // Optionally show a loader or spinner while checking for the user
    return <div>Loading...</div>;
  }

  return (
    <AuthContext.Provider value={{ user, tokens, login, logout }}>
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
