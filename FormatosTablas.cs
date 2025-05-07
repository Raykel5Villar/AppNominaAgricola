using System.Runtime.InteropServices;
#pragma warning disable CS8604

namespace AppNomina {
    public class FormatosTablas {
        private readonly IPersistencia _storage;
        public FormatosTablas() {
            string? appPath = AppDomain.CurrentDomain.BaseDirectory;
            _storage = new SQLiteStorage($"Data Source={appPath}nomina.db");
        }


        public void Formato_Empleados(){
            List<Empleado> empleados = _storage.Get_Empleados();

            string[] nombre_columnas1 = {"Empleado ID", "Nombre", "Fecha Nacimiento", "Precio Por Hora", "Cuenta de Banco"};
            List<string> IDs = new List<string>();
            List<string> Nombres = new List<string>();
            List<string> FechaNacimiento = new List<string>();
            List<string> PrecioHora = new List<string>();
            List<string> CuentaBanco = new List<string>();

            foreach (var emp in empleados) {
                IDs.Add(emp.ID.ToString());
                Nombres.Add(emp.Nombre);
                FechaNacimiento.Add(emp.FechaNacimiento.ToShortDateString());
                PrecioHora.Add(emp.PrecioPorHora.ToString());
                CuentaBanco.Add(emp.CuentaBanco);
            }

            Formato_de_tabla(nombre_columnas:nombre_columnas1, column1:IDs, column2:Nombres, column3:FechaNacimiento, column4:PrecioHora, column5:CuentaBanco, tipo:"Empleados");
        }

        public void Formato_Registro(){
            List<Empleado> empleados = _storage.Get_Empleados();
            List<RegistroLaboral> registros = _storage.Get_RegistroLaboral();

            string[] nombre_columnas1 = {"Registro ID", "ID Empleado", "Nombre Empleado", "Horas Trabajadas"};
            List<string> IDs = new List<string>();
            List<string> IDs_Empleados = new List<string>();
            List<string> NombreEmpleados = new List<string>();
            List<string> HorasTrabajadas = new List<string>();

            foreach (var reg in registros){
                foreach (var emp in empleados) {
                    if (emp.ID == reg.IdEmpleado) {
                        IDs.Add(reg.ID.ToString());
                        IDs_Empleados.Add(emp.ID.ToString());
                        NombreEmpleados.Add(emp.Nombre);
                        HorasTrabajadas.Add(reg.CantidadHorasTrabajadas.ToString());
                        break;
                    }
                }
            }
            Formato_de_tabla(nombre_columnas:nombre_columnas1, column1:IDs, column2:IDs_Empleados, column3:NombreEmpleados, column4:HorasTrabajadas, tipo:"Registros");
        }
        public void Formato_Nominas(){
            List<Empleado> empleados = _storage.Get_Empleados();
            List<DetalleNomina> detallesnomina = _storage.Get_DetalleNomina();

            string[] nombre_columnas1 = {"Nomina ID", "Fecha", "ID Empleado", "Empleado Nombre", "Monto", "Cuenta de Banco"};
            List<string> IDs = new List<string>();
            List<string> Fechas = new List<string>();
            List<string> IDs_Empleados = new List<string>();
            List<string> Nombres_Empleados = new List<string>();
            List<string> Monto = new List<string>();
            List<string> CuentaBanco = new List<string>();


            foreach (var detalle in detallesnomina){
                foreach (var emp in empleados) {
                    if (emp.ID == detalle.IdEmpleado) {
                        IDs.Add(detalle.ID.ToString());
                        Fechas.Add(detalle.Fecha.ToShortDateString());
                        IDs_Empleados.Add(emp.ID.ToString());
                        Nombres_Empleados.Add(emp.Nombre);
                        Monto.Add($"{detalle.Monto:C}");
                        CuentaBanco.Add(detalle.CuentaBanco);
                        break;
                    }
                }
            }

            Formato_de_tabla(nombre_columnas:nombre_columnas1, column1:IDs, column2:Fechas, column3:IDs_Empleados, column4:Nombres_Empleados, column5:Monto, column6:CuentaBanco, tipo:"Nominas");
        }
        public void Formato_de_tabla(string[] nombre_columnas, List<string> column1, List<string> column2, [Optional]List<string> column3, [Optional]List<string> column4, [Optional]List<string> column5, [Optional]List<string> column6, [Optional]List<string> column7, [Optional]string tipo) {
            Console.WriteLine();
            List<string[]> valores_tabla = new List<string[]>();

            if (tipo == "Empleados") {
                for (int i = 0; i < column1.Count; i++) {

                    valores_tabla.Add(new string[] { column1[i], column2[i], column3[i], column4[i], column5[i]} );
                }
            }
            else if (tipo == "Registros") {
                for (int i = 0; i < column1.Count; i++) {

                    valores_tabla.Add(new string[] { column1[i], column2[i], column3[i], column4[i]} );
                }
            }
            else if (tipo == "Nominas") {
                for (int i = 0; i < column1.Count; i++) {

                    valores_tabla.Add(new string[] { column1[i], column2[i], column3[i], column4[i], column5[i], column6[i]} );
                }
            }

            int tamano_maximo = 0;

            // Determinar tamaño maximo de las columnas
            foreach(var col in nombre_columnas) { 
                if (col.Length > tamano_maximo){
                    tamano_maximo = col.Length;
                }
            }

            // Determinar maximo de los valores de las columnas
            foreach (var emp in valores_tabla) { 
                for (int i = 0; i < emp.Count(); i++) {
                    if (emp[i].Length > tamano_maximo) {
                        tamano_maximo = emp[i].Length;
                    }
                }
            } 

            tamano_maximo += 1;

            int table_length = 0;

            // Encontrar el tamaño maximo de la tabla
            for (int i = 0; i < nombre_columnas.Count(); i++) { 
                table_length += nombre_columnas[i].Length + 1 + tamano_maximo - nombre_columnas[i].Length + 1;
                
            }
            
            Console.Write(CenterText(new string('=', table_length+1)));
            Console.WriteLine();

            string format = "";

            // Dar formato a cada linea de la tabla
            for (int i = 0; i < nombre_columnas.Count(); i++) {
                format += $"| {{{i},-{tamano_maximo}}}";
            
            }

            format += "|";

            // Imprimimos las columnas
            Console.WriteLine(CenterText(string.Format(format, nombre_columnas.ToArray())));
            Console.Write(CenterText(new string('=', table_length+1)));
            Console.WriteLine();

            // Imprimimos los valores de la tabla
            foreach (var emp in valores_tabla) {
                Console.WriteLine(CenterText(string.Format(format, emp.ToArray())));
            }

            Console.Write(CenterText(new string('-', table_length+1)));
            Console.WriteLine();
            
            if (tipo == "Nominas") {
                // Console.Write(CenterText(new string(' ', table_length - tamano_maximo - 14)));
                Console.WriteLine(CenterText($"Total de Nomina: {Calcular_Total_Nomina():C}"));
            }
            Console.ReadLine();
        }
        public double Calcular_Total_Nomina(){
            double total = 0.0;
            List<DetalleNomina> detallesnomina = _storage.Get_DetalleNomina();
            foreach (var detalle in detallesnomina) {
                total += detalle.Monto;
            }
            return total;
        }
        public string CenterText(string texto) {
            int consoleWidth = Console.WindowWidth;
            int padding = (consoleWidth - texto.Length) / 2;
            string final = new string(' ', Math.Max(0, padding)) + texto;
            return final;
        }
    }
}