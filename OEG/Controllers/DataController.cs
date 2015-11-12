using OEG.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace OEG.Controllers
{
    public class DataController : Controller
    {
        // GET: Data
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public String Run(string key)
        {
            if (key == "Sw4OjrY6QW")
            { 
            runOEGStaff();
            runSchoolStaff();
            runParticipants();
            runCleanup();
            
            return "Success";
            }
            else
            {
                return "Fail";
            }
        }

        static void runParticipants()
        {
            try
            {
                //https://survey.qualtrics.com/WRAPI/ControlPanel/api.php?Request=getLegacyResponseData&User=apiuser&Token=lksjdfJdJklajdf3asdae3&Format=XML&SurveyID=SV_123456789

                string[] QuestionIDs = new string[] { 
                        "EAC1","EAC2","EAC3","EAC4",
                        "ECN1","ECN2","ECN3","ECN4",
                        "ERR1","ERR2","ERR3","ERR4",
                        "EUN1","EUN2","EUN3","EUN4",
                        "OCA1","OCA2","OCA3","OCA4",
                        "OCR1","OCR2","OCR3","OCR4",
                        "OFL1","OFL2","OFL3","OFL4",
                        "OGP1","OGP2","OGP3","OGP4",
                        "OLD1","OLD2","OLD3","OLD4",
                        "ORR1","ORR2","ORR3","ORR4",
                        "SAC1","SAC2","SAC3","SAC4",
                        "SEM1","SEM2","SEM3","SEM4",
                        "SGS1","SGS2","SGS3","SGS4",
                        "SPW1","SPW2","SPW3","SPW4",
                        "SRR1","SRR2","SRR3","SRR4",
                        "SSP1","SSP2","SSP3","SSP4",
                        "PQA1","PQA2","PQA3"
                };

                oeg_reportsEntities db = new oeg_reportsEntities();

                IEnumerable<Surveys> sv = db.Surveys.Where(x => x.SurveyType == "Participant").ToList();
                int sCount = 0;
                foreach (Surveys s in sv)
                {
                    sCount++;
                    System.Diagnostics.Debug.WriteLine("Starting survey " + s.SurveyName + "(" + sCount + "/" + sv.Count() + ")");


                    string Url = "https://survey.qualtrics.com/WRAPI/ControlPanel/api.php?";
                    string function = "Request=getLegacyResponseData";
                    string user = "User=pammert@oeg.vic.edu.au";
                    string token = "Token=zlHrARdXDOn6ep1ZKo2Jb8vpBVUMb6odWPkbEPQL";
                    string list = "SurveyID=" + s.SurveyCode.Trim(); //st caths pre
                    string format = "Format=XML";
                    string Version = "Version=2.5";
                    string requestUrl = Url + function + "&" + user + "&" + token + "&" + format + "&" + list + "&" + Version;

                    HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                    try
                    {
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(response.GetResponseStream());

                    XmlNodeReader reader = new XmlNodeReader(xmlDoc);
                    DataSet ds = new DataSet();
                    ds.ReadXml(reader);
                    reader.Close();
                    int rCount = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        rCount++;
                        //if (rCount == 52) System.Diagnostics.Debug.WriteLine("Now"); 
                        //for each response we need to write a record for each question in the response.
                        //only deal with finished repsonses
                        if (dr["Finished"].ToString() == "1")
                        {
                            //does response exist in data
                            string rep = dr["ResponseID"].ToString();
                            ReportData r = db.ReportDatas.Where(x => x.ResponseID == rep).FirstOrDefault();
                            if (r == null)
                            {
                                System.Diagnostics.Debug.WriteLine("Creating Response Record");
                                foreach (string q in QuestionIDs)
                                {
                                    if (dr.Table.Columns.Contains(q)) db.ReportDatas.Add(CreateParicipantEntry(dr, q));
                                }
                                System.Diagnostics.Debug.WriteLine("Adding Response (" + rCount + "/" + ds.Tables[0].Rows.Count + ")");
                            }
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("Saving survey to DB");
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Finished survey " + s.SurveyName);
                                        }
                    catch (Exception ex)
                    {
                        //problem with this survey kick onto next
                        System.Diagnostics.Debug.WriteLine("PROBLEM!");
                    }

                }
                System.Diagnostics.Debug.WriteLine("Finished Upload");
                
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                
            }
        }

        static void runCleanup()
        {
            try
            {

                oeg_reportsEntities db = new oeg_reportsEntities();
                oeg_lookupsEntities db2 = new oeg_lookupsEntities();

                System.Diagnostics.Debug.WriteLine("Starting Remove unmatched");
                db.Database.ExecuteSqlCommand("RemoveIncompletes");
                System.Diagnostics.Debug.WriteLine("Finished Remove unmatched");

                System.Diagnostics.Debug.WriteLine("Starting EmployeeNumber Update");
                db.Database.ExecuteSqlCommand("UpdateEmployeeNumber");
                System.Diagnostics.Debug.WriteLine("Finished EmployeeNumber Update");


                ////update lookup table from Gaia
                db.Database.ExecuteSqlCommand("CleanoutLookups");

                var source = from f in db.ReportDatas
                             select f;


                var employees = (from f in source
                                 where f.EmployeeName != null
                                 select new { EmployeeNumber = f.EmployeeNumber }).Distinct();

                string emp = "";
                foreach (string s in employees.Select(o => o.EmployeeNumber))
                {
                    emp += s + ",";
                }

                emp = emp.Remove(emp.Length - 1);

                var lkEmployess = db2.GetEmployees(emp).ToList();

                foreach (GetEmployees_Result r in lkEmployess)
                {
                    tblHR_Entities hr = new tblHR_Entities();
                    hr.EntityID = r.EntityID;
                    hr.FullName = r.FullName;
                    db.tblHR_Entities.Add(hr);
                }

                db.SaveChanges();

                //get jobcodes for every user
                var users = (from f in db.Users
                             where f.EmployeeNumber != null
                             select new { EmployeeNumber = f.EmployeeNumber }).Distinct();

                string u = "";
                foreach (string s in users.Select(o => o.EmployeeNumber))
                {
                    int n;
                    bool isNumeric = int.TryParse(s, out n);
                    if (isNumeric)
                    {
                        u += s + ",";
                    }
                }

                u = u.Remove(u.Length - 1);

                var lkUsers = db2.GetRosteredJobcodesCSVByEmployeeNumbers(u,null,null).ToList();

                foreach(GetRosteredJobcodesCSVByEmployeeNumbers_Result r in lkUsers)
                {
                    User us = db.Users.Where(x => x.EmployeeNumber == r.EmployeeID.ToString()).FirstOrDefault();
                    if (us != null && r.Jobcodes_CSV != null)
                    {
                        us.JobCodes = r.Jobcodes_CSV.Replace(" ", "");
                        db.Entry(us).State = EntityState.Modified;
                    }
                }

                db.SaveChanges();

                var jobcodes = (from f in source
                                select new { JobCode = f.JobCode }).Distinct();

                string jc = "";
                foreach (string s in jobcodes.Select(o => o.JobCode))
                {
                    jc += s + ",";
                }

                jc = jc.Remove(jc.Length - 1);


                var lkjobcodes = db2.GetPrograms(jc).ToList();

                foreach (GetPrograms_Result r in lkjobcodes)
                {
                    tblProgram j = new tblProgram();
                    j.Duration = r.Duration;
                    j.JobCode = r.JobCode;
                    j.JobFrom = r.JobFrom;
                    j.SchoolCode = r.Client;
                    j.Venue = r.Venue;
                    j.Year = r.Year.ToString();
                    j.YearLvl = r.YearLvl;

                    db.tblPrograms.Add(j);
                }

                db.SaveChanges();

                System.Diagnostics.Debug.WriteLine("Starting ATLAS Data Update");
                db.Database.ExecuteSqlCommand("UpdateATLASData");
                System.Diagnostics.Debug.WriteLine("Finished ATLAS Data Update");

                System.Diagnostics.Debug.WriteLine("Starting EmployeeName Update");
                db.Database.ExecuteSqlCommand("UpdateEmployeeName");
                System.Diagnostics.Debug.WriteLine("Finished EmployeeName Update");

                System.Diagnostics.Debug.WriteLine("Starting Question Text Update");
                db.Database.ExecuteSqlCommand("UpdateQuestionText");
                System.Diagnostics.Debug.WriteLine("Finished Question Text Update");

                System.Diagnostics.Debug.WriteLine("Starting Factor Text Update");
                db.Database.ExecuteSqlCommand("UpdateFactorText");
                System.Diagnostics.Debug.WriteLine("Finished Factor Text Update");

                System.Diagnostics.Debug.WriteLine("Complete!");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);

            }

        }

        static void runOEGStaff()
        {
            try
            {
                //https://survey.qualtrics.com/WRAPI/ControlPanel/api.php?Request=getLegacyResponseData&User=apiuser&Token=lksjdfJdJklajdf3asdae3&Format=XML&SurveyID=SV_123456789

                string[] QuestionIDs = new string[] { 
                        "OEGS1R","OEGS1Days","OEGS2EN","OEGS2EN_TEXT",
                        "OEGS2R","OEGS2Days","OEGS3EN","OEGS3EN_TEXT",
                        "OEGS3R","OEGS3Days","GLQ4","GLQ5","GLQ6",
                        "GLQ7","GLQ7_TEXT","GLQ8","GLQ9","GLQ9_TEXT",
                        "GLQ10","GLQ11","GLQ11_TEXT","GLQ12","GLQ13",
                        "GLQ13_TEXT","GLQ14","GLQ15","GLQ16","GLQ17",
                        "GLQ18","GLQ19","GLQ20","GLQ21","GLQ22","GLQ23",
                        "GLQ24","GLQ25","GLQ26"
                };

                oeg_reportsEntities db = new oeg_reportsEntities();

                IEnumerable<Surveys> sv = db.Surveys.Where(x => x.SurveyType == "OEG Staff").ToList();
                int sCount = 0;
                foreach (Surveys s in sv)
                {
                    sCount++;
                    System.Diagnostics.Debug.WriteLine("Starting OEG Staff survey " + s.SurveyName + "(" + sCount + "/" + sv.Count() + ")");


                    string Url = "https://survey.qualtrics.com/WRAPI/ControlPanel/api.php?";
                    string function = "Request=getLegacyResponseData";
                    string user = "User=pammert@oeg.vic.edu.au";
                    string token = "Token=zlHrARdXDOn6ep1ZKo2Jb8vpBVUMb6odWPkbEPQL";
                    string list = "SurveyID=" + s.SurveyCode.Trim(); //st caths pre
                    string format = "Format=XML";
                    string Version = "Version=2.5";
                    string requestUrl = Url + function + "&" + user + "&" + token + "&" + format + "&" + list + "&" + Version;

                    HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                    try 
                    { 
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(response.GetResponseStream());

                    XmlNodeReader reader = new XmlNodeReader(xmlDoc);
                    DataSet ds = new DataSet();
                    ds.ReadXml(reader);
                    reader.Close();
                    int rCount = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        rCount++;
                        //if (rCount == 52) System.Diagnostics.Debug.WriteLine("Now"); 
                        //for each response we need to write a record for each question in the response.
                        //only deal with finished repsonses
                        if (dr["Finished"].ToString() == "1")
                        {
                            //does response exist in data
                            string rep = dr["ResponseID"].ToString();
                            StaffReportData r = db.StaffReportData.Where(x => x.ResponseID == rep).FirstOrDefault();
                            if (r == null)
                            {
                                System.Diagnostics.Debug.WriteLine("Creating Response Record");
                                foreach (string q in QuestionIDs)
                                {
                                    if (dr.Table.Columns.Contains(q)) db.StaffReportData.Add(CreateOEGStaffEntry(dr, q));
                                }
                                System.Diagnostics.Debug.WriteLine("Adding Response (" + rCount + "/" + ds.Tables[0].Rows.Count + ")");
                            }
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("Saving survey to DB");
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Finished survey " + s.SurveyName);
                    }
                    catch (Exception ex)
                    {
                        //problem with this survey kick onto next
                        System.Diagnostics.Debug.WriteLine("PROBLEM!");
                    }

                    }
                System.Diagnostics.Debug.WriteLine("Finished Upload");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                
            }
        }

        static void runSchoolStaff()
        {
            try
            {
                //https://survey.qualtrics.com/WRAPI/ControlPanel/api.php?Request=getLegacyResponseData&User=apiuser&Token=lksjdfJdJklajdf3asdae3&Format=XML&SurveyID=SV_123456789

                string[] QuestionIDs = new string[] { 
                        "SSQ3","SSQ4","SSQ5",
                        "SSQ6","SSQ7","SSQ8","SSQ9","SSQ10","SSQ11","SSQ12"
                };

                oeg_reportsEntities db = new oeg_reportsEntities();

                IEnumerable<Surveys> sv = db.Surveys.Where(x => x.SurveyType == "School Staff").ToList();
                int sCount = 0;
                foreach (Surveys s in sv)
                {
                    sCount++;
                    System.Diagnostics.Debug.WriteLine("Starting School Staff survey " + s.SurveyName + "(" + sCount + "/" + sv.Count() + ")");


                    string Url = "https://survey.qualtrics.com/WRAPI/ControlPanel/api.php?";
                    string function = "Request=getLegacyResponseData";
                    string user = "User=pammert@oeg.vic.edu.au";
                    string token = "Token=zlHrARdXDOn6ep1ZKo2Jb8vpBVUMb6odWPkbEPQL";
                    string list = "SurveyID=" + s.SurveyCode.Trim(); //st caths pre
                    string format = "Format=XML";
                    string Version = "Version=2.5";
                    string requestUrl = Url + function + "&" + user + "&" + token + "&" + format + "&" + list + "&" + Version;

                    HttpWebRequest request = WebRequest.Create(requestUrl) as HttpWebRequest;
                    try 
                    { 

                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(response.GetResponseStream());

                    XmlNodeReader reader = new XmlNodeReader(xmlDoc);
                    DataSet ds = new DataSet();
                    ds.ReadXml(reader);
                    reader.Close();
                    int rCount = 0;
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        rCount++;
                        //if (rCount == 52) System.Diagnostics.Debug.WriteLine("Now"); 
                        //for each response we need to write a record for each question in the response.
                        //only deal with finished repsonses
                        if (dr["Finished"].ToString() == "1")
                        {
                            //does response exist in data
                            string rep = dr["ResponseID"].ToString();
                            ReportData r = db.ReportDatas.Where(x => x.ResponseID == rep).FirstOrDefault();
                            if (r == null)
                            {
                                System.Diagnostics.Debug.WriteLine("Creating Response Record");
                                foreach (string q in QuestionIDs)
                                {
                                    if (dr.Table.Columns.Contains(q)) db.StaffReportData.Add(CreateSchoolStaffEntry(dr, q));
                                }
                                System.Diagnostics.Debug.WriteLine("Adding Response (" + rCount + "/" + ds.Tables[0].Rows.Count + ")");
                            }
                        }
                    }
                    System.Diagnostics.Debug.WriteLine("Saving survey to DB");
                    db.SaveChanges();
                    System.Diagnostics.Debug.WriteLine("Finished survey " + s.SurveyName);
                    }
                    catch (Exception ex)
                    {
                        //problem with this survey kick onto next
                        System.Diagnostics.Debug.WriteLine("PROBLEM!");
                    }

                }
                System.Diagnostics.Debug.WriteLine("Finished Upload");


            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                
            }
        }

        static ReportData CreateParicipantEntry(DataRow dr, string QuestionID)
        {
            ReportData ret = new ReportData();

            ret.ResponseID = dr["ResponseID"].ToString();
            ret.JobCode = dr["JC"].ToString();
            ret.Group = dr["Q3"].ToString();
            ret.ID = dr["Q2"].ToString();
            int Q4;
            bool res = int.TryParse(dr["Q4"].ToString(), out Q4);
            ret.PrePost = res ? Q4 : 1;
            ret.QuestionID = QuestionID;
            ret.Factor = QuestionID.Substring(0, 3);
            if (ret.Factor != "PQA")
            {
                int Score;
                bool res1 = int.TryParse(dr[QuestionID].ToString(), out Score);
                ret.Score = res1 ? Score : 1;
            }
            else
            {
                ret.Score = 1;
                ret.QualResponse = dr[QuestionID].ToString();
                switch (QuestionID)
                {
                    case "PQA1":
                        ret.PQA1 = dr[QuestionID].ToString();
                        break;
                    case "PQA2":
                        ret.PQA2 = dr[QuestionID].ToString();
                        break;
                    case "PQA3":
                        ret.PQA3 = dr[QuestionID].ToString();
                        break;
                }
            }
            return ret;
        }

        static StaffReportData CreateOEGStaffEntry(DataRow dr, string QuestionID)
        {
            StaffReportData ret = new StaffReportData();

            ret.ResponseID = dr["ResponseID"].ToString();
            ret.JobCode = dr["JC"].ToString();
            ret.Group = dr["OEGSGN"].ToString();
            ret.EmployeeNumber = dr["OEGS1EN"].ToString();
            ret.QuestionID = QuestionID;
            if (dr[QuestionID].ToString().Length > 0)
            {
                int Score;
                bool res1 = int.TryParse(dr[QuestionID].ToString(), out Score);
                if (res1)
                {
                    ret.Score = Score;
                }
                else
                {
                    ret.QualResponse = dr[QuestionID].ToString();
                }
            }
            return ret;
        }

        static StaffReportData CreateSchoolStaffEntry(DataRow dr, string QuestionID)
        {
            StaffReportData ret = new StaffReportData();

            ret.ResponseID = dr["ResponseID"].ToString();
            ret.JobCode = dr["JC"].ToString();
            ret.Group = dr["SSQ2"].ToString();
            ret.QuestionID = QuestionID;
            if (dr[QuestionID].ToString().Length > 0)
            {
                int Score;
                bool res1 = int.TryParse(dr[QuestionID].ToString(), out Score);
                if (res1)
                {
                    ret.Score = Score;
                }
                else
                {
                    ret.QualResponse = dr[QuestionID].ToString();
                }
            }
            return ret;
        }

    
    }
}