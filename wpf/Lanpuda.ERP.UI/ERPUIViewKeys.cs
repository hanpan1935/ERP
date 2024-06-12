using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.UI
{
    public static class ERPUIViewKeys
    {
        public const string ERPHome = "ERPHome";

        public const string ProductCategory_Paged = "ProductCategory_Paged";
        public const string Product_Paged = "Product_Paged";
        public const string ProductUnit_Paged = "ProductUnit_Paged";

        #region Sales
        public const string Customer_Paged = "Customer_Paged";
        public const string Customer_Edit = "Customer_Edit";

        public const string SalesOrder_Paged = "SalesOrder_Paged";
        public const string SalesOrder_Edit = "SalesOrder_Edit";

        public const string SalesPrice_Paged = "SalesPrice_Paged";
        public const string SalesPrice_Edit = "SalesPrice_Edit";


        public const string SalesReturnApply_Paged = "SalesReturnApply_Paged";
        public const string SalesReturnApply_Edit = "SalesReturnApply_Edit";

        public const string ShipmentApply_Paged = "ShipmentApply_Paged";
        public const string ShipmentApply_Edit = "ShipmentApply_Edit";
        #endregion


        #region Purchase

        public const string Supplier_Paged = "Supplier_Paged";
        public const string Supplier_Edit = "Supplier_Edit";

        public const string PurchasePrice_Paged = "PurchasePrice_Paged";
        public const string PurchasePrice_Edit = "PurchasePrice_Edit";

        public const string PurchaseOrder_Paged = "PurchaseOrder_Paged";

        public const string PurchaseApply_Paged = "PurchaseApply_Paged";

        public const string PurchaseOrder_Edit = "PurchaseOrder_Edit";

        public const string ArrivalNotice_Paged = "ArrivalNotice_Paged";
        public const string ArrivalNotice_Edit = "ArrivalNotice_Edit";

        public const string PurchaseReturnApply_Paged = "PurchaseReturnApply_Paged";
        public const string PurchaseReturnApply_Edit = "PurchaseReturnApply_Edit";

        #endregion


        #region Production
        public const string Bom_Paged = "Bom_Paged";
        public const string Bom_Edit = "Bom_Edit";


        public const string MaterialApply_Paged = "MaterialApply_Paged";
        public const string MaterialApply_Edit = "MaterialApply_Edit";


        public const string MaterialReturnApply_Paged = "MaterialReturnApply_Paged";
        public const string MaterialReturnApply_Edit = "MaterialReturnApply_Edit";

        public const string Mrp_Paged = "Mrp_Paged";

        public const string Mps_Paged = "Mps_Paged";
        public const string Mps_Edit = "Mps_Edit";


        public const string WorkOrder_Paged = "WorkOrder_Paged";
        public const string WorkOrder_Edit = "WorkOrder_Edit";

        public const string WorkOrderStorageApply_Paged = "WorkOrderStorageApply_Paged";


        public const string Workshop_Paged = "Workshop_Paged";
        public const string Workshop_Edit = "Workshop_Edit";

        #endregion


        #region Warehouse
        public const string Inventory_Paged = "Inventory_Paged";
        public const string InventoryLog_Paged = "InventoryLog_Paged";


        public const string InventoryMove_Paged = "InventoryMove_Paged";
        public const string InventoryMove_Edit = "InventoryMove_Edit";

        public const string Location_Paged = "Location_Paged";
        public const string Location_Edit = "Location_Edit";


        public const string OtherOut_Paged = "OtherOut_Paged";
        public const string OtherOut_Edit = "OtherOut_Edit";

        public const string OtherStorage_Paged = "OtherStorage_Paged";
        public const string OtherStorage_Edit = "OtherStorage_Edit";

        public const string PurchaseReturn_Paged = "PurchaseReturn_Paged";
        public const string PurchaseReturn_Edit = "PurchaseReturn_Edit";

        public const string PurchaseStorage_Paged = "PurchaseStorage_Paged";
        public const string PurchaseStorage_Edit = "PurchaseStorage_Edit";

        public const string SafetyInventory_Paged = "SafetyInventory_Paged";
        public const string SafetyInventory_Edit = "SafetyInventory_Edit";

        public const string SalesOut_Paged = "SalesOut_Paged";
        public const string SalesOut_Edit = "SalesOut_Edit";


        public const string SalesReturn_Paged = "SalesReturn_Paged";
        public const string SalesReturn_Edit = "SalesReturn_Edit";


        public const string Warehouse_Paged = "Warehouse_Paged";
        public const string Warehouse_Edit = "Warehouse_Edit";

        public const string WorkOrderOut_Paged = "WorkOrderOut_Paged";
        public const string WorkOrderOut_Edit = "WorkOrderOut_Edit";

        public const string WorkOrderReturn_Paged = "WorkOrderReturn_Paged";
        public const string WorkOrderReturn_Edit = "WorkOrderReturn_Edit";

        public const string WorkOrderStorage_Paged = "WorkOrderStorage_Paged";
        public const string WorkOrderStorage_Edit = "WorkOrderStorage_Edit";

        public const string InventoryCheck_Paged = "InventoryCheck_Paged";

        public const string InventoryTransform_Paged = "InventoryTransform_Paged";

        #endregion

        #region QualityManagement
        public const string ArrivalInspection_Paged = "ArrivalInspection_Paged";
        public const string FinalInspection_Paged = "FinalInspection_Paged";
        public const string ProcessInspection_Paged = "ProcessInspection_Paged";
        #endregion
    }
}
