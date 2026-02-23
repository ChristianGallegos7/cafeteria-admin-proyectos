import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { LoginResponseDto } from '../interfaces/login.interface';

const ROL_CLIENTE = 'Cliente';
const ROL_ADMINISTRADOR = 'Administrador';

/**
 * Obtiene el usuario de sessionStorage
 */
function getUsuario(): LoginResponseDto | null {
  const usuarioStr = sessionStorage.getItem('usuario');
  if (usuarioStr) {
    return JSON.parse(usuarioStr) as LoginResponseDto;
  }
  return null;
}

/**
 * Guard para rutas de administrador
 * Solo permite acceso a usuarios con rol Administrador
 */
export const adminGuard: CanActivateFn = () => {
  const router = inject(Router);
  const usuario = getUsuario();

  if (!usuario) {
    router.navigate(['/login']);
    return false;
  }

  if (usuario.nombreRol !== ROL_ADMINISTRADOR) {
    router.navigate(['/menu']);
    return false;
  }

  return true;
};

/**
 * Guard para rutas de kiosko (cliente)
 * Solo permite acceso a usuarios con rol Cliente
 */
export const kioskGuard: CanActivateFn = () => {
  const router = inject(Router);
  const usuario = getUsuario();

  if (!usuario) {
    router.navigate(['/login']);
    return false;
  }

  if (usuario.nombreRol !== ROL_CLIENTE) {
    router.navigate(['/admin']);
    return false;
  }

  return true;
};

/**
 * Guard para login
 * Redirige si ya hay sesión activa
 */
export const loginGuard: CanActivateFn = () => {
  const router = inject(Router);
  const usuario = getUsuario();

  if (usuario) {
    if (usuario.nombreRol === ROL_ADMINISTRADOR) {
      router.navigate(['/admin']);
    } else {
      router.navigate(['/menu']);
    }
    return false;
  }

  return true;
};
