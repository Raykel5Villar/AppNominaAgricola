using System.Text;

namespace AppNomina {
    public class DetalleNomina {
        private int id;
        private DateTime fecha;
        private int id_empleado;
        private double monto;
        private string? cuentabanco;

        public int ID { 
            get { return id; } 
            set { id = value;}
                }
        public DateTime Fecha { 
            get { return fecha; } 
            set { fecha = value;}
                }

        public int IdEmpleado { 
            get { return id_empleado; } 
            set { id_empleado = value;}
                }
        public double Monto { 
            get { return monto; } 
            set { monto = value;}
                }

        public string? CuentaBanco { 
            get { return cuentabanco; } 
            set { cuentabanco = value;}
                }

        public DetalleNomina() {
            fecha = new DateTime();
            id_empleado = 0;
            monto = 0;
            cuentabanco = "";
        }
        public DetalleNomina(DateTime fecha, int id_empleado, double monto, string? cuentabanco){
            this.fecha = fecha;
            this.id_empleado = id_empleado;
            this.monto = monto;
            this.cuentabanco=cuentabanco;
        }
        public DetalleNomina(int id, DateTime fecha, int id_empleado, double monto, string? cuentabanco):this(fecha, id_empleado, monto, cuentabanco){
            this.id = id;
        }
    }
}