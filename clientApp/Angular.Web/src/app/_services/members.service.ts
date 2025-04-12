import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { of, tap } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/Member';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);

  baseUrl= environment.apiUrl;

  members = signal<Member[]>([]);

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users').subscribe({
       next: members=> this.members.set(members) 
      });

  }
  getMember(username: string) {
    console.log("Memberservice username :", username);
    const member = this.members().find(x => x.userName === username);
    if (member !== undefined) return of(member);
    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }
  updateMember(member:Member) {
    return this.http.put(this.baseUrl + 'users', member).pipe(
      tap(() => {
        this.members.update(members=>members.map(m=>m.userName === member.userName? member:m))
      })
    );
  }

}
