using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Diagnostics;
using System.Net;
using System.Web;

namespace AgendaClientes
{   

    class Cliente
    {
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CasaODepto { get; set; }
        public string Barrio { get; set; }
        public List<string> Telefonos { get; set; } = new List<string>();
        public string Observacion { get; set; }
        public List<string> Pedidos { get; set; } = new List<string>();
        public DateTime FechaCreacion { get; set; }

       
        public Cliente()
        {
            FechaCreacion = DateTime.Now;
        }

        public override string ToString()
        {
            return $"Dirección: {Direccion}, Casa/Depto: {CasaODepto}, Barrio: {Barrio}, Teléfonos: {string.Join(", ", Telefonos)}, Nombre: {Nombre}, , Observación: {Observacion}, Pedidos: {string.Join(", ", Pedidos)}, Fecha de Creación: {FechaCreacion}";
        }
    }

    class Program
    {
        static List<Cliente> clientes = new List<Cliente>();
        static string rutaArchivoClientes = "clientes.txt";
        static string rutaCarpetaPedidos = "Pedidos";

        static void Main(string[] args)
        {
            Console.Clear();
            if (File.Exists(rutaArchivoClientes))
            {
                CargarClientes();
            }

            Directory.CreateDirectory(rutaCarpetaPedidos);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("1. Agregar Cliente");
                Console.WriteLine("2. Buscar Clientes");
                Console.WriteLine("3. Actualizar Cliente");
                Console.WriteLine("4. Borrar Cliente");
                Console.WriteLine("5. Agregar Pedido");
                Console.WriteLine("6. Mostrar Pedidos del Día");
                Console.WriteLine("7. Mostrar Pedidos de Otra Fecha");
                Console.WriteLine("8. Borrar Pedidos del Día"); // Nueva opción
                Console.WriteLine("9. Salir");               
                Console.Write("Seleccione una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        AgregarCliente();
                        break;
                    case "2":
                        BuscarClientes();
                        break;
                    case "3":
                        ActualizarCliente();
                        break;
                    case "4":
                        BorrarCliente();
                        break;
                    case "5":
                        AgregarPedido();
                        break;
                    case "6":
                        MostrarPedidosDelDia();
                        break;
                    case "7":
                        MostrarPedidosDeOtraFecha();
                        break;
                    case "8":
                        BorrarPedido();// Llamar a la nueva función
                        break;
                    case "9":
                        GuardarClientes();
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Presione cualquier tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }


        static void CargarClientes()
        {
            using (StreamReader sr = new StreamReader(rutaArchivoClientes))
            {
                while (!sr.EndOfStream)
                {
                    string linea = sr.ReadLine();
                    string[] datos = linea.Split('|');
                    Cliente cliente = new Cliente
                    {
                        Nombre = datos[0],
                        Direccion = datos[1],
                        CasaODepto = datos[2],
                        Barrio = datos[3],
                        Telefonos = datos[4].Split(',').ToList(),
                        Observacion = datos[5],
                        Pedidos = datos[6].Split(',').ToList(),
                        FechaCreacion = DateTime.Parse(datos[7])
                    };
                    clientes.Add(cliente);
                }
            }
        }

        static void GuardarClientes()
        {
            using (StreamWriter sw = new StreamWriter(rutaArchivoClientes))
            {
                foreach (var cliente in clientes)
                {
                    sw.WriteLine($"{cliente.Nombre}|{cliente.Direccion}|{cliente.CasaODepto}|{cliente.Barrio}|{string.Join(",", cliente.Telefonos)}|{cliente.Observacion}|{string.Join(",", cliente.Pedidos)}|{cliente.FechaCreacion}");
                }
            }
        }

        static void AgregarCliente()
        {
            while (true)
            {
                Cliente nuevoCliente = new Cliente();

                Console.Write("Ingrese el nombre del cliente: ");
                nuevoCliente.Nombre = Console.ReadLine();

                Console.Write("Ingrese la dirección del cliente: ");
                nuevoCliente.Direccion = Console.ReadLine().ToLower();

                // Buscar clientes con direcciones que contengan la dirección ingresada
                var clientesConDireccionSimilar = clientes.Where(c => c.Direccion.ToLower().Contains(nuevoCliente.Direccion)).ToList();
                if (clientesConDireccionSimilar.Any())
                {
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine("Clientes con direcciones similares:");
                    for (int i = 0; i < clientesConDireccionSimilar.Count; i++)
                    {
                        Console.WriteLine(new string('-', 50));
                        Console.WriteLine($"{i + 1}. {clientesConDireccionSimilar[i]}");
                    }

                    Console.WriteLine(new string('-', 50));
                    Console.Write("¿Desea agregar otro cliente con la misma dirección? (s/n): ");
                    if (!RespuestaSiNo())
                    {
                        return;
                    }
                }

                Console.Write("Ingrese si es casa o departamento: ");
                nuevoCliente.CasaODepto = Console.ReadLine();

                Console.Write("Ingrese el barrio del cliente: ");
                nuevoCliente.Barrio = Console.ReadLine();

                do
                {
                    Console.Write("Ingrese el teléfono del cliente: ");
                    nuevoCliente.Telefonos.Add(Console.ReadLine());

                    Console.Write("¿Desea agregar otro teléfono? (s/n): ");
                } while (RespuestaSiNo());

                Console.Write("Ingrese una observación para el cliente: ");
                nuevoCliente.Observacion = Console.ReadLine();

                clientes.Add(nuevoCliente);

                Console.WriteLine("Cliente agregado exitosamente.");

                GuardarClientes();

                Console.Write("¿Desea agregar otro cliente? (s/n): ");
                if (!RespuestaSiNo())
                {
                    break;
                }
            }
        }

        static void BuscarClientes()
        {
            Console.Write("Ingrese el nombre, dirección o teléfono del cliente a buscar: ");
            string busqueda = Console.ReadLine().ToLower(); // Convertir la búsqueda a minúsculas

            var clientesEncontrados = clientes
                .Where(c =>
                    c.Nombre.ToLower().Contains(busqueda) ||
                    c.Direccion.ToLower().Contains(busqueda) ||
                    c.Telefonos.Any(t => t.ToLower().Contains(busqueda))
                )
                .ToList();

            if (!clientesEncontrados.Any())
            {
                Console.WriteLine("No se encontraron clientes con ese criterio.");
                return;
            }

            for (int i = 0; i < clientesEncontrados.Count; i++)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{i + 1}. {clientesEncontrados[i]}");
            }

            Console.WriteLine(new string('-', 50));
            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }


