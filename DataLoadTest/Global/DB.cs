using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DataLoadTest.Global
{
    public class DB
    {
        // 1. DB 연결 정보
        string connStr = "user id=DEV_ORA_TEST;" +
            "password=DEV_ORA_TEST;" +
            "data source=(DESCRIPTION=(ADDRESS=" +
            "(PROTOCOL=tcp)(HOST=192.168.0.110)" +
            "(PORT=1521))(CONNECT_DATA=" +
            "(SID=orcl)))";

        private static int columnCount = 15;
        private static int loadCount = 5;

        public static string[] column1 = new string[columnCount];
        public static string[] column2 = new string[columnCount];
        public static string[] column3 = new string[columnCount];
        public static string[] column4 = new string[columnCount];
        public static string[] column5 = new string[columnCount];
        public static string[] column6 = new string[columnCount];
        public static string[] column7 = new string[columnCount];
        public static string[] column8 = new string[columnCount];
        public static string[] column9 = new string[columnCount];
        public static string[] column10 = new string[columnCount];
        public static string[] column11 = new string[columnCount];
        public static string[] column12 = new string[columnCount];
        public static string[] column13 = new string[columnCount];
        public static string[] column14 = new string[columnCount];
        public static string[] column15 = new string[columnCount];

        public void OracleConnection()
        {
            OracleConnection conn = new OracleConnection(connStr);

            try
            {
                conn.Open();
                Console.WriteLine("Oracle DB Connection Successful!");
            }
            catch (OracleException ex)
            {
                Console.WriteLine("--- DB ERROR!!! ---");
                Console.WriteLine(ex.ToString());
            }

            // 2. DB 명령어 실행
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;

            cmd.CommandText = "SELECT * FROM MILLION ORDER BY column1 ASC";
            //cmd.ExecuteReader();

            OracleDataReader reader = cmd.ExecuteReader();

            for (int i = 0; reader.Read(); i++)
            {
                if (i >= loadCount) break;
                column1[i] = reader.GetString(0);
                Console.WriteLine(column1[i]);
            }



            Console.WriteLine("셀렉트 햇어용~");
            // 3. DB 종료
            reader.Close();
            conn.Close();
            conn.Dispose();
        }

        

        
    }
}
