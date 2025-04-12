import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { Member } from '../../_models/Member';
import { AccountService } from '../../_services/account.service';
import { MembersService } from '../../_services/members.service';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule,FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;
    }

  }
  member?: Member;
  private accountService =  inject(AccountService)
  private memberService =  inject(MembersService)
  private toastr =  inject(ToastrService)
  ngOnInit(): void {
    this.loadMember();
  }
  loadMember() {
    const user = this.accountService.currentUser();
    if (!user) return;
    this.memberService.getMember(user.userName).subscribe({
      next: member => {
        console.log("Ima edit", member);
        this.member = member;
      }
    })
  }
  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        console.log(this.member);
        this.toastr.success("Profile Updated successfully");
        this.editForm?.reset(this.member);
      }
      })
  }
}

//function HostListstner(arg0: string, arg1: string[]) {
//    throw new Error('Function not implemented.');
//}
