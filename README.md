# Principios SOLID — Sistema de Pedidos

Aplicación práctica de los principios SOLID en C# utilizando un Sistema de Pedidos como temática común.

---

## Integrantes

- Franco Acuña Córdoba
- Dennis Segura
- Daniel Picado

---

## Temática

**Sistema de Pedidos** — Todos los ejemplos están relacionados con la gestión de pedidos: clientes, productos, cálculo de totales, descuentos, notificaciones y métodos de pago.

---

## Lenguaje

C# — .NET 10 — Aplicaciones de consola

---

## Instrucciones para ejecutar

Los ejemplos están diseñados en Visual Studio. Para ejecutar cualquiera de ellos:

1. Abrír el archivo `.sln` del principio que se decea visualizar
2. Presionár **F5** o click en el botón de ejecutar
3. El programa corre automáticamente en la consola

---

## Principios SOLID

### S — Single Responsibility Principle
Cada clase debe tener una única razón para cambiar. Una clase no debe encargarse de más de una responsabilidad.

### O — Open/Closed Principle
Las clases deben estar abiertas para extenderse pero cerradas para modificarse. Se agrega funcionalidad nueva sin tocar el código que ya funciona.

### L — Liskov Substitution Principle
Las subclases deben poder sustituir a su clase base sin romper el comportamiento del programa.

### I — Interface Segregation Principle
Ninguna clase debería depender de métodos que no usa. Es mejor tener muchas interfaces específicas que una sola interfaz general.

### D — Dependency Inversion Principle
Los módulos de alto nivel no deben depender de los de bajo nivel. Ambos deben depender de abstracciones.

---

## Ejemplos programados

### S — Single Responsibility Principle (Franco Acuña)
**Carpeta:** `SRP/`

Demuestra cómo separar una clase que hacía todo (gestionar datos, calcular total, enviar correo y guardar en base de datos) en tres clases con una sola responsabilidad cada una: `Pedido`, `CalculadorPedido` y `NotificadorPedido`. Incluye la versión correcta y la versión mal aplicada para comparación.

### O — Open/Closed Principle (Franco Acuña)
**Carpeta:** `OCP/`

Demuestra cómo aplicar descuentos a un pedido sin modificar el código existente. Se define la interfaz `IDescuento` y cada tipo de descuento (VIP, Temporada, Nuevo Cliente) es una clase separada que la implementa. Incluye la versión incorrecta con un método lleno de `if/else` que debe modificarse cada vez que se agrega un descuento nuevo.

### L — Liskov Substitution Principle (Daniel Picado)
**Carpeta:** `LSP/`
Demuestra como aplicar el principio de en un sistema de inventario, basandonos en que si sustituimos la clase padre que en este caso es producto por una clase hija el programa no deberia de comportarse diferente deveria seguir su funcionalidad normal

### I — Interface Segregation Principle (Dennis Segura)
**Carpeta:** `ISP/`

### D — Dependency Inversion Principle (Dennis Segura)
**Carpeta:** `DIP/`

