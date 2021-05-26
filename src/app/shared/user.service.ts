import { Injectable } from '@angular/core';
import { User } from './user.model'
import { HttpClient } from '@angular/common/http'
import { HttpHeaders } from '@angular/common/http';  
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Component, OnInit, Input } from '@angular/core'; 
 

@Injectable({
  providedIn: 'root'
})
export class UserService {

 readonly baseURL = 'http://localhost:26561/api/User'
  public formData: User = new User();
  public users: any;
  public f: Observable<User[]>;
  public d: User[] = [];

  constructor(private httpClient: HttpClient) {
  }



  postUser(user: User)
  {
    const body = { name: user.Name, email: user.Email, password: user.Password, surname: user.Surname, role: user.Role };
    return this.httpClient.post(this.baseURL + '/CreateUser',body);
  }
  getUsers(): Observable<User[]> {
    let srbn = this.httpClient.get<User[]>(this.baseURL);
    
    return srbn;
  }
}