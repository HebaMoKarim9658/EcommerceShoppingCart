import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { RegistrationService } from 'src/app/Services/registration.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  DuplicateUserName = false;
  GenderValidation = false ;



  constructor(private myService: RegistrationService , private router: Router) { }

  ngOnInit(): void {
  }


  registrationForm = new FormGroup({
    username: new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(20),
    Validators.pattern("^(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$")]),

    Email: new FormControl('',[Validators.required, Validators.pattern("^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$")]),

    Password: new FormControl('', [Validators.required, Validators.minLength(8), Validators.maxLength(20),
    Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,}')]),

    phoneNumber: new FormControl('',[Validators.required , Validators.minLength(11) , Validators.maxLength(11)]),

    gender: new FormControl('', [Validators.required])
  })

  formValid = () => {return this.registrationForm.valid && !this.DuplicateUserName}


  Submit(RegesteredUser){
    console.log(this.registrationForm.controls.phoneNumber)
    if(this.registrationForm.controls.gender.status === "INVALID") {this.GenderValidation = true}
    if (this.registrationForm.valid && !this.DuplicateUserName){
      let  data = {Email: RegesteredUser.Email , Username: RegesteredUser.username.toString() ,
         Password: RegesteredUser.Password , PhoneNumber : RegesteredUser.phoneNumber 
         ,Gender: RegesteredUser.gender}

         console.log(data)
         let subscrription = this.myService.RegisterUser(data)
        .subscribe((data) => {
          if(data){
            this.router.navigate(['/login']);
          }
          console.log(data)
        },
          (err) => {
            console.log(err.error.value.errors[0])
          })

          console.log(subscrription)
    }
  }

  checkUserName(user): any {
    console.log(user.username.toString())

    let subscrription = this.myService.validateUsername(user.username.toString())
    .subscribe((data) => {
      console.log(data)
      this.DuplicateUserName = true;
    },
      (err) => {
        this.DuplicateUserName = false;
         console.log(err.statusText);
      })
  }



  
}
