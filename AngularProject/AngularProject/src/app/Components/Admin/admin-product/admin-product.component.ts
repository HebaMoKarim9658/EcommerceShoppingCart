import { Component, OnInit } from '@angular/core';
import {ProductsService} from 'src/app/Services/products.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-admin-product',
  templateUrl: './admin-product.component.html',
  styleUrls: ['./admin-product.component.css']
})
export class AdminProductComponent implements OnInit {

  subscriber;
  Products:any[]=[];;
  FilterdProduct: any[]=[];
  _productID;
  constructor(private myServices:ProductsService,private router:Router) { }

  ngOnInit(): void {
    
    this.subscriber = this.myServices.getAllProducts().subscribe((products:any[])=>{
      console.log(products);
      if(products){
        this.FilterdProduct =  this.Products = products;
      }

    },(error)=>{
      console.log(error);
    })
  }

  
  filterProduct(productName:string){

    console.log("Enterd");
    console.log(productName);
    this.FilterdProduct = (productName)?
    this.Products.filter(p => p.title.toLowerCase().includes(productName.toLowerCase())):this.Products;

  }

  DeleteProduct(productID){

    console.log(productID);

    this.myServices.DeleteProduct(productID).subscribe((response)=>{
      console.log(response);
    },(error)=>{
      console.log(error);
    });


  var _Deltedproduct = this.FilterdProduct.find(p => p.productID==productID);
  var index =  this.FilterdProduct.indexOf(_Deltedproduct);
    this.FilterdProduct.splice(index,1);

  }

  goUpdatePage(id){
    this._productID = id;
    this.router.navigateByUrl(`/updateProduct/${id}`);

  }


  addProduct(){
    this.router.navigateByUrl('/CreateProduct')
  }




}
