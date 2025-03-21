import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { Member } from '../_models/Member';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
  private http = inject(HttpClient);

  baseUrl= environment.apiUrl;


  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + 'users');
  }
  getMember(username: string) {
    console.log("Memberservice username :", username)
    console.log(" username :", this.http.get<Member>(this.baseUrl + 'users/' + username));


    return this.http.get<Member>(this.baseUrl + 'users/' + username);
  }

}
