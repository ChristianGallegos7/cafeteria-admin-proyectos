# Cafeteria Frontend - Angular

## Stack
- **Framework**: Angular 21 (standalone components)
- **Estilos**: Tailwind CSS 4
- **Alertas**: SweetAlert2 via AlertService
- **HTTP**: HttpClient con environment.urlApi

## Comandos
- `npm start` - Servidor desarrollo (puerto 4200)
- `npm run build` - Build producción

## Estructura
```
src/app/
├── components/       # Componentes organizados por módulo
├── services/         # Servicios inyectables
├── dtos/             # Data Transfer Objects
└── environments/     # Configuración por ambiente
```

## Reglas de TypeScript (OBLIGATORIO)

### Tipado estricto
- SIEMPRE tipar parámetros de funciones y retornos
- NUNCA usar `any` - usar tipos específicos o `unknown`
- Usar interfaces/classes para objetos complejos
- Usar `readonly` para propiedades inmutables

```typescript
// MAL
function calcular(datos) { ... }
const items: any[] = [];

// BIEN
function calcular(datos: PedidoDto): number { ... }
const items: ProductoDto[] = [];
```

### DTOs y Modelos
- Crear DTOs para comunicación con API
- Usar `!` solo cuando el valor está garantizado
- Preferir `interface` para DTOs, `class` para modelos con lógica

```typescript
// DTO para API
export interface ProductoDto {
  id: number;
  nombre: string;
  precio: number;
  categoriaId: number;
}
```

### Formularios Reactivos
- Usar `FormBuilder` con `inject()`
- Agregar `Validators` apropiados
- Usar `getRawValue()` para extraer valores tipados
- Manejar errores con `hasError()`

### Servicios
- Un servicio por entidad/dominio
- Usar `inject()` en lugar de constructor
- Retornar `Observable<T>` tipado
- Usar `AlertService` para feedback al usuario

### Componentes
- Standalone components (imports en @Component)
- Nombres descriptivos en español para variables de negocio
- Usar `@if`, `@for` en lugar de *ngIf, *ngFor

## Convenciones de código
- Nombres de archivos: kebab-case (login-service.ts)
- Clases: PascalCase (LoginService)
- Métodos/variables: camelCase (enviarFormulario)
- Constantes: UPPER_SNAKE_CASE
- Interfaces DTO: sufijo Dto (UsuarioDto)

## Manejo de errores
```typescript
this.servicio.metodo().subscribe({
  next: (response) => {
    this.alertService.success('Operación exitosa');
  },
  error: (err) => {
    this.alertService.error(err.error?.mensaje || 'Error desconocido');
  },
});
```

## Backend API
- URL base: environment.urlApi
- Endpoints usan PascalCase: `/Usuario/iniciarSesion`
- Errores retornan `{ mensaje: string }`
- Token JWT en sessionStorage
