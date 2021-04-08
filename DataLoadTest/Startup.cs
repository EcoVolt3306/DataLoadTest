using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;
using System.Threading;
using System.Data;
using Blazored.SessionStorage;
using Blazored.Toast;
using Global;

namespace DataLoadTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            #region ADDED

            // Nuget Package
            services.AddBlazoredSessionStorage(config =>
                config.JsonSerializerOptions.WriteIndented = true);

            services.AddBlazoredToast();

            // Nuget Packge End

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddHttpClient();
            services.AddMvc();

            services.AddControllersWithViews();

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true; // consent required
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession(options =>
            {
                options.Cookie.Name = ".DTK.Session";
                options.IdleTimeout = TimeSpan.FromDays(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddHttpContextAccessor();

            // Server Side Blazor doesn't register HttpClient by default
            if (!services.Any(x => x.ServiceType == typeof(HttpClient)))
            {
                // Setup HttpClient for server side in a client side compatible fashion
                services.AddScoped<HttpClient>(s =>
                {
                    // Creating the URI helper needs to wait until the JS Runtime is initialized, so defer it.      
                    var uriHelper = s.GetRequiredService<NavigationManager>();
                    return new HttpClient
                    {
                        BaseAddress = new Uri(uriHelper.BaseUri)
                    };
                });
            }

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/ErrorInfo");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region ORIGIN

            //app.UseHttpsRedirection();
            //app.UseStaticFiles();

            //app.UseRouting();

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapBlazorHub();
            //    endpoints.MapFallbackToPage("/_Host");
            //});

            #endregion

            #region ADDED

            app.UseSession();
            app.UseCookiePolicy();

            #endregion

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            #region ADDED

            gVar.DBInfo.Host = glClass.Common.ConvertString(Configuration["DBInfo:Host"], "");
            gVar.DBInfo.Port = glClass.Common.ConvertString(Configuration["DBInfo:Port"], "");
            gVar.DBInfo.SID = glClass.Common.ConvertString(Configuration["DBInfo:SID"], "");
            gVar.DBInfo.ServiceName = glClass.Common.ConvertString(Configuration["DBInfo:ServiceName"], "");
            gVar.DBInfo.Database = glClass.Common.ConvertString(Configuration["DBInfo:Database"], "");
            gVar.DBInfo.UserID = glClass.Common.ConvertString(Configuration["DBInfo:UserId"], "");
            gVar.DBInfo.Password = glClass.Common.ConvertString(Configuration["DBInfo:UserPwd"], "");
            //Global.gVar.DBInfo.Password = glClass.Cryptography.SimpleDecrypt(glClass.Common.ConvertString(Configuration["DBInfo:UserPwd"], "")).Replace("\0", "");

            gVar.DBType = glClass.DBConn.DBConn.GetDBType(glClass.Common.ConvertString(Configuration["DBInfo:DBType"], ""));

            // Check Config
            //CheckAppConfig();
            LoadList();

            // Timer
            InitTimer();

            app.UseResponseCaching();



            #endregion

            #region Changed

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action}");
                endpoints.MapControllers();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapRazorPages();
            });

            #endregion
        }

        private void InitTimer()
        {
            System.Timers.Timer tmOneSec = new System.Timers.Timer();

            tmOneSec.Interval = 1000;
            tmOneSec.Elapsed += TmOneSec_Elapsed;

            tmOneSec.Start();
        }

        private bool IsInited { get; set; } = false;

        private void TmOneSec_Elapsed(object sender, ElapsedEventArgs e)
        {
            DateTime dtNow = DateTime.Now;

            try
            {
                if ((dtNow.Second % 3) == 0)
                {
                    new Thread(() => DbStateCheck()).Start();
                }

                if ((dtNow.Second % 5) == 0)
                {
                    //new Thread(() => CheckAppConfig()).Start();
                    //new Thread(() => LoadList()).Start();
                }

                new Thread(() => CheckUserConnectState()).Start();
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }

        private void LoadList()
        {
             try{

                DataTable listData = Api.Common.GetMillon();
                //gVar.dtbox = Api.Common.GetMillon();
                gVar.newDT = Api.Common.GetMillon();

                if (listData != null)
                {
                    for (int i = 0; i < listData.Rows.Count; i++)   // i->Rows
                    {
                        gVar.col[0, i] = glClass.Common.ConvertString(listData.Rows[i][0]); // 00 01 02 03 04...
                        gVar.col[1, i] = glClass.Common.ConvertString(listData.Rows[i][1]); // 10 11 12 13 14....
                        gVar.col[2, i] = glClass.Common.ConvertString(listData.Rows[i][2]);
                        gVar.col[3, i] = glClass.Common.ConvertString(listData.Rows[i][3]);
                        gVar.col[4, i] = glClass.Common.ConvertString(listData.Rows[i][4]);
                        gVar.col[5, i] = glClass.Common.ConvertString(listData.Rows[i][5]);
                        gVar.col[6, i] = glClass.Common.ConvertString(listData.Rows[i][6]);
                        gVar.col[7, i] = glClass.Common.ConvertString(listData.Rows[i][7]);
                        gVar.col[8, i] = glClass.Common.ConvertString(listData.Rows[i][8]);
                        gVar.col[9, i] = glClass.Common.ConvertString(listData.Rows[i][9]);
                        gVar.col[10, i] = glClass.Common.ConvertString(listData.Rows[i][10]);
                        gVar.col[11, i] = glClass.Common.ConvertString(listData.Rows[i][11]);
                        gVar.col[12, i] = glClass.Common.ConvertString(listData.Rows[i][12]);
                        gVar.col[13, i] = glClass.Common.ConvertString(listData.Rows[i][13]);
                        gVar.col[14, i] = glClass.Common.ConvertString(listData.Rows[i][14]);

                        //gVar.dic1[1] = "k";
                        //gVar.dic1[2] = "k";


                    }
                }



            } catch(Exception e){
                 Console.WriteLine(e.Message);
            }
}
        /// <summary>
        ///  Fnc TEST AREA
        /// </summary>







        private void CheckAppConfig()
        {
            try
            {
                DataTable loadData = Api.Common.GetAppConfig();

                if (loadData != null)
                {
                    for (int i = 0; i < loadData.Rows.Count; i++)
                    {
                        string col1 = glClass.Common.ConvertString(loadData.Rows[i][0]);
                        string col2 = glClass.Common.ConvertString(loadData.Rows[i][1]);

                        switch (col1.ToLower())
                        {
                            case "g-1":
                                //gVar.Column7 = col2;
                                break;
                            case "g-2":
                                //gVar.Column8 = col2;
                                break;
                                //case "work_flag_hour":
                                //    gVar.WorkFlagHour = configValue;
                                //    break;
                                //case "login_image":
                                //    gVar.AppThemeInfo.LoginImg = configValue;
                                //    break;
                                //case "provider":
                                //    gVar.Provider = configValue;
                                //    break;
                                //case "app_full_name":
                                //    gVar.AppFullName = configValue;
                                //    break;
                                //case "app_name":
                                //    gVar.AppName = configValue;
                                //    break;
                                //case "cust_name":
                                //    gVar.CustName = configValue;
                                //    break;
                                //case "cust_full_name":
                                //    gVar.CustFullName = configValue;
                                //    break;
                                //case "session_out_min":
                                //    gVar.SessionOutMin = glClass.Common.ConvertInt(configValue);
                                //    break;
                                //case "theme":
                                //    gVar.AppThemeInfo.SetTheme = configValue;
                                //    break;
                                //case "style_color":
                                //    gVar.AppThemeInfo.AppStyleColor = glClass.Common.ConvertColor(configValue.StartsWith("#") ? configValue : string.Format("#{0}", configValue), System.Drawing.Color.DarkRed);
                                //    break;
                                //case "style_fore_color":
                                //    gVar.AppThemeInfo.AppStyleForeColor = glClass.Common.ConvertColor(configValue.StartsWith("#") ? configValue : string.Format("#{0}", configValue), System.Drawing.Color.DarkRed);
                                //    break;
                        }
                    }

                    //if (string.IsNullOrEmpty(Global.gVar.CustFullName))
                    //{
                    //    Global.gVar.AppTitle = string.Format("[{0}] - {1}", Global.gVar.AppName, Global.gVar.AppFullName);
                    //}
                    //else
                    //{
                    //    Global.gVar.AppTitle = string.Format("[{0}] - {1}", Global.gVar.CustFullName, Global.gVar.AppFullName);
                    //}



                    //if (string.IsNullOrEmpty(Global.gVar.Column7))
                    //{
                    //    Global.gVar.Column7 = string.Format("[{0}] - {1}", Global.gVar.Column8, Global.gVar.Column9);
                    //}
                    //else
                    //{
                    //    Global.gVar.Column7 = string.Format("[{0}] - {1}", Global.gVar.Column9, Global.gVar.Column10);
                    //}
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }

        private void CheckUserConnectState()
        {
            try
            {
                List<string> logoutKeys = new List<string>();

                if (gVar.LoginUserInfo != null && gVar.LoginUserInfo.Count > 0)
                {
                    foreach (var kvp in gVar.LoginUserInfo)
                    {
                        if (kvp.Value.UserInfo != null)
                        {
                            TimeSpan checkTimeGap = DateTime.Now - kvp.Value.UserInfo.LastCheckTime;

                            if (checkTimeGap.TotalSeconds > 30)
                            {
                                //glClass.Log.FileLog(glClass.Log.LogFlag.Test, string.Format("{0} ->  checkTime Out : [{1}]", kvp.Value.UserInfo.UserIdx, DateTime.Now.ToLongDateString()));

                                logoutKeys.Add(kvp.Key);

                                // DB Logout
                                //Api.Login.SetUserLogOut(kvp.Value.UserInfo.UserIdx.ToString());
                            }
                            else
                            {
                                // DB Logged in renew
                                //Api.Login.SetUserSessionRenew(kvp.Value.UserInfo.UserIdx.ToString(), kvp.Value.UserInfo.UserIP);
                            }
                        }
                    }

                    foreach (var key in logoutKeys)
                    {
                        if (gVar.LoginUserInfo.ContainsKey(key))
                        {
                            gVar.LoginUserInfo.Remove(key);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }








        #region DB State Check

        private bool PrevDBState { get; set; } = false;

        private void DbStateCheck()
        {
            try
            {
                //DBStateChanger();

                this.PrevDBState = gVar.DBState;

                using (var db = new glClass.DBConn.DBConn(gVar.DBInfo, gVar.DBType))
                {
                    string time = db.GetDateTime();

                    if (string.IsNullOrEmpty(time))
                    {
                        gVar.DBState = false;
                    }
                    else
                    {
                        gVar.DBState = true;
                    }
                }
            }
            catch (Exception ex)
            {
                glClass.Log.FileLog(glClass.Log.LogFlag.Err, string.Format("{0} -> {1}", glClass.Common.GetNowMethodName(), ex.Message.ToString()));
            }
        }

        #endregion
    }
}
