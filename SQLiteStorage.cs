using Microsoft.Data.Sqlite;

namespace AppNomina {
    public class SQLiteStorage: IPersistencia {
        private readonly string _connectionString;

        public SQLiteStorage(string connectionString) {
            _connectionString = connectionString;
            InitializeDatabase();
        }

        private void InitializeDatabase() {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = "PRAGMA foreign_keys = ON;";
            command.ExecuteNonQuery();

            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS empleados (
                    Id INTEGER PRIMARY KEY,
                    Nombre TEXT NOT NULL,
                    Fecha_Nacimiento TEXT NOT NULL,
                    Precio_Por_Hora REAL NOT NULL,
                    Cuenta_Banco TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();

            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS registrolaboral (
                    Id INTEGER PRIMARY KEY,
                    Id_Empleado INTEGER NOT NULL,
                    Cantidad_Horas_Trabajadas REAL NOT NULL
                );
            ";
            command.ExecuteNonQuery();

            command.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS detallenomina (
                    Id INTEGER PRIMARY KEY,
                    Fecha TEXT NOT NULL,
                    Id_Empleado INTEGER NOT NULL,
                    Monto REAL NOT NULL,
                    Cuenta_Banco TEXT NOT NULL
                );
            ";
            command.ExecuteNonQuery();

        }

        // Empleados
        public List<Empleado> Get_Empleados() {
            var empleados = new List<Empleado>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM empleados;";

            using var reader = command.ExecuteReader();
            while (reader.Read()) {
                var empleado = new Empleado {
                    ID = reader.GetInt32(0),
                    Nombre = reader.GetString(1),
                    FechaNacimiento = DateTime.Parse(reader.GetString(2)),
                    PrecioPorHora = double.Parse(reader.GetString(3)),
                    CuentaBanco = reader.GetString(4)
                    
                };
                empleados.Add(empleado);
            }

            return empleados;
        }

        public void Agregar_Empleado(Empleado empleado){
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO empleados (Nombre, Fecha_Nacimiento, Precio_Por_Hora, Cuenta_Banco) VALUES (@nombre, @fechanacimiento, @precioporhora, @cuentabanco);";
            command.Parameters.AddWithValue("@nombre", empleado.Nombre);
            command.Parameters.AddWithValue("@fechanacimiento", empleado.FechaNacimiento.ToShortDateString());
            command.Parameters.AddWithValue("@precioporhora", empleado.PrecioPorHora);
            command.Parameters.AddWithValue("@cuentabanco", empleado.CuentaBanco);
            command.ExecuteNonQuery();
        }

        public void Actualizar_Empleado(int id_empleado, Empleado empleado) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE empleados
                SET Nombre = @nombre, Fecha_Nacimiento = @fechanacimiento, Precio_Por_Hora = @precioporhora, Cuenta_Banco = @cuentabanco
                WHERE Id = @id
            ";
            command.Parameters.AddWithValue("@id", id_empleado);
            command.Parameters.AddWithValue("@nombre", empleado.Nombre);
            command.Parameters.AddWithValue("@fechanacimiento", empleado.FechaNacimiento.ToShortDateString());
            command.Parameters.AddWithValue("@precioporhora", empleado.PrecioPorHora);
            command.Parameters.AddWithValue("@cuentabanco", empleado.CuentaBanco);
            command.ExecuteNonQuery();
        }

        public void Eliminar_Empleado(int id_empleado) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM empleados WHERE Id = @id;";
            command.Parameters.AddWithValue("@id", id_empleado);
            command.ExecuteNonQuery();
        }

        // Registros Laborales
        public List<RegistroLaboral> Get_RegistroLaboral() {
            var registros = new List<RegistroLaboral>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM registrolaboral;";

            using var reader = command.ExecuteReader();
            while (reader.Read()) {
                var registro = new RegistroLaboral {
                    ID = reader.GetInt32(0),
                    IdEmpleado = int.Parse(reader.GetString(1)),
                    CantidadHorasTrabajadas = double.Parse(reader.GetString(2))
                };
                registros.Add(registro);
            }

            return registros;
        }

        public void Agregar_Registro(RegistroLaboral registro){
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO registrolaboral (Id_Empleado, Cantidad_Horas_Trabajadas) VALUES (@id_empleado, @cantidadhorastrabajadas);";
            command.Parameters.AddWithValue("@id_empleado", registro.IdEmpleado);
            command.Parameters.AddWithValue("@cantidadhorastrabajadas", registro.CantidadHorasTrabajadas);
            command.ExecuteNonQuery();
        }

        public void Actualizar_Registro(int id_registro, RegistroLaboral registro) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE registrolaboral
                SET Id_Empleado = @id_empleado, Cantidad_Horas_Trabajadas = @cantidadhorastrabajadas
                WHERE Id = @id
            ";
            command.Parameters.AddWithValue("@id", id_registro);
            command.Parameters.AddWithValue("@id_empleado", registro.IdEmpleado);
            command.Parameters.AddWithValue("@cantidadhorastrabajadas", registro.CantidadHorasTrabajadas);
            command.ExecuteNonQuery();
        }

        public void Eliminar_Registro(int id_registro) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM registrolaboral WHERE Id = @id;";
            command.Parameters.AddWithValue("@id", id_registro);
            command.ExecuteNonQuery();
        }

        // Detalles Nomina
        public List<DetalleNomina> Get_DetalleNomina() {
            var detalles = new List<DetalleNomina>();

            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM detallenomina;";

            using var reader = command.ExecuteReader();
            while (reader.Read()) {
                var detalle = new DetalleNomina {
                    ID = reader.GetInt32(0),
                    Fecha = DateTime.Parse(reader.GetString(1)),
                    IdEmpleado = int.Parse(reader.GetString(2)),
                    Monto = double.Parse(reader.GetString(3)),
                    CuentaBanco = reader.GetString(4)
                };
                detalles.Add(detalle);
            }

            return detalles;
        }

        public void Agregar_Detalle_Nomina(DetalleNomina detalle){
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "INSERT INTO detallenomina (Fecha, Id_Empleado, Monto, Cuenta_Banco) VALUES (@fecha, @id_empleado, @monto, @cuentabanco);";
            command.Parameters.AddWithValue("@fecha", $"{detalle.Fecha.ToShortDateString()}");
            command.Parameters.AddWithValue("@id_empleado", detalle.IdEmpleado);
            command.Parameters.AddWithValue("@monto", detalle.Monto);
            command.Parameters.AddWithValue("@cuentabanco", detalle.CuentaBanco);
            command.ExecuteNonQuery();
        }
        
        public void Eliminar_Detalle_Nomina(int id_detalle) {
            using var connection = new SqliteConnection(_connectionString);
            connection.Open();

            var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM detallenomina WHERE Id = @id;";
            command.Parameters.AddWithValue("@id", id_detalle);
            command.ExecuteNonQuery();
        }
    
    }
}
