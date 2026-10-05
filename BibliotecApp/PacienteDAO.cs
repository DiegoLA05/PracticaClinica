using appclinica;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace App_Clinica
{
    public class PacienteDAO
    {
        public int insertar(Paciente paciente)
        {
            string conectString = @"server=localhoust;" +
                                    "database=clinicadb;" +
                                    "user ID=ROOT;" +
                                    "Pssword=;" +
                                    "port=3308;";
            string sql = "insert into paciente(nombre,apellido_p,apellido_m,telefono,";
            return 0;
        }
        public ArrayList ListarTodos()
        {
            ArrayList pacientes;
            AccesoDatos accesoDatos = new AccesoDatos();
            string sql = "SELECT * from paciente;";
            pacientes=accesoDatos.EjecutarConsulta(sql);
            return pacientes;
        }
        public void editar()
        {

        }

        public void eliminar()
        {

        }
    }
}
