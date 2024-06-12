using DevExpress.Mvvm.ModuleInjection;
using Lanpuda.Client.Mvvm;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lanpuda.ERP;
using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.ProductUnits;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.Customers.Edits;
using Lanpuda.ERP.SalesManagement.SalesOrders.Edits;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.SalesManagement.SalesPrices;
using Lanpuda.ERP.SalesManagement.SalesPrices.Edits;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Edits;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Edits;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.InventoryLogs;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.Locations.Edits;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Edits;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Edits;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Edits;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Edits;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Edits;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves.Edits;
using Lanpuda.ERP.WarehouseManagement.OtherOuts;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Edits;
using Lanpuda.ERP.WarehouseManagement.OtherStorages;
using Lanpuda.ERP.WarehouseManagement.OtherStorages.Edits;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Edits;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Edits;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Edits;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Edits;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Edits;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Edits;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.ProductionManagement.Boms.Edits;
using Lanpuda.ERP.ProductionManagement.MaterialApplies;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Edits;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Edits;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.ProductionManagement.Mpses.Edits;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Edits;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Lanpuda.ERP.ProductionManagement.Workshops.Edits;
using Lanpuda.ERP.QualityManagement.ArrivalInspections;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using Lanpuda.ERP.QualityManagement.FinalInspections;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies;
using Lanpuda.ERP.Homes;

namespace Lanpuda.ERP.UI
{
    public class ERPUIBootstraper
    {
        private IModuleManager _uiModuleManager;
        private readonly IServiceProvider _serviceProvider;

        public ERPUIBootstraper(IModuleManager moduleManager, IServiceProvider serviceProvider)
        {
            _uiModuleManager = moduleManager;
            _serviceProvider = serviceProvider;
        }


        public void Load()
        {

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ERPHome,
           () =>
           {
               var viewModel = _serviceProvider.GetService<HomeViewModel>();
               return viewModel;
           },
          typeof(HomeView)));


