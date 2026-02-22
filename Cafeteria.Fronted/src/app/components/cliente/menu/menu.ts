import { Component } from '@angular/core';
import { Navbar } from '../shared/navbar/navbar';

@Component({
  selector: 'app-menu',
  imports: [Navbar],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu {}
