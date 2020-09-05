import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShoppingCartService } from '../../../Services/shopping-cart.service';
import { Subscription } from 'rxjs';
import { Router } from '@angular/router';
import { ProductsService } from '../../../Services/products.service';
import { OrderService } from '../../../Services/order.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {

  constructor(private cartService: ShoppingCartService,
    private productService: ProductsService, private orderService: OrderService
    , private router: Router) { }
  Order;
  lastProduct: boolean = false;

  ngOnInit(): void {
    console.log("yndhhadlw2ty")
    console.log("lastproduct", this.lastProduct)
    this.orderService.GetShoppingCartItems().subscribe(res => {
      this.Order = res
      console.log("res" + res)
    }, (err) => {
      console.log(err)
    })
  }
  checkOut(orderID) {
    //console.log("hy3mlcheck")
    this.cartService.checkOut(orderID).subscribe(res => {
      //console.log("kda 5las 3ml cheackout")
      this.productService.removeCartCount()
      this.ngOnInit()
    },
      err => {
        console.log("error" + err)
      })
  }
  removeProduct(orderID, productID, quantity) {//h5od qunatity
    //console.log(orderID, productID)
    if (this.Order.orderDetails.length == 1) {
      //console.log("25erProduct")
      this.lastProduct = true;
    }
    this.cartService.removeProduct(orderID, productID).subscribe(res => {
      if (this.lastProduct == true) {
        this.cartService.cancelOrder(orderID).subscribe(cancelRes => {
          this.cartService.checkOut(orderID).subscribe(result => {
            if (result != 0) {
              this.productService.removeCartCount()
              this.lastProduct = false;
              this.ngOnInit()
            }
          }, (err => {

          }))
        },
          (err => {
            console.log("error" + err)
          })
        )
      }
      else {
        this.productService.deletefromCartCount(quantity)
        this.ngOnInit()
      }
    })
  }
}