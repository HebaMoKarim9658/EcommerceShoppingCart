import { Component, OnInit, Input, OnDestroy, Output, EventEmitter } from '@angular/core';
import { AccountService } from '../../../Services/account.service';
import { OrderService } from '../../../Services/order.service';
import { Subscription } from 'rxjs';
import { AppComponent } from '../../../app.component';
import { ProductsService } from '../../../Services/products.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  constructor(private AccService: AccountService,
    private productService: ProductsService,
    private OrderService: OrderService) {

  }
  @Input('product') product;
  Quantity;
  GetQuantity() {
    this.OrderService.GetCurrentOrder().subscribe(CurrOrderID => {
      this.OrderService.GetProductQuantity(this.product.productID, CurrOrderID).
        subscribe(result => {
          console.log(result)
          this.Quantity = result
        })
    })
  }
  ngOnInit(): void {
    this.GetQuantity()
    // this.OrderService.
    //   GetProductQuantity(this.product.productID).subscribe(result => {
    //     this.Quantity = result;
    //   })
  }
  AddCart() {
    this.OrderService.GetCurrentOrder().subscribe(CurrOrderID => {
      if (CurrOrderID != 0) {
        console.log("msh zerooooooooooo")
        this.OrderService.AddProductsToOrder(this.product.productID, CurrOrderID).
          subscribe(result => {
            if (result) {
              this.productService.addCartCount();
            }
          })
      }
      else if (CurrOrderID == 0) {
        console.log("dh zeroooo")
        this.OrderService.AddOrder().
          subscribe(NewOrderID => {
            if (NewOrderID) {
              this.OrderService.AddProductsToOrder(this.product.productID, NewOrderID["orderID"]).
                subscribe(result => {
                  if (result) {
                    this.productService.addCartCount();
                  }
                })
            }
          })
      }
      this.ngOnInit()
    })
  }
}