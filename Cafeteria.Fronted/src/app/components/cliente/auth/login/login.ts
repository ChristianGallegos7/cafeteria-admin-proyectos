import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AlertService } from '../../../../services/alert-service';
import { LoginService } from '../../../../services/login-service';

const ROL_CLIENTE = 'Cliente';
const ROL_ADMINISTRADOR = 'Administrador';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private readonly fb = inject(FormBuilder);
  private readonly alertService = inject(AlertService);
  private readonly loginService = inject(LoginService);
  private readonly router = inject(Router);

  loginForm = this.fb.group({
    correo: ['', [Validators.required, Validators.email]],
    clave: ['', Validators.required],
  });

  enviarFormulario(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      this.alertService.error('Por favor completa todos los campos correctamente');
      return;
    }

    const { correo, clave } = this.loginForm.getRawValue();

    this.alertService.loading('Iniciando sesion...');

    this.loginService.iniciarSesion({ correo: correo!, clave: clave! }).subscribe({
      next: (response) => {
        this.alertService.close();

        // Guardar sesion
        sessionStorage.setItem('token', response.token);
        sessionStorage.setItem('usuario', JSON.stringify(response));

        // Redirigir segun rol
        if (response.nombreRol === ROL_ADMINISTRADOR) {
          this.alertService.success(`Bienvenido ${response.nombre}`);
          this.router.navigate(['/admin']);
        } else if (response.nombreRol === ROL_CLIENTE) {
          this.alertService.success(`Kiosko activado - ${response.nombre}`);
          this.router.navigate(['/menu']);
        } else {
          this.alertService.error('Rol no reconocido');
        }
      },
      error: (err) => {
        this.alertService.close();
        this.alertService.error(err.error?.mensaje || 'Credenciales incorrectas');
      },
    });
  }
}
