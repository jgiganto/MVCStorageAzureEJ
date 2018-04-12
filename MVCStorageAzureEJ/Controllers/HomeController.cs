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

namespace MVCStorageAzureEJ.Controllers
{
    public class HomeController : Controller
    {
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
            ViewBag.Message = "Your application description page.";

            return View();
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