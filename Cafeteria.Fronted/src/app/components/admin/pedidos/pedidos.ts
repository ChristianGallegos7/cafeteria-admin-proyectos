import { Component, computed, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { Router } from '@angular/router';

import { LoginResponseDto } from '../../../interfaces/login.interface';
import { PedidoResponseDto } from '../../../interfaces/pedido.interface';
import { AlertService } from '../../../services/alert-service';
import { PedidoApiService } from '../../../services/pedido-api.service';

@Component({
  selector: 'app-admin-pedidos',
  imports: [],
  templateUrl: './pedidos.html',
  styleUrl: './pedidos.css',
})
export class AdminPedidos implements OnInit, OnDestroy {
  usuario: LoginResponseDto | null = null;

  private readonly router = inject(Router);
  private readonly pedidoApiService = inject(PedidoApiService);
  private readonly alertService = inject(AlertService);

  readonly pedidos = signal<PedidoResponseDto[]>([]);
  readonly cargando = signal(true);

  readonly pendientes = computed(() =>
    this.pedidos().filter(p => p.estado === 'Pendiente').length
  );
  readonly enPreparacion = computed(() =>
    this.pedidos().filter(p => p.estado === 'EnPreparacion').length
  );
  readonly listos = computed(() =>
    this.pedidos().filter(p => p.estado === 'Listo').length
  );
  readonly totalHoy = computed(() => this.pedidos().length);

  private pollingInterval: ReturnType<typeof setInterval> | null = null;

  ngOnInit(): void {
    this.cargarUsuario();
    this.cargarPedidos();
    this.pollingInterval = setInterval(() => this.cargarPedidos(), 5000);
  }

  ngOnDestroy(): void {
    if (this.pollingInterval) {
      clearInterval(this.pollingInterval);
    }
  }

  private cargarUsuario(): void {
    const usuarioStr = sessionStorage.getItem('usuario');
    if (usuarioStr) {
      this.usuario = JSON.parse(usuarioStr) as LoginResponseDto;
    }
  }

  private cargarPedidos(): void {
    this.pedidoApiService.obtenerPedidosDelDia().subscribe({
      next: (data) => {
        this.pedidos.set(data);
        this.cargando.set(false);
      },
      error: () => {
        this.cargando.set(false);
      },
    });
  }

  cambiarEstado(id: number, estado: string): void {
    this.pedidoApiService.cambiarEstado(id, estado).subscribe({
      next: (pedidoActualizado) => {
        this.pedidos.update(lista =>
          lista.map(p => p.id === id ? pedidoActualizado : p)
        );
      },
      error: (err) => {
        this.alertService.error(err.error?.mensaje || 'Error al cambiar el estado');
      },
    });
  }

  cerrarSesion(): void {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('usuario');
    this.router.navigate(['/login']);
  }

  estadoLabel(estado: string): string {
    const labels: Record<string, string> = {
      Pendiente: 'Pendiente',
      EnPreparacion: 'En Preparación',
      Listo: 'Listo',
      Entregado: 'Entregado',
    };
    return labels[estado] ?? estado;
  }

  formatearHora(fechaIso: string): string {
    return new Date(fechaIso).toLocaleTimeString('es-MX', {
      hour: '2-digit',
      minute: '2-digit',
    });
  }
}
