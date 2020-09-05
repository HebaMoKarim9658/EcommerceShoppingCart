import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductsService } from 'src/app/Services/products.service';

@Component({
  selector: 'app-update-product',
  templateUrl: './update-product.component.html',
  styleUrls: ['./update-product.component.css']
})
export class UpdateProductComponent implements OnInit {
  productName;
  price;
  details;
  CID;
  _productID;
  imageSrc;
  constructor(private route: ActivatedRoute,
    private myServices: ProductsService,
    private router: Router) { }

  ngOnInit(): void {
    this._productID = this.route.snapshot.params["id"];

    this.myServices.getProductById(this._productID).subscribe((Product) => {
      console.log(Product);
      this.CID = Product["categoryID"];
      this.productName = Product["title"];
      this.details = Product["details"];
      this.price = Product["price"];
      this.imageSrc = Product["image"];

    }, (error) => {
      console.log(error);
    })


  }
  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        console.log(this.imageSrc)
        this.imageSrc = reader.result as string;
        // this.myForm.patchValue({
        //   fileSource: reader.result
        // });
      };
    }
  }
  saveChanges() {
    let product = {
      productID: this._productID,
      categoryID: this.CID,
      title: this.productName,
      price: this.price,
      details: this.details,
      image: this.imageSrc
    }
    console.log(product);
    console.log(this._productID);
    this.myServices.updateProduct(this._productID, product).subscribe((response) => {
      console.log(response);
    }, (error) => {
      console.log(error);
    });

    this.router.navigateByUrl('/admin');

  }

}
