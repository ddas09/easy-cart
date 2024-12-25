import './Login.css';
import { toast } from 'react-toastify';
import React, { useState } from 'react';
import { AuthResponse } from '../../../models/AuthResponse';
import apiService from '../../../services/APIService';
import { useAuth } from '../../../contexts/AuthContext';

const Login = () => {
  const { login } = useAuth();
  const [loading, setLoading] = useState(false);

  const handleLogin = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);

    const formData = new FormData(e.currentTarget);
    const email = formData.get('email')?.toString().trim() || '';
    const password = formData.get('password')?.toString().trim() || '';

    if (!email || !password) {
      toast.error('Please fill in all the fields.');
      setLoading(false);
      return;
    }

    try {
      const { data, status } = await apiService.post<AuthResponse>('auth/login', formData);

      if (status === 'success') {
        login(data as AuthResponse);
        toast.success('Logged in successfully.');
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="login-container">
      <form onSubmit={handleLogin} className="login-form">
        <h1>Login</h1>
        <div>
          <label>Email</label>
          <input type="email" name="email" placeholder="Enter your email" required />
        </div>
        <div>
          <label>Password</label>
          <input type="password" name="password" placeholder="Enter your password" required />
        </div>
        <button type="submit" disabled={loading}>
          {loading ? 'Logging in...' : 'Login'}
        </button>
        <p>Don't have an account yet? <a href="/register">Register here</a></p>
      </form>
    </div>
  );
};

export default Login;
