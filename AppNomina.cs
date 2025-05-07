using System.Security.Principal;
using Microsoft.Data.Sqlite;


namespace AppNomina {

    public class AppNomina {
        private readonly IPersistencia _storage;
        public AppNomina() {
            string? appPath = AppDomain.CurrentDomain.BaseDirectory;
            _storage = new SQLiteStorage($"Data Source={appPath}nomina.db");

        }
        string menu_interactivo = "Applicacion Gestion De Nomina Agricola";
        int consoleWidth = Console.WindowWidth;

        public void Iniciar() {
            
            bool corriendo = true;
            GeneradorReportesTXT reportes = new GeneradorReportesTXT();
            FormatosTablas tablas = new FormatosTablas();

            char seleccion;
            while (corriendo) {
                Console.Clear(); 
                string border = new string('=', menu_interactivo.Length + 4);
                Console.WriteLine(CenterText(border));
                Console.WriteLine(CenterText($"| {menu_interactivo} |"));
                Console.WriteLine(CenterText(border));
                Console.WriteLine(CenterText("1) Mostrar Datos"));
                Console.WriteLine(CenterText("2) Agregar Datos"));
                Console.WriteLine(CenterText("3) Actualizar Datos"));
                Console.WriteLine(CenterText("4) Eliminar Datos"));
                Console.WriteLine(CenterText("5) Generar Detalles Nomina"));
                Console.WriteLine(CenterText("6) Generar Reporte Empresa"));
                Console.WriteLine(CenterText("7) Generar Reporte Banco"));
                Console.WriteLine(CenterText("8) Salir"));
                Console.WriteLine();

                Console.Write(CenterText("Seleccione una función: "));

                seleccion = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (seleccion) {
                    case '1':
                        char seleccion2;
                        Console.Clear(); 
                        border = new string('=', menu_interactivo.Length + 4);
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText($"| {menu_interactivo} |"));
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText("1) Mostrar Empleados"));
                        Console.WriteLine(CenterText("2) Mostrar Registros"));
                        Console.WriteLine(CenterText("3) Mostrar Detalles Nomina"));
                        Console.WriteLine();

                        Console.Write(CenterText("Seleccione una función: "));
                        seleccion2 = Console.ReadKey().KeyChar;

                        switch (seleccion2) {
                            case '1':
                                Mostrar_Datos("Empleados", tablas);
                                break;
                            case '2':
                                Mostrar_Datos("Registros", tablas);
                                break;
                            case '3':
                                Mostrar_Datos("Nominas", tablas);
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine(CenterText("Opcion No Valida."));
                                Console.ReadLine();
                                break;
                        }
                        break;
                    case '2':
                        char seleccion3;
                        Console.Clear(); 
                        border = new string('=', menu_interactivo.Length + 4);
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText($"| {menu_interactivo} |"));
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText("1) Agregar Empleado"));
                        Console.WriteLine(CenterText("2) Agregar Registro"));
                        Console.WriteLine();
                        Console.Write(CenterText("Seleccione una función: "));
                        seleccion3 = Console.ReadKey().KeyChar;

