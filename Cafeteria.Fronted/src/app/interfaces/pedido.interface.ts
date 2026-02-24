export interface PedidoItemResponseDto {
  readonly productoId: number;
  readonly nombreProducto: string;
  readonly cantidad: number;
  readonly precioUnitario: number;
  readonly subtotal: number;
}

export interface PedidoResponseDto {
  readonly id: number;
  readonly numeroPedido: string;
  readonly tipoPedido: string;
  readonly estado: string;
  readonly total: number;
  readonly fechaCreacion: string;
  readonly items: PedidoItemResponseDto[];
}

export interface CambiarEstadoDto {
  readonly estado: string;
}
