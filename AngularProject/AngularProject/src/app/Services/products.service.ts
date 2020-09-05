import { Injectable, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class ProductsService {

  baseURL = "https://htla2yapi.azurewebsites.net";
  constructor(private myClient: HttpClient) { }

  cartCount: number = 0;
  myEvent = new EventEmitter<Number>();
  addCartCount() {
    this.cartCount += 1;
    this.myEvent.emit(this.cartCount);
    return this.cartCount;
  }
  deletefromCartCount(quantity) {
    if (this.cartCount == 0)
      this.cartCount = 0;
    else
      this.cartCount -= quantity;
    this.myEvent.emit(this.cartCount);
  }
  removeCartCount() {
    this.cartCount = 0;
    this.myEvent.emit(this.cartCount);
    return this.cartCount;
  }
  getAllProducts() {
    return this.myClient.get(`${this.baseURL}/api/products`);
  }

  addProduct(product) {
    return this.myClient.post(`${this.baseURL}/api/products`, product);
  }

  DeleteProduct(productID) {
    return this.myClient.delete(`${this.baseURL}/api/products/${productID}`);
  }

  getProductById(productID) {
    return this.myClient.get(`${this.baseURL}/api/products/${productID}`);
  }

  updateProduct(productID, product) {
    return this.myClient.put(`${this.baseURL}/api/Products/${productID}`, product);
  }

}
