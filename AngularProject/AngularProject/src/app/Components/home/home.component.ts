import { Component, OnInit } from '@angular/core';
import { ProductsService } from 'src/app/Services/products.service'

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  products;

  constructor(private myServices:ProductsService) { }

  ngOnInit(): void {
    this.myServices.getAllProducts().subscribe((Products)=>{
    

      if(Products){
        this.products = Products;
      }
    this.products = this.products.slice(1,10);
    console.log(this.products);

    });

  }

}
