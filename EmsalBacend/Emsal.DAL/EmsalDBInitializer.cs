
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;


namespace Emsal.DAL
{
    public class EmsalDBInitializer : DropCreateDatabaseIfModelChanges<EmsalDBContext>
    {
        public class Configuration : DbMigrationsConfiguration<EmsalDBContext>
        {
            public Configuration()
            {
                AutomaticMigrationsEnabled = true;
            }
        }

        protected override void Seed(EmsalDBContext context)
        {
            //IList<tblMehsulElan> defaultMehsulElans = new List<tblMehsulElan>();

            //defaultMehsulElans.Add(new tblMehsulElan() { Name = "Elan 1", Description = "First Elan" });
            //defaultMehsulElans.Add(new tblMehsulElan() { Name = "Elan 2", Description = "Second Elan" });

            //foreach (tblMehsulElan std in defaultMehsulElans)
            //    context.tblMehsulElans.Add(std);

            base.Seed(context);
        }


    }
}
