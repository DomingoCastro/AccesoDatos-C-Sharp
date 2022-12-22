using System.Data;
using System.Data.SqlClient;


namespace AccesoDatos

{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //LeerRegistrosDept();
            //InsertarDatosDept();
            //EliminarDatosConParametros();
            //LeerRegistrosDept();
            //MostrarEMP();
            //ModificarSala();
            MostrarEnfermos();
            EliminarEnfermos();
            MostrarEnfermos();
        }


        static void MostrarEnfermos()
        {
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            SqlDataReader lector;
            string sql = "select INSCRIPCION, APELLIDO, DIRECCION FROM ENFERMO";
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            cn.Open();
            lector = com.ExecuteReader();
            while (lector.Read())
            {
                string inscripcion = lector["INSCRIPCION"].ToString();
                string apellido = lector["APELLIDO"].ToString();
                string direccion = lector["DIRECCION"].ToString();
                Console.WriteLine("Fecha de inscripcion: " + inscripcion + " " + "Apellido: " + apellido + " " + "Direccion: " + direccion);
            }
            lector.Close();
        }

        static void EliminarEnfermos()
        {
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            com.Connection = cn;
            com.CommandType = System.Data.CommandType.Text;
            cn.Open();
            Console.WriteLine("Dime el numero de inscripcion para eliminar el enfermo");
            int idenfermo = int.Parse(Console.ReadLine());
            string sqldelete = "DELETE FROM ENFERMO WHERE INSCRIPCION =@INSCRIPCION";
            SqlParameter parameters = new SqlParameter("@INSCRIPCION", idenfermo);
            com.Parameters.Add(parameters);
            com.CommandText = sqldelete;
            int deletes = com.ExecuteNonQuery();
            
            cn.Close();
            com.Parameters.Clear();
            Console.WriteLine("Enfermos eliminados: " + deletes);

        }

        static void ModificarSala()
        {
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            SqlDataReader lector;
            string sql = "select SALA_COD, NOMBRE FROM SALA";
            
            

            com.Connection= cn;
            com.CommandType= System.Data.CommandType.Text;
            com.CommandText= sql;
            cn.Open();
            lector = com.ExecuteReader();
            while (lector.Read())
            {
                string SALA_COD = lector["SALA_COD"].ToString();
                string NOMBRE = lector["NOMBRE"].ToString();
                Console.WriteLine("Numero de sala: "+ SALA_COD + " " + "Nombre de sala: " + NOMBRE);
            }
            lector.Close();
            string sqlmod = "update SALA SET NOMBRE=@NOMBRE WHERE SALA_COD=@SALACOD";
            Console.WriteLine("Que ID de sala desea modificar?");
            string idsala = Console.ReadLine();
            Console.WriteLine("Que nombre le quieres poner?");
            string nombrenuevo = Console.ReadLine();
            SqlParameter parameters = new SqlParameter("@NOMBRE", nombrenuevo);
            SqlParameter identsala = new SqlParameter("@SALACOD", idsala);
            com.Parameters.Add(identsala);
            com.Parameters.Add(parameters);
            com.CommandText = sqlmod;
            int actualizados = com.ExecuteNonQuery();
            cn.Close();
            com.Parameters.Clear();
            Console.WriteLine("Registro actualizado: " + actualizados);



        }

        static void MostrarEMP()
        {
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            SqlDataReader lector;
            Console.WriteLine("Escribe el numero del departamento");
            int numero = int.Parse(Console.ReadLine());
            string sql = "Select APELLIDO, SALARIO, OFICIO FROM EMP WHERE DEPT_NO =@NUMERO";
            SqlParameter parameter = new SqlParameter("@NUMERO", numero);
            com.Parameters.Add(parameter);
            com.Connection= cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText= sql;
            cn.Open();
            lector = com.ExecuteReader();
            while (lector.Read()){
                string apellido = lector["APELLIDO"].ToString();
                string oficio = lector["OFICIO"].ToString() ;
                string salario = lector["SALARIO"].ToString();
                Console.WriteLine("Apellido: "+ apellido + " " + "Oficio: " + oficio+ " " + "Salario: " +salario);
            }
            cn.Close();
            com.Parameters.Clear()
;        }


        static void EliminarDatosConParametros()
        {
            // ES MUY IMPORTANTE CON PARAMETROS PONER EL COM.PARAMETERS.ADD(PARAMETRO QUE HEMOS AGREGADO EN ESTE CASO PARAMETERS)
            // Y CERRAR AL FINAL DEL TODO EL PARAMETRO LIMPIANDOLO CON COM.PARAMETERS.CLEAR()
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA";
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            Console.WriteLine("Indica el numero del departamento a eliminar:");
            int numero = int.Parse(Console.ReadLine());
            string sql = "DELETE FROM DEPT WHERE DEPT_NO = @NUMERO";
            SqlParameter parameter = new SqlParameter("@NUMERO", numero);
            com.Parameters.Add(parameter);
            com.Connection= cn;
            com.CommandType = System.Data.CommandType.Text;
            com.CommandText = sql;
            cn.Open();
            int eliminados = com.ExecuteNonQuery();
            cn.Close();
            com.Parameters.Clear();
            Console.WriteLine("Registros eliminados: " + eliminados);
        }

        static void InsertarDatosDept()
        {
            //necesitamos nuestra cadena de conexion
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA";
            // DECLARAMOS NUESTRO OBJETO DE ACCESO A DATOS
            // EXCEPTO dataReader porque no vamos a leer si no a insertar
            SqlConnection cn = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand();
            string sql = "Insert into DEPT VALUES (88, 'INFORMATICA', 'OVIEDO')";
            // CONFIGURAMOS NUESTRO COMANDO
            // INDICAMOS CONEXION
            com.Connection= cn;
            // TIPO DE CONSULTA QUE VAMOS A REALIZAR
            com.CommandType= System.Data.CommandType.Text;
            // LA CONSULTA SQL
            com.CommandText= sql;
            // ENTRAMOS Y SALIMOS DE LA CONEXION
            cn.Open();
            // EJECUTAMOS LA CONSULTA CON EL METODO ExecuteNonQuery()
            // QUE NOS DEVUELVE U NENTERO CON EL NUMERO DE REGISTROS AFECTADOS
            int insertados = com.ExecuteNonQuery();
            //SALIMOS
            cn.Close();
            Console.WriteLine("Registros insertados: " + insertados);
        }
        static void LeerRegistrosDept()
        {
            string connectionString = @"Data Source = LOCALHOST\SQLEXPRESS; Initial Catalog = HOSPITAL; Persist Security Info = True; User ID = SA";
            // DECLARAMOS LOS OBJETOS A UTILIZAR
            // EL OBJETO CONNECTION SIEMPRE DEBE LLEVAR LA CADENA DE CONEXION EN SU CONSTRUCTOR
            SqlConnection cn = new SqlConnection(connectionString);
            // INSTNACIAMOS EL COMANDO, QUE ES EL ENCARGADO DE LAS CONSULTAS
            SqlCommand com = new SqlCommand();
            // COMO VAMOS A LEER DATOS, DECLARAMOS UN CURSOR
            // UN CURSOR NUNCA SE INSTANCIA, SOLAMENTO PODEMOS CREARLO A PARTIR DE NUESTRA CONSULTA
            SqlDataReader lector;
            // DECLARAMOS NUESTRA CONSULTA
            string sql = "select * from DEPT";
            // CONFIGURAMOS NUESTRO COMMAND, INDICAMOS A NUESTRO COMANDO LA CONEXION A UTILIZAR
            com.Connection= cn;
            // DEBEMOS INDICAR EL TIPO DE CONSULTA
            com.CommandType = System.Data.CommandType.Text;
            // INDICAMOS LA PROPIA CONSULTA
            com.CommandText = sql;
            // UNA VEZ FINALIZADA LA CONFIGURACION DEBEMOS EJECUTAR LA CONSULTA, PARA ELLO ABRIMOS LA CONEXION
            // SIEMPRE SERA ENTRAR Y SALIR
            cn.Open();
            // EJECUTAMOS LA CONSULTA MEDIANTE EL COMANDO
            // AL SER UNA CONSULTA DE SELECCION, SE UTILIZA EL METODO 
            //executeReader(), que nos devuelve un datareader (cursor/lector)
            lector = com.ExecuteReader();
            // Para leer el reader contiene un metodo Read() que lee una fila y devuelve true/false si ha podido leer
            // Cada vez que ejecutamos Read() se mueve una fila. Solamente podemos ir hacia adelante.
            //lector.Read();
            // READ ES BOOLEAN POR LO QUE SI QUEREMOS RECORRER TODOS LOS REGISTROS DEBEMOS UTILZIAR UN BUCLE CONDICIONAL
            // Para leer los datos de una columna se utilizar  lector["COLUMNA"], tambien podemos utilziar lector[indice]
            // RECUPERAMOS EL NOMBRE
            //string nombre = lector["DNOMBRE"].ToString();
            //Console.WriteLine(nombre);
            while (lector.Read())
            {
                string nombre = lector["DNOMBRE"].ToString();
                string localidad = lector["LOC"].ToString() ;
                string numero = lector["DEPT_NO"].ToString();
                Console.WriteLine("Numero: "+ numero +" Nombre: " + nombre + " " + "Localidad: " + localidad);
            }

            //UNA VEZ QUE HEMOS LEIDO SIEMPRE SE CIERRA EL CURSOR Y LA CONEXION
            lector.Close();
            cn.Close();
        }
    }
}