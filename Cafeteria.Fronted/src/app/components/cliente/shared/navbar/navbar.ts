import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'client-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar {}
