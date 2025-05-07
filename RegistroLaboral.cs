namespace AppNomina {
    public class RegistroLaboral {

        private int id;
        private int id_empleado;
        private double cantidadhorastrabajadas;

        public int ID { 
            get { return id; } 
            set { id = value;}
                }
        public int IdEmpleado { 
            get { return id_empleado; } 
            set { id_empleado = value;}
                }
        public double CantidadHorasTrabajadas { 
            get { return cantidadhorastrabajadas; } 
            set { cantidadhorastrabajadas = value;}
                }

        public RegistroLaboral() {
            id_empleado = 0;
            cantidadhorastrabajadas = 0;
        }
        
        public RegistroLaboral(int id_empleado, double cantidadhorastrabajadas) {
            this.id_empleado = id_empleado;
            this.cantidadhorastrabajadas = cantidadhorastrabajadas;
        }

        public RegistroLaboral(int id, int id_empleado, double cantidadhorastrabajadas):this(id_empleado,cantidadhorastrabajadas) {
            this.id = id;
        }

    }
}