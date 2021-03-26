using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global
{
    public class gVar
    {
        public static string Provider { get; set; } = "DTK";

        public static string CustName { get; set; } = "Cust Name";
        public static string CustFullName { get; set; } = "Cust Full Name";
        public static string AppName { get; set; } = "Title";
        public static string AppFullName { get; set; } = "Full Title";

        public static string AppTitle { get; set; } = "App Title";

        public static string WorkFlagHour { get; set; } = "8";

        public static AppThemeData AppThemeInfo = new AppThemeData();

        public static int SessionOutMin { get; set; } = 30;

        public static bool DBState { get; set; } = false;
        public static glClass.DBConn.DBMS DBType { get; set; } = glClass.DBConn.DBMS.Oracle;
        public static glClass.DBConn.DBInfo DBInfo { get; set; } = new glClass.DBConn.DBInfo();

        public static ErrorData ErrorInfo { get; set; } = new ErrorData();

        public static Dictionary<string, LoginUserData> LoginUserInfo { get; set; } = new Dictionary<string, LoginUserData>();
    }
}
