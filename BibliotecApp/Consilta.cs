using System;
using System.Collections.Generic;
using System.Text;

namespace App_Clinica
{
    public class Consilta
    {
        public string fecha_hora {  get; set; }
        public Paciente paciente { get; set; }
        public Medico medico { get; set; }
        public string no_consultorio { get; set; }
        public string diagnostico { get; set; }
        public string receta { get; set; }


    }
}