            RegisterProductManagementModule();
            RegisterSalesManagementModule();
            RegisterPurchaseManagement();
            RegisterWarehouseManagementModule();
            RegisterProductionManagement();
            RegisterQualityManagement();
        }


        private void RegisterProductManagementModule()
        {

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ProductCategory_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<ERP.BasicData.ProductCategories.ProductCategoryPagedViewModel>();
                 return viewModel;
             },
            typeof(ERP.BasicData.ProductCategories.ProductCategoryPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ProductUnit_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<ProductUnitPagedViewModel>();
                return viewModel;
            },
           typeof(ProductUnitPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Product_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<ProductPagedViewModel>();
                return viewModel;
            },
           typeof(ProductPagedView)));

        }

        private void RegisterSalesManagementModule()
        {
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Customer_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<CustomerPagedViewModel>();
                 return viewModel;
             },
            typeof(CustomerPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Customer_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<CustomerEditViewModel>();
                 return viewModel;
             },
            typeof(CustomerEditView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesOrder_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesOrderPagedViewModel>();
                 return viewModel;
             },
            typeof(SalesOrderPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesOrder_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesOrderEditViewModel>();
                 return viewModel;
             },
            typeof(SalesOrderEditView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesPrice_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesPricePagedViewModel>();
                 return viewModel;
             },
            typeof(SalesPricePagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesPrice_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesPriceEditViewModel>();
                 return viewModel;
             },
            typeof(SalesPriceEditView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesReturnApply_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesReturnApplyPagedViewModel>();
                 return viewModel;
             },
            typeof(SalesReturnApplyPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesReturnApply_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesReturnApplyEditViewModel>();
                 return viewModel;
             },
            typeof(SalesReturnApplyEditView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ShipmentApply_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<ShipmentApplyPagedViewModel>();
                 return viewModel;
             },
            typeof(ShipmentApplyPagedView)));

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ShipmentApply_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<ShipmentApplyEditViewModel>();
                 return viewModel;
             },
            typeof(ShipmentApplyEditView)));


        }

        private void RegisterPurchaseManagement()
        {
            

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Supplier_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SupplierPagedViewModel>();
                 return viewModel;
             },
            typeof(SupplierPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Supplier_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SupplierEditViewModel>();
                 return viewModel;
             },
            typeof(SupplierEditView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchasePrice_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchasePricePagedViewModel>();
                 return viewModel;
             },
            typeof(PurchasePricePagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchasePrice_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchasePriceEditViewModel>();
                 return viewModel;
             },
            typeof(PurchasePriceEditView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseApply_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchaseApplyPagedViewModel>();
                 return viewModel;
             },
            typeof(PurchaseApplyPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseOrder_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchaseOrderPagedViewModel>();
                 return viewModel;
             },
            typeof(PurchaseOrderPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseOrder_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchaseOrderEditViewModel>();
                 return viewModel;
             },
            typeof(PurchaseOrderEditView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ArrivalNotice_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<ArrivalNoticePagedViewModel>();
                 return viewModel;
             },
            typeof(ArrivalNoticePagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ArrivalNotice_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<ArrivalNoticeEditViewModel>();
                 return viewModel;
             },
            typeof(ArrivalNoticeEditView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseReturnApply_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchaseReturnApplyPagedViewModel>();
                 return viewModel;
             },
            typeof(PurchaseReturnApplyPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseReturnApply_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchaseReturnApplyEditViewModel>();
                 return viewModel;
             },
            typeof(PurchaseReturnApplyEditView)));

        }

        private void RegisterWarehouseManagementModule()
        {
            

            //Inventory
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Inventory_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<InventoryPagedViewModel>();
                 return viewModel;
             },
            typeof(InventoryPagedView)));


            //InventoryLog
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.InventoryLog_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<InventoryLogPagedViewModel>();
                 return viewModel;
             },
            typeof(InventoryLogPagedView)));

            //InventoryMove
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.InventoryMove_Paged,
           () =>
           {
               var viewModel = _serviceProvider.GetService<InventoryMovePagedViewModel>();
               return viewModel;
           },
          typeof(InventoryMovePagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.InventoryMove_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<InventoryMoveEditViewModel>();
                 return viewModel;
             },
            typeof(InventoryMoveEditView)));


            ///Location
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Location_Paged,
           () =>
           {
               var viewModel = _serviceProvider.GetService<LocationPagedViewModel>();
               return viewModel;
           },
          typeof(LocationPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Location_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<LocationEditViewModel>();
                 return viewModel;
             },
            typeof(LocationEditView)));


            ///OtherOut
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.OtherOut_Paged,
          () =>
          {
              var viewModel = _serviceProvider.GetService<OtherOutPagedViewModel>();
              return viewModel;
          },
         typeof(OtherOutPagedView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.OtherOut_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<OtherOutEditViewModel>();
                 return viewModel;
             },
            typeof(OtherOutEditView)));


            ///OtherStorage
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.OtherStorage_Paged,
          () =>
          {
              var viewModel = _serviceProvider.GetService<OtherStoragePagedViewModel>();
              return viewModel;
          },
         typeof(OtherStoragePagedView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.OtherStorage_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<OtherStorageEditViewModel>();
                 return viewModel;
             },
            typeof(OtherStorageEditView)));


            ///PurchaseReturn
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseReturn_Paged,
          () =>
          {
              var viewModel = _serviceProvider.GetService<PurchaseReturnPagedViewModel>();
              return viewModel;
          },
         typeof(PurchaseReturnPagedView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseReturn_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchaseReturnEditViewModel>();
                 return viewModel;
             },
            typeof(PurchaseReturnEditView)));



            ///PurchaseStorage
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseStorage_Paged,
          () =>
          {
              var viewModel = _serviceProvider.GetService<PurchaseStoragePagedViewModel>();
              return viewModel;
          },
         typeof(PurchaseStoragePagedView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.PurchaseStorage_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<PurchaseStorageEditViewModel>();
                 return viewModel;
             },
            typeof(PurchaseStorageEditView)));



            ///SafetyInventory
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SafetyInventory_Paged,
          () =>
          {
              var viewModel = _serviceProvider.GetService<SafetyInventoryPagedViewModel>();
              return viewModel;
          },
         typeof(SafetyInventoryPagedView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SafetyInventory_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SafetyInventoryEditViewModel>();
                 return viewModel;
             },
            typeof(SafetyInventoryEditView)));


            ///SalesOut
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesOut_Paged,
          () =>
          {
              var viewModel = _serviceProvider.GetService<SalesOutPagedViewModel>();
              return viewModel;
          },
         typeof(SalesOutPagedView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesOut_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesOutEditViewModel>();
                 return viewModel;
             },
            typeof(SalesOutEditView)));


            ///SalesReturn
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesReturn_Paged,
         () =>
         {
             var viewModel = _serviceProvider.GetService<SalesReturnPagedViewModel>();
             return viewModel;
         },
        typeof(SalesReturnPagedView)));



            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.SalesReturn_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<SalesReturnEditViewModel>();
                 return viewModel;
             },
            typeof(SalesReturnEditView)));

            ///Warehouse
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Warehouse_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<WarehousePagedViewModel>();
                return viewModel;
            },
            typeof(WarehousePagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Warehouse_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WarehouseEditViewModel>();
                 return viewModel;
             },
            typeof(WarehouseEditView)));


            ///WorkOrderOut
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrderOut_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<WorkOrderOutPagedViewModel>();
                return viewModel;
            },
            typeof(WorkOrderOutPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrderOut_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WorkOrderOutEditViewModel>();
                 return viewModel;
             },
            typeof(WorkOrderOutEditView)));



            ///WorkOrderReturn
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrderReturn_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<WorkOrderReturnPagedViewModel>();
                return viewModel;
            },
            typeof(WorkOrderReturnPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrderReturn_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WorkOrderReturnEditViewModel>();
                 return viewModel;
             },
            typeof(WorkOrderReturnEditView)));


            ///WorkOrderStorage
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrderStorage_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<WorkOrderStoragePagedViewModel>();
                return viewModel;
            },
            typeof(WorkOrderStoragePagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrderStorage_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WorkOrderStorageEditViewModel>();
                 return viewModel;
             },
            typeof(WorkOrderStorageEditView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.InventoryCheck_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<InventoryCheckPagedViewModel>();
                return viewModel;
            },
            typeof(InventoryCheckPagedView)));

        }

        private void RegisterProductionManagement()
        {
            


            ///bom
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Bom_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<BomPagedViewModel>();
                 return viewModel;
             },
            typeof(BomPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Bom_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<BomEditViewModel>();
                 return viewModel;
             },
            typeof(BomEditView)));


            //MaterialApply
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.MaterialApply_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<MaterialApplyPagedViewModel>();
                 return viewModel;
             },
            typeof(MaterialApplyPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.MaterialApply_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<MaterialApplyEditViewModel>();
                 return viewModel;
             },
            typeof(MaterialApplyEditView)));


            //MaterialReturnApply
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.MaterialReturnApply_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<MaterialReturnApplyPagedViewModel>();
                 return viewModel;
             },
            typeof(MaterialReturnApplyPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.MaterialReturnApply_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<MaterialReturnApplyEditViewModel>();
                 return viewModel;
             },
            typeof(MaterialReturnApplyEditView)));


            //Mps
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Mps_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<MpsPagedViewModel>();
                 return viewModel;
             },
            typeof(MpsPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Mps_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<MpsEditViewModel>();
                 return viewModel;
             },
            typeof(MpsEditView)));

          


            //WorkOrder
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrder_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<WorkOrderPagedViewModel>();
                return viewModel;
            },
            typeof(WorkOrderPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrder_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WorkOrderEditViewModel>();
                 return viewModel;
             },
            typeof(WorkOrderEditView)));


            //Workshop
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Workshop_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WorkshopPagedViewModel>();
                 return viewModel;
             },
            typeof(WorkshopPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.Workshop_Edit,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WorkshopEditViewModel>();
                 return viewModel;
             },
            typeof(WorkshopEditView)));

            //WorkOrderStorageApply

            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.WorkOrderStorageApply_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<WorkOrderStorageApplyPagedViewModel>();
                 return viewModel;
             },
            typeof(WorkOrderStorageApplyPagedView)));

        }

        private void RegisterQualityManagement()
        {
            
            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ArrivalInspection_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<ArrivalInspectionPagedViewModel>();
                 return viewModel;
             },
            typeof(ArrivalInspectionPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.ProcessInspection_Paged,
             () =>
             {
                 var viewModel = _serviceProvider.GetService<ProcessInspectionPagedViewModel>();
                 return viewModel;
             },
            typeof(ProcessInspectionPagedView)));


            _uiModuleManager.Register(RegionNames.MainContentRegion, new Module(ERPUIViewKeys.FinalInspection_Paged,
            () =>
            {
                var viewModel = _serviceProvider.GetService<FinalInspectionPagedViewModel>();
                return viewModel;
            },
            typeof(FinalInspectionPagedView)));
        }
    }
}
