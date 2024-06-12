using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using Lanpuda.Account.UI;
using Lanpuda.Client.Start.Cores;
using Lanpuda.Client.Theme;
using Lanpuda.Client.Theme.Entities;
using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.ERP.UI;
using Lanpuda.Identity.UI;
using Lanpuda.PermissionManagement.UI;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp;
using Volo.Abp.Modularity;

namespace Lanpuda.Client.Start
{
    [DependsOn(typeof(ThemeModule))]
    [DependsOn(typeof(AccountUIModule))]
    [DependsOn(typeof(IdentityUIModule))]
    [DependsOn(typeof(ERPUIModule))]
    public class ClientStartModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<MainWindow>();
            context.Services.AddSingleton<ClientStartBootstraper>();
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);

        }

        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);

            var bootstraper = context.ServiceProvider.GetRequiredService<ClientStartBootstraper>();
            bootstraper.Load();

            ViewLocator.Default = new ViewLocator(new Assembly[] {
            typeof(App).Assembly,
            typeof(ThemeModule).Assembly ,
            typeof(AccountUIModule).Assembly ,
            typeof(IdentityUIModule).Assembly ,
            typeof(PermissionManagementUIModule).Assembly,
            }
           );


            var menuService = context.ServiceProvider.GetRequiredService<IMenuService>();

            AppModule appModuleManage = new AppModule() { Key = "AccountUI", DisplayName = "个人设置" };
            AppModule appModuleERP = new AppModule() { Key = "ERPUI", DisplayName = "ERP系统" ,IsDefault = true};
            //AppModule appModuleLims = new AppModule() { Key = "LIMSUI", DisplayName = "LIMS系统" };
            AppModule appModuleIdentity = new AppModule() { Key = "IdentityUI", DisplayName = "用户权限" };
            menuService.AddAppModule(appModuleERP);
            //menuService.AddAppModule(appModuleLims);
            menuService.AddAppModule(appModuleIdentity);
            menuService.AddAppModule(appModuleManage);
        }
    }

}
