import { Routes } from '@angular/router';
import { Menu } from './components/cliente/menu/menu';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'menu',
    pathMatch: 'full'
  },
  {
    path: 'menu',
    component: Menu
  }
];
