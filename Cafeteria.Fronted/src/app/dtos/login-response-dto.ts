export class LoginResponseDto {
  id!: number;
  nombre!: string;
  apellido!: string;
  correo!: string;
  clave!: string | null;
  esActivo!: boolean;
  rolId!: number;
  nombreRol!: string;
  token!: string;
}
