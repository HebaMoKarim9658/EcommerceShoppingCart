import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {

  baseURL = "https://htla2yapi.azurewebsites.net";
  constructor(private myClient: HttpClient) { }

  GetShoppingCartItems() {
    let token = localStorage.getItem('token');
    let headers = new HttpHeaders();
    headers = headers.set('Authorization', 'Bearer ' + token);
    return this.myClient.get(`${this.baseURL}/api/Orders/GetCurrentOrderDetails`,
      { headers: headers });
  }
  checkOut(orderID) {
    console.log("ra7llcheckoutahooooooo" + orderID)
    return this.myClient.get(`${this.baseURL}/api/Orders/CheckOut/` + orderID)
  }
  cancelOrder(orderID) {
    console.log("ra7llcancel" + orderID)
    return this.myClient.get(`${this.baseURL}/api/Orders/CancelOrder/` + orderID)
  }
  removeProduct(orderID, productID) {
    return this.myClient.get(`${this.baseURL}/api/OrderDetails/removeProduct/OrderID=` + orderID +
      `&ProductID=` + productID)
  }
}
