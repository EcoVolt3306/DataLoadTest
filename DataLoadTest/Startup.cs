using DataLoadTest.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OracleClient;

using System.Data;
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
            services.AddSingleton<WeatherForecastService>();
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

        }


        private void CheckAppConfig()
        {
            try
            {
                DataTable configData = Api.Common.GetAppConfig();

                if (configData != null)
                {
                    for (int i = 0; i < configData.Rows.Count; i++)
                    {
                        string configType = glClass.Common.ConvertString(configData.Rows[i][0]);
                        string configValue = glClass.Common.ConvertString(configData.Rows[i][1]);

                        switch (configType.ToLower())
                        {
                            case "work_flag_hour":
                                gVar.WorkFlagHour = configValue;
                                break;
                            case "login_image":
                                gVar.AppThemeInfo.LoginImg = configValue;
                                break;
                            case "provider":
                                gVar.Provider = configValue;
                                break;
                            case "app_full_name":
                                gVar.AppFullName = configValue;
                                break;
                            case "app_name":
                                gVar.AppName = configValue;
                                break;
                            case "cust_name":
                                gVar.CustName = configValue;
                                break;
                            case "cust_full_name":
                                gVar.CustFullName = configValue;
                                break;
                            case "session_out_min":
                                gVar.SessionOutMin = glClass.Common.ConvertInt(configValue);
                                break;
                            case "theme":
                                gVar.AppThemeInfo.SetTheme = configValue;
                                break;
                            case "style_color":
                                gVar.AppThemeInfo.AppStyleColor = glClass.Common.ConvertColor(configValue.StartsWith("#") ? configValue : string.Format("#{0}", configValue), System.Drawing.Color.DarkRed);
                                break;
                            case "style_fore_color":
                                gVar.AppThemeInfo.AppStyleForeColor = glClass.Common.ConvertColor(configValue.StartsWith("#") ? configValue : string.Format("#{0}", configValue), System.Drawing.Color.DarkRed);
                                break;
                        }
                    }

                    if (string.IsNullOrEmpty(Global.gVar.CustFullName))
                    {
                        Global.gVar.AppTitle = string.Format("[{0}] - {1}", Global.gVar.AppName, Global.gVar.AppFullName);
                    }
                    else
                    {
                        Global.gVar.AppTitle = string.Format("[{0}] - {1}", Global.gVar.CustFullName, Global.gVar.AppFullName);
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