        static void ActualizarCliente()
        {
            Console.Write("Ingrese el nombre, dirección o teléfono del cliente a actualizar: ");
            string busqueda = Console.ReadLine().ToLower();
            var clientesEncontrados = clientes
                .Where(c =>
                    c.Nombre.ToLower().Contains(busqueda) ||
                    c.Direccion.ToLower().Contains(busqueda) ||
                    c.Telefonos.Any(t => t.ToLower().Contains(busqueda))
                )
                .ToList();

            if (!clientesEncontrados.Any())
            {
                Console.WriteLine("No se encontraron clientes con ese criterio.");
                return;
            }

            for (int i = 0; i < clientesEncontrados.Count; i++)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{i + 1}. {clientesEncontrados[i]}");
            }
            Console.WriteLine(new string('-', 50));
            Console.Write("Seleccione el índice del cliente a actualizar: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= clientesEncontrados.Count)
            {
                Cliente cliente = clientesEncontrados[indice - 1];

                Console.WriteLine("Seleccione el campo a actualizar:");
                Console.WriteLine("1. Nombre");
                Console.WriteLine("2. Dirección");
                Console.WriteLine("3. Casa/Depto");
                Console.WriteLine("4. Barrio");
                Console.WriteLine("5. Teléfonos");
                Console.WriteLine("6. Observación");
                Console.WriteLine("7. Todos los datos");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        Console.Write("Nuevo nombre: ");
                        cliente.Nombre = Console.ReadLine();
                        break;
                    case "2":
                        Console.Write("Nueva dirección: ");
                        cliente.Direccion = Console.ReadLine();
                        break;
                    case "3":
                        Console.Write("Nuevo casa/departamento: ");
                        cliente.CasaODepto = Console.ReadLine();
                        break;
                    case "4":
                        Console.Write("Nuevo barrio: ");
                        cliente.Barrio = Console.ReadLine();
                        break;
                    case "5":
                        Console.Write("Nuevos teléfonos (separados por comas): ");
                        cliente.Telefonos = Console.ReadLine().Split(',').ToList();
                        break;
                    case "6":
                        Console.Write("Nueva observación: ");
                        cliente.Observacion = Console.ReadLine();
                        break;
                    case "7":
                        Console.Write("Nuevo nombre: ");
                        cliente.Nombre = Console.ReadLine();
                        Console.Write("Nueva dirección: ");
                        cliente.Direccion = Console.ReadLine();
                        Console.Write("Nuevo casa/departamento: ");
                        cliente.CasaODepto = Console.ReadLine();
                        Console.Write("Nuevo barrio: ");
                        cliente.Barrio = Console.ReadLine();
                        Console.Write("Nuevos teléfonos (separados por comas): ");
                        cliente.Telefonos = Console.ReadLine().Split(',').ToList();
                        Console.Write("Nueva observación: ");
                        cliente.Observacion = Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        return;
                }

                Console.WriteLine("Cliente actualizado exitosamente.");
                GuardarClientes();
            }
            else
            {
                Console.WriteLine("Índice no válido.");
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void BorrarCliente()
        {
            Console.Write("Ingrese el nombre, dirección o teléfono del cliente a borrar: ");
            string busqueda = Console.ReadLine();
            var clientesEncontrados = clientes.Where(c => c.Nombre.Contains(busqueda) || c.Direccion.Contains(busqueda) || c.Telefonos.Any(t => t.Contains(busqueda))).ToList();

            if (!clientesEncontrados.Any())
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("No se encontraron clientes con ese criterio.");
                return;
            }

            for (int i = 0; i < clientesEncontrados.Count; i++)
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine($"{i + 1}. {clientesEncontrados[i]}");
            }
            
