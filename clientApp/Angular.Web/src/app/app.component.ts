import { CommonModule } from '@angular/common';
import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { NgxSpinnerComponent } from 'ngx-spinner';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { AccountService } from './_services/account.service';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule, NavComponent, HomeComponent, NgxSpinnerComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
  
export class AppComponent implements OnInit{
 
  private accountservice = inject(AccountService)
  
  users: any;

  ngOnInit(): void {
    
    this.setcurrentuser();
  }
  setcurrentuser() {
    const userString = localStorage.getItem('user');
    if (!userString) return;
    const user = JSON.parse(userString);
    this.accountservice.currentUser.set(user)
    console.log(this.accountservice.currentUser)
  }
  
}
