using System.Text;

namespace AppNomina {
    public class Empleado {
        private int id;
        private string? nombre;
        private DateTime fechanacimiento;
        private double precioporhora;
        private string? cuentabanco;

        public int ID { 
            get { return id; } 
            set { id = value;}
                }
        public string? Nombre { 
            get { return nombre; } 
            set { nombre = value;}
                }

        public DateTime FechaNacimiento { 
            get { return fechanacimiento; } 
            set { fechanacimiento = value;}
                }
        public double PrecioPorHora { 
            get { return precioporhora; } 
            set { precioporhora = value;}
                }

        public string? CuentaBanco { 
            get { return cuentabanco; } 
            set { cuentabanco = value;}
                }

        public Empleado() {
            nombre = "";
            fechanacimiento = new DateTime();
            precioporhora = 0;
            cuentabanco = "";
        }
        public Empleado(string? nombre, DateTime fechanacimiento, double precioporhora, string? cuentabanco){
            this.nombre = nombre;
            this.fechanacimiento = fechanacimiento;
            this.precioporhora = precioporhora;
            this.cuentabanco=cuentabanco;
        }
        public Empleado(int id, string? nombre, DateTime fechanacimiento, double precioporhora, string? cuentabanco):this(nombre, fechanacimiento, precioporhora, cuentabanco){
            this.id = id;
        }
    }
}