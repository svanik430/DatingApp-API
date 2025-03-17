import { Component, inject, OnInit } from '@angular/core';
import { Member } from '../../_models/Member';
import { MembersService } from '../../_services/members.service';
import { MemberCardComponent } from '../../members/member-card/member-card.component';


@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit{
  private memberService = inject(MembersService)
  members: Member[] =[];
  ngOnInit():void {
    this.loadMembers();
  }
  loadMembers() {
    this.memberService.getMembers().subscribe({
      next: members => {
        this.members = members
        console.log("Memberlist", members)
      }
    
      })
  }
}
