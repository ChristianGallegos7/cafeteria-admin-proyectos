import { Component, inject, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Subscription } from 'rxjs';
import { LoginResponseDto } from '../../../../interfaces/login.interface';
import { PedidoService, TipoPedido } from '../../../../services/pedido.service';

@Component({
  selector: 'client-navbar',
  imports: [RouterLink],
  templateUrl: './navbar.html',
  styleUrl: './navbar.css',
})
export class Navbar implements OnInit, OnDestroy {
  tipoPedido: TipoPedido = null;
  usuarioKiosko: LoginResponseDto | null = null;

  private readonly pedidoService = inject(PedidoService);
  private subscription?: Subscription;

  ngOnInit(): void {
    this.subscription = this.pedidoService.tipoPedido$.subscribe((tipo) => {
      this.tipoPedido = tipo;
    });

    this.cargarUsuarioKiosko();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  cambiarTipoPedido(): void {
    this.pedidoService.limpiarTipoPedido();
  }

  private cargarUsuarioKiosko(): void {
    const usuarioStr = sessionStorage.getItem('usuario');
    if (usuarioStr) {
      this.usuarioKiosko = JSON.parse(usuarioStr) as LoginResponseDto;
    }
  }
}
