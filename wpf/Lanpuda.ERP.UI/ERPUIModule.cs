using Lanpuda.Client.Theme;
using Lanpuda.Client.Theme.Entities;
using Lanpuda.Client.Theme.Services.MenuServices;
using Lanpuda.ERP.Permissions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Lanpuda.ERP.UI
{
    [DependsOn(typeof(AbpAutoMapperModule))]
    [DependsOn(typeof(ERPHttpApiClientModule))]
    [DependsOn(typeof(ThemeModule))]
    public class ERPUIModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSingleton<ERPUIBootstraper>();
            Configure<AbpAutoMapperOptions>(options =>
            {
                //Add all mappings defined in the assembly of the MyModule class
                options.AddMaps<ERPUIModule>();
            });

            JsonSerializerOptions options = new()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnApplicationInitialization(context);
        }


        public override void OnPostApplicationInitialization(ApplicationInitializationContext context)
        {
            base.OnPostApplicationInitialization(context);
            var bootstraper = context.ServiceProvider.GetRequiredService<ERPUIBootstraper>();
            bootstraper.Load();

            //
            var menuService = context.ServiceProvider.GetRequiredService<IMenuService>();
            List<MenuItem> menuItems = new List<MenuItem>();
            menuService.SetMenusByModuleKey("ERPUI", menuItems);
            //首页
            MenuItem homeMenu = new MenuItem() { MenuHeader = "ERP首页", TargetKey = ERPUIViewKeys.ERPHome };
            menuItems.Add(homeMenu);


            //产品管理
            MenuItem basicDataMenu = new MenuItem() { MenuHeader = "产品管理", PermissionName = ERPPermissions.BasicData.Default };

            MenuItem basicDataSettingMenu = new MenuItem() { MenuHeader = "基础设置", PermissionName = ERPPermissions.BasicData.BasicSetting };

            MenuItem productCategoryMenu = new MenuItem() { MenuHeader = "产品信息", PermissionName = ERPPermissions.Product.Default ,TargetKey = ERPUIViewKeys.Product_Paged};
            MenuItem productUnit = new MenuItem() { MenuHeader = "产品单位", PermissionName = ERPPermissions.ProductUnit.Default, TargetKey = ERPUIViewKeys.ProductUnit_Paged };
            MenuItem product = new MenuItem() { MenuHeader = "产品分类", PermissionName = ERPPermissions.ProductCategory.Default, TargetKey = ERPUIViewKeys.ProductCategory_Paged };

            basicDataSettingMenu.Children.Add(productCategoryMenu);
            basicDataSettingMenu.Children.Add(productUnit);
            basicDataSettingMenu.Children.Add(product);
            basicDataMenu.Children.Add(basicDataSettingMenu);
            menuItems.Add(basicDataMenu);

            #region 销售管理
            MenuItem salesMenu = new MenuItem() { MenuHeader = "销售管理", PermissionName = ERPPermissions.SalesManagement.Default };

            MenuItem salesProcess = new MenuItem() { MenuHeader = "业务流程", PermissionName = ERPPermissions.SalesManagement.BusinessProcess };

            MenuItem salesPrice = new MenuItem() { MenuHeader = "销售报价", PermissionName = ERPPermissions.SalesPrice.Default, TargetKey = ERPUIViewKeys.SalesPrice_Paged };
            MenuItem salesOrder = new MenuItem() { MenuHeader = "销售订单", PermissionName = ERPPermissions.SalesOrder.Default, TargetKey = ERPUIViewKeys.SalesOrder_Paged };
            MenuItem shipmentApply = new MenuItem() { MenuHeader = "发货申请", PermissionName = ERPPermissions.ShipmentApply.Default, TargetKey = ERPUIViewKeys.ShipmentApply_Paged };
            MenuItem salesReturnApply = new MenuItem() { MenuHeader = "退货申请", PermissionName = ERPPermissions.SalesReturnApply.Default, TargetKey = ERPUIViewKeys.SalesReturnApply_Paged };

            MenuItem salesSetting = new MenuItem() { MenuHeader = "基础设置", PermissionName = ERPPermissions.SalesManagement.BasicSetting };
            MenuItem customer = new MenuItem() { MenuHeader = "客户信息", PermissionName = ERPPermissions.Customer.Default, TargetKey = ERPUIViewKeys.Customer_Paged };

            salesMenu.Children.Add(salesProcess);
            salesMenu.Children.Add(salesSetting);

            salesProcess.Children.Add(salesPrice);
            salesProcess.Children.Add(salesOrder);
            salesProcess.Children.Add(shipmentApply);
            salesProcess.Children.Add(salesReturnApply);

            salesSetting.Children.Add(customer);

            menuItems.Add(salesMenu);
            #endregion

            #region 采购管理
            MenuItem purchaseMenu = new MenuItem() { MenuHeader = "采购管理", PermissionName = ERPPermissions.PurchaseManagement.Default };

            MenuItem purchaseProcessMenu = new MenuItem() { MenuHeader = "业务流程", PermissionName = ERPPermissions.PurchaseManagement.BusinessProcess };
            MenuItem purchaseettingMenu = new MenuItem() { MenuHeader = "基础设置", PermissionName = ERPPermissions.PurchaseManagement.BasicSetting };

            MenuItem purchasePrice = new MenuItem() { MenuHeader = "采购报价", PermissionName = ERPPermissions.PurchasePrice.Default, TargetKey = ERPUIViewKeys.PurchasePrice_Paged };
            MenuItem purchaseApply = new MenuItem() { MenuHeader = "采购申请", PermissionName = ERPPermissions.PurchaseApply.Default, TargetKey = ERPUIViewKeys.PurchaseApply_Paged };
            MenuItem purchaseOrder = new MenuItem() { MenuHeader = "采购订单", PermissionName = ERPPermissions.PurchaseOrder.Default, TargetKey = ERPUIViewKeys.PurchaseOrder_Paged };
            MenuItem arrivalNotice = new MenuItem() { MenuHeader = "来料通知", PermissionName = ERPPermissions.ArrivalNotice.Default, TargetKey = ERPUIViewKeys.ArrivalNotice_Paged };
            MenuItem purchaseReturnApply = new MenuItem() { MenuHeader = "退货申请", PermissionName = ERPPermissions.PurchaseReturnApply.Default, TargetKey = ERPUIViewKeys.PurchaseReturnApply_Paged };


            MenuItem supplie = new MenuItem() { MenuHeader = "供应商", PermissionName = ERPPermissions.Supplier.Default, TargetKey = ERPUIViewKeys.Supplier_Paged };

            purchaseProcessMenu.Children.Add(purchasePrice);
            purchaseProcessMenu.Children.Add(purchaseApply);
            purchaseProcessMenu.Children.Add(purchaseOrder);
            purchaseProcessMenu.Children.Add(arrivalNotice);
            purchaseProcessMenu.Children.Add(purchaseReturnApply);

            purchaseettingMenu.Children.Add(supplie);

            purchaseMenu.Children.Add(purchaseProcessMenu);
            purchaseMenu.Children.Add(purchaseettingMenu);
            menuItems.Add(purchaseMenu);
            #endregion



            #region 生产管理

            MenuItem productionMenu = new MenuItem() { MenuHeader = "生产管理", PermissionName = ERPPermissions.ProductionManagement.Default };
            MenuItem productionProcess = new MenuItem() { MenuHeader = "业务流程", PermissionName = ERPPermissions.ProductionManagement.BusinessProcess };
            MenuItem productionSetting = new MenuItem() { MenuHeader = "基础设置", PermissionName = ERPPermissions.ProductionManagement.BasicSetting };

            MenuItem mps = new MenuItem() { MenuHeader = "生产计划", PermissionName = ERPPermissions.Mps.Default, TargetKey = ERPUIViewKeys.Mps_Paged };
            MenuItem workOrder = new MenuItem() { MenuHeader = "生产工单", PermissionName = ERPPermissions.WorkOrder.Default, TargetKey = ERPUIViewKeys.WorkOrder_Paged };
            MenuItem materialApply = new MenuItem() { MenuHeader = "领料申请", PermissionName = ERPPermissions.MaterialApply.Default, TargetKey = ERPUIViewKeys.MaterialApply_Paged };
            MenuItem workOrderStorageApply = new MenuItem() { MenuHeader = "入库申请", PermissionName = ERPPermissions.WorkOrderStorageApply.Default, TargetKey = ERPUIViewKeys.WorkOrderStorageApply_Paged };
            MenuItem materialReturnApply = new MenuItem() { MenuHeader = "退料申请", PermissionName = ERPPermissions.MaterialReturnApply.Default, TargetKey = ERPUIViewKeys.MaterialReturnApply_Paged };

            MenuItem workshop = new MenuItem() { MenuHeader = "生产车间", PermissionName = ERPPermissions.Workshop.Default, TargetKey = ERPUIViewKeys.Workshop_Paged };
            MenuItem bom = new MenuItem() { MenuHeader = "生产BOM", PermissionName = ERPPermissions.Bom.Default, TargetKey = ERPUIViewKeys.Bom_Paged };


            productionProcess.Children.Add(mps);
            productionProcess.Children.Add(workOrder);
            productionProcess.Children.Add(materialApply);
            productionProcess.Children.Add(workOrderStorageApply);
            productionProcess.Children.Add(materialReturnApply);

            productionSetting.Children.Add(workshop);
            productionSetting.Children.Add(bom);

            productionMenu.Children.Add(productionProcess);
            productionMenu.Children.Add(productionSetting);

            menuItems.Add(productionMenu);
            #endregion



            #region 库存管理
            MenuItem warehouseMenu = new MenuItem() { MenuHeader = "库存管理", PermissionName = ERPPermissions.WarehouseManagement.Default };
            MenuItem warehouseProcess = new MenuItem() { MenuHeader = "业务流程", PermissionName = ERPPermissions.WarehouseManagement.BusinessProcess };
            MenuItem warehouseReport = new MenuItem() { MenuHeader = "管理报表", PermissionName = ERPPermissions.WarehouseManagement.Report };
            MenuItem warehouseSetting = new MenuItem() { MenuHeader = "基础设置", PermissionName = ERPPermissions.WarehouseManagement.BasicSetting };

            MenuItem salesOut = new MenuItem() { MenuHeader = "销售出库", PermissionName = ERPPermissions.SalesOut.Default, TargetKey = ERPUIViewKeys.SalesOut_Paged };
            MenuItem salesReturn = new MenuItem() { MenuHeader = "销售退货", PermissionName = ERPPermissions.SalesReturn.Default, TargetKey = ERPUIViewKeys.SalesReturn_Paged };
            MenuItem purchaseStorage = new MenuItem() { MenuHeader = "采购入库", PermissionName = ERPPermissions.PurchaseStorage.Default, TargetKey = ERPUIViewKeys.PurchaseStorage_Paged };
            MenuItem purchaseReturn = new MenuItem() { MenuHeader = "采购退货", PermissionName = ERPPermissions.PurchaseReturn.Default, TargetKey = ERPUIViewKeys.PurchaseReturn_Paged };
            MenuItem workOrderOut = new MenuItem() { MenuHeader = "生产领料", PermissionName = ERPPermissions.WorkOrderOut.Default, TargetKey = ERPUIViewKeys.WorkOrderOut_Paged };
            MenuItem workOrderStorage = new MenuItem() { MenuHeader = "工单入库", PermissionName = ERPPermissions.WorkOrderStorage.Default, TargetKey = ERPUIViewKeys.WorkOrderStorage_Paged };
            MenuItem workOrderReturn = new MenuItem() { MenuHeader = "生产退料", PermissionName = ERPPermissions.WorkOrderReturn.Default, TargetKey = ERPUIViewKeys.WorkOrderReturn_Paged };
            MenuItem otherStorage = new MenuItem() { MenuHeader = "其他入库", PermissionName = ERPPermissions.OtherStorage.Default, TargetKey = ERPUIViewKeys.OtherStorage_Paged };
            MenuItem otherOut = new MenuItem() { MenuHeader = "其他出库", PermissionName = ERPPermissions.OtherOut.Default, TargetKey = ERPUIViewKeys.OtherOut_Paged };
            MenuItem inventoryMove = new MenuItem() { MenuHeader = "库存调拨", PermissionName = ERPPermissions.InventoryMove.Default, TargetKey = ERPUIViewKeys.InventoryMove_Paged };
            MenuItem inventoryCheck = new MenuItem() { MenuHeader = "库存盘点", PermissionName = ERPPermissions.InventoryCheck.Default, TargetKey = ERPUIViewKeys.InventoryCheck_Paged };
            //MenuItem inventoryTransform = new MenuItem() { MenuHeader = "形态转换", PermissionName = ERPPermissions.InventoryTransform.Default, TargetKey = ERPUIViewKeys.InventoryTransform_Paged};
                    
            MenuItem inventory = new MenuItem() { MenuHeader = "库存查询", PermissionName = ERPPermissions.Inventory.Default, TargetKey = ERPUIViewKeys.Inventory_Paged };
            MenuItem inventoryLog = new MenuItem() { MenuHeader = "库存流水", PermissionName = ERPPermissions.InventoryLog.Default, TargetKey = ERPUIViewKeys.InventoryLog_Paged };
            
            MenuItem warehouse = new MenuItem() { MenuHeader = "仓库设置", PermissionName = ERPPermissions.Warehouse.Default, TargetKey = ERPUIViewKeys.Warehouse_Paged };
            MenuItem location = new MenuItem() { MenuHeader = "库位设置", PermissionName = ERPPermissions.Location.Default, TargetKey = ERPUIViewKeys.Location_Paged };
            MenuItem safetyInventory = new MenuItem() { MenuHeader = "安全库存", PermissionName = ERPPermissions.SafetyInventory.Default, TargetKey = ERPUIViewKeys.SafetyInventory_Paged };


            warehouseProcess.Children.Add(salesOut);
            warehouseProcess.Children.Add(salesReturn);
            warehouseProcess.Children.Add(purchaseStorage);
            warehouseProcess.Children.Add(purchaseReturn);
            warehouseProcess.Children.Add(workOrderOut);
            warehouseProcess.Children.Add(workOrderStorage);
            warehouseProcess.Children.Add(workOrderReturn);
            warehouseProcess.Children.Add(otherStorage);
            warehouseProcess.Children.Add(otherOut);
            warehouseProcess.Children.Add(inventoryMove);
            warehouseProcess.Children.Add(inventoryCheck);
            //warehouseProcess.Children.Add(inventoryTransform);

            warehouseReport.Children.Add(inventory);
            warehouseReport.Children.Add(inventoryLog);

            warehouseSetting.Children.Add(warehouse);
            warehouseSetting.Children.Add(location);
            warehouseSetting.Children.Add(safetyInventory);


            warehouseMenu.Children.Add(warehouseProcess);
            warehouseMenu.Children.Add(warehouseReport);
            warehouseMenu.Children.Add(warehouseSetting);

            menuItems.Add(warehouseMenu);
            #endregion


            #region 质量管理
            MenuItem qualityMenu = new MenuItem() { MenuHeader = "质量管理", PermissionName = ERPPermissions.QualityManagement.Default };
            MenuItem qualityProcess = new MenuItem() { MenuHeader = "业务流程", PermissionName = ERPPermissions.WarehouseManagement.BusinessProcess };

            MenuItem arrivalInspection = new MenuItem() { MenuHeader = "来料检验", PermissionName = ERPPermissions.ArrivalInspection.Default, TargetKey = ERPUIViewKeys.ArrivalInspection_Paged };
            MenuItem processInspection = new MenuItem() { MenuHeader = "过程检验", PermissionName = ERPPermissions.ProcessInspection.Default, TargetKey = ERPUIViewKeys.ProcessInspection_Paged };
            MenuItem finalInspection = new MenuItem() { MenuHeader = "产品终检", PermissionName = ERPPermissions.FinalInspection.Default, TargetKey = ERPUIViewKeys.FinalInspection_Paged };


            qualityProcess.Children.Add(arrivalInspection);
            qualityProcess.Children.Add(processInspection);
            qualityProcess.Children.Add(finalInspection);

            qualityMenu.Children.Add(qualityProcess);

            menuItems.Add(qualityMenu);
            #endregion
        }
    }
}
