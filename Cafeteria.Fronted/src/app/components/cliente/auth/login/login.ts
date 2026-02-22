import { Component, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { AlertService } from '../../../../services/alert-service';
import { LoginService } from '../../../../services/login-service';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  fb = inject(FormBuilder);
  alertService = inject(AlertService);
  private loginService = inject(LoginService);

  loginForm = this.fb.group({
    correo: ['', [Validators.required, Validators.email]],
    clave: ['', Validators.required],
  });

  enviarFormulario() {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      this.alertService.error('Por favor completa todos los campos correctamente');
      return;
    }
    const { correo, clave } = this.loginForm.getRawValue();

    this.alertService.loading('Iniciando sesión...');

    this.loginService.iniciarSesion({ correo: correo!, clave: clave! }).subscribe({
      next: (response) => {
        console.log('Respuesta del servidor:', response);
        this.alertService.close();
        sessionStorage.setItem('token', response.token);
        sessionStorage.setItem('usuario', JSON.stringify(response));
        this.alertService.success(`Bienvenido ${response.nombre}`);
        // TODO: redirigir al dashboard/menu
      },
      error: (err) => {
        this.alertService.close();
        this.alertService.error(err.error?.mensaje || 'Credenciales incorrectas');
      },
    });
  }
}
