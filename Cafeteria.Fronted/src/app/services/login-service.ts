import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { LoginRequestDto, LoginResponseDto } from '../interfaces/login.interface';
import { environment } from './../../environments/environment.development';

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
