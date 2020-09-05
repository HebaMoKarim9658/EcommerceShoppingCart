import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule, Validators, FormBuilder, FormGroup, FormControl } from '@angular/forms';
import { CategoriesService } from 'src/app/Services/categories.service';
import { ProductsService } from 'src/app/Services/products.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-product',
  templateUrl: './create-product.component.html',
  styleUrls: ['./create-product.component.css']
})
export class CreateProductComponent implements OnInit {

  IsFileSelected = true;
  IsCategorySelected = true;

  imageSrc: string = null;

  categories: any;
  categorySelected;

  constructor(private categoriesService: CategoriesService, private productService: ProductsService, private router: Router) { }

  ngOnInit() {
    this.categoriesService.getAllCategories().subscribe(
      (data) => {
        console.log(data);
        this.categories = data;
      },
      (err) => { console.log(err) }

    );
  }

  selectChangeHandler(e) {
    this.categorySelected = e.target.value;
  }


  CreateProductForm = new FormGroup({
    category: new FormControl('', Validators.required),
    price: new FormControl('', Validators.required),
    details: new FormControl('', Validators.required),
    title: new FormControl('', Validators.required),
    // file: new FormControl('',Validators.required)
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required]),
  })

  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.CreateProductForm.patchValue({
          fileSource: reader.result
        });
      };
    }
  }


  Submit(formData) {

    let data = {
      Title: formData.title, Details: formData.details, Image: this.imageSrc,
      Price: formData.price, CategoryID: formData.category
    };

    if (this.CreateProductForm.controls.fileSource.status === "INVALID") { this.IsFileSelected = false }
    if (this.CreateProductForm.controls.category.status === "INVALID") { this.IsCategorySelected = false }

    if (this.CreateProductForm.valid) {
      this.productService.addProduct(data).subscribe(
        (data) => {
          console.log(data)
          this.router.navigateByUrl('/admin')
        }

      )
    }
  }
}
