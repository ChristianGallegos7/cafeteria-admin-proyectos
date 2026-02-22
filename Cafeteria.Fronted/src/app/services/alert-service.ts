import { Injectable } from '@angular/core';
import Swal, { SweetAlertIcon } from 'sweetalert2';

@Injectable({
  providedIn: 'root',
})
export class AlertService {
  // Alerta de éxito
  success(mensaje: string, titulo: string = 'Éxito') {
    return Swal.fire({
      icon: 'success',
      title: titulo,
      text: mensaje,
      confirmButtonColor: '#4f46e5',
    });
  }

  // Alerta de error
  error(mensaje: string, titulo: string = 'Error') {
    return Swal.fire({
      icon: 'error',
      title: titulo,
      text: mensaje,
      confirmButtonColor: '#4f46e5',
    });
  }

  // Alerta de advertencia
  warning(mensaje: string, titulo: string = 'Advertencia') {
    return Swal.fire({
      icon: 'warning',
      title: titulo,
      text: mensaje,
      confirmButtonColor: '#4f46e5',
    });
  }

  // Alerta informativa
  info(mensaje: string, titulo: string = 'Información') {
    return Swal.fire({
      icon: 'info',
      title: titulo,
      text: mensaje,
      confirmButtonColor: '#4f46e5',
    });
  }

  // Confirmación con Sí/No
  async confirm(
    mensaje: string,
    titulo: string = '¿Estás seguro?',
    textoConfirmar: string = 'Sí',
    textoCancelar: string = 'Cancelar',
  ): Promise<boolean> {
    const result = await Swal.fire({
      icon: 'question',
      title: titulo,
      text: mensaje,
      showCancelButton: true,
      confirmButtonColor: '#4f46e5',
      cancelButtonColor: '#dc2626',
      confirmButtonText: textoConfirmar,
      cancelButtonText: textoCancelar,
    });
    return result.isConfirmed;
  }

  // Toast (notificación pequeña)
  toast(mensaje: string, icono: SweetAlertIcon = 'success') {
    return Swal.fire({
      icon: icono,
      title: mensaje,
      toast: true,
      position: 'top-end',
      showConfirmButton: false,
      timer: 3000,
      timerProgressBar: true,
    });
  }

  // Loading (pantalla de carga)
  loading(mensaje: string = 'Cargando...') {
    return Swal.fire({
      title: mensaje,
      allowOutsideClick: false,
      allowEscapeKey: false,
      didOpen: () => {
        Swal.showLoading();
      },
    });
  }

  // Cerrar alerta actual
  close() {
    Swal.close();
  }
}
