using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Oracle.ManagedDataAccess.Client;

using glClass;

namespace glClass.DBConn
{
    [Flags]
    public enum DBMS { MSSql = 0, PostgreSql = 1, Oracle = 2 }
    //public enum DBMS { None = 0, Oracle = 1, MSSql = 2, MySql = 3, PostgreSql = 4 }

    public class DBInfo
    {
        public string Host { get; set; }
        public string Port { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        /// <summary>
        /// ORACLE
        /// </summary>
        public string SID { get; set; }
        /// <summary>
        /// ORACLE
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// POSTGRESQL
        /// </summary>
        public string Database { get; set; }
    }

    public class OraParams
    {
        public string CmdText { get; set; }
        public CommandType CmdType { get; set; }
        private List<OracleParameter> _params = new List<OracleParameter>();
        public List<OracleParameter> Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }
        private Dictionary<string, object> _values = new Dictionary<string, object>();
        public Dictionary<string, object> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }

        public int ArrayBindCount { get; set; }
    }

    public class PostParams
    {
        public string CmdText { get; set; }
        public CommandType CmdType { get; set; }
        private List<Npgsql.NpgsqlParameter> _params = new List<Npgsql.NpgsqlParameter>();
        public List<Npgsql.NpgsqlParameter> Params
        {
            get
            {
                return _params;
            }
            set
            {
                _params = value;
            }
        }
        private Dictionary<string, object> _values = new Dictionary<string, object>();
        public Dictionary<string, object> Values
        {
            get
            {
                return _values;
            }
            set
            {
                _values = value;
            }
        }
    }

    public class DBConn : IDisposable
    {
        private DBInfo DBConInfo = new DBInfo();

        private string ConInfo = string.Empty;

        private DBMS DBType = DBMS.Oracle;

        protected static string ThisName = "DBConn";

        private bool disposed = false;

        private int timeOut = 500;

        public int TimeOut
        {
            get
            {
                return this.timeOut;
            }
            set
            {
                this.timeOut = value;
            }
        }

        private OracleConnection OraDbConn;
        private OracleCommand OraDbCmd;
        private NpgsqlConnection PgDbConn;
        private NpgsqlCommand PgDbCmd;

        public T GetParams<T>(object data)
        {
            return (T)data;
        }

        public static DBMS GetDBType(string dbmsType)
        {
            if (string.IsNullOrEmpty(dbmsType) == false)
            {
                switch (dbmsType.ToLower())
                {
                    case "mssql":
                    case "ms-sql":
                        return DBMS.MSSql;
                    case "postgresql":
                        return DBMS.PostgreSql;
                    case "oracle":
                    default:
                        return DBMS.Oracle;
                }
            }
            else
            {
                return DBMS.Oracle;
            }
        }

        public DBConn(DBInfo dbInfo, DBMS dbType)
        {
            try
            {
                this.DBConInfo = dbInfo;
                this.DBType = dbType;

                switch (this.DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        if (string.IsNullOrEmpty(dbInfo.Port)) dbInfo.Port = "5432";

                        this.ConInfo = string.Format("Host={0};Username={1};Password={2};Database={3};timeout=500;", this.DBConInfo.Host, this.DBConInfo.UserID, this.DBConInfo.Password, this.DBConInfo.Database);
                        break;
                    case DBMS.Oracle:
                    default:
                        if (string.IsNullOrEmpty(dbInfo.Port)) dbInfo.Port = "1521";

                        if (string.IsNullOrEmpty(this.DBConInfo.ServiceName) == false)
                        {
                            this.ConInfo = string.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVICE_NAME={2})));", this.DBConInfo.Host, this.DBConInfo.Port, this.DBConInfo.ServiceName);
                            this.ConInfo = string.Format("{0}User Id={1};Password={2};Connection Timeout=500;", this.ConInfo, this.DBConInfo.UserID, this.DBConInfo.Password);
                        }
                        else
                        {
                            this.ConInfo = string.Format("Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))(CONNECT_DATA=(SERVER=DEDICATED)(SID={2})));", this.DBConInfo.Host, this.DBConInfo.Port, this.DBConInfo.SID);
                            this.ConInfo = string.Format("{0}User Id={1};Password={2};", this.ConInfo, this.DBConInfo.UserID, this.DBConInfo.Password);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }

        public void Dispose(bool disposing = true)
        {
            try
            {
                if (!disposed)
                {
                    if (disposing)
                    {
                        // Free other state (managed objects).
                        this.DBClose();
                    }
                    // Free your own state (unmanaged objects).
                    // Set large fields to null.
                    disposed = true;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }

        void IDisposable.Dispose() { }

        public bool DBConnect()
        {
            try
            {
                switch (this.DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        if (this.PgDbConn == null)
                        {
                            switch (this.DBType)
                            {
                                case DBMS.PostgreSql:
                                    this.PgDbConn = new NpgsqlConnection();
                                    break;
                            }
                        }

                        if ((this.PgDbConn.State == ConnectionState.Broken)
                            || (this.PgDbConn.State == ConnectionState.Closed))
                        {

                            if (this.PgDbConn.State != ConnectionState.Closed) this.PgDbConn.Close();

                            this.PgDbConn.ConnectionString = this.ConInfo; // +";Max Pool Size=50;Connection Timeout=60;"; <-- 안됨..

                            this.PgDbConn.Open();

                            return true;
                        }
                        break;
                    case DBMS.Oracle:
                    default:
                        if (this.OraDbConn == null) this.OraDbConn = new OracleConnection();

                        if ((this.OraDbConn.State == ConnectionState.Broken)
                            || (this.OraDbConn.State == ConnectionState.Closed))
                        {

                            if (this.OraDbConn.State != ConnectionState.Closed) this.OraDbConn.Close();

                            this.OraDbConn.ConnectionString = this.ConInfo; // +";Max Pool Size=50;Connection Timeout=60;"; <-- 안됨..

                            this.OraDbConn.Open();

                            return true;
                        }
                        break;
                }

                InitDbCommand();

                return true;
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                return false;
            }
        }

        private void InitDbCommand()
        {
            try
            {
                switch (this.DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        if (this.PgDbConn == null || this.PgDbConn.State != ConnectionState.Open)
                        {
                            if (this.DBConnect())
                            {
                                this.PgDbCmd = this.PgDbConn.CreateCommand();

                                this.PgDbCmd.Connection = this.PgDbConn;
                                this.PgDbCmd.CommandTimeout = this.timeOut;
                            }
                            else
                            {
                                throw new Exception("Connect fail..");
                            }
                        }
                        break;
                    case DBMS.Oracle:
                    default:
                        if (this.OraDbConn == null || this.OraDbConn.State != ConnectionState.Open)
                        {
                            if (this.DBConnect())
                            {
                                this.OraDbCmd = this.OraDbConn.CreateCommand();

                                this.OraDbCmd.Connection = this.OraDbConn;
                                this.OraDbCmd.CommandTimeout = this.timeOut;
                            }
                            else
                            {
                                throw new Exception("Connect fail..");
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                switch (this.DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        this.PgDbConn = null;
                        this.PgDbCmd = null;
                        break;
                    case DBMS.Oracle:
                    default:
                        this.OraDbConn = null;
                        this.OraDbCmd = null;
                        break;
                }
            }
        }

        public void DBClose()
        {
            try
            {
                switch (this.DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        if (this.PgDbConn != null && this.PgDbConn.State != ConnectionState.Closed)
                            this.PgDbConn.Close();
                        break;
                    case DBMS.Oracle:
                    default:
                        if (this.OraDbConn != null && this.OraDbConn.State != ConnectionState.Closed)
                            this.OraDbConn.Close();
                        break;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }

        // 단일 값 반환
        public string ToValue(string query)
        {
            string result = string.Empty;

            try
            {
                InitDbCommand();

                switch (this.DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        this.PgDbCmd.CommandText = query;

                        result = glClass.Common.ConvertString(this.PgDbCmd.ExecuteScalar());
                        break;
                    case DBMS.Oracle:
                    default:
                        this.OraDbCmd.CommandText = query;

                        result = glClass.Common.ConvertString(this.OraDbCmd.ExecuteScalar());
                        break;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = string.Empty;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        // DB 서버 시간 불러오기
        public string GetDateTime()
        {
            string query = string.Empty;

            switch (this.DBType)
            {
                case DBMS.MSSql:
                    query = "select GETDATE();";
                    break;
                case DBMS.Oracle:
                    query = "SELECT TO_CHAR(SYSDATE, 'YYYY-MM-DD HH24:MI:SS') NOWTIME FROM DUAL";
                    break;
                case DBMS.PostgreSql:
                    query = "select now();";
                    break; ;
            }

            return this.ToValue(query);
        }

        public int ToExcute(object dbParams)
        {
            int result = -99;

            try
            {
                switch (DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        result = this.ToExcute((dbParams as glClass.DBConn.PostParams));
                        break;
                    case DBMS.Oracle:
                    default:
                        result = this.ToExcute((dbParams as glClass.DBConn.OraParams));
                        break;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = -99;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        public System.Data.DataSet ToDataSet(object dbParams)
        {
            DataSet result = null;

            try
            {
                switch (DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        result = this.ToDataSet((dbParams as glClass.DBConn.PostParams));
                        break;
                    case DBMS.Oracle:
                    default:
                        result = this.ToDataSet((dbParams as glClass.DBConn.OraParams));
                        break;
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = null;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        public System.Data.DataTable ToDataTable(object dbParams)
        {
            DataTable result = null;

            try
            {
                DataSet ds = null;

                switch (DBType)
                {
                    case DBMS.MSSql:
                        break;
                    case DBMS.PostgreSql:
                        //ds = ToDataSet(this.GetParams<glClass.DBConn.PostParams>(dbParams));
                        ds = this.ToDataSet((dbParams as glClass.DBConn.PostParams));
                        break;
                    case DBMS.Oracle:
                    default:
                        //ds = ToDataSet(this.GetParams<glClass.DBConn.OraParams>(dbParams));
                        ds = this.ToDataSet((dbParams as glClass.DBConn.OraParams));
                        break;
                }

                if (ds != null)
                {
                    result = ds.Tables[0];
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
                
                result = null;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        #region PostgreSQL

        // DB 데이터 셋 반환
        public System.Data.DataSet ToDataSet(PostParams dbParams)
        {
            DataSet result = new DataSet();

            try
            {
                if (dbParams != null)
                {
                    InitDbCommand();

                    if (this.PgDbCmd != null)
                    {
                        this.PgDbCmd.CommandText = dbParams.CmdText;
                        this.PgDbCmd.CommandType = dbParams.CmdType;

                        if (dbParams.Params != null && dbParams.Params.Count > 0)
                        {
                            this.PgDbCmd.Parameters.AddRange(dbParams.Params.ToArray());
                        }

                        if (dbParams.Values != null && dbParams.Values.Count > 0)
                        {
                            if (this.PgDbCmd.Parameters.Count > 0)
                            {
                                for (int i = 0; i < this.PgDbCmd.Parameters.Count; i++)
                                {
                                    if (dbParams.Values.ContainsKey(this.PgDbCmd.Parameters[i].ParameterName))
                                    {
                                        this.PgDbCmd.Parameters[i].Value = dbParams.Values[this.PgDbCmd.Parameters[i].ParameterName];
                                    }
                                }
                            }
                        }

                        #region Origin Source

                        //NpgsqlDataAdapter postDAdapter = new NpgsqlDataAdapter((NpgsqlCommand)this.DbCmd);
                        //postDAdapter.Fill(result);
                        //postDAdapter.Dispose();

                        #endregion

                        using (var trans = ((NpgsqlConnection)this.PgDbConn).BeginTransaction())
                        {
                            this.PgDbCmd.Transaction = trans;

                            DataSet tmpDs = new DataSet();

                            NpgsqlDataAdapter postDAdapter = new NpgsqlDataAdapter((NpgsqlCommand)this.PgDbCmd);
                            postDAdapter.Fill(tmpDs);
                            postDAdapter.Dispose();

                            if (tmpDs != null && tmpDs.Tables[0].Rows.Count > 0)
                            {
                                string tmpFirstData = glClass.Common.ConvertString(tmpDs.Tables[0].Rows[0][0]);

                                if ((string.IsNullOrEmpty(tmpFirstData) == false) && tmpFirstData.IndexOf("unnamed") > -1)
                                {
                                    for (int i = 0; i < tmpDs.Tables[0].Rows.Count; i++)
                                    {
                                        string tmpCursorName = glClass.Common.ConvertString(tmpDs.Tables[0].Rows[i][0]);

                                        this.PgDbCmd.CommandText = string.Format("FETCH ALL IN \"{0}\"", tmpCursorName);
                                        this.PgDbCmd.CommandType = CommandType.Text;

                                        result.Tables.Add(string.Format("result {0}", i));

                                        NpgsqlDataAdapter dataAdapter = new NpgsqlDataAdapter((NpgsqlCommand)this.PgDbCmd);
                                        dataAdapter.Fill(result.Tables[i]);
                                        dataAdapter.Dispose();
                                    }
                                }
                                else
                                {
                                    result = tmpDs;
                                }
                            }

                            trans.Commit();
                        }
                    }
                    else
                    {
                        throw new Exception("DB Command is null");
                    }
                }
                else
                {
                    throw new Exception("Parameter is not init");
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = null;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        // 실행문
        public int ToExcute(PostParams dbParams)
        {
            int result = -99;

            try
            {
                if (dbParams != null)
                {
                    InitDbCommand();

                    if (this.PgDbCmd != null)
                    {

                        this.PgDbCmd.CommandText = dbParams.CmdText;
                        this.PgDbCmd.CommandType = dbParams.CmdType;

                        if (dbParams.Params != null && dbParams.Params.Count > 0)
                        {
                            this.PgDbCmd.Parameters.AddRange(dbParams.Params.ToArray());
                        }

                        if (dbParams.Values != null && dbParams.Values.Count > 0)
                        {
                            if (this.PgDbCmd.Parameters.Count > 0)
                            {
                                for (int i = 0; i < this.PgDbCmd.Parameters.Count; i++)
                                {
                                    if (dbParams.Values.ContainsKey(this.PgDbCmd.Parameters[i].ParameterName))
                                    {
                                        this.PgDbCmd.Parameters[i].Value = dbParams.Values[this.PgDbCmd.Parameters[i].ParameterName];
                                    }
                                }
                            }
                        }

                        result = this.PgDbCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        throw new Exception("DB Command is null");
                    }
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = -99;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        #endregion

        #region Oracle

        // DB 데이터 셋 반환
        public System.Data.DataSet ToDataSet(OraParams dbParams)
        {
            DataSet result = new DataSet();

            try
            {
                if (dbParams != null)
                {
                    InitDbCommand();

                    this.OraDbCmd.CommandText = dbParams.CmdText;
                    this.OraDbCmd.CommandType = dbParams.CmdType;

                    if (dbParams.Params != null && dbParams.Params.Count > 0)
                    {
                        this.OraDbCmd.Parameters.AddRange(dbParams.Params.ToArray());
                    }

                    if (dbParams.Values != null && dbParams.Values.Count > 0)
                    {
                        if (this.OraDbCmd.Parameters.Count > 0)
                        {
                            for (int i = 0; i < this.OraDbCmd.Parameters.Count; i++)
                            {
                                if (dbParams.Values.ContainsKey(this.OraDbCmd.Parameters[i].ParameterName))
                                {
                                    this.OraDbCmd.Parameters[i].Value = dbParams.Values[this.OraDbCmd.Parameters[i].ParameterName];
                                }
                            }
                        }
                    }

                    OracleDataAdapter oraDAdapter = new OracleDataAdapter((OracleCommand)this.OraDbCmd);
                    oraDAdapter.Fill(result);
                    oraDAdapter.Dispose();
                }
                else
                {
                    throw new Exception("Parameter is not init");
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = null;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        // 실행문
        public int ToExcute(OraParams dbParams)
        {
            int result = -99;

            try
            {
                if (dbParams != null)
                {
                    InitDbCommand();

                    this.OraDbCmd.CommandText = dbParams.CmdText;
                    this.OraDbCmd.CommandType = dbParams.CmdType;

                    if (dbParams.ArrayBindCount > 0)
                        this.OraDbCmd.ArrayBindCount = dbParams.ArrayBindCount;

                    if (dbParams.Params != null && dbParams.Params.Count > 0)
                    {
                        this.OraDbCmd.Parameters.AddRange(dbParams.Params.ToArray());
                    }

                    if (dbParams.Values != null && dbParams.Values.Count > 0)
                    {
                        if (this.OraDbCmd.Parameters.Count > 0)
                        {
                            for (int i = 0; i < this.OraDbCmd.Parameters.Count; i++)
                            {
                                if (dbParams.Values.ContainsKey(this.OraDbCmd.Parameters[i].ParameterName))
                                {
                                    this.OraDbCmd.Parameters[i].Value = dbParams.Values[this.OraDbCmd.Parameters[i].ParameterName];
                                }
                            }
                        }
                    }

                    result = this.OraDbCmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));

                result = -99;
            }
            finally
            {
                DBClose();
            }

            return result;
        }

        #endregion
    }
}
