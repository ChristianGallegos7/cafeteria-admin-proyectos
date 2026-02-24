export interface CarritoItemDto {
  readonly productoId: number;
  readonly nombre: string;
  readonly precio: number;
  readonly imagenUrl: string;
  cantidad: number;
}

export interface CrearPedidoItemDto {
  readonly productoId: number;
  readonly cantidad: number;
}

export interface CrearPedidoRequestDto {
  readonly tipoPedido: 'aqui' | 'llevar';
  readonly items: CrearPedidoItemDto[];
}
