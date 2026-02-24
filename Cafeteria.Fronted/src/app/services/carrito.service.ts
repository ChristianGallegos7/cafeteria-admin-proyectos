import { computed, Injectable, signal } from '@angular/core';

import { CarritoItemDto, CrearPedidoRequestDto } from '../interfaces/carrito.interface';
import { Producto } from '../interfaces/productos.interface';

@Injectable({ providedIn: 'root' })
export class CarritoService {
  private readonly _items = signal<CarritoItemDto[]>([]);
  private readonly _abierto = signal(false);

  readonly items = this._items.asReadonly();
  readonly abierto = this._abierto.asReadonly();

  readonly totalItems = computed(() =>
    this._items().reduce((sum, item) => sum + item.cantidad, 0)
  );

  readonly totalPrecio = computed(() =>
    this._items().reduce((sum, item) => sum + item.precio * item.cantidad, 0)
  );

  agregar(producto: Producto): void {
    this._items.update(items => {
      const existente = items.find(i => i.productoId === producto.id);
      if (existente) {
        return items.map(i =>
          i.productoId === producto.id
            ? { ...i, cantidad: i.cantidad + 1 }
            : i
        );
      }
      return [...items, {
        productoId: producto.id,
        nombre: producto.nombre,
        precio: producto.precio,
        imagenUrl: producto.imagenUrl,
        cantidad: 1,
      }];
    });
  }

  aumentar(productoId: number): void {
    this._items.update(items =>
      items.map(i =>
        i.productoId === productoId ? { ...i, cantidad: i.cantidad + 1 } : i
      )
    );
  }

  disminuir(productoId: number): void {
    this._items.update(items =>
      items
        .map(i =>
          i.productoId === productoId ? { ...i, cantidad: i.cantidad - 1 } : i
        )
        .filter(i => i.cantidad > 0)
    );
  }

  quitar(productoId: number): void {
    this._items.update(items =>
      items.filter(i => i.productoId !== productoId)
    );
  }

  limpiar(): void {
    this._items.set([]);
  }

  abrirCarrito(): void {
    this._abierto.set(true);
  }

  cerrarCarrito(): void {
    this._abierto.set(false);
  }

  construirPedido(tipoPedido: 'aqui' | 'llevar'): CrearPedidoRequestDto {
    return {
      tipoPedido,
      items: this._items().map(i => ({
        productoId: i.productoId,
        cantidad: i.cantidad,
      })),
    };
  }
}
