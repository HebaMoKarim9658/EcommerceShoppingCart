import { Component, OnInit, OnDestroy } from '@angular/core';
import {ProductsService} from 'src/app/Services/products.service';
import { CategoriesService } from 'src/app/Services/categories.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.css']
})
export class ProductsComponent implements OnInit, OnDestroy {

  subscriber;
  Products:any[]=[];;
  Categories;
  Cate_subscriber;
  id;
  FilterdProduct: any[]=[];

  constructor (
    private myServices:ProductsService,
     private myCategoryServices:CategoriesService,
     private route:ActivatedRoute
     ){}
  
  ngOnDestroy(): void {
    this.subscriber.unsubscribe();
  }

  ngOnInit(): void {

    this.subscriber = this.myServices.getAllProducts().subscribe((products:any[])=>{
      console.log(products);
      if(products){
        this.FilterdProduct =  this.Products = products;
      }

      this.route.queryParamMap.subscribe((params)=>{

        this.id = params.get('id');
        this.FilterdProduct = (this.id)?
        this.Products.filter(p => p.categoryID == this.id ):this.Products;
  
      });

    },(error)=>{
      console.log(error);
    })


    this.Cate_subscriber = this.myCategoryServices.getAllCategories().subscribe((categories)=>{
      console.log(categories);
      if(categories){
        this.Categories = categories;
      }
    },(error)=>{
      console.log(error);
      
    })


  }

  filterProduct(productName:string){

    console.log(productName);
    this.FilterdProduct = (productName)?
    this.Products.filter(p => p.title.toLowerCase().includes(productName.toLowerCase())):this.Products;

  }

}
