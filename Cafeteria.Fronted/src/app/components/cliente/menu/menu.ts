import { ChangeDetectorRef, Component, inject, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Producto } from '../../../interfaces/productos.interface';
import { CarritoService } from '../../../services/carrito.service';
import { PedidoService, TipoPedido } from '../../../services/pedido.service';
import { ProductosService } from '../../../services/productos-service';
import { Carrito } from '../carrito/carrito';
import { Navbar } from '../shared/navbar/navbar';

@Component({
  selector: 'app-menu',
  imports: [Navbar, Carrito],
  templateUrl: './menu.html',
  styleUrl: './menu.css',
})
export class Menu implements OnInit, OnDestroy {
  productos: Producto[] = [];
  cargando = true;
  tipoPedido: TipoPedido = null;
  mostrarModal = false;

  private readonly productosService = inject(ProductosService);
  private readonly pedidoService = inject(PedidoService);
  readonly carritoService = inject(CarritoService);
  private readonly cdr = inject(ChangeDetectorRef);
  private subscription?: Subscription;

  ngOnInit(): void {
    this.subscription = this.pedidoService.tipoPedido$.subscribe((tipo) => {
      this.tipoPedido = tipo;
      this.mostrarModal = tipo === null;
      this.cdr.detectChanges();
    });

    this.obtenerProductosActivos();
  }

  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }

  obtenerProductosActivos(): void {
    this.cargando = true;
    this.productosService.obtenerProductosActivos().subscribe({
      next: (data) => {
        this.productos = data;
        this.cargando = false;
        this.cdr.detectChanges();
      },
      error: (err) => {
        console.error('Error al cargar productos', err);
        this.cargando = false;
        this.cdr.detectChanges();
      },
    });
  }

  seleccionarTipoPedido(tipo: 'aqui' | 'llevar'): void {
    this.pedidoService.seleccionarTipoPedido(tipo);
  }

  agregarAlCarrito(producto: Producto): void {
    const eraVacio = this.carritoService.totalItems() === 0;
    this.carritoService.agregar(producto);
    if (eraVacio) {
      this.carritoService.abrirCarrito();
    }
  }
}
