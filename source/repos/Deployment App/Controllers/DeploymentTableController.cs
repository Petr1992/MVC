using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Deployment_App.Controllers;
using Deployment_App.Models;

namespace Deployment_App.Controllers
{
    public class DeploymentTableController : Controller
    {

        public static List<DeploymentTable> GetDeploymentTable()
        {
            List<DeploymentTable> deploymentTable_List = new List<DeploymentTable>();


            string oracleDbConnection = Globals.ConStr
                + "User Id=" + Globals.Lg
                         + ";Password=" + Globals.Pwd
                         + ";";


            OracleConnection conn = new OracleConnection(oracleDbConnection);
            OracleCommand cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT DEPLOYMENT_ID,  "
                                + "TASK_ID_LIST, "
                                + "ISSUE, "
                                + "DATE_DEPLOY, "
                                + "DEPLOYER, "

                                + "TYPE_EXCEPTION, "
                                + "COMMENT_DEPLOY, "
                                + "DEVELOPER_MAIL_DATE, "
                                + "DEVELOPER_MAIL_SUBJECT, "
                                + "DEVELOPER_MAIL_SENDER, "
                                + "DATA_REPORT, "
                                + "GIT, "
                                + "IS_FIX FROM SYS.DEPLOYMENT_LOG ORDER BY rowid desc";


            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var dp_t = new DeploymentTable();
                dp_t.DEPLOYMENT_ID = Convert.ToInt64(reader["DEPLOYMENT_ID"]);
                dp_t.TASK_ID_LIST = reader["TASK_ID_LIST"].ToString();
                dp_t.ISSUE = reader["ISSUE"].ToString();
                if (reader["DATE_DEPLOY"].ToString() != "") { dp_t.DATE_DEPLOY = Convert.ToDateTime(reader["DATE_DEPLOY"]); }

                dp_t.DEPLOYER = reader["DEPLOYER"].ToString();
                dp_t.TYPE_EXCEPTION = reader["TYPE_EXCEPTION"].ToString();
                dp_t.COMMENT_DEPLOY = reader["COMMENT_DEPLOY"].ToString();
                if (reader["DEVELOPER_MAIL_DATE"].ToString() != "") { dp_t.MAIL_DATE = Convert.ToDateTime(reader["DEVELOPER_MAIL_DATE"]); }

                dp_t.MAIL_SUBJECT = reader["DEVELOPER_MAIL_SUBJECT"].ToString();
                dp_t.MAIL_SENDER = reader["DEVELOPER_MAIL_SENDER"].ToString();
                dp_t.DATA_REPORT = reader["DATA_REPORT"].ToString();
                dp_t.GIT = reader["GIT"].ToString();
                if (reader["IS_FIX"].ToString() != "") { dp_t.IS_FIX = Convert.ToInt16(reader["IS_FIX"]); }


                deploymentTable_List.Add(dp_t);
            }
            conn.Close();
            conn.Dispose();
            cmd.Dispose();

            return deploymentTable_List;
        }

