using Deployment_App.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Web.Security;
using System.IO;
using System.Text;
using System.Security.Policy;
using System.Web.Hosting;
using System.Web.UI.WebControls;

class Globals {
    public static string Pwd, Lg, ConStr;
    // GET: Account
}
namespace Deployment_App.Controllers
{

    public class AccountController : Controller
    {
       
        public static string ReadWithFile()
        {
            string textFromFile = "";
            using (FileStream fstream = new FileStream(HostingEnvironment.MapPath("~/App_Data/tns.txt"), FileMode.Open))
            {
                byte[] b = new byte[1024];
                long k = fstream.Length;
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fstream.Read(b, 0, Convert.ToInt32(k)) > 0)
                {
                    textFromFile = temp.GetString(b);
                    
                }
                textFromFile = textFromFile.Remove(textFromFile.LastIndexOf(@";")+1);
                return textFromFile;
            }


        }

        public ActionResult Login()
        {
            return View();
        }

        

        public static void SafeConnect (string lg, string pwd, string constr)
            {
                Globals.Lg = lg;
                Globals.Pwd = pwd;
                Globals.ConStr = constr;
            }

        public static string DBConnect(string login,string password)
        {
            try { 
                var s = ReadWithFile();
            string oracleDbConnection = s
            + "User Id=" + login
                              + ";Password=" + password
                              + ";";

            OracleConnection conn = new OracleConnection(oracleDbConnection);

            //Open database connection
            conn.Open();            
            conn.Close();
            conn.Dispose();
            return s;
            }
            catch (Exception ex)
            {
                 return "Error";                
            }

        }
    [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                
               var s = DBConnect(model.Name, model.Password);
                if (s != "Error")
                {
                    try
                    {
                        SafeConnect(model.Name, model.Password, s);

                        return RedirectToAction("Index", "Home", model);
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", ex);
                    }
                }
                else 
                {
                    return View();
                }

            }


            return View(model);

        }

     }


    

}
