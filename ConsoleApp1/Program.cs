using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            APIService oAPI = new APIService();
            //xDBInitialData(oAPI);

            //oAPI.AddClass("101", "一年一班", "101_st@edu.com", "李萬隆");
            //oAPI.AddClass("102", "一年二班", "102_st@edu.com", "趙公館");
            //AppClass oClassInfo = oAPI.GetClassInfo("102");
            //List<AppClass> oClassInfoList = oAPI.FindClassInfo(null, "四年", "陳");
            //int nResult = oAPI.UpdateClassInfo("101", null, 10);
            //int nResult = oAPI.DeleteClass("101");
            /*
            var db = new AppDBContext();
            */
            // add student 
            /*
            oAPI.AddStudent("101", "1", "林忠孝", "uiomklul", "10101_st@edu.com", "M");
            oAPI.AddStudent("101", "2", "陳仁愛", "uiomklul", "10102_st@edu.com", "F");
            oAPI.AddStudent("102", "1", "趙信義", "uiomklul", "10201_st@edu.com", "M");
            oAPI.AddStudent("101", "3", "李和平", "uiomklul", "10103_st@edu.com", "F");
             */
            // get student info
            AppStudent oStudent = oAPI.GetStudentInfo("101", "2");
            oStudent = oAPI.GetStudentInfo("101", "5");

            // find student info 
            List<AppStudent> oStudentAry = oAPI.FindStudentInfo(0, "101", null, null, null, null);
            oStudentAry = oAPI.FindStudentInfo(0, null, null, null, null, "M");

            // update stdent info 
            //int nResult = oAPI.UpdateStudentInfo(
        }

        //-------------------------------------------------------------
        static private int xDBInitialData(APIService oAPI)
        {
            int nResult = oAPI.AddClass("401", "四年一班", "401_st@edu.com", "陳坪林");
            if (nResult > 0)
                nResult = oAPI.AddClass("402", "四年二班", "402_st@edu.com", "林景美");
            return (nResult);
            //var comp1 = new AppCompany { ID = "MIGO", Name = "功典", Email = "piggy_liu@migosoft.com" };
            //var comp2 = new AppCompany { ID = "KKK", Name = "第二間公司", Email = "kency_huang@migosoft.com" };
            //var compList = new List<AppCompany> { comp1, comp2 };
            /*
            var class1 = new AppClass { ID = "401", Name = "四年一班", Email = "401_st@edu.com", Teacher = "陳坪林" };
            var class2 = new AppClass { ID = "402", Name = "四年二班", Email = "402_st@edu.com", Teacher = "林景美" };
            var classList = new List<AppClass> { class1, class2 };

            string error = "";
            try
            {
                //compList.ForEach(c => db.AppCompanies.Add(c));
                classList.ForEach(c => oDb.AppClasses.Add(c));
                oDb.SaveChanges();
                return (1);
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return (-1);
            }
             */

        }
        /*
        //-------------------------------------------------------------
        static public int AddClass(AppDBContext db, string strID, string strName, string strEmail, string strTeacher)
        {
            AppClass oClass = db.AppClasses.Find(strID);
            if (oClass == null)
            {
                var class1 = new AppClass { ID = strID, Name = strName, Email = strEmail, Teacher = strTeacher };
                db.AppClasses.Add(class1);
                try
                {
                    db.SaveChanges();
                    return (1);
                }
                catch (Exception exc)
                {
                    return (-1);
                }
            }
            else  // exists
                return (0);

        }
        */
    }
}
