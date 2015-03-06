
namespace ConsoleApp1
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class DataInitializer : DropCreateDatabaseIfModelChanges<AppDBContext>
    {
        //  CreateDatabaseIfNotExists
        // DropCreateDatabaseIfModelChanges
        // DropCreateDatabaseAlways

        public DataInitializer()
            : base()
        {
            GenLog.logger.Info("DataInitializer");
        }

        protected override void Seed(AppDBContext db)
        {
            //this.log.Info("Initialize database...");

            var class1 = new AppClass { ID = "401", Name = "四年一班", Email = "401_st@edu.com" , Teacher = "陳坪林"};
            var class2 = new AppClass { ID = "402", Name = "四年二班", Email = "402_st@edu.com" , Teacher = "林景美"};
            var classList = new List<AppClass> { class1, class2 };

            string error = "";
            try
            {
                classList.ForEach(c => db.AppClasses.Add(c));
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                error = ex.Message;
            }
        }
    }
}