        // GET: DeploymentTable
        public ActionResult Views()
        {
            if (Globals.Lg != null || Globals.Pwd != null)
            {
                ViewBag.UserName = Globals.Lg;
                var s = GetDeploymentTable();
                return View(s);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        // GET: DeploymentTable/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: DeploymentTable/Create
        [HttpGet]
        public ActionResult Create()
        {

            if (Globals.Lg != null || Globals.Pwd != null)
            {
                ViewBag.UserName = Globals.Lg;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: DeploymentTable/Create
        [HttpPost]
        public ActionResult Create(DeploymentTable model)
        {
            try
            {
                // TODO: Add insert logic here

                string oracleDbConnection = Globals.ConStr
                             + "User Id=" + Globals.Lg
                         + ";Password=" + Globals.Pwd
                             + ";";


                OracleConnection conn = new OracleConnection(oracleDbConnection);
                OracleCommand cmd = new OracleCommand();
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO SYS.DEPLOYMENT_LOG ("
                                    + "TASK_ID_LIST,"
                                    + "ISSUE,"
                                    + "DATE_DEPLOY,"
                                    + "lower(DEPLOYER),"
                                    + "TYPE_EXCEPTION,"
                                    + "COMMENT_DEPLOY,"
                                    + "DEVELOPER_MAIL_DATE,"
                                    + "DEVELOPER_MAIL_SUBJECT,"
                                    + "DEVELOPER_MAIL_SENDER,"
                                    + "DATA_REPORT,"
                                    + "GIT,"
                                    + "IS_FIX)"
                                     + "VALUES(" +                                    
                                    "'" + model.TASK_ID_LIST + "'," +
                                    "'" + model.ISSUE + "',to_date('" +
                                        model.DATE_DEPLOY + "','dd.mm.yyyy hh24:mi:ss')," +
                                    "'" + model.DEPLOYER + "'," +
                                    "'" + model.TYPE_EXCEPTION + "'," +
                                    "'" + model.COMMENT_DEPLOY + "',to_date('" +
                                    model.MAIL_DATE + "','dd.mm.yyyy hh24:mi:ss')," +
                                    "'" + model.MAIL_SUBJECT + "'," +
                                    "'" + model.MAIL_SENDER + "'," +
                                    "'" + model.DATA_REPORT + "'," +
                                    "'" + model.GIT + "'," +
                                    model.IS_FIX + ") ";


                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                cmd.Dispose();

                return RedirectToAction("Views", "DeploymentTable");
            }
            catch (Exception ex)
            {
                if (ex.HResult == 1017) { return RedirectToAction("Login", "Account"); }
                else
                {
                    return View();
                }
            }
        }


        public DeploymentTable GetRow(int id)
        {

            var dp_t = new DeploymentTable();
            string oracleDbConnection = Globals.ConStr
                            + "User Id=" + Globals.Lg
                         + ";Password=" + Globals.Pwd
                            + ";";


            OracleConnection conn = new OracleConnection(oracleDbConnection);
            OracleCommand cmd = new OracleCommand();
            conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT DEPLOYMENT_ID,  "
                                + "TASK_ID_LIST, "
                                + "ISSUE, "
                                + "DATE_DEPLOY, "
                                + "DEPLOYER, "
                                + "TYPE_EXCEPTION, "
                                + "COMMENT_DEPLOY, "
                                + "DEVELOPER_MAIL_DATE, "
                                + "DEVELOPER_MAIL_SUBJECT, "
                                + "DEVELOPER_MAIL_SENDER, "
                                + "DATA_REPORT, "
                                + "GIT, "
                                + "IS_FIX FROM SYS.DEPLOYMENT_LOG where DEPLOYMENT_ID =  " + Convert.ToString(id);


            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {

                dp_t.DEPLOYMENT_ID = Convert.ToInt64(reader["DEPLOYMENT_ID"]);
                dp_t.TASK_ID_LIST = reader["TASK_ID_LIST"].ToString();
                dp_t.ISSUE = reader["ISSUE"].ToString();
                if (reader["DATE_DEPLOY"].ToString() != "") { dp_t.DATE_DEPLOY = Convert.ToDateTime(reader["DATE_DEPLOY"]); }

                dp_t.DEPLOYER = reader["DEPLOYER"].ToString();
                dp_t.TYPE_EXCEPTION = reader["TYPE_EXCEPTION"].ToString();
                dp_t.COMMENT_DEPLOY = reader["COMMENT_DEPLOY"].ToString();
                if (reader["DEVELOPER_MAIL_DATE"].ToString() != "") { dp_t.MAIL_DATE = Convert.ToDateTime(reader["DEVELOPER_MAIL_DATE"]); }

                dp_t.MAIL_SUBJECT = reader["DEVELOPER_MAIL_SUBJECT"].ToString();
                dp_t.MAIL_SENDER = reader["DEVELOPER_MAIL_SENDER"].ToString();
                dp_t.DATA_REPORT = reader["DATA_REPORT"].ToString();
                dp_t.GIT = reader["GIT"].ToString();
                if (reader["IS_FIX"].ToString() != "") { dp_t.IS_FIX = Convert.ToInt16(reader["IS_FIX"]); }

            }
            conn.Close();
            conn.Dispose();
            cmd.Dispose();

            return dp_t;
        }
    
        // GET: DeploymentTable/Edit/5
        [HttpGet]
        public ActionResult Edit(int c )
        {
            var st = GetRow(c);

            return View(st);
        }
    


        // POST: DeploymentTable/Edit/5
        [HttpPost]
        public ActionResult Edit(DeploymentTable model)
        {
            string oracleDbConnection = Globals.ConStr
                            + "User Id=" + Globals.Lg
                         + ";Password=" + Globals.Pwd
                            + ";";


            OracleConnection conn = new OracleConnection(oracleDbConnection);
            OracleCommand cmd = new OracleCommand();
            conn.Open();
            try
            {

                cmd.Connection = conn;
                cmd.CommandText = "UPDATE SYS.DEPLOYMENT_LOG SET "
                                   + "TASK_ID_LIST = '" + model.TASK_ID_LIST + "',"
                                   + "ISSUE = '" + model.ISSUE + "',"
                                   + "DATE_DEPLOY = to_date('" + model.DATE_DEPLOY + "','dd.mm.yyyy hh24:mi:ss'),"
                                   + "DEPLOYER = lower('" + model.DEPLOYER + "'),"
                                   + "TYPE_EXCEPTION = '" + model.TYPE_EXCEPTION + "',"
                                   + "COMMENT_DEPLOY = '" + model.COMMENT_DEPLOY + "',"
                                   + "DEVELOPER_MAIL_DATE = to_date('" + model.MAIL_DATE + "','dd.mm.yyyy hh24:mi:ss'),"
                                   + "DEVELOPER_MAIL_SUBJECT = '" + model.MAIL_SUBJECT + "',"
                                   + "DEVELOPER_MAIL_SENDER = '" + model.MAIL_SENDER + "',"
                                   + "DATA_REPORT = '" + model.DATA_REPORT + "',"
                                   + "GIT = '" + model.GIT + "',"
                                   + "IS_FIX = '" + model.IS_FIX + "' WHERE DEPLOYMENT_ID =  " + model.DEPLOYMENT_ID;


                cmd.ExecuteNonQuery();
                conn.Close();
                conn.Dispose();
                cmd.Dispose();


                return RedirectToAction("Views", "DeploymentTable");

            }
            catch (Exception ex)
            {
                if (ex.HResult == 1017) { return RedirectToAction("Login", "Account"); }
                else { return View(); }
            }
        }

        // GET: DeploymentTable/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeploymentTable/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
