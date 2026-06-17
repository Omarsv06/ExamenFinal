# Sistema de Gestión Académica

Este proyecto es una aplicación web desarrollada en **ASP.NET Core MVC** para la gestión de cursos y docentes. Permite realizar operaciones CRUD (Crear, Leer, Actualizar y Eliminar) sobre los cursos registrados en el sistema.

---

## Funcionalidades

- ✔ Listado de cursos con filtros por materia y disponibilidad
- ✔ Registro de nuevos cursos
- ✔ Edición de cursos existentes
- ✔ Eliminación de cursos
- ✔ Visualización de detalles de cada curso
- ✔ Gestión de docentes asociados a los cursos
- ✔ API JSON de cursos

---

## Arquitectura del proyecto

El proyecto sigue una arquitectura basada en:

- MVC (Model - View - Controller)
- Patrón Repositorio Genérico
- Entity Framework Core

---

## Tecnologías utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- C#
- Bootstrap (interfaz)
- LINQ

---

## Estructura principal

- Controllers → Lógica de control
- Models → Entidades del sistema
- Views → Interfaces (Razor)
- ViewModels → Modelos para formularios
- Repositories → Acceso a datos
- Data → DbContext e inicialización de BD

