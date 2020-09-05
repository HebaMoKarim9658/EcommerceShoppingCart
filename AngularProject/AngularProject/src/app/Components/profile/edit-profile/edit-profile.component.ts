import { Component, OnInit } from '@angular/core';
import { EditProfileService } from 'src/app/Services/edit-profile.service'
import { AccountService } from 'src/app/Services/account.service'
import { FormGroup, Validators, FormControl } from '@angular/forms';

import { Router } from '@angular/router';
@Component({
  selector: 'app-edit-profile',
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent implements OnInit {

  currentUserEdit;
  subscrription;
  imageSrc: string;

  constructor(private myServices: EditProfileService, private AccServices: AccountService, private router:Router) { }

  ngOnInit(): void {
    this.subscrription = this.AccServices.GetCurrentUserInfo()
      .subscribe((data) => {
        this.currentUserEdit = data;
        console.log(this.currentUserEdit)
      },
        (err) => {
          console.log(err.statusText);
        })
  }

  myForm = new FormGroup({
    password: new FormControl(),
    file: new FormControl('', [Validators.required]),
    fileSource: new FormControl('', [Validators.required]),
    confirmPassword: new FormControl(),
    email: new FormControl('', [Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")])
  });

  IsConfirmedPassword = () => { return this.myForm.value.password != this.myForm.value.confirmPassword }


  onFileChange(event) {
    const reader = new FileReader();
    if (event.target.files && event.target.files.length) {
      const [file] = event.target.files;
      reader.readAsDataURL(file);
      reader.onload = () => {
        this.imageSrc = reader.result as string;
        this.myForm.patchValue({
          fileSource: reader.result
        });
      };
    }
  }

  saveChanges() {

    console.log(this.myForm.value && this.IsConfirmedPassword)
    let body = { id: this.currentUserEdit.id, password: this.myForm.value.password, Image: this.imageSrc, Email: this.myForm.value.email };

    if (this.myForm.valid && this.IsConfirmedPassword) {
      this.subscrription = this.AccServices.UpdateUser(body)
        .subscribe((data) => {
          this.currentUserEdit = data;
          console.log(this.currentUserEdit)
          this.router.navigate(['/profile']);
        },
          (err) => {
            console.log(err.statusText);
          })

    }
  }
}
