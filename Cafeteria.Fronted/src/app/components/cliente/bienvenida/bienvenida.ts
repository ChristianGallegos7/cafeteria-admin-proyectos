import { Component, inject } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bienvenida',
  templateUrl: './bienvenida.html',
  styleUrl: './bienvenida.css',
})
export class Bienvenida {
  private router = inject(Router);

  irAlMenu(): void {
    this.router.navigate(['/menu']);
  }
}
