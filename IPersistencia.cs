

namespace AppNomina {

    public interface IPersistencia {
        List<Empleado> Get_Empleados();
        void Agregar_Empleado(Empleado empleado);
        void Actualizar_Empleado(int id_empleado, Empleado empleado);
        void Eliminar_Empleado(int id_empleado);

        List<RegistroLaboral> Get_RegistroLaboral();
        void Agregar_Registro(RegistroLaboral registrolaboral);
        void Actualizar_Registro(int id_registro, RegistroLaboral registro);
        void Eliminar_Registro(int id_registro);

        List<DetalleNomina> Get_DetalleNomina();
        void Agregar_Detalle_Nomina(DetalleNomina detallenomina);
        void Eliminar_Detalle_Nomina(int id_nomina);

    }
}