import { Component, inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LoginResponseDto } from '../../../interfaces/login.interface';

@Component({
  selector: 'app-admin-pedidos',
  imports: [],
  templateUrl: './pedidos.html',
  styleUrl: './pedidos.css',
})
export class AdminPedidos implements OnInit {
  usuario: LoginResponseDto | null = null;

  private readonly router = inject(Router);

  ngOnInit(): void {
    this.cargarUsuario();
  }

  private cargarUsuario(): void {
    const usuarioStr = sessionStorage.getItem('usuario');
    if (usuarioStr) {
      this.usuario = JSON.parse(usuarioStr) as LoginResponseDto;
    }
  }

  cerrarSesion(): void {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('usuario');
    this.router.navigate(['/login']);
  }
}
