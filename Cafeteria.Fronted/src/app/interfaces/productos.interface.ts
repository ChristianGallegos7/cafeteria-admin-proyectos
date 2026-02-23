export interface Producto {
  id: number;
  nombre: string;
  descripcion: string;
  precio: number;
  imagenUrl: string;
  esActivo: boolean;
  esDestacado: boolean;
  tiempoPreparacion: number;
  categoriaId: number;
  nombreCategoria: string;
}
