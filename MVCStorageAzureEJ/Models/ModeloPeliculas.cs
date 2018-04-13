using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace MVCStorageAzureEJ.Models
{
    public class ModeloPeliculas
    {
        private string uripleiculas;
        private string path;
        XDocument docxml;

        public ModeloPeliculas(string uri,string path)
        {
            uripleiculas = uri;
            this.path = path;
            docxml = XDocument.Load(uri);
        }

        public List<Pelicula> GetPeliculas()
        {
            var consulta = from pelis in docxml.Descendants("peliculas")
                           select new Pelicula
                           {
                               Titulo = pelis.Element("titulo").Value,
                               TituloOriginal = pelis.Element("titulooriginal").Value,
                               Descripcion = pelis.Element("descripcion").Value,
                               Poster = pelis.Element("poster").Value,
                               Escena =
                               new List<Escena>(from esce in pelis.Descendants("escena")
                                                select new Escena
                                                {
                                                    TituloEscena=esce.Element("tituloescena").Value,
                                                    Descripcion=esce.Element("descripcion").Value,
                                                    Imagen=esce.Element("imagen").Value
                                                })
                           };
            return consulta.ToList();
        }
    }
}