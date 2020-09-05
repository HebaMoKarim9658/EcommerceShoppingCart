import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/Services/account.service';
import { OrderService } from '../../Services/order.service';
import { ShoppingCartService } from '../../Services/shopping-cart.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  currentUser;
  isDataLoaded = false;
  PendingOrders;
  AcceptedOrders;
  RejectedOrders;
  constructor(private router: Router, private orderService: OrderService, private shoppingService: ShoppingCartService, private currentUserService: AccountService) { }

  ngOnInit(): void {
    let subscrription = this.currentUserService.GetCurrentUserInfo()
      .subscribe((data) => {
        this.currentUser = data;
        console.log(this.currentUser)
        this.isDataLoaded = true;
      },
        (err) => {
          console.log(err.statusText);
          this.isDataLoaded = false;
        })
    console.log(subscrription)
    this.GetPendingOrders()
    this.GetRejectedOrders()
    this.GetAcceptedOrders()
  }
  GoEdit() {
    this.router.navigateByUrl('/editProfile');
  }

  GetPendingOrders() {
    this.orderService.GetPendingOrders().subscribe((response) => {
      this.PendingOrders = response
      console.log("pending" + this.PendingOrders)
    }, (error => {
    }))
  }
  GetRejectedOrders() {
    this.orderService.GetRejectedOrders().subscribe((response) => {
      this.RejectedOrders = response
      console.log("rejected" + this.RejectedOrders)
    }, (error => {
    }))
  }
  GetAcceptedOrders() {
    this.orderService.GetAcceptedOrders().subscribe((response) => {
      this.AcceptedOrders = response
      console.log("accepted" + this.AcceptedOrders)
    }, (error => {
    }))
  }
  CancelOrder(id) {
    this.shoppingService.cancelOrder(id).subscribe((response) => {
      this.ngOnInit()
    }, (error => {

    }))
  }
}
