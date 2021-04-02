using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global
{
    public class gVar
    {
        //public static string column1 { get; set; } = "C1";
        //public static string column2 { get; set; } = "C2";
        //public static string column3 { get; set; } = "C3";
        //public static string column4 { get; set; } = "C4";
        //public static string column5 { get; set; } = "C5";
        //public static string column6 { get; set; } = "C6";
        //public static string column7 { get; set; } = "C7";
        //public static string column8 { get; set; } = "C8";
        //public static string column9 { get; set; } = "C9";
        //public static string column10 { get; set; } = "C10";
        //public static string column11 { get; set; } = "C11";
        //public static string column12 { get; set; } = "C12";
        //public static string column13 { get; set; } = "C13";
        //public static string column14 { get; set; } = "C14";
        //public static string column15 { get; set; } = "C15";

        // 2-dimensional array
        public static string[,] col = new string[15, 5];    
       







        public static AppThemeData AppThemeInfo = new AppThemeData();

        public static int SessionOutMin { get; set; } = 30;

        public static bool DBState { get; set; } = false;
        public static glClass.DBConn.DBMS DBType { get; set; } = glClass.DBConn.DBMS.Oracle;
        public static glClass.DBConn.DBInfo DBInfo { get; set; } = new glClass.DBConn.DBInfo();

        public static ErrorData ErrorInfo { get; set; } = new ErrorData();

        public static Dictionary<string, LoginUserData> LoginUserInfo { get; set; } = new Dictionary<string, LoginUserData>();
    }
}
