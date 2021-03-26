using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace Global
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

                column1[i] = reader.GetString(0);
                column2[i] = reader.GetString(1);
                column3[i] = reader.GetString(2);
                column4[i] = reader.GetString(3);
                column5[i] = reader.GetString(4);
                column6[i] = reader.GetString(5);
                column7[i] = reader.GetString(6);
                column8[i] = reader.GetString(7);
                column9[i] = reader.GetString(8);
                column10[i] = reader.GetString(9);
                column11[i] = reader.GetString(10);
                column12[i] = reader.GetString(11);
                column13[i] = reader.GetString(12);
                column14[i] = reader.GetString(13);
                column15[i] = reader.GetString(14);
                Console.WriteLine(column1[i] + " " + column2[i] + column3[i] + " " + column4[i] + " " +
                    column5[i] + " " + column6[i] + " " + column7[i] + " " + column8[i] + " " +
                    column9[i] + " " + column10[i] + " " + column11[i] + " " + column12[i] + " " +
                    column13[i] + " " + column14[i] + " " + column15[i] + " ");
            }



            Console.WriteLine("셀렉트 햇어용~");
            // 3. DB 종료
            reader.Close();
            conn.Close();
            conn.Dispose();
        }

        

        
    }
}
