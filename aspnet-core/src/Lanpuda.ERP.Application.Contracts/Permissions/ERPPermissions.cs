using System.Collections;
using Volo.Abp.Reflection;

namespace Lanpuda.ERP.Permissions;

public class ERPPermissions
{
    public const string GroupName = "ERP";


    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(ERPPermissions));
    }


    public class MenuType   
    {
        public const string BasicSetting = ".BasicSetting";
        public const string BusinessProcess = ".BusinessProcess";
        public const string Report = ".Report";
    }

    #region 产品管理
    public class BasicData
    {
        public const string Default = GroupName + ".BasicData";
        public const string BasicSetting = Default + MenuType.BasicSetting;
    }

    public class ProductCategory
    {
        public const string Default = BasicData.BasicSetting + ".ProductCategory";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class ProductUnit
    {
        public const string Default = BasicData.BasicSetting + ".ProductUnit";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class Product
    {
        public const string Default = BasicData.BasicSetting + ".Product";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    #endregion

    #region 销售管理
    public class SalesManagement
    {
        public const string Default = GroupName + ".SalesManagement";
        public const string BusinessProcess = Default + MenuType.BusinessProcess;
        public const string BasicSetting = Default + MenuType.BasicSetting;
    }


    public class SalesPrice
    {
        public const string Default = SalesManagement.BusinessProcess + ".SalesPrice";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }


    public class SalesOrder
    {
        public const string Default = SalesManagement.BusinessProcess + ".SalesOrder";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
        public const string Close = Default + ".Close";
        public const string CreateMps = Default + ".CreateMps";
    }


    public class ShipmentApply
    {
        public const string Default = SalesManagement.BusinessProcess + ".ShipmentApply";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";

    }


    public class SalesReturnApply
    {
        public const string Default = SalesManagement.BusinessProcess + ".SalesReturnApply";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }



    public class Customer
    {
        public const string Default = SalesManagement.BasicSetting + ".Customer";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    #endregion


    #region 采购管理


    public class PurchaseManagement
    {
        public const string Default = GroupName + ".PurchaseManagement";
        public const string BusinessProcess = Default + MenuType.BusinessProcess;
        public const string BasicSetting = Default + MenuType.BasicSetting;
        public const string Report = Default + MenuType.Report;
    }


    public class PurchasePrice
    {
        public const string Default = PurchaseManagement.BusinessProcess + ".PurchasePrice";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }


    public class PurchaseApply
    {
        public const string Default = PurchaseManagement.BusinessProcess + ".PurchaseApply";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
        public const string Accept = Default + ".Accept";
        public const string CreatePurchaseOrder = Default + ".CreatePurchaseOrder";
    }


    public class PurchaseOrder
    {
        public const string Default = PurchaseManagement.BusinessProcess + ".PurchaseOrder";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
        public const string Close = Default + ".Close";
    }

    public class ArrivalNotice
    {
        public const string Default = PurchaseManagement.BusinessProcess + ".ArrivalNotice";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }


    public class PurchaseReturnApply
    {
        public const string Default = PurchaseManagement.BusinessProcess + ".PurchaseReturnApply";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }


    public class Supplier
    {
        public const string Default = PurchaseManagement.BasicSetting + ".Supplier";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    #endregion


    #region ProductionManagement

    public class ProductionManagement
    {
        public const string Default = GroupName + ".ProductionManagement";
        public const string BusinessProcess = Default + MenuType.BusinessProcess;
        public const string BasicSetting = Default + MenuType.BasicSetting;
    }


    public class Mps
    {
        public const string Default = ProductionManagement.BusinessProcess + ".Mps";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
        public const string MRP = Default + ".MRP";
        public const string Profile = Default + ".Profile";
    }

    public class WorkOrder
    {
        public const string Default = ProductionManagement.BusinessProcess + ".WorkOrder";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }



    public class MaterialApply
    {
        public const string Default = ProductionManagement.BusinessProcess + ".MaterialApply";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }


    public class WorkOrderStorageApply
    {
        public const string Default = ProductionManagement.BusinessProcess + ".WorkOrderStorageApply";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }


    public class MaterialReturnApply
    {
        public const string Default = ProductionManagement.BusinessProcess + ".MaterialReturnApply";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }


    public class Bom
    {
        public const string Default = ProductionManagement.BasicSetting + ".Bom";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }


    public class Workshop
    {
        public const string Default = ProductionManagement.BasicSetting + ".Workshop";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
    #endregion


    #region WarehouseManagement
    public class WarehouseManagement
    {
        public const string Default = GroupName + ".WarehouseManagement";
        public const string BusinessProcess = Default + MenuType.BusinessProcess;
        public const string BasicSetting = Default + MenuType.BasicSetting;
        public const string Report = Default + MenuType.Report;
    }


    public class SalesOut
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".SalesOut";
        public const string Update = Default + ".Update";
        public const string Out = Default + ".Out";
    }


    public class SalesReturn
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".SalesReturn";
        public const string Update = Default + ".Update";
        public const string Storage = Default + ".Storage";
    }


    public class PurchaseStorage
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".PurchaseStorage";
        public const string Update = Default + ".Update";
        public const string Storage = Default + ".Storage";
    }


    public class PurchaseReturn
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".PurchaseReturn";
        public const string Update = Default + ".Update";
        public const string Out = Default + ".Out";
    }


    public class WorkOrderOut
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".WorkOrderOut";
        public const string Update = Default + ".Update";
        public const string Out = Default + ".Out";
    }


    public class WorkOrderReturn
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".WorkOrderReturn";
        public const string Update = Default + ".Update";
        public const string Storage = Default + ".Storage";
    }


    public class WorkOrderStorage
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".WorkOrderStorage";
        public const string Update = Default + ".Update";
        public const string Storage = Default + ".Storage";
    }

    public class OtherOut
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".OtherOut";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Out  = Default + ".Out";
    }



    public class OtherStorage
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".OtherStorage";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Storage = Default + ".Storage";
    }



    public class InventoryMove
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".InventoryMove";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Move = Default + ".Move";
    }



    public class InventoryCheck
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".InventoryCheck";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }


    public class InventoryTransform
    {
        public const string Default = WarehouseManagement.BusinessProcess + ".InventoryTransform";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
        public const string Confirm = Default + ".Confirm";
    }

    public class Inventory
    {
        public const string Default = WarehouseManagement.Report + ".Inventory";
    }


    public class InventoryLog
    {
        public const string Default = WarehouseManagement.Report + ".InventoryLog";
    }


    public class Warehouse
    {
        public const string Default = WarehouseManagement.BasicSetting + ".Warehouse";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }



    public class Location
    {
        public const string Default = WarehouseManagement.BasicSetting + ".Location";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }


    public class SafetyInventory
    {
        public const string Default = WarehouseManagement.BasicSetting + ".SafetyInventory";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }


    #endregion



    #region QualityManagement

    public class QualityManagement
    {
        public const string Default = GroupName + ".QualityManagement";
        public const string BusinessProcess = Default + MenuType.BusinessProcess;
        public const string BasicSetting = Default + MenuType.BasicSetting;
    }

    public class ArrivalInspection
    {
        public const string Default = QualityManagement.BusinessProcess + ".ArrivalInspection";
        public const string Update = Default + ".Update";
        public const string Confirm = Default + ".Confirm";
    }


    public class ProcessInspection
    {
        public const string Default = QualityManagement.BusinessProcess + ".ProcessInspection";
        public const string Update = Default + ".Update";
        public const string Confirm = Default + ".Confirm";
    }

    public class FinalInspection
    {
        public const string Default = QualityManagement.BusinessProcess + ".FinalInspection";
        public const string Update = Default + ".Update";
        public const string Confirm = Default + ".Confirm";
    }
    #endregion








}
