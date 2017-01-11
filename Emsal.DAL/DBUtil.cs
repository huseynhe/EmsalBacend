using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Emsal.DAL
{
    public class DBUtil
    {
        public DBUtil()
        {
            SqlConnection conn = new SqlConnection(
                DBUtil.ConnectionString);
        }

        public static String ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["EmsalDBConnectionString"].ConnectionString; }
        }

    }
}
