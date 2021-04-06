using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using Global;
using System.Threading.Tasks;

namespace glClass
{
    public class Common
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetNowMethodName()
        {
            var st = new StackTrace(new StackFrame(1));
            return st.GetFrame(0).GetMethod().Name;
        }

        public static int ConvertInt(object obj, int fail = 0)
        {
            try
            {
                string tmp = obj.ToString().Replace(",", "");
                double dblTmp = ConvertDouble(tmp, Convert.ToDouble(fail));

                return Convert.ToInt32(dblTmp);
            }
            catch
            {
                return fail;
            }
        }

        public static double ConvertDouble(object obj, double fail = 0.0)
        {
            try
            {
                string tmp = obj.ToString().Replace(",", "");

                return Convert.ToDouble(tmp);
            }
            catch
            {
                return fail;
            }
        }

        public static bool ConvertBool(object obj, bool fail = false)
        {
            try
            {
                if (global::glClass.Common.ConvertString(obj) == "1") return true;

                return Convert.ToBoolean(obj);
            }
            catch
            {
                return fail;
            }
        }

        public static DateTime ConvertDateTime(object obj)
        {
            try
            {
                return Convert.ToDateTime(obj);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string ConvertString(object obj, string fail = "")
        {
            try
            {
                return Convert.ToString(obj);
            }
            catch
            {
                return fail;
            }
        }

        public static Color ConvertColor(object colorCode, Color fail)
        {
            try
            {
                return System.Drawing.ColorTranslator.FromHtml(ConvertString(colorCode));
            }
            catch
            {
                return fail;
            }
        }

        public static Microsoft.AspNetCore.Components.MarkupString ConvertMarkupString(object obj, string fail = "")
        {
            try
            {
                string tmpString = glClass.Common.ConvertString(obj, fail);

                tmpString = tmpString.Replace("\n", "").Replace("\r", "<br />");

                return (Microsoft.AspNetCore.Components.MarkupString)tmpString;
            }
            catch
            {
                return (Microsoft.AspNetCore.Components.MarkupString)fail;
            }
        }

        public static string[] SplitWithOutSpace(string originData, char sChar)
        {
            try
            {
                return originData.Split(sChar).Where(s => !string.IsNullOrEmpty(s)).ToArray();
            }
            catch (Exception ex)
            {
                global::glClass.Log.FileLog(Log.LogFlag.Err, ex.Message.ToString());

                return new string[0];
            }
        }
    }

    public class Log
    {
        /// <summary>
        /// Err : 정의 한 에러
        /// Ex : 예외 상황
        /// Event : 
        /// DBEx : 데이터 베이스 예외 상황
        /// Action : 사용자 액션 로그
        /// </summary>
        [Flags]
        public enum LogFlag
        {
            Err, Event, Action, Test, Origin
        }

        /// <summary>
        /// 파일 로그 작성
        /// traceName = Function Name
        /// </summary>
        /// <param name="strLogFlag">파일명</param>
        /// <param name="strLog">로그</param>
        public static void FileLog(LogFlag logFlag, string logData, string traceName = null)
        {
            StringBuilder strDirPath = new StringBuilder();

            strDirPath.AppendFormat("{0}\\Log\\{1}\\", Directory.GetCurrentDirectory(), logFlag.ToString());

            try
            {
                logData = DateTime.Now.ToString("HH:mm:ss.fff - ") + logData;

                DirectoryInfo dirInfor = new DirectoryInfo(@strDirPath.ToString());

                if (dirInfor.Exists == false)
                {
                    dirInfor.Create();
                }

                StringBuilder filePath = new StringBuilder();
                if (string.IsNullOrEmpty(traceName))
                {
                    filePath.AppendFormat("{0}\\{1}.{2}.log", strDirPath, logFlag.ToString(), DateTime.Now.ToString("yyyyMMdd"));
                }
                else
                {
                    filePath.AppendFormat("{0}\\{1}.{2}.log", strDirPath, traceName, DateTime.Now.ToString("yyyyMMdd"));
                }

                File.AppendAllText(filePath.ToString(), logData + "\r\n");
            }
            catch
            {
                //SystmeLog(logData, EventLogEntryType.Warning, 1);
            }
        }

        /// <summary>
        /// 파일 로그 작성
        /// traceName = Function Name
        /// </summary>
        /// <param name="strLogFlag">파일명</param>
        /// <param name="strLog">로그</param>
        public static void FileLog2Path(LogFlag logFlag, string logData, string fileRootPath, string traceName = null)
        {
            StringBuilder strDirPath = new StringBuilder();
            strDirPath.AppendFormat("{0}\\Log\\{1}\\", fileRootPath, logFlag.ToString());

            try
            {
                logData = DateTime.Now.ToString("HH:mm:ss.fff - ") + logData;

                DirectoryInfo dirInfor = new DirectoryInfo(@strDirPath.ToString());

                if (dirInfor.Exists == false)
                {
                    dirInfor.Create();
                }

                StringBuilder filePath = new StringBuilder();
                if (string.IsNullOrEmpty(traceName))
                {
                    filePath.AppendFormat("{0}\\{1}.{2}.log", strDirPath, logFlag.ToString(), DateTime.Now.ToString("yyyyMMdd"));
                }
                else
                {
                    filePath.AppendFormat("{0}\\{1}.{2}.log", strDirPath, traceName, DateTime.Now.ToString("yyyyMMdd"));
                }

                File.AppendAllText(filePath.ToString(), logData + "\r\n");
            }
            catch
            {
                //SystmeLog(logData, EventLogEntryType.Warning, 1);
            }
        }

        public static void DelFileLog(int keepDay)
        {
            new System.Threading.Thread(delegate () { DeleteLogByDay(keepDay); }).Start();
        }

        private static void DeleteLogByDay(int keepDay)
        {
            try
            {
                string folderPath = string.Format("{0}\\Log\\", Directory.GetCurrentDirectory());

                DirectoryInfo logFolder = new DirectoryInfo(folderPath);

                foreach (DirectoryInfo dir in logFolder.GetDirectories())
                {
                    foreach (FileInfo file in dir.GetFiles())
                    {
                        if (file.Extension.ToLower() != ".log")
                        {
                            continue;
                        }

                        if (file.CreationTime < DateTime.Now.AddDays(-keepDay))
                        {
                            file.Delete();
                        }
                    }
                }

                logFolder = null;
            }
            catch (Exception ex)
            {
                FileLog(LogFlag.Err, string.Format("DeleteLogByDay exception ({0})", ex.Message.ToString()));
            }
        }
    }

    public class Cryptography
    {
        private static string Key = "DTKCRYPT";

        // 키
        private static readonly string NewKey = "DTKCRYPT01234567890123456789012345678901";
        //128bit (16자리)
        private static readonly string Key_128 = NewKey.Substring(0, 128 / 8);
        //256bit (32자리)               
        private static readonly string Key_256 = NewKey.Substring(0, 256 / 8);

        private static byte[] keyBytes = ASCIIEncoding.ASCII.GetBytes(Key);

        // String Data 암호화
        public static string SimpleEncrypt(string data)
        {
            try
            {
                // 암호화 알고리즘중 RC2 암호화를 하려면 RC를
                // DES알고리즘을 사용하려면 DESCryptoServiceProvider 객체를 선언한다.
                //RC2 rc2 = new RC2CryptoServiceProvider();
                DESCryptoServiceProvider encr = new DESCryptoServiceProvider();

                // 대칭키 배치
                encr.Key = keyBytes;
                encr.IV = keyBytes;

                // 암호화는 스트림(바이트 배열)을
                // 대칭키에 의존하여 암호화 하기때문에 먼저 메모리 스트림을 생성한다.
                MemoryStream ms = new MemoryStream();

                //만들어진 메모리 스트림을 이용해서 암호화 스트림 생성
                CryptoStream cryStream = new CryptoStream(ms, encr.CreateEncryptor(), CryptoStreamMode.Write);

                // 데이터를 바이트 배열로 변경
                byte[] dataBytes = Encoding.UTF8.GetBytes(data.ToCharArray());

                // 암호화 스트림에 데이터 씀
                cryStream.Write(dataBytes, 0, dataBytes.Length);
                cryStream.FlushFinalBlock();

                // 암호화 완료 (string으로 컨버팅해서 반환)
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        // String Data 복호화
        public static string SimpleDecrypt(string data)
        {
            try
            {
                // 암호화 알고리즘중 RC2 암호화를 하려면 RC를
                // DES알고리즘을 사용하려면 DESCryptoServiceProvider 객체를 선언한다.
                //RC2 rc2 = new RC2CryptoServiceProvider();
                DESCryptoServiceProvider decr = new DESCryptoServiceProvider();

                // 대칭키 배치
                decr.Key = keyBytes;
                decr.IV = keyBytes;

                // 암호화는 스트림(바이트 배열)을
                // 대칭키에 의존하여 암호화 하기때문에 먼저 메모리 스트림을 생성한다.
                MemoryStream ms = new MemoryStream();

                //만들어진 메모리 스트림을 이용해서 암호화 스트림 생성
                CryptoStream cryStream = new CryptoStream(ms, decr.CreateDecryptor(), CryptoStreamMode.Write);

                //데이터를 바이트배열로 변경한다.
                byte[] dataBytes = Convert.FromBase64String(data);

                //변경된 바이트배열을 암호화 한다.
                cryStream.Write(dataBytes, 0, dataBytes.Length);
                cryStream.FlushFinalBlock();

                //암호화 한 데이터를 스트링으로 변환해서 리턴
                return Encoding.UTF8.GetString(ms.GetBuffer());
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        //AES 128 암호화.., CBC, PKCS7, 예외발생하면 null
        public static string EncryptAES128(string plain)
        {
            try
            {
                //바이트로 변환 
                byte[] plainBytes = Encoding.UTF8.GetBytes(plain);

                //레인달 알고리듬
                RijndaelManaged rm = new RijndaelManaged();
                //자바에서 사용한 운용모드와 패딩방법 일치시킴(AES/CBC/PKCS5Padding)
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 128;

                //메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream();

                //key, iv값 정의
                ICryptoTransform encryptor = rm.CreateEncryptor(Encoding.UTF8.GetBytes(Key_128), Encoding.UTF8.GetBytes(Key_128));
                //크립토스트림을 키와 IV값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                //크립트스트림에 바이트배열을 쓰고 플러시..
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();

                //메모리스트림에 담겨있는 암호화된 바이트배열을 담음
                byte[] encryptBytes = memoryStream.ToArray();

                //베이스64로 변환
                string encryptString = Convert.ToBase64String(encryptBytes);

                //스트림 닫기.
                cryptoStream.Close();
                memoryStream.Close();

                return encryptString;
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        //AES128 복호화.., CBC, PKCS7, 예외발생하면 null
        public static string DecryptAES128(string encrypt)
        {
            try
            {
                //base64를 바이트로 변환 
                byte[] encryptBytes = Convert.FromBase64String(encrypt);
                //byte[] encryptBytes = Encoding.UTF8.GetBytes(encryptString);

                //레인달 알고리듬
                RijndaelManaged rm = new RijndaelManaged();
                //자바에서 사용한 운용모드와 패딩방법 일치시킴(AES/CBC/PKCS5Padding)
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 128;

                //메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream(encryptBytes);

                //key, iv값 정의
                ICryptoTransform decryptor = rm.CreateDecryptor(Encoding.UTF8.GetBytes(Key_128), Encoding.UTF8.GetBytes(Key_128));
                //크립토스트림을 키와 IV값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                //복호화된 데이터를 담을 바이트 배열을 선언한다. 
                byte[] plainBytes = new byte[encryptBytes.Length];

                int plainCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                //복호화된 바이트 배열을 string으로 변환
                string plainString = Encoding.UTF8.GetString(plainBytes, 0, plainCount);

                //스트림 닫기.
                cryptoStream.Close();
                memoryStream.Close();

                return plainString;
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        //AES 256 암호화.., CBC, PKCS7, 예외발생하면 null
        public static string EncryptAES256(string plain)
        {
            try
            {
                //바이트로 변환 
                byte[] plainBytes = Encoding.UTF8.GetBytes(plain);

                //레인달 알고리듬
                RijndaelManaged rm = new RijndaelManaged();
                //자바에서 사용한 운용모드와 패딩방법 일치시킴(AES/CBC/PKCS5Padding)
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 256;

                //메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream();

                //key, iv값 정의
                ICryptoTransform encryptor = rm.CreateEncryptor(Encoding.UTF8.GetBytes(Key_256), Encoding.UTF8.GetBytes(Key_128));
                //크립토스트림을 키와 IV값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                //크립트스트림에 바이트배열을 쓰고 플러시..
                cryptoStream.Write(plainBytes, 0, plainBytes.Length);
                cryptoStream.FlushFinalBlock();

                //메모리스트림에 담겨있는 암호화된 바이트배열을 담음
                byte[] encryptBytes = memoryStream.ToArray();

                //베이스64로 변환
                string encryptString = Convert.ToBase64String(encryptBytes);

                //스트림 닫기.
                cryptoStream.Close();
                memoryStream.Close();

                return encryptString;
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        // AES 256 암호화... bytes
        public static byte[] EncryptAES256ForBytes(byte[] plain)
        {
            try
            {
                //레인달 알고리듬
                RijndaelManaged rm = new RijndaelManaged();
                //자바에서 사용한 운용모드와 패딩방법 일치시킴(AES/CBC/PKCS5Padding)
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 256;

                //메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream();

                //key, iv값 정의
                ICryptoTransform encryptor = rm.CreateEncryptor(Encoding.UTF8.GetBytes(Key_256), Encoding.UTF8.GetBytes(Key_128));
                //크립토스트림을 키와 IV값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);

                //크립트스트림에 바이트배열을 쓰고 플러시..
                cryptoStream.Write(plain, 0, plain.Length);
                cryptoStream.FlushFinalBlock();

                //메모리스트림에 담겨있는 암호화된 바이트배열을 담음
                byte[] encryptBytes = memoryStream.ToArray();

                //스트림 닫기.
                cryptoStream.Close();
                memoryStream.Close();

                return encryptBytes;
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        //AES256 복호화.., CBC, PKCS7, 예외발생하면 null
        public static string DecryptAES256(string encrypt)
        {
            try
            {
                //base64를 바이트로 변환 
                byte[] encryptBytes = Convert.FromBase64String(encrypt);
                //byte[] encryptBytes = Encoding.UTF8.GetBytes(encryptString);

                //레인달 알고리듬
                RijndaelManaged rm = new RijndaelManaged();
                //자바에서 사용한 운용모드와 패딩방법 일치시킴(AES/CBC/PKCS5Padding)
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 256;

                //메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream(encryptBytes);

                //key, iv값 정의
                ICryptoTransform decryptor = rm.CreateDecryptor(Encoding.UTF8.GetBytes(Key_256), Encoding.UTF8.GetBytes(Key_128));
                //크립토스트림을 키와 IV값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                //복호화된 데이터를 담을 바이트 배열을 선언한다. 
                byte[] plainBytes = new byte[encryptBytes.Length];

                int plainCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                //복호화된 바이트 배열을 string으로 변환
                string plainString = Encoding.UTF8.GetString(plainBytes, 0, plainCount);

                //스트림 닫기.
                cryptoStream.Close();
                memoryStream.Close();

                return plainString;
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        // AES 256 복호화... bytes
        public static byte[] DecryptAES256ForBytes(byte[] encrypt)
        {
            try
            {
                //레인달 알고리듬
                RijndaelManaged rm = new RijndaelManaged();
                //자바에서 사용한 운용모드와 패딩방법 일치시킴(AES/CBC/PKCS5Padding)
                rm.Mode = CipherMode.CBC;
                rm.Padding = PaddingMode.PKCS7;
                rm.KeySize = 256;

                //메모리스트림 생성
                MemoryStream memoryStream = new MemoryStream(encrypt);

                //key, iv값 정의
                ICryptoTransform decryptor = rm.CreateDecryptor(Encoding.UTF8.GetBytes(Key_256), Encoding.UTF8.GetBytes(Key_128));
                //크립토스트림을 키와 IV값으로 메모리스트림을 이용하여 생성
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);

                //복호화된 데이터를 담을 바이트 배열을 선언한다. 
                byte[] plainBytes = new byte[encrypt.Length];

                int plainCount = cryptoStream.Read(plainBytes, 0, plainBytes.Length);

                ////복호화된 바이트 배열을 string으로 변환
                //string plainString = Encoding.UTF8.GetString(plainBytes, 0, plainCount);

                //스트림 닫기.
                cryptoStream.Close();
                memoryStream.Close();

                return plainBytes;
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }

        //SHA256 해쉬 함수 암호화.., 예외발생하면 null
        public static string EncryptSHA256(string plain)
        {
            try
            {
                //바이트로 변환 
                byte[] plainBytes = Encoding.UTF8.GetBytes(plain);

                SHA256Managed sm = new SHA256Managed();

                byte[] encryptBytes = sm.ComputeHash(plainBytes);

                //hex.. 16진수
                //string encryptString = BitConverter.ToString(encryptBytes).Replace("-", "").ToLower();

                //base64
                string encryptString = Convert.ToBase64String(encryptBytes);

                return encryptString;
            }
            catch (Exception ex)
            {
                Log.FileLog(Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                return null;
            }
        }
    }

    public static class Helper
    {
        public enum AlertType { Success, Error, Waring, Info, Question };

        /// <summary>
        /// 0 : Check OK
        /// 1 : 비밀번호 8자 이상 필요
        /// 2 : 숫자, 영문, 특수문자가 모두 필요
        /// 3 : 동일문자 3가지 이상 존재
        /// 4 : 연속된 문자 존재
        /// 5 : 연속된 키보드 배열 문자 존재
        /// 99 : Exception Errors
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static int CheckPassword(string pwd)
        {
            int result = 99;

            try
            {
                pwd = pwd.Trim();

                // 숫자, 영문, 특수문자를 추출하여 각각의 변수에 저장
                string numberChar = Regex.Replace(pwd, "[a-zA-Z~!@#\\$%\\^&\\*\\(\\)\\-_=\\+/\\.,<>\\?\\[\\]\\{\\}]", "", RegexOptions.Singleline);
                string englishChar = Regex.Replace(pwd, "[0-9~!@#\\$%\\^&\\*\\(\\)\\-_=\\+/\\.,<>\\?]", "", RegexOptions.Singleline);
                string specialChar = Regex.Replace(pwd, "[a-zA-Z0-9]", "", RegexOptions.Singleline);

                // 패스워드 길이 확인
                if (pwd.Length >= 8)
                {
                    // 3가지 모두가 있는지 확인
                    if (!string.IsNullOrEmpty(numberChar) && !string.IsNullOrEmpty(englishChar) && !string.IsNullOrEmpty(specialChar))
                    {
                        // 숫자 중복 확인
                        for (int i = 0; i < numberChar.Length; i++)
                        {
                            if (numberChar.Replace(numberChar.Substring(i, 1), "").Length <= numberChar.Length - 3)
                            {
                                result = 3;

                                return result;
                            }
                        }

                        // 문자 중복 확인
                        for (int i = 0; i < englishChar.Length; i++)
                        {
                            if (englishChar.ToLower().Replace(englishChar.Substring(i, 1).ToLower(), "").Length <= englishChar.Length - 3)
                            {
                                result = 3;

                                return result;
                            }
                        }

                        // 특수문자 중복 확인
                        for (int i = 0; i < specialChar.Length; i++)
                        {
                            if (specialChar.Replace(specialChar.Substring(i, 1).ToLower(), "").Length <= specialChar.Length - 3)
                            {
                                result = 3;

                                return result;
                            }
                        }

                        // 연속된 문자 확인
                        int sortCnt = 0;
                        int beforAscii = 0;
                        for (int i = 0; i < pwd.Length; i++)
                        {
                            int ascii = Convert.ToInt32(Convert.ToChar(pwd.Substring(i, 1).ToLower()));

                            if (i != 0)
                            {
                                if (Math.Abs(beforAscii - ascii) == 1)
                                {
                                    sortCnt++;
                                }
                                else
                                {
                                    sortCnt = 0;
                                }

                                if (sortCnt >= 2)
                                {
                                    result = 4;

                                    return result;
                                }
                            }
                            beforAscii = ascii;
                        }

                        // 연속된 키보드 배열 확인
                        string keyPadList = "qwe|wer|ert|rty|tyu|yui|uio|iop|asd|sdf|dfg|fgh|ghj|hjk|jkl|zxc|xcv|cvb|vbn|bnm|" +
                                            "!@#|@#$|#$%|$%^|%^&|^&*|&*(|*()|()_|)_+|p[]|[]A|0-=|l;'|nm,|m,.|,./|<>?|l:\"|[]\\|`12|~!@|~12";
                        string[] arrCtnKeyPad = glClass.Common.SplitWithOutSpace(keyPadList, '|');

                        for (int i = 0; i < arrCtnKeyPad.Length; i++)
                        {
                            string checker = arrCtnKeyPad[i];

                            if (checker.IndexOf("A") > -1)
                            {
                                checker.Replace("A", "|");
                            }

                            if (pwd.ToLower().IndexOf(checker) > -1)
                            {
                                result = 5;

                                return result;
                            }
                        }

                        result = 0;
                    }
                    else
                    {
                        result = 2;
                    }
                }
                else
                {
                    result = 1;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, ex.Message.ToString(), glClass.Common.GetNowMethodName());

                result = 99;
            }

            return result;
        }

        public static void ShowAlert(this Microsoft.JSInterop.IJSRuntime jSRuntime, AlertType alertType, string title, string msg)
        {
            try
            {
                jSRuntime.InvokeVoidAsync("ShowAlertBox", alertType.ToString().ToLower(), title, msg);
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }

        public static async Task<LoginData> SetSessionCheckTime(Blazored.SessionStorage.ISessionStorageService sessionStorage)
        {
            try
            {
                LoginData loginInfo = await sessionStorage.GetItemAsync<LoginData>("IS");

                loginInfo.ChangeTime = DateTime.Now;

                await sessionStorage.SetItemAsync<LoginData>("IS", loginInfo);

                return loginInfo;
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                return null;
            }
        }

        public static void Go1(this Microsoft.JSInterop.IJSRuntime JS, string on)
        {
            try
            {
                JS.InvokeVoidAsync("go2", on);
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }
    }
}