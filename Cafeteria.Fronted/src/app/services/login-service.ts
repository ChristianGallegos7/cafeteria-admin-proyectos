import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginResponseDto } from '../dtos/login-response-dto';
import { environment } from './../../environments/environment.development';
import { LoginRequestDto } from './../dtos/login-dto';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  http = inject(HttpClient);
  env = environment;

  iniciarSesion(loginDto: LoginRequestDto) {
    return this.http.post<LoginResponseDto>(this.env.urlApi + '/Usuario/iniciarSesion', loginDto);
  }
}
