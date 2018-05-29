using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Waste_Tracker
{
    public class ConnectionHelper
    {
        public static SqlConnection GetConn()
        {
            string connectionStr = "Data Source=compassbiazure.database.windows.net; Initial Catalog=FieldSiteDB; Persist Security Info=True; User ID=FieldApps;Password=K%Th8#30!";
            SqlConnection conn = new SqlConnection(connectionStr);
            return conn;
        }
    }
}