                        switch (seleccion3) {
                            case '1':
                                Agregar_Empleado();
                                break;
                            case '2':
                                Agregar_Registro();
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine(CenterText("Opcion No Valida."));
                                Console.ReadLine();
                                break;
                        }
                        break;
                    case '3':
                        char seleccion4;
                        Console.Clear(); 
                        border = new string('=', menu_interactivo.Length + 4);
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText($"| {menu_interactivo} |"));
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText("1) Actualizar Empleado"));
                        Console.WriteLine(CenterText("2) Actualizar Registro"));
                        Console.WriteLine();
                        Console.Write(CenterText("Seleccione una función: "));
                        seleccion4 = Console.ReadKey().KeyChar;

                        switch (seleccion4) {
                            case '1':
                                Actualizar_Empleado();
                                break;
                            case '2':
                                Actualizar_Registro();
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine(CenterText("Opcion No Valida."));
                                Console.ReadLine();
                                break;
                        }
                        break;
                    case '4':
                        char seleccion5;
                        Console.Clear(); 
                        border = new string('=', menu_interactivo.Length + 4);
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText($"| {menu_interactivo} |"));
                        Console.WriteLine(CenterText(border));
                        Console.WriteLine(CenterText("1) Eliminar Empleado"));
                        Console.WriteLine(CenterText("2) Eliminar Registro"));
                        Console.WriteLine(CenterText("3) Eliminar Detalle Nomina"));
                        Console.WriteLine();
                        Console.Write(CenterText("Seleccione una función: "));
                        seleccion5 = Console.ReadKey().KeyChar;

                        switch (seleccion5) {
                            case '1':
                                Eliminar_Empleado();
                                break;
                            case '2':
                                Eliminar_Registro();
                                break;
                            case '3':
                                Eliminar_Detalle();
                                break;
                            default:
                                Console.WriteLine();
                                Console.WriteLine(CenterText("Opcion No Valida."));
                                Console.ReadLine();
                                break;
                        }
                        break;
                    case '5':
                        Agregar_Actualizar_Detalle();
                        break;
                    case '6':
                        reportes.Guardar_Reporte_Empresa();
                        break;
                    case '7':
                        reportes.Guardar_Reporte_Banco();
                        break;
                    case '8':
                        Console.WriteLine();
                        Console.WriteLine(CenterText("Saliendo del programa..."));
                        Environment.Exit(0);
                        corriendo = false;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine(CenterText("Opcion No Valida."));
                        Console.ReadLine();
                        break;
                }
            }
        }

        public string CenterText(string texto) {
            int padding = (consoleWidth - texto.Length) / 2;
            string final = new string(' ', Math.Max(0, padding)) + texto;
            return final;
        }
        public void Mostrar_Datos(string? tipo, FormatosTablas tablas){
            if (tipo == "Empleados"){
                tablas.Formato_Empleados();
            }
            else if (tipo == "Registros"){
                tablas.Formato_Registro();
            }
            else if (tipo == "Nominas"){
                tablas.Formato_Nominas();
            }
        }

        // Empleados
        public Empleado Agregar_Actualizar_Empleado() {
            Console.WriteLine();
            Console.Write(CenterText("Ingrese el nombre del empleado: "));
            string? nombre = Console.ReadLine();
            if (string.IsNullOrEmpty(nombre)) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"El Nombre no puede estar vacio, Intente de Nuevo!!"));
                Console.ReadLine();
                return new Empleado();
            }

            Console.Write(CenterText($"Ingrese la fecha de nacimiento de {nombre}, (MM-DD-YYYY): "));
            string? fecha = Console.ReadLine();
            DateTime fechanacimiento = DateTime.TryParse(fecha, out fechanacimiento) ? fechanacimiento : new DateTime();
            if (fechanacimiento.Year == 1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato de Fecha No Valido '{fechanacimiento.ToShortDateString()}', Intente de Nuevo!!"));
                Console.ReadLine();
                return new Empleado();
            }

            Console.Write(CenterText($"Ingrese el precio por hora de {nombre}: "));
            string? precio = Console.ReadLine();
            double precioporhora = double.TryParse(precio, out precioporhora) ? precioporhora : -1;

            if (precioporhora == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{precio}', Favor escribir un numero.."));
                Console.ReadLine();
                return new Empleado();
            }
            else if (precioporhora <= 0) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Precio Por Hora no puede ser cero o negativo '{precio}'."));
                Console.ReadLine();
                return new Empleado();
            }
            
            Console.Write(CenterText($"Ingrese el numero de cuenta de banco de {nombre}: "));
            string? cuentabanco = Console.ReadLine();
            if (string.IsNullOrEmpty(cuentabanco)) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"El Numero de Cuenta no puede estar vacio, Intente de Nuevo!!"));
                Console.ReadLine();
                return new Empleado();
            }

            Empleado empleado = new Empleado(nombre, fechanacimiento, precioporhora, cuentabanco);
            return empleado;
        }   

        public void Agregar_Empleado() {
            Empleado empleado = Agregar_Actualizar_Empleado();

            if (string.IsNullOrEmpty(empleado.Nombre)) {
                return;
            }
            else {
                _storage.Agregar_Empleado(empleado);
                Console.WriteLine();
                Console.WriteLine(CenterText($"Empleado {empleado.Nombre} Agregado al Sistema Satisfactoriamente."));
                Console.ReadLine();   
            }
        }

        public void Actualizar_Empleado() {
            Console.WriteLine();
            Console.Write(CenterText($"Ingrese ID del empleado a actualizar: "));
            string? id_String = Console.ReadLine();
            int id = int.TryParse(id_String, out id) ? id : -1;

            if (id == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{id}', Favor escribir un numero.."));
                Console.ReadLine();
                return;
            }
            else if (id <= 0) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"ID no puede ser cero o negativo '{id}'."));
                Console.ReadLine();
                return;
            }

            List<Empleado> empleados = _storage.Get_Empleados();
            foreach (var emp in empleados) {
                if (emp.ID == id) {
                    Empleado empleado = Agregar_Actualizar_Empleado();
                    _storage.Actualizar_Empleado(id, empleado);
                    Console.WriteLine();
                    Console.WriteLine(CenterText($"Se han actualizado los datos del Empleado {empleado.Nombre}"));
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine(CenterText($"El empleado con ID '{id}' no fue encontrado en el sistema."));
            Console.ReadLine();
        }

        public void Eliminar_Empleado() {
            Console.WriteLine();
            Console.Write(CenterText($"Ingrese ID del empleado a Eliminar: "));
            string? id_String = Console.ReadLine();
            int id = int.TryParse(id_String, out id) ? id : -1;

            if (id == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{id}', Favor escribir un numero.."));
                Console.ReadLine();
                return;
            }
            else if (id <= 0) {
                Console.WriteLine(CenterText($" ID no puede ser cero o negativo '{id}'."));
                Console.ReadLine();
                return;
            }

            List<Empleado> empleados = _storage.Get_Empleados();
            foreach (var emp in empleados) {
                if (emp.ID == id) {
                    _storage.Eliminar_Empleado(id);
                    Console.WriteLine();
                    Console.WriteLine(CenterText($"Se ha Eliminado el empleado del sistema."));
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine(CenterText($"El empleado con ID '{id}' no fue encontrado en el sistema."));
            Console.ReadLine();
        }

        // Registros Laborales
        public RegistroLaboral Agregar_Actualizar_Registro() {
            Console.WriteLine();
            Console.Write(CenterText("Ingrese el Id del empleado: "));
            string? id_String = Console.ReadLine();
            int id = int.TryParse(id_String, out id) ? id : -1;

            if (id == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{id}', Favor escribir un numero.."));
                Console.ReadLine();
                return new RegistroLaboral();
            }
            else if (id <= 0) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"ID no puede ser cero o negativo '{id}'."));
                Console.ReadLine();
                return new RegistroLaboral();
            }

            Console.Write(CenterText($"Ingrese la cantidad de horas trabajadas: "));
            string? horas = Console.ReadLine();
            double cantidadhoras = double.TryParse(horas, out cantidadhoras) ? cantidadhoras : -1;

            if (cantidadhoras == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{horas}', Favor escribir un numero.."));
                Console.ReadLine();
                return new RegistroLaboral();
            }
            else if (cantidadhoras <= 0) {
                Console.WriteLine();
                Console.WriteLine(CenterText($" Cantidad de horas no puede ser cero o negativo '{horas}'."));
                Console.ReadLine();
                return new RegistroLaboral();
            }
            
            RegistroLaboral registro = new RegistroLaboral(id, cantidadhoras);
            return registro;
        }   

        public void Agregar_Registro() {
            RegistroLaboral registro = Agregar_Actualizar_Registro();

            if (registro.IdEmpleado == 0) {
                return;
            }
            else {
                List<RegistroLaboral> registros = _storage.Get_RegistroLaboral();
                foreach (var reg in registros){
                    if (reg.IdEmpleado == registro.IdEmpleado){
                        _storage.Eliminar_Registro(reg.ID);
                    }
                }
                _storage.Agregar_Registro(registro);
                Console.WriteLine();
                Console.WriteLine(CenterText($"Registro del empleado id {registro.IdEmpleado} Agregado al Sistema Satisfactoriamente."));
                Console.ReadLine();   
            }
        }

        public void Actualizar_Registro() {
            Console.WriteLine();
            Console.Write(CenterText($"Ingrese ID del registro a actualizar: "));
            string? id_String = Console.ReadLine();
            int id = int.TryParse(id_String, out id) ? id : -1;

            if (id == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{id}', Favor escribir un numero.."));
                Console.ReadLine();
                return;
            }
            else if (id <= 0) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"ID no puede ser cero o negativo '{id}'."));
                Console.ReadLine();
                return;
            }

            List<RegistroLaboral> registros = _storage.Get_RegistroLaboral();
            foreach (var reg in registros) {
                if (reg.ID == id) {
                    RegistroLaboral registro = Agregar_Actualizar_Registro();
                    _storage.Actualizar_Registro(id, registro);
                    Console.WriteLine();
                    Console.WriteLine(CenterText($"Se ha actualizado el registro."));
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine(CenterText($"El registro con ID '{id}' no fue encontrado en el sistema."));
            Console.ReadLine();
        }

        public void Eliminar_Registro() {
            Console.WriteLine();
            Console.Write(CenterText($"Ingrese ID del registro a Eliminar: "));
            string? id_String = Console.ReadLine();
            int id = int.TryParse(id_String, out id) ? id : -1;

            if (id == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{id}', Favor escribir un numero.."));
                Console.ReadLine();
                return;
            }
            else if (id <= 0) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"ID no puede ser cero o negativo '{id}'.."));
                Console.ReadLine();
                return;
            }

            List<RegistroLaboral> registros = _storage.Get_RegistroLaboral();
            foreach (var reg in registros) {
                if (reg.ID == id) {
                    _storage.Eliminar_Registro(id);
                    Console.WriteLine();
                    Console.WriteLine(CenterText($"Se ha eliminado el registro del sistema."));
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine(CenterText($"El registro con ID '{id}' no fue encontrado en el sistema."));
            Console.ReadLine();
        }

        // Detalles Nomina
        public void Agregar_Actualizar_Detalle() {

            List<DetalleNomina> nuevodetalleNominas = new List<DetalleNomina>();
            Console.WriteLine();
            Console.WriteLine(CenterText("Favor Tomar en cuenta que el sistema genera nominas en intervalos de 1 Mes"));
            Console.Write(CenterText($"Ingrese la fecha de nomina a agregar o actualizar, (MM-DD-YYYY): "));
            string? fecha = Console.ReadLine();
            DateTime fechanomina = DateTime.TryParse(fecha, out fechanomina) ? fechanomina : new DateTime();

            if (fechanomina.Year == 1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato de Fecha No Valido '{fechanomina.ToShortDateString()}', Intente de Nuevo!!"));
                Console.ReadLine();
                return;
            }

            fechanomina = new DateTime(fechanomina.Year,fechanomina.Month, 1);

            List<Empleado> empleados= _storage.Get_Empleados();
            List<RegistroLaboral> registros = _storage.Get_RegistroLaboral();
            List<DetalleNomina> detallesnomina = _storage.Get_DetalleNomina();

            foreach (var reg in registros) {
                foreach (var emp in empleados) {
                    if (emp.ID == reg.IdEmpleado) {
                        DetalleNomina detalle = new DetalleNomina(fechanomina, emp.ID, emp.PrecioPorHora*reg.CantidadHorasTrabajadas, emp.CuentaBanco);
                        nuevodetalleNominas.Add(detalle);
                    }
                }
            }

            foreach (var nom in detallesnomina) {
                if (nom.Fecha == fechanomina) {
                    _storage.Eliminar_Detalle_Nomina(nom.ID);
                }
            }

            foreach (var nom in nuevodetalleNominas) {
                _storage.Agregar_Detalle_Nomina(nom);
            }
            Console.WriteLine();
            Console.WriteLine(CenterText("Nomina a Sido Generada Con Exito."));
            Console.ReadLine();
        }
        public void Eliminar_Detalle() {
            Console.WriteLine();
            Console.Write(CenterText($"Ingrese ID de la Nomina a Eliminar: "));
            string? id_String = Console.ReadLine();
            int id = int.TryParse(id_String, out id) ? id : -1;

            if (id == -1) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"Formato Incorrecto '{id}', Favor escribir un numero.."));
                Console.ReadLine();
                return;
            }
            else if (id <= 0) {
                Console.WriteLine();
                Console.WriteLine(CenterText($"ID no puede ser cero o negativo '{id}'.."));
                Console.ReadLine();
                return;
            }

            List<DetalleNomina> detalles = _storage.Get_DetalleNomina();
            foreach (var detalle in detalles) {
                if (detalle.ID == id) {
                    _storage.Eliminar_Detalle_Nomina(id);
                    Console.WriteLine();
                    Console.WriteLine(CenterText($"Se ha eliminado la nomina del sistema."));
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine();
            Console.WriteLine(CenterText($"La nomina con ID '{id}' no fue encontrado en el sistema."));
            Console.ReadLine();
        }
    }
}