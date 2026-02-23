import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

export type TipoPedido = 'aqui' | 'llevar' | null;

@Injectable({
  providedIn: 'root',
})
export class PedidoService {
  private readonly STORAGE_KEY = 'tipoPedido';
  private readonly tipoPedidoSubject = new BehaviorSubject<TipoPedido>(this.obtenerTipoGuardado());

  readonly tipoPedido$: Observable<TipoPedido> = this.tipoPedidoSubject.asObservable();

  get tipoPedido(): TipoPedido {
    return this.tipoPedidoSubject.value;
  }

  seleccionarTipoPedido(tipo: TipoPedido): void {
    if (tipo) {
      sessionStorage.setItem(this.STORAGE_KEY, tipo);
    } else {
      sessionStorage.removeItem(this.STORAGE_KEY);
    }
    this.tipoPedidoSubject.next(tipo);
  }

  limpiarTipoPedido(): void {
    sessionStorage.removeItem(this.STORAGE_KEY);
    this.tipoPedidoSubject.next(null);
  }

  private obtenerTipoGuardado(): TipoPedido {
    const guardado = sessionStorage.getItem(this.STORAGE_KEY);
    if (guardado === 'aqui' || guardado === 'llevar') {
      return guardado;
    }
    return null;
  }
}