            Console.WriteLine(new string('-', 50));
            Console.Write("Seleccione el índice del cliente a borrar: ");
            if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= clientesEncontrados.Count)
            {
                clientes.Remove(clientesEncontrados[indice - 1]);
                Console.WriteLine("Cliente borrado exitosamente.");
                GuardarClientes();
            }
            else
            {
                Console.WriteLine("Índice no válido.");
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void AgregarPedido()
        {
            Console.Write("Ingrese la descripción del pedido: ");
            string descripcionPedidoSinFecha = Console.ReadLine();
            string fechaPedidoD = DateTime.Now.ToString("ddMMyyyy");
            string descripcionPedido = descripcionPedidoSinFecha +" el "+ fechaPedidoD;

            Console.Write("Ingrese la dirección del pedido: ");
            string direccion = Console.ReadLine().ToLower();

            // Cambiar la comparación para usar Contains en lugar de ==
            var clientesConMismaDireccion = clientes.Where(c => c.Direccion.ToLower().Contains(direccion)).ToList();

            Cliente clienteSeleccionado = null;

            if (clientesConMismaDireccion.Any())
            {
                Console.WriteLine(new string('-', 50));
                Console.WriteLine("Clientes con una dirección que contiene el texto ingresado:");
                for (int i = 0; i < clientesConMismaDireccion.Count; i++)
                {
                    Console.WriteLine(new string('-', 50));
                    Console.WriteLine($"{i + 1}. {clientesConMismaDireccion[i]}");
                    Console.WriteLine(new string('-', 50));
                }
                

                Console.Write("¿Desea agregar el pedido a uno de estos clientes? (s/n): ");
                if (RespuestaSiNo())
                {
                    Console.Write("Seleccione el índice del cliente: ");
                    if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= clientesConMismaDireccion.Count)
                    {
                        
                        clienteSeleccionado = clientesConMismaDireccion[indice - 1];
                    }
                    else
                    {
                        Console.WriteLine("Índice no válido.");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("No se encontraron clientes con una dirección que contenga el texto ingresado.");
            }


            if (clienteSeleccionado == null)
            {
                clienteSeleccionado = new Cliente();

                Console.Write("Ingrese el nombre del cliente: ");
                clienteSeleccionado.Nombre = Console.ReadLine();

                clienteSeleccionado.Direccion = direccion;

                Console.Write("Ingrese si es casa o departamento: ");
                clienteSeleccionado.CasaODepto = Console.ReadLine();

                Console.Write("Ingrese el barrio del cliente: ");
                clienteSeleccionado.Barrio = Console.ReadLine();

                do
                {
                    Console.Write("Ingrese el teléfono del cliente: ");
                    clienteSeleccionado.Telefonos.Add(Console.ReadLine());

                    Console.Write("¿Desea agregar otro teléfono? (s/n): ");
                } while (RespuestaSiNo());

                Console.Write("Ingrese una observación para el cliente: ");
                clienteSeleccionado.Observacion = Console.ReadLine();

                clientes.Add(clienteSeleccionado);
            }

            

            Console.Write("Ingrese una observación para el pedido: ");
            string observacionPedido = Console.ReadLine();

            string fechaPedido = DateTime.Now.ToString("ddMMyyyy");
            string rutaArchivoPedido = Path.Combine(rutaCarpetaPedidos, $"{fechaPedido}.txt");

            using (StreamWriter sw = new StreamWriter(rutaArchivoPedido, true))
            {
                sw.WriteLine($"Descripción del Pedido: {descripcionPedido},Dirección: {clienteSeleccionado.Direccion}, Casa/Depto: {clienteSeleccionado.CasaODepto}, Barrio: {clienteSeleccionado.Barrio}, Teléfonos: {string.Join(", ", clienteSeleccionado.Telefonos)}, Cliente: {clienteSeleccionado.Nombre}, Observación del Pedido: {observacionPedido}, Observación del Cliente: {clienteSeleccionado.Observacion}");
            }

            clienteSeleccionado.Pedidos.Add(descripcionPedido);
            GuardarClientes();

            Console.WriteLine("Pedido agregado exitosamente.");
            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void MostrarPedidosDelDia()
        {
            string fechaPedido = DateTime.Now.ToString("ddMMyyyy");
            string rutaArchivoPedido = Path.Combine(rutaCarpetaPedidos, $"{fechaPedido}.txt");

            Console.Clear();
            if (File.Exists(rutaArchivoPedido))
            {
                using (StreamReader sr = new StreamReader(rutaArchivoPedido))
                {
                    while (!sr.EndOfStream)
                    {
                        Console.WriteLine(sr.ReadLine());
                        Console.WriteLine(new string('-', 50));
                    }
                }
            }
            else
            {
                Console.WriteLine("No hay pedidos para el día de hoy.");
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static void MostrarPedidosDeOtraFecha()
        {
            Console.Write("Ingrese la fecha de los pedidos a mostrar (ddMMyyyy): ");
            string fechaPedido = Console.ReadLine();
            string rutaArchivoPedido = Path.Combine(rutaCarpetaPedidos, $"{fechaPedido}.txt");

            Console.Clear();
            if (File.Exists(rutaArchivoPedido))
            {
                using (StreamReader sr = new StreamReader(rutaArchivoPedido))
                {
                    while (!sr.EndOfStream)
                    {
                        Console.WriteLine(sr.ReadLine());
                        Console.WriteLine(new string('-', 50));
                    }
                }
            }
            else
            {
                Console.WriteLine("No hay pedidos para esa fecha.");
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }

        static bool RespuestaSiNo()
        {
            while (true)
            {
                
                string respuesta = Console.ReadLine().Trim();

                // Convertir a minúsculas para facilitar la comparación
                string respuestaLower = respuesta.ToLower();

                // Comprobación de variantes aceptables para sí y no
                if (respuestaLower == "s" || respuestaLower == "si" || respuestaLower == "sí" || respuestaLower == "sí")
                {
                    return true;
                }
                else if (respuestaLower == "n" || respuestaLower == "no")
                {
                    return false;
                }
                else
                {
                    Console.Write("Respuesta no válida. Ingrese 's' o 'n': ");
                }
            }
        }
        static void BorrarPedido()
        {
            // Obtener la fecha actual en formato ddMMyyyy
            string fechaPedido = DateTime.Now.ToString("ddMMyyyy");
            string rutaArchivoPedido = Path.Combine(rutaCarpetaPedidos, $"{fechaPedido}.txt");

            Console.Clear();
            if (File.Exists(rutaArchivoPedido))
            {
                // Leer todos los pedidos del archivo
                var pedidos = new List<string>();
                using (StreamReader sr = new StreamReader(rutaArchivoPedido))
                {
                    while (!sr.EndOfStream)
                    {
                        pedidos.Add(sr.ReadLine());
                    }
                }

                // Verificar si hay pedidos para mostrar
                if (!pedidos.Any())
                {
                    Console.WriteLine("No hay pedidos para borrar.");
                    Console.WriteLine("Presione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                    return;
                }

                // Mostrar los pedidos existentes
                Console.WriteLine("Pedidos del día:");
                for (int i = 0; i < pedidos.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {pedidos[i]}");
                }

                Console.WriteLine(new string('-', 50));
                Console.Write("Seleccione el índice del pedido a borrar: ");
                if (int.TryParse(Console.ReadLine(), out int indice) && indice > 0 && indice <= pedidos.Count)
                {
                    // Borrar el pedido seleccionado
                    pedidos.RemoveAt(indice - 1);

                    // Reescribir el archivo con los pedidos restantes
                    using (StreamWriter sw = new StreamWriter(rutaArchivoPedido))
                    {
                        foreach (var pedido in pedidos)
                        {
                            sw.WriteLine(pedido);
                        }
                    }

                    Console.WriteLine("Pedido borrado exitosamente.");
                }
                else
                {
                    Console.WriteLine("Índice no válido.");
                }
            }
            else
            {
                Console.WriteLine("No hay pedidos para el día de hoy.");
            }

            Console.WriteLine("Presione cualquier tecla para volver al menú...");
            Console.ReadKey();
        }



    }

}
