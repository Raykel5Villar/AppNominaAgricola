namespace AppNomina {
    public class GeneradorReportesTXT {
        private readonly IPersistencia _storage;
        private string file_empresa = "Reporte_Empresa.txt";
        private string file_banco = "Reporte_Bancos.txt";
        private List<string> empleados_empresa = new List<string>();
        private List<string> empleados_banco = new List<string>();
        
        public GeneradorReportesTXT() {
            string? appPath = AppDomain.CurrentDomain.BaseDirectory;
            _storage = new SQLiteStorage($"Data Source={appPath}nomina.db");

        }
        public void Guardar_Reporte_Empresa(){
            List<Empleado> empleados= _storage.Get_Empleados();
            List<RegistroLaboral> registros = _storage.Get_RegistroLaboral();
            List<DetalleNomina> detallesnomina = _storage.Get_DetalleNomina();

            foreach (var detalle in detallesnomina) {
                foreach (var reg in registros) {
                    foreach (var emp in empleados) {
                        if (emp.ID == reg.IdEmpleado && emp.ID == detalle.IdEmpleado) {
                            string emplado_txt = $"Empleado ID: {emp.ID}; Empleado: {emp.Nombre}; Nacimiento: {emp.FechaNacimiento.ToShortDateString()}; Precio Por Hora: {emp.PrecioPorHora}; Horas Trabajadas: {reg.CantidadHorasTrabajadas}; Nomina: {detalle.Monto:C}; Fecha Nomina: {detalle.Fecha.ToShortDateString()}; Numero Cuenta {detalle.CuentaBanco}";
                            empleados_empresa.Add(emplado_txt);
                        }
                    }
                }
            }
            File.WriteAllLines(file_empresa, empleados_empresa);
            Console.WriteLine();
            Console.WriteLine(CenterText($@"Reporte Empleado Guardado con exito en: {Environment.CurrentDirectory}\{file_empresa}"));
            Console.ReadLine();
        }

        public void Guardar_Reporte_Banco(){
            List<Empleado> empleados= _storage.Get_Empleados();
            List<RegistroLaboral> registros = _storage.Get_RegistroLaboral();
            List<DetalleNomina> detallesnomina = _storage.Get_DetalleNomina();

            foreach (var detalle in detallesnomina) {
                foreach (var reg in registros) {
                    foreach (var emp in empleados) {
                        if (emp.ID == reg.IdEmpleado && emp.ID == detalle.IdEmpleado) {
                            string emplado_txt = $"Empleado: {emp.Nombre}, Nomina: {detalle.Monto:C}, Fecha Nomina: {detalle.Fecha.ToShortDateString()}, Numero Cuenta {detalle.CuentaBanco}";
                            empleados_banco.Add(emplado_txt);
                        }
                    }
                }
            }
            File.WriteAllLines(file_banco, empleados_banco);
            Console.WriteLine();
            Console.WriteLine(CenterText($@"Reporte Banco Guardado con exito en: {Environment.CurrentDirectory}\{file_banco}"));
            Console.ReadLine();
        }

        public string CenterText(string texto) {
            int consoleWidth = Console.WindowWidth;
            int padding = (consoleWidth - texto.Length) / 2;
            string final = new string(' ', Math.Max(0, padding)) + texto;
            return final;
        }
    
    }
}