import './Register.css';
import { toast } from 'react-toastify';
import React, { useState } from 'react';
import { useAuth } from '../../../contexts/AuthContext';
import { AuthResponse } from '../../../models/AuthResponse';
import apiService from '../../../services/APIService';

const Register = () => {  
  const { login } = useAuth();
  const [loading, setLoading] = useState(false);

  const handleRegister = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoading(true);

    const formData = new FormData(e.currentTarget);
    const email = formData.get('email')?.toString().trim() || '';
    const password = formData.get('password')?.toString().trim() || '';
    const confirmPassword = formData.get('confirmPassword')?.toString().trim() || '';

    if (!email || !password || !confirmPassword) {
      toast.error('Please fill in all the fields.');
      setLoading(false);
      return;
    }

    if (password !== confirmPassword) {
      toast.error('Entered passwords do not match.');
      setLoading(false);
      return;
    }

    try {
      const { data, status } = await apiService.post<AuthResponse>('auth/register', formData); // Replace with your register endpoint

      if (status === 'success') {
        login(data as AuthResponse);
        toast.success('Account created successfully.');
      }
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="register-container">
      <form onSubmit={handleRegister} className="register-form">
        <h1>Create Account</h1>
        <div>
          <label>Email</label>
          <input type="email" name="email" placeholder="Enter your email" required />
        </div>
        <div>
          <label>Password</label>
          <input type="password" name="password" placeholder="Enter your password" required />
        </div>
        <div>
          <label>Confirm Password</label>
          <input type="password" name="confirmPassword" placeholder="Confirm your password" required />
        </div>
        <button type="submit" disabled={loading}>
          {loading ? 'Signing you up...' : 'Create account'}
        </button>
        <p>Already have an account? <a href="/login">Login here</a></p>
      </form>
    </div>
  );
};

export default Register;
