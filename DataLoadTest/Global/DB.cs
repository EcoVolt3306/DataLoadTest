using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DataLoadTest.Global
{
    public class DB
    {
        // 1. DB 연결  
        string connStr { get; set; } = "user id=DEV_ORA_TEST;password=DEV_ORA_TEST;" +
        "data source=(DESCRIPTION=(ADDRESS=" +
        "(PROTOCOL=tcp)(HOST=192.168.0.110)" +
        "(PORT=1521))(CONNECT_DATA=" +
        "(SID=orcl)))";

        //OracleConnection conn = new OracleConnection(connStr);



        public void AAA()
        {
            
            OracleConnection conna = new OracleConnection(connStr);
            conna.Open();
            System.Console.WriteLine("TTTTEEEEESSSSSTTTT");
            
            
        }

        
    }
}
