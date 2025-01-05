import { ProductInformation, ProductResponse } from '../../../../models/ProductResponse';
import apiService from '../../../../services/APIService';
import ManageProduct from '../ManageProduct/ManageProduct';
import './ProductList.css';
import { useEffect, useState } from 'react';

interface ProductListProps {
  searchTerm: string;
  isAdmin: boolean;
}

const ProductList = ({ searchTerm, isAdmin }: ProductListProps) => {
  const [products, setProducts] = useState<ProductInformation[]>([]);
  const [filteredProducts, setFilteredProducts] = useState<ProductInformation[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [showManageProduct, setShowManageProduct] = useState<boolean>(false);
  const [editingProduct, setEditingProduct] = useState<ProductInformation | null>(null);

  useEffect(() => {
    const fetchProducts = async () => {
      setLoading(true);
      try {
        const response = await apiService.get<ProductResponse>('/products');
        if (response.status === 'success' && response.data) {
          setProducts(response.data.products);
          setFilteredProducts(response.data.products);
        }
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

  const handleSaveProduct = async (product: ProductInformation) => {
    try {
      if (product.id === 0) {
        // Add new product
        const response = await apiService.post<ProductInformation>('/products', product);
        if (response.status === 'success') {
          setProducts([...products, response.data as ProductInformation]);
        }
      } else {
        // Update existing product
        const response = await apiService.put<ProductInformation>(`/products/${product.id}`, product);
        if (response.status === 'success') {
          const updatedProducts = products.map((p) =>
            p.id === product.id ? response.data : p
          );
          setProducts(updatedProducts as ProductInformation[]);
        }
      }
    } finally {
      setShowManageProduct(false);
      setEditingProduct(null);
    }
  };

  const handleDeleteProduct = async (id: number) => {
    const response = await apiService.delete<ProductResponse>(`/products/${id}`);
    if (response.status === 'success') {
      setProducts(products.filter((product) => product.id !== id)); // Remove product by ID
    }
  };

  const handleAddProduct = () => {
    setEditingProduct(null); // Clear any existing product for new addition
    setShowManageProduct(true); // Show product management form
  };

  const handleEditProduct = (product: ProductInformation) => {
    setEditingProduct(product); // Set the product to be edited
    setShowManageProduct(true); // Show product management form
  };

  return (
    <div className="product-list-container">
      {isAdmin && (
        <button className="add-product-button" onClick={handleAddProduct}>
          <span className="plus-sign">+</span>
        </button>
      )}
      {loading ? (
        <p>Loading products...</p>
      ) : filteredProducts.length > 0 ? (
        <div className="product-grid">
          {filteredProducts.map((product) => (
            <div key={product.id} className="product-card">
              <img src={product.imageURL} alt={product.name} />
              <h3>{product.name}</h3>
              <p>{product.description}</p>
              <p>Price: <strong>${product.price.toFixed(2)}</strong></p>
              <p>Stock: <strong>{product.stock}</strong></p>
              {isAdmin && (
                <div className="admin-buttons">
                  <button onClick={() => handleEditProduct(product)}>Edit</button>
                  <button onClick={() => handleDeleteProduct(product.id)}>Delete</button>
                </div>
              )}
            </div>
          ))}
        </div>
      ) : (
        <p>No products found.</p>
      )}

      {showManageProduct && (
        <ManageProduct
          product={editingProduct || undefined}
          onSave={handleSaveProduct}
          onCancel={() => setShowManageProduct(false)}
        />
      )}
    </div>
  );
};

export default ProductList;
