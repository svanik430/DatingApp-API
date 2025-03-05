import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit } from '@angular/core';
import { RegisterComponent } from '../register/register.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RegisterComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
  http = inject(HttpClient);
  users: any;
  registerMode = false;

  ngOnInit() {
    this.getusers();
  }
  registerToggle() {
    this.registerMode = !this.registerMode;
  }
  cancelRegisterMode(event: boolean) {
    this.registerMode = event;
  }
  getusers() {
    console.log("Iam get");
    this.http.get('http://localhost:5296/api/users/').subscribe({
      next: response => this.users = response,
      error: error => console.log(error),
      complete: () => console.log("Request is completed")
    })

  }
}
