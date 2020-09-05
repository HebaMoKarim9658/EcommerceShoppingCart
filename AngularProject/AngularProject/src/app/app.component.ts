import { Component, OnInit, OnDestroy } from '@angular/core';
import { AccountService } from './Services/account.service';
import { OrderService } from './Services/order.service';
import { Subscription } from 'rxjs';
import { ProductsService } from './Services/products.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit, OnDestroy {
  constructor(public accService: AccountService,
    public productService: ProductsService
    , public orderService: OrderService) {
  }
  ngOnDestroy(): void {
    this.Subscription.unsubscribe()
  }
  ngOnInit(): void {
    this.orderService.GetTotalQuantity().subscribe(result => {
      this.Total = result
    })
    this.productService.myEvent.subscribe((count) => {
      this.Total = count;
    })
  }
  title = 'project';
  Total;
  Subscription: Subscription;
  fun() {
    console.log("Eventttttttttttttttttttttt")
  }

}
