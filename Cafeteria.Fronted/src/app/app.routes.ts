import { Routes } from '@angular/router';
import { adminGuard, kioskGuard, loginGuard } from './guards/auth.guard';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/cliente/bienvenida/bienvenida').then((m) => m.Bienvenida),
  },
  {
    path: 'menu',
    loadComponent: () => import('./components/cliente/menu/menu').then((m) => m.Menu),
    canActivate: [kioskGuard],
  },
  {
    path: 'login',
    loadComponent: () => import('./components/cliente/auth/login/login').then((m) => m.Login),
    canActivate: [loginGuard],
  },
  {
    path: 'admin',
    loadComponent: () => import('./components/admin/pedidos/pedidos').then((m) => m.AdminPedidos),
    canActivate: [adminGuard],
  },
];
