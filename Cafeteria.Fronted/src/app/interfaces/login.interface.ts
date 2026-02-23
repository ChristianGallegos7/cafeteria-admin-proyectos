export interface LoginRequestDto {
  correo: string;
  clave: string;
}

export interface LoginResponseDto {
  id: number;
  nombre: string;
  apellido: string;
  correo: string;
  clave: string | null;
  esActivo: boolean;
  rolId: number;
  nombreRol: string;
  token: string;
}
