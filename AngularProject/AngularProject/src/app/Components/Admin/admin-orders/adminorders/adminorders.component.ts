import { Component, OnInit } from '@angular/core';
import { OrderService } from 'src/app/Services/order.service';

@Component({
  selector: 'app-adminorders',
  templateUrl: './adminorders.component.html',
  styleUrls: ['./adminorders.component.css']
})
export class AdminordersComponent implements OnInit {

  constructor(private orderService: OrderService) { }
  AllOrders;

  ngOnInit(): void {
    this.GetAllOrders()
  }
  GetAllOrders() {
    this.orderService.GetAllOrders().subscribe((response) => {
      this.AllOrders = response
    })
  }
  onChange(value, currentOrder) {
    this.orderService.UpdateOrder(currentOrder, value).subscribe((res) => {
      //console.log("state " + value);
    });
  }
}
