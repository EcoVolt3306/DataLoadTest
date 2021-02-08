using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OracleClient;

namespace DataLoadTest.Global
{
    public class DB
    {
        // 1. DB 연결  
        string connStr = "user id=DEV_ORA_TEST;password=DEV_ORA_TEST;" +
        "data source=(DESCRIPTION=(ADDRESS=" +
        "(PROTOCOL=tcp)(HOST=192.168.0.110)" +
        "(PORT=1521))(CONNECT_DATA=" +
        "(SID=orcl)))";
        


        
    }
}
