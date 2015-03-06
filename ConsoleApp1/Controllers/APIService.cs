using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class APIService
    {
        private AppDBContext m_oDB;
        private string m_strError;

         public APIService()
         {
              m_oDB = new AppDBContext();
              m_strError = "";
         }

         public string GetLastError()
         {
             return (m_strError);
         }

         //-------------------------------------------------------------
         public int AddClass(string strID, string strName, string strEmail, string strTeacher)
         {
             AppClass oClass = m_oDB.AppClasses.Find(strID);
             if (oClass == null)
             {
                 var oClassNew = new AppClass { ID = strID, Name = strName, Email = strEmail, Teacher = strTeacher };
                 m_oDB.AppClasses.Add(oClassNew);
                 try
                 {
                     m_oDB.SaveChanges();
                     GenLog.logger.Info("Add Class : " + strID);
                     m_strError = "";
                     return (1);
                 }
                 catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                 {
                     string error = "";
                     foreach (var item in ex.EntityValidationErrors)
                     {
                         foreach (var item2 in item.ValidationErrors)
                             error += item2.ErrorMessage + ",";         // item2.PropertyName 
                     }
                     if (error != "") error = error.Substring(0, error.Length - 1);
                     GenLog.logger.Error("AddClass Fail : " + error);
                     m_strError = error;
                     return (-2);
                 }
                 catch (Exception exc)
                 {
                     GenLog.logger.Error("AddClass Fail : " + exc.Message);
                     m_strError = exc.Message;
                     return (-1);
                 }
             }
             else  // exists
             {
                 GenLog.logger.Error("AddClass Fail : '" + strID + "'  Exist");
                 m_strError = "The class id ( " + strID + " ) already exists.";
                 return (0);
             }

         }

         //-------------------------------------------------------------
         public AppClass GetClassInfo(string strID)
         {
             AppClass oClass = m_oDB.AppClasses.Find(strID);
             m_strError = "";
             return (oClass);
         }

         //-------------------------------------------------------------
         public List<AppClass> FindClassInfo(string strID, string strName, string strTeacher)
         {
             var qClass = from s in m_oDB.AppClasses
                                 select s;
             if (strID != null && strID != "")
                 qClass = qClass.Where(s => s.ID.Contains(strID));
             if (strName != null && strName != "")
                 qClass = qClass.Where(s => s.Name.Contains(strName));
             if (strTeacher != null && strTeacher != "")
                 qClass = qClass.Where(s => s.Teacher.Contains(strTeacher));

             //qClass.FirstOrDefault();
             m_strError = "";
             return (qClass.ToList<AppClass>());
         }

        //-------------------------------------------------------------
         public int UpdateClassInfo(string strID, string strTeacher, int nStuCount, bool bIncrease)
         {
             AppClass oClass = m_oDB.AppClasses.Find(strID);
             if (oClass == null)
             {
                 GenLog.logger.Error("UpdateClsssInfo Fail :  The class id ( " + strID + " ) does not exist.");
                 m_strError = " The class id ( " + strID + " ) does not exist.";
                 return (-1);
             }
             if (strTeacher != null && strTeacher != "")
                 oClass.Teacher = strTeacher;
             if (nStuCount >= 0)
             {
                 if (bIncrease)
                     oClass.Stu_Count += nStuCount;
                 else
                     oClass.Stu_Count = nStuCount;
             }
             oClass.LastupdateTime = DateTime.Now;

             m_oDB.Entry(oClass).State = System.Data.Entity.EntityState.Modified;

             try
             {
                 m_oDB.SaveChanges();
                 GenLog.logger.Info("UpdateClassInfo : " + strID);
                 m_strError = "";
                 return (1);
             }
             catch (System.Data.Entity.Validation.DbEntityValidationException ex)
             {
                 string error = "";
                 foreach (var item in ex.EntityValidationErrors)
                 {
                     foreach (var item2 in item.ValidationErrors)
                         error += item2.ErrorMessage + ",";         // item2.PropertyName 
                 }
                 if (error != "") error = error.Substring(0, error.Length - 1);
                 GenLog.logger.Error("UpdateClassInfo Fail : " + error);
                 m_strError = error;
                 return (-2);
             }
             catch (Exception exc)
             {
                 GenLog.logger.Error("UpdateClassInfo : Fail : " + exc.Message);
                 return (-1);
             }
         }

        //-------------------------------------------------------------
         public int DeleteClass(string strID)
         {
             AppClass oClass = m_oDB.AppClasses.Find(strID);
             if (oClass == null)
             {
                 GenLog.logger.Error("DeleteClass Fail :  The class id ( " + strID + " ) does not exist.");
                 m_strError = " The class id ( " + strID + " ) does not exist.";
                 return (-1);
             }
             m_oDB.AppClasses.Remove(oClass);
             try
             {
                 m_oDB.SaveChanges();
                 GenLog.logger.Info("Delete Class : " + strID);
                 m_strError = "";
                 return (1);
             }
             catch (Exception exc)
             {
                 GenLog.logger.Error("DeleteClass  Fail : " + exc.Message);
                 m_strError = "DeleteClass Fail : " + exc.Message;
                 return (-1);
             }
         }

        //-------------------------------------------------------------
         public int AddStudent(string strClassID, string strID, string strName, string strPassword, string strEmail, string strGender)
         {
             AppStudent oStudent = m_oDB.AppStudent.Find(strClassID, strID);
             if (oStudent == null)
             {
                 var oStudentNew = new AppStudent { ClassID = strClassID, ID = strID, Name = strName, Password = strPassword, Email = strEmail, Gender = strGender };
                 m_oDB.AppStudent.Add(oStudentNew);
                 try
                 {
                     m_oDB.SaveChanges();
                     GenLog.logger.Info("Add Student : " + strID);
                     UpdateClassInfo(strClassID, null, 1, true);
                     m_strError = "";
                     return (oStudentNew.Sno);
                 }
                 catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                 {
                     string error = "";
                     foreach (var item in ex.EntityValidationErrors)
                     {
                         foreach (var item2 in item.ValidationErrors)
                             error += item2.ErrorMessage + ",";         // item2.PropertyName 
                     }
                     if (error != "") error = error.Substring(0, error.Length - 1);
                     GenLog.logger.Error("AddStudent Fail : " + error);
                     m_strError = error;
                     return (-2);
                 }
                 catch (Exception exc)
                 {
                     GenLog.logger.Error("AddStudent : Fail : " + exc.Message);
                     m_strError = exc.Message;
                     return (-1);
                 }
             }
             else  // exists
             {
                 GenLog.logger.Error("AddStudent Fail : The class/student id ( " + strClassID + " / " + strID + " ) already exists.");
                 m_strError = "The class/student  ( " + strClassID + " / " + strID + " ) already exists.";
                 return (0);
             }
         }

         //-------------------------------------------------------------
         public AppStudent GetStudentInfo(string strClassID, string strID)
         {
             AppStudent oStudent = m_oDB.AppStudent.Find(strClassID, strID);
             m_strError = "";
             return (oStudent);
         }

         //-------------------------------------------------------------
         public List<AppStudent> FindStudentInfo(int nSno, string strClassID, string strID, string strName, string strEmail, string strGender)
         {
             var qStudent = from s in m_oDB.AppStudent
                          select s;
             if (nSno > 0)
                 qStudent = qStudent.Where(s => s.Sno.Equals(nSno));
             if (strClassID != null && strClassID != "")
                 qStudent = qStudent.Where(s => s.ClassID.Equals(strClassID));
             if (strID != null && strID != "")
                 qStudent = qStudent.Where(s => s.ID.Equals(strID));
             if (strName != null && strName != "")
                 qStudent = qStudent.Where(s => s.Name.Equals(strName));
             if (strEmail != null && strEmail != "")
                 qStudent = qStudent.Where(s => s.Email.Equals(strEmail));
             if (strGender != null && strGender != "")
                 qStudent = qStudent.Where(s => s.Gender.Equals(strGender));

             m_strError = "";
             return (qStudent.ToList<AppStudent>());
         }

         //-------------------------------------------------------------
         public int UpdateStudentInfo(string strClassID, string strID, string strName, string strPassword, string strEmail, string strGender)
         {
             AppStudent oStudent = m_oDB.AppStudent.Find(strClassID, strID);
             if (oStudent == null)
             {
                 GenLog.logger.Error("UpdateStudentInfo Fail  :  The class/student id ( " + strClassID + " / " + strID + " ) does not exist.");
                 m_strError = "The class/student id ( " + strClassID + " / " + strID + " ) does not exist.";
                 return (-1);
             }
             return (xUpdateStudentInfo(oStudent, strName, strPassword, strEmail, strGender));
         }

         //-------------------------------------------------------------
         public int UpdateStudentInfo(int nSno, string strName, string strPassword, string strEmail, string strGender)
         {
             var qStudent = from s in m_oDB.AppStudent
                            select s;
             if (nSno > 0)
                 qStudent = qStudent.Where(s => s.Sno.Equals(nSno));

             AppStudent oStudent = qStudent.FirstOrDefault();
             if (oStudent == null)
             {
                 GenLog.logger.Error("UpdateStudentInfo Fail  :  The sno ( " + nSno.ToString() + " ) does not exist.");
                 m_strError = "The Sno ( " + nSno.ToString() + " ) does not exist.";
                 return (-1);
             }

             return (xUpdateStudentInfo(oStudent, strName, strPassword, strEmail, strGender));
         }


         //-------------------------------------------------------------
         private int xUpdateStudentInfo(AppStudent oStudent, string strName, string strPassword, string strEmail, string strGender)
         {
             if (strName != null && strName != "")
                 oStudent.Name = strName;
             if (strPassword != null && strPassword != "")
                 oStudent.Password = strPassword;
             if (strEmail != null && strEmail != "")
                 oStudent.Email = strEmail;
             if (strGender != null && strGender != "")
                 oStudent.Gender = strGender;
             oStudent.LastupdateTime = DateTime.Now;

             m_oDB.Entry(oStudent).State = System.Data.Entity.EntityState.Modified;

             try
             {
                 m_oDB.SaveChanges();
                 GenLog.logger.Info("UpdateStudentInfo : '" + oStudent.ClassID + " / " + oStudent.ID + "'");
                 m_strError = "";
                 return (1);
             }
             catch (System.Data.Entity.Validation.DbEntityValidationException ex)
             {
                 string error = "";
                 foreach (var item in ex.EntityValidationErrors)
                 {
                     foreach (var item2 in item.ValidationErrors)
                         error += item2.ErrorMessage + ",";         // item2.PropertyName 
                 }
                 if (error != "") error = error.Substring(0, error.Length - 1);
                 GenLog.logger.Error("UpdateStudentInfo Fail : " + error);
                 m_strError = error;
                 return (-2);
             }
             catch (Exception exc)
             {
                 GenLog.logger.Error("UpdateStudentIfno : Fail : " + exc.Message);
                 m_strError = exc.Message;
                 return (-1);
             }
         }

         //-------------------------------------------------------------
         public int DeleteStudent(string strClassID, string strID)
         {
             AppStudent oStudent = m_oDB.AppStudent.Find(strClassID, strID);
             if (oStudent == null)
             {
                 GenLog.logger.Error("DeleteStudent Fail  :  The class/student id ( " + strClassID + " / " + strID + " ) does not exist.");
                 m_strError = "The class/student id ( " + strClassID + " / " + strID + " ) does not exist.";
                 return (-1);
             }
             return (xDeleteStudent(oStudent));
         }

         //-------------------------------------------------------------
         public int DeleteStudent(int nSno)
         {
             var qStudent = from s in m_oDB.AppStudent
                            select s;
             if (nSno > 0)
                 qStudent = qStudent.Where(s => s.Sno.Equals(nSno));

             AppStudent oStudent = qStudent.FirstOrDefault();
             if (oStudent == null)
             {
                 GenLog.logger.Error("DeleteStudent Fail  :  The sno ( " + nSno.ToString() + " ) does not exist.");
                 m_strError = "The Sno ( " + nSno.ToString() + " ) does not exist.";
                 return (-1);
             }
             return (xDeleteStudent(oStudent));
         }

         //-------------------------------------------------------------
         private int xDeleteStudent(AppStudent oStudent)
        {
             m_oDB.AppStudent.Remove(oStudent);
             try
             {
                 m_oDB.SaveChanges();
                 GenLog.logger.Info("DeleteStudent : '" + oStudent.ClassID + " / " + oStudent.ID);
                 UpdateClassInfo(oStudent.ClassID, null, -1, true);
                 m_strError = "";
                 return (1);
             }
             catch (Exception exc)
             {
                 GenLog.logger.Error("Delete Student : Fail : " + exc.Message);
                 m_strError = exc.Message;
                 return (-1);
             }
        }
    }
}

