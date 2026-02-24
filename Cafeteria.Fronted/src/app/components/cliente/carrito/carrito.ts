import { DecimalPipe } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import QRCode from 'qrcode';

import { PedidoResponseDto } from '../../../interfaces/pedido.interface';
import { AlertService } from '../../../services/alert-service';
import { CarritoService } from '../../../services/carrito.service';
import { PedidoApiService } from '../../../services/pedido-api.service';
import { PedidoService } from '../../../services/pedido.service';

@Component({
  selector: 'app-carrito',
  imports: [DecimalPipe],
  templateUrl: './carrito.html',
  styleUrl: './carrito.css',
})
export class Carrito {
  readonly carritoService = inject(CarritoService);

  private readonly pedidoService = inject(PedidoService);
  private readonly pedidoApiService = inject(PedidoApiService);
  private readonly alertService = inject(AlertService);

  readonly pedidoConfirmado = signal<PedidoResponseDto | null>(null);
  readonly qrDataUrl = signal<string>('');
  readonly enviando = signal(false);

  async confirmarPedido(): Promise<void> {
    if (this.carritoService.items().length === 0) {
      this.alertService.warning('Agrega al menos un producto al carrito');
      return;
    }

    const tipoPedido = this.pedidoService.tipoPedido;
    if (!tipoPedido) {
      this.alertService.warning('Selecciona el tipo de pedido primero');
      return;
    }

    const confirmado = await this.alertService.confirm(
      `Se realizará un pedido ${tipoPedido === 'aqui' ? 'para comer aquí' : 'para llevar'} con ${this.carritoService.totalItems()} producto(s) por $${this.carritoService.totalPrecio().toFixed(2)}`,
      '¿Confirmar pedido?',
      'Sí, pedir',
      'Revisar'
    );

    if (!confirmado) return;

    this.enviando.set(true);
    const dto = this.carritoService.construirPedido(tipoPedido);

    this.pedidoApiService.crearPedido(dto).subscribe({
      next: async (pedido) => {
        const qrTexto = [
          `Pedido: ${pedido.numeroPedido}`,
          `Tipo: ${pedido.tipoPedido === 'aqui' ? 'Para comer aqui' : 'Para llevar'}`,
          `Total: $${pedido.total.toFixed(2)}`,
          `Items: ${pedido.items.map(i => `${i.cantidad}x ${i.nombreProducto}`).join(', ')}`,
        ].join('\n');

        const dataUrl = await QRCode.toDataURL(qrTexto, { width: 220, margin: 1 });

        this.qrDataUrl.set(dataUrl);
        this.pedidoConfirmado.set(pedido);
        this.carritoService.limpiar();
        this.enviando.set(false);
      },
      error: (err) => {
        this.enviando.set(false);
        this.alertService.error(err.error?.mensaje || 'Error al enviar el pedido');
      },
    });
  }

  nuevoPedido(): void {
    this.pedidoConfirmado.set(null);
    this.qrDataUrl.set('');
    this.carritoService.cerrarCarrito();
  }
}
