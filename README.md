Agenda de Clientes y Pedidos
Este programa es una aplicación de consola en C# que permite gestionar una agenda de clientes y sus pedidos. La aplicación ofrece funcionalidades para agregar, buscar, actualizar y borrar clientes, así como para agregar y mostrar pedidos asociados a estos clientes. Además, los pedidos se almacenan en archivos separados por fecha, lo que facilita su consulta y gestión.

Características Principales
Gestión de Clientes:

Agregar nuevos clientes con información detallada (nombre, dirección, teléfonos, observaciones, etc.).

Buscar clientes por nombre, dirección o teléfono.

Actualizar información de clientes existentes.

Borrar clientes de la agenda.

Gestión de Pedidos:

Agregar pedidos asociados a clientes existentes o nuevos.

Mostrar pedidos del día actual.

Mostrar pedidos de una fecha específica.

Borrar pedidos del día actual.

Persistencia de Datos:

Los clientes se guardan en un archivo de texto (clientes.txt) para su persistencia entre sesiones.

Los pedidos se almacenan en archivos separados por fecha en una carpeta llamada Pedidos.

Estructura del Proyecto
Clase Cliente:

Representa a un cliente con propiedades como nombre, dirección, teléfonos, observaciones, pedidos y fecha de creación.

Incluye un método ToString() para mostrar la información del cliente de manera formateada.

Clase Program:

Contiene la lógica principal de la aplicación.

Implementa un menú interactivo para que el usuario seleccione las operaciones a realizar.

Maneja la carga y guardado de clientes desde/hacia el archivo clientes.txt.

Gestiona la creación y consulta de archivos de pedidos en la carpeta Pedidos.

Instrucciones de Uso
Ejecución del Programa:

Al ejecutar el programa, se mostrará un menú con las opciones disponibles.

Seleccione una opción ingresando el número correspondiente y siga las instrucciones en pantalla.

Agregar un Cliente:

Seleccione la opción "1. Agregar Cliente" en el menú.

Ingrese la información solicitada (nombre, dirección, teléfonos, etc.).

El cliente se agregará a la lista y se guardará en el archivo clientes.txt.

Buscar Clientes:

Seleccione la opción "2. Buscar Clientes".

Ingrese un criterio de búsqueda (nombre, dirección o teléfono).

Se mostrarán los clientes que coincidan con el criterio.

Actualizar un Cliente:

Seleccione la opción "3. Actualizar Cliente".

Busque el cliente que desea actualizar.

Seleccione el campo a actualizar y proporcione la nueva información.

Borrar un Cliente:

Seleccione la opción "4. Borrar Cliente".

Busque el cliente que desea borrar.

Confirme la eliminación del cliente.

Agregar un Pedido:

Seleccione la opción "5. Agregar Pedido".

Ingrese la descripción del pedido y la dirección del cliente.

Asocie el pedido a un cliente existente o cree uno nuevo.

El pedido se guardará en un archivo con la fecha actual en la carpeta Pedidos.

Mostrar Pedidos del Día:

Seleccione la opción "6. Mostrar Pedidos del Día".

Se mostrarán todos los pedidos registrados para el día actual.

Mostrar Pedidos de Otra Fecha:

Seleccione la opción "7. Mostrar Pedidos de Otra Fecha".

Ingrese la fecha en formato ddMMyyyy para ver los pedidos de esa fecha.

Borrar Pedidos del Día:

Seleccione la opción "8. Borrar Pedidos del Día".

Seleccione el pedido que desea borrar de la lista de pedidos del día.

Salir:

Seleccione la opción "9. Salir" para guardar los cambios y cerrar la aplicación.

Requisitos
.NET Framework: El programa está desarrollado en C# y requiere .NET Framework para su ejecución.

Permisos de Escritura/Lectura: La aplicación necesita permisos para leer y escribir archivos en el directorio donde se ejecuta.

Consideraciones
Formato de Fecha: Los pedidos se almacenan en archivos con nombres en formato ddMMyyyy.txt. Asegúrese de ingresar las fechas en este formato al buscar pedidos de una fecha específica.

Persistencia de Datos: Los datos de los clientes se guardan automáticamente en clientes.txt al salir de la aplicación. Los pedidos se guardan en archivos separados por fecha en la carpeta Pedidos.

Ejemplo de Uso
plaintext
Copy
1. Agregar Cliente
2. Buscar Clientes
3. Actualizar Cliente
4. Borrar Cliente
5. Agregar Pedido
6. Mostrar Pedidos del Día
7. Mostrar Pedidos de Otra Fecha
8. Borrar Pedidos del Día
9. Salir
Seleccione una opción: 1
Ingrese el nombre del cliente: Juan Pérez
Ingrese la dirección del cliente: Calle Falsa 123
Ingrese si es casa o departamento: Casa
Ingrese el barrio del cliente: Centro
Ingrese el teléfono del cliente: 123456789
¿Desea agregar otro teléfono? (s/n): n
Ingrese una observación para el cliente: Cliente preferencial
Cliente agregado exitosamente.
Contribuciones
Si deseas contribuir a este proyecto, siéntete libre de hacer un fork y enviar un pull request con tus mejoras. ¡Todas las contribuciones son bienvenidas!

Licencia
Este proyecto está bajo la licencia MIT. Consulta el archivo LICENSE para más detalles.
