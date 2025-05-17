using System;
using Microsoft.Data.SqlClient;

namespace CapaDatos
{

    public class Personas 
    {
        //Atributos de la clase personas para ser heredados en otras clases
        public int id;
        public string Nombre;
        public string Apellido;
        public string tipo;

    }
    //clase estudiante para ser heredada de la clase persona sus atributos
    public class Estudiante : Personas
    {

        public string carrera = "ingenieria en software";


    }

    public class Docente : Personas
    {

        public string materia = "programacion";

    }  


    public class PersonaDatos
    {

        public string conexion = "Server=.;Database=Escuela;Integrated Security=true";

        public Personas ObtenerPersonasPorId(int idBusqueda)
        {

            using(SqlConnection conn = new SqlConnection(conexion))
            {

                conn.Open();

                //Consulta SQL para buscar por Id
                string query = "SELECT * FROM Personas WHERE Id = @Id";

                //Comando SSQL
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", idBusqueda);
                // Agrega el parametro de busqueda

                //Ejecutando la consulta
                SqlDataReader reader = cmd.ExecuteReader();

                // Verifica si hay resultado 
                if (reader.Read())
                {

                    //Se determina el tipo de persona

                    string tipo = reader["Tipo"].ToString();
                    Personas personas;

                    //si es estudiantes, se instacia Estudiante
                    if (tipo == "Personas")
                        personas = new Estudiante();
                    else
                        //si es docente, se instancia Docente
                        personas = new Docente();

                    //Se asignan valores desde las base
                    //de datos a los atributos de la clase
                    personas.id = (int)reader["id"];
                    personas.Nombre = reader["Nombre"].ToString();
                    personas.Apellido = reader["Apellidp"].ToString();
                    personas.tipo = tipo;

                    return personas; // Devulve la persona encontra

                }

                return null; // Si no se encuentra, duvuleve null
            }
        }
    }
}
