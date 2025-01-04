export interface ProductInformation {
    id: number;
    name: string;
    description: string;
    price: number;
    stock: number;
    imageURL: string;
}
  
export interface ProductResponse {
    products: ProductInformation[];
}
  