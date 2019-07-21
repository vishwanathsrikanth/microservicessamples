import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { IUser } from './../../models/user.interface'

@Injectable()
export class UserService {

  usersUrl = '/api/user/';

  constructor(private _http: HttpClient) { }

  getUser(): Observable<IUser> {
    return this._http.get<IUser>(this.usersUrl);
  }
}
