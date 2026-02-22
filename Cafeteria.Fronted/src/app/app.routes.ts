import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/cliente/bienvenida/bienvenida').then((m) => m.Bienvenida),
  },
  {
    path: 'menu',
    loadComponent: () => import('./components/cliente/menu/menu').then((m) => m.Menu),
  },
  {
    path: 'login',
    loadComponent: () => import('./components/cliente/auth/login/login').then((m) => m.Login),
  },
];
