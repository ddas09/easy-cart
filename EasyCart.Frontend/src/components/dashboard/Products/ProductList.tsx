import React, { useEffect, useState } from 'react';
import apiService from '../../../services/APIService';
import { ProductInformation, ProductResponse } from '../../../models/ProductResponse';
import './ProductList.css';

interface ProductListProps {
  searchTerm: string;
}

const ProductList: React.FC<ProductListProps> = ({ searchTerm }) => {
  const [products, setProducts] = useState<ProductInformation[]>([]);
  const [filteredProducts, setFilteredProducts] = useState<ProductInformation[]>([]);
  const [loading, setLoading] = useState<boolean>(true);

  // Mock data generator
  const generateMockProducts = (): ProductInformation[] => [
    {
      id: 1,
      name: "Wireless Headphones",
      description: "Noise-cancelling wireless headphones with long battery life.",
      price: 99.99,
      stock: 15,
      imageURL: "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=200",
    },
    {
      id: 2,
      name: "Smartphone",
      description: "Latest model with high performance and great camera quality.",
      price: 699.99,
      stock: 8,
      imageURL: "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=200",
    },
    {
      id: 3,
      name: "Gaming Laptop",
      description: "High-performance gaming laptop with a powerful GPU.",
      price: 1499.99,
      stock: 5,
      imageURL: "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=200",
    },
    {
      id: 4,
      name: "Smartwatch",
      description: "Stylish smartwatch with fitness tracking capabilities.",
      price: 199.99,
      stock: 25,
      imageURL: "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=200",
    },
    {
      id: 5,
      name: "Bluetooth Speaker",
      description: "Portable Bluetooth speaker with excellent sound quality.",
      price: 49.99,
      stock: 30,
      imageURL: "https://images.unsplash.com/photo-1542314831-068cd1dbfeeb?crop=entropy&cs=tinysrgb&fit=max&fm=jpg&q=80&w=200",
    },
  ];
  

  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      try {
        // Uncomment the line below to fetch from the real API
        // const response = await apiService.get<ProductResponse>('/products');
        // if (response.status === 'success' && response.data) {
        //   setProducts(response.data.products);
        //   setFilteredProducts(response.data.products);
        // }

        // Comment this line when using the real API
        const mockData = generateMockProducts();
        setProducts(mockData);
        setFilteredProducts(mockData);
      } finally {
        setLoading(false);
      }
    };

    fetchProducts();
  }, []);

  useEffect(() => {
    const filtered = products.filter((product) =>
      product.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    setFilteredProducts(filtered);
  }, [searchTerm, products]);

  return (
    <div className="product-list-container">
      {loading ? (
        <p className="loading-text">Loading products...</p>
      ) : filteredProducts.length > 0 ? (
        <div className="product-grid">
          {filteredProducts.map((product) => (
            <div key={product.id} className="product-card">
              <img src={product.imageURL} alt={product.name} />
              <h3>{product.name}</h3>
              <p>{product.description}</p>
              <p>
                <strong>${product.price.toFixed(2)}</strong>
              </p>
              <p className="product-stock">Stock: {product.stock}</p>
            </div>
          ))}
        </div>
      ) : (
        <p className="no-products-text">No products found.</p>
      )}
    </div>
  );
};

export default ProductList;
