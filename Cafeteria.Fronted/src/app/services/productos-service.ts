import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { Producto } from '../interfaces/productos.interface';

@Injectable({
  providedIn: 'root',
})
export class ProductosService {
  private readonly env = environment;
  private readonly http = inject(HttpClient);

  private readonly headers = new HttpHeaders({
    'Cache-Control': 'no-cache, no-store, must-revalidate',
    Pragma: 'no-cache',
  });

  obtenerProductosActivos(): Observable<Producto[]> {
    return this.http.get<Producto[]>(`${this.env.urlApi}/Producto/activos`, {
      headers: this.headers,
    });
  }
}
