import { Component, EventEmitter, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountservice = inject(AccountService)
  toastr = inject(ToastrService)
  /*usersFromHomeComponent = input.required<any>()*/
  cancelRegister = output<boolean>();
  model: any = {};
  register() {
    this.accountservice.register(this.model).subscribe(
      {
        //next: response => { console.log(response); this.cancel(); },
        next: response => response,
        error: error => this.toastr.error(error.error)
      })  

  }
  cancel() {
    this.cancelRegister.emit(false);
  }
}
