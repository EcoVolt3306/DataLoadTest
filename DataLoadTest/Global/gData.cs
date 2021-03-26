using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Global
{
    public class LoginData
    {
        // LocalStorage에 저장되는 로그인 데이터로 
        // 보안성 검토에 위반될수도 있기에 아래와 같이 변수명을 변경한다.
        // UserIdx는 AES256으로 암호화하여 진행한다.

        // UserIdx -> AES256 로 암호화
        public string ColorData { get; set; } = string.Empty;
        // CheckTime
        // 사용자가 어떤 행위를 할 때마다 체크하기 위한 변수
        // DB 세션 아웃 변수 체크를 위하여 
        public DateTime ChangeTime { get; set; } = DateTime.Now;
    }

    public class LoginUserData
    {
        public UserData UserInfo { get; set; } = new UserData();
        public List<PageData> PageInfo { get; set; } = new List<PageData>();
    }

    /// <summary>
    /// Global Error Data
    /// </summary>
    public class ErrorData
    {
        public string ErrorTitle { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class UserData
    {
        public int UserIdx { get; set; }
        public string UserId { get; set; }
        public string UserPwd { get; set; }
        public string UserName { get; set; }
        public string UserEngName { get; set; }
        public string UserPhone { get; set; }
        public int UserPermission { get; set; }
        public bool IsUserLock { get; set; }
        public bool IsFirstLogin { get; set; }
        public string LastLoginIP { get; set; }
        public string LastLoginTime { get; set; }

        public List<bool> PageList { get; set; }

        public string UserIP { get; set; }
        // 로그인이 유지되어 있는지 확인하기 위한 시간 변수
        public DateTime LastCheckTime { get; set; }
    }

    public class AppThemeData
    {
        public string LoginImg { get; set; } = "01.jpg";
        public System.Drawing.Color AppStyleColor { get; set; } = System.Drawing.Color.DarkCyan;
        public System.Drawing.Color AppStyleForeColor { get; set; } = System.Drawing.Color.White;

        public string AppStyleColorHex { get { return "#" + this.AppStyleColor.R.ToString("X2") + this.AppStyleColor.G.ToString("X2") + this.AppStyleColor.B.ToString("X2"); } }
        public string AppStyleForeColorHex { get { return "#" + this.AppStyleForeColor.R.ToString("X2") + this.AppStyleForeColor.G.ToString("X2") + this.AppStyleForeColor.B.ToString("X2"); } }

        public string MainBgColor { get; set; } = "#2a2a2a";
        public string ThemeColor { get; set; } = "#3e3e3e";

        public string SetTheme
        {
            set
            {
                if (string.IsNullOrEmpty(value) == false)
                {
                    switch (value.ToLower())
                    {
                        case "dark":
                            this.MainBgColor = "#2a2a2a";
                            this.ThemeColor = "#3e3e3e";
                            break;
                        case "light":
                            this.MainBgColor = "#ffffff";
                            this.ThemeColor = "#95a5a6";
                            break;
                    }
                }
            }
        }
    }

    public class PageData
    {
        public int PageIdx { get; set; }
        public int ParentIdx { get; set; }
        public string PageName { get; set; }
        public string PageMemo { get; set; }
        public string PagePath1 { get; set; }
        public string PagePath2 { get; set; }
        public string PageIcon { get; set; }
        public int PageOrder { get; set; }

        public int ChildrenCount { get; set; }

        public int DefaultPerm { get; set; }
    }
}
