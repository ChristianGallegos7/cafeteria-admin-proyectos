import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../environments/environment';
import { CrearPedidoRequestDto } from '../interfaces/carrito.interface';
import { CambiarEstadoDto, PedidoResponseDto } from '../interfaces/pedido.interface';
import { LoginResponseDto } from '../interfaces/login.interface';

@Injectable({ providedIn: 'root' })
export class PedidoApiService {
  private readonly http = inject(HttpClient);
  private readonly baseUrl = `${environment.urlApi}/Pedido`;

  private getHeaders(): HttpHeaders {
    const usuarioStr = sessionStorage.getItem('usuario');
    if (!usuarioStr) return new HttpHeaders();
    const usuario = JSON.parse(usuarioStr) as LoginResponseDto;
    return new HttpHeaders({ Authorization: `Bearer ${usuario.token}` });
  }

  crearPedido(dto: CrearPedidoRequestDto): Observable<PedidoResponseDto> {
    return this.http.post<PedidoResponseDto>(
      `${this.baseUrl}/crear`,
      dto,
      { headers: this.getHeaders() }
    );
  }

  obtenerPedidosDelDia(): Observable<PedidoResponseDto[]> {
    return this.http.get<PedidoResponseDto[]>(
      this.baseUrl,
      { headers: this.getHeaders() }
    );
  }

  cambiarEstado(id: number, estado: string): Observable<PedidoResponseDto> {
    const dto: CambiarEstadoDto = { estado };
    return this.http.put<PedidoResponseDto>(
      `${this.baseUrl}/${id}/estado`,
      dto,
      { headers: this.getHeaders() }
    );
  }
}
