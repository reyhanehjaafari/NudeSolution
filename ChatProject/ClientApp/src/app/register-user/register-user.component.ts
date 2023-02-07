import { Component, Inject } from '@angular/core';
import User from '../data-model/user';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register-user',
  templateUrl: './register-user.component.html',
})
export class RegisterUserComponent {

  user!: User;
  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
    this.user = new User();
  }

  signUp() {
    this.http.post("https://localhost:7067/api/User/create", this.user.registerUser()).subscribe(x => {

    });

    console.log("Hello" + JSON.stringify(this.user))
  }
}
