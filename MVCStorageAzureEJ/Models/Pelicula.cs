using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCStorageAzureEJ.Models
{
    public class Pelicula
    {
        public String Titulo { get; set; }
        public String TituloOriginal { get; set; }
        public String Descripcion { get; set; }
        public String Poster { get; set; }
        public List<Escena> Escena { get; set; }
    }
}