import { ProductInformation } from '../../../../models/ProductResponse';
import './ManageProduct.css';
import { useState } from 'react';

interface ManageProductProps {
  product?: ProductInformation; // Optional, passed when editing
  onSave: (product: ProductInformation) => void; // Callback to save the product
  onCancel: () => void; // Callback to cancel the operation
}

const ManageProduct = ({ product, onSave, onCancel }: ManageProductProps) => {
  const [formData, setFormData] = useState<ProductInformation>({
    id: product?.id || 0,
    name: product?.name || '',
    description: product?.description || '',
    price: product?.price || 0,
    stock: product?.stock || 0,
    imageURL: product?.imageURL || '',
  });

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData({
      ...formData,
      [name]: name === 'price' || name === 'stock' ? parseFloat(value) : value,
    });
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSave(formData);
  };

  return (
    <div>
      {/* Modal for adding/editing products */}
      <div className="modal-overlay">
        <div className="modal-content">
          <h2>{product ? 'Edit Product' : 'Add Product'}</h2>
          <form onSubmit={handleSubmit}>
            <label>
              Name:
              <input
                type="text"
                name="name"
                value={formData.name}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Description:
              <textarea
                name="description"
                value={formData.description}
                onChange={handleChange}
                required
              />
            </label>
            <label>
              Price:
              <input
                type="number"
                name="price"
                value={formData.price}
                onChange={handleChange}
                required
                step="0.01"
              />
            </label>
            <label>
              Stock:
              <input
                type="number"
                name="stock"
                value={formData.stock}
                onChange={handleChange}
                required
              />
            </label>
            <div className="form-actions">
              <button type="submit">Save</button>
              <button type="button" onClick={onCancel}>
                Cancel
              </button>
            </div>
          </form>
        </div>
      </div>
    </div>
  );
};

export default ManageProduct;