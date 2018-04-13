using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.File;
using Microsoft.Azure;
using System.IO;
using MVCStorageAzureEJ.Models;

namespace MVCStorageAzureEJ.Controllers
{
    public class HomeController : Controller
    {
        ModeloPeliculas modelo;
        public HomeController()
        {
            Uri uri = HttpContext.Request.Url;
            String rutauri = uri.Scheme + "://" + uri.Authority
                + "/XML/EscenasPeliculas2.xml";
            String path =
                HttpContext.Server.MapPath("~/XML/EscenasPeliculas2.xml");

            modelo = new ModeloPeliculas(rutauri,path);

        }
         
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase fichero,string accion)
        {
            String clave =
               CloudConfigurationManager.GetSetting("cuentastorage");
            CloudStorageAccount cuenta =
                CloudStorageAccount.Parse(clave);
            CloudFileClient cliente =
                cuenta.CreateCloudFileClient();
            CloudFileShare recurso = cliente.GetShareReference("ficheros");
            CloudFileDirectory directorio = recurso.GetRootDirectoryReference();

            if (accion == "subir")
            {
                CloudFile file = directorio.GetFileReference(fichero.FileName);

                file.UploadFromStream(fichero.InputStream);
                // file.UploadText(texto, System.Text.Encoding.UTF8, null, null, null);
                
            }
            else if (accion == "listar")
            {
                IEnumerable<IListFileItem> lista = directorio.ListFilesAndDirectories();
                String listado = "";
                List<String> listad = new List<string>();
                foreach (IListFileItem item in lista)
                {
                    listado = item.Uri.ToString();
                    int pos = listado.LastIndexOf("/") + 1;
                    String fich = listado.Substring(pos);
                    listad.Add(fich);
                }
                ViewBag.listado = listad;
            }

            return View();
        }
        public ActionResult Pelis()
        {
            List<Pelicula> pelis = modelo.GetPeliculas();

            return View(pelis);
        }

        public ActionResult About()
        {
             

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}