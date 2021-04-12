using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Global;

namespace Api
{
    public class Common
    {

        public static string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("[");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("]");
            }
            return JSONString.ToString();
        }


        public static string ConvertDT2Json(System.Data.DataTable table, bool isWithSuccess = false)
        {
            var JSONString = new System.Text.StringBuilder();
            if (table.Rows.Count > 0)
            {
                if (isWithSuccess)
                {
                    JSONString.Append("{\"success\":\"ok\", \"data\":[");
                }
                else
                {
                    JSONString.Append("{\"data\":[");
                }
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("[");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        string appendText = table.Rows[i][j].ToString()
                                                            .Replace("[", "(")
                                                            .Replace("]", ")")
                                                            .Replace(@"\", @"\\")
                                                            .Replace("\"", "\\\"")
                                                            .Replace(((char)13).ToString(), "<br />")
                                                            .Replace(((char)10).ToString(), "")
                                                            .Trim();

                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + appendText + "\",");
                            //JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + appendText + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("]");
                    }
                    else
                    {
                        JSONString.Append("],");
                    }
                }
                JSONString.Append("]}");
            }
            else
            {
                if (table.Columns.Count > 0)
                {
                    JSONString.Append("{\"data\":[");
                    JSONString.Append("[");
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"NoData\",");
                        }
                        else if (i == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"NoData\"");
                        }
                    }
                    JSONString.Append("]");
                    JSONString.Append("]}");
                }
            }

            return JSONString.ToString();
        }

        public static DataTable GetMillon()
        {

            DataTable result = null;

            try
            {
                using (var db = new glClass.DBConn.DBConn(gVar.DBInfo, gVar.DBType))
                {
                    try
                    {
                        object dbParams = null;

                        switch (gVar.DBType)
                        {
                            case glClass.DBConn.DBMS.MSSql:
                                break;
                            case glClass.DBConn.DBMS.PostgreSql:
                                break;
                            case glClass.DBConn.DBMS.Oracle:
                            default:
                                dbParams = new glClass.DBConn.OraParams();

                                (dbParams as glClass.DBConn.OraParams).CmdType = CommandType.StoredProcedure;
                                (dbParams as glClass.DBConn.OraParams).CmdText = "LOAD_MIL.SP_LOAD_MIL";

                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_MNG_TYPE", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));

                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN1", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN2", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN3", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN4", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN5", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN6", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN7", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN8", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN9", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN10", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN11", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN12", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN13", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN14", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN15", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("cur_DATA", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor));
                                (dbParams as glClass.DBConn.OraParams).Params[(dbParams as glClass.DBConn.OraParams).Params.Count - 1].Direction = ParameterDirection.Output;

                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_MNG_TYPE", "get");
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN1", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN2", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN3", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN4", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN5", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN6", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN7", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN8", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN9", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN10", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN11", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN12", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN13", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN14", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN15", string.Empty);
                                break;
                        }

                        using (var dt = db.ToDataTable(dbParams))
                        {
                            if ((dt != null) && (dt.Rows.Count > 0))
                            {
                                result = dt;
                            }
                            else
                            {
                                throw new Exception("ER1 Need to check DB status");
                            }

                            dt.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetMillon ERROR > " + ex);

                result = null;
            }


            return result;
        }

        public static DataTable GetAppConfig()
        {
            DataTable result = null;

            try
            {
                using (var db = new glClass.DBConn.DBConn(gVar.DBInfo, gVar.DBType))
                {
                    try
                    {
                        object dbParams = null;

                        switch (gVar.DBType)
                        {
                            case glClass.DBConn.DBMS.MSSql:
                                break;
                            case glClass.DBConn.DBMS.PostgreSql:
                                dbParams = new glClass.DBConn.PostParams();

                                (dbParams as glClass.DBConn.PostParams).CmdType = CommandType.StoredProcedure;
                                (dbParams as glClass.DBConn.PostParams).CmdText = "public.mgmt_common_app_config_mng";

                                (dbParams as glClass.DBConn.PostParams).Params.Add(new Npgsql.NpgsqlParameter("p_mng_type", NpgsqlTypes.NpgsqlDbType.Text));
                                (dbParams as glClass.DBConn.PostParams).Params.Add(new Npgsql.NpgsqlParameter("p_config_type", NpgsqlTypes.NpgsqlDbType.Text));
                                (dbParams as glClass.DBConn.PostParams).Params.Add(new Npgsql.NpgsqlParameter("p_config_value", NpgsqlTypes.NpgsqlDbType.Text));

                                (dbParams as glClass.DBConn.PostParams).Values.Add("p_mng_type", "get");
                                (dbParams as glClass.DBConn.PostParams).Values.Add("p_config_type", string.Empty);
                                (dbParams as glClass.DBConn.PostParams).Values.Add("p_config_value", string.Empty);
                                break;
                            case glClass.DBConn.DBMS.Oracle:
                            default:
                                dbParams = new glClass.DBConn.OraParams();

                                (dbParams as glClass.DBConn.OraParams).CmdType = CommandType.StoredProcedure;
                                (dbParams as glClass.DBConn.OraParams).CmdText = "LOAD_MIL.SP_LOAD_MIL";

                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_MNG_TYPE", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN7", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("COLUMN8", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("cur_DATA", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor));
                                (dbParams as glClass.DBConn.OraParams).Params[(dbParams as glClass.DBConn.OraParams).Params.Count - 1].Direction = ParameterDirection.Output;

                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_MNG_TYPE", "get");
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN7", string.Empty);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("COLUMN8", string.Empty);
                                break;
                        }

                        using (var dt = db.ToDataTable(dbParams))
                        {
                            if ((dt != null) && (dt.Rows.Count > 0))
                            {
                                result = dt;
                            }
                            else
                            {
                                throw new Exception("Need to check DB status");
                            }

                            dt.Dispose();
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = null;
            }

            return result;
        }

        public static bool UpdateUserProfile(string userIdx, string userName, string userEngName, string userPhone)
        {
            bool result = false;

            try
            {
                using (var db = new glClass.DBConn.DBConn(gVar.DBInfo, gVar.DBType))
                {
                    try
                    {
                        object dbParams = null;

                        switch (gVar.DBType)
                        {
                            case glClass.DBConn.DBMS.MSSql:
                                break;
                            case glClass.DBConn.DBMS.PostgreSql:
                                break;
                            case glClass.DBConn.DBMS.Oracle:
                            default:
                                dbParams = new glClass.DBConn.OraParams();

                                (dbParams as glClass.DBConn.OraParams).CmdType = CommandType.StoredProcedure;
                                (dbParams as glClass.DBConn.OraParams).CmdText = "load_mil.sp_load_mil";

                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_USER_IDX", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_USER_NAME", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_USER_ENG_NAME", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_USER_PHONE", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("cur_DATA", Oracle.ManagedDataAccess.Client.OracleDbType.RefCursor));
                                (dbParams as glClass.DBConn.OraParams).Params[(dbParams as glClass.DBConn.OraParams).Params.Count - 1].Direction = ParameterDirection.Output;

                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_USER_IDX", userIdx);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_USER_NAME", userName);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_USER_ENG_NAME", userEngName);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_USER_PHONE", userPhone);
                                break;
                        }

                        using (var dt = db.ToDataTable(dbParams))
                        {
                            if (dt != null && dt.Rows.Count > 0)
                            {
                                string dbResultName = glClass.Common.ConvertString(dt.Rows[0][0]);

                                if (dbResultName.ToLower() == "ng")
                                {
                                    result = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }

            return result;
        }

        public static void SetUserAction(string actionName, string actionDbName, string actionType, string actionParams, string userIdx, string clientIp)
        {
            new Thread(() => SetUserActionLog(actionName, actionDbName, actionType, actionParams, userIdx, clientIp)).Start();
        }

        private static void SetUserActionLog(string actionName, string actionDbName, string actionType, string actionParams, string userIdx, string clientIp)
        {
            try
            {
                using (var db = new glClass.DBConn.DBConn(gVar.DBInfo, gVar.DBType))
                {
                    try
                    {
                        object dbParams = null;

                        switch (gVar.DBType)
                        {
                            case glClass.DBConn.DBMS.MSSql:
                                break;
                            case glClass.DBConn.DBMS.PostgreSql:
                                break;
                            case glClass.DBConn.DBMS.Oracle:
                            default:
                                dbParams = new glClass.DBConn.OraParams();

                                (dbParams as glClass.DBConn.OraParams).CmdType = CommandType.StoredProcedure;
                                (dbParams as glClass.DBConn.OraParams).CmdText = "MGMT_COMMON.SP_SET_ACTION_LOG";

                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_ACTION_NAME", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_ACTION_DB_NAME", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_ACTION_TYPE", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_ACTION_PARAMS", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_USER_IDX", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));
                                (dbParams as glClass.DBConn.OraParams).Params.Add(new Oracle.ManagedDataAccess.Client.OracleParameter("P_CLIENT_IP", Oracle.ManagedDataAccess.Client.OracleDbType.Varchar2));

                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_ACTION_NAME", actionName);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_ACTION_DB_NAME", actionDbName);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_ACTION_TYPE", actionType);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_ACTION_PARAMS", actionParams);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_USER_IDX", userIdx);
                                (dbParams as glClass.DBConn.OraParams).Values.Add("P_CLIENT_IP", clientIp);
                                break;
                        }

                        db.ToExcute(dbParams);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        db.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }
    }
}
