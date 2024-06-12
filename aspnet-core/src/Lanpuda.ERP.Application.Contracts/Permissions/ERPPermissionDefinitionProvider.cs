using Lanpuda.ERP.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Lanpuda.ERP.Permissions;

public class ERPPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(ERPPermissions.GroupName, L("Permission:ERP"));


        #region basicData
        //1
        var basicData = myGroup.AddPermission(ERPPermissions.BasicData.Default, L("Permission:BasicData"));

        //2
        var basicDataBasicSetting = basicData.AddChild(ERPPermissions.BasicData.BasicSetting, L("Permission:BasicSetting"));


        //3
        var productCategory = basicDataBasicSetting.AddChild(ERPPermissions.ProductCategory.Default, L("Permission:ProductCategory"));
        var productUnit = basicDataBasicSetting.AddChild(ERPPermissions.ProductUnit.Default, L("Permission:ProductUnit"));
        var product = basicDataBasicSetting.AddChild(ERPPermissions.Product.Default, L("Permission:Product"));


        //4

        product.AddChild(ERPPermissions.Product.Create, L("Permission:Create"));
        product.AddChild(ERPPermissions.Product.Update, L("Permission:Update"));
        product.AddChild(ERPPermissions.Product.Delete, L("Permission:Delete"));

        productUnit.AddChild(ERPPermissions.ProductUnit.Create, L("Permission:Create"));
        productUnit.AddChild(ERPPermissions.ProductUnit.Update, L("Permission:Update"));
        productUnit.AddChild(ERPPermissions.ProductUnit.Delete, L("Permission:Delete"));


        productCategory.AddChild(ERPPermissions.ProductCategory.Create, L("Permission:Create"));
        productCategory.AddChild(ERPPermissions.ProductCategory.Update, L("Permission:Update"));
        productCategory.AddChild(ERPPermissions.ProductCategory.Delete, L("Permission:Delete"));

      

       
        #endregion


        #region SalesManagement
        //1
        var salesManagement = myGroup.AddPermission(ERPPermissions.SalesManagement.Default, L("Permission:SalesManagement"));

        var salesManagementBusinessProcess = salesManagement.AddChild(ERPPermissions.SalesManagement.BusinessProcess, L("Permission:BusinessProcess"));
        var salesManagementBasicSetting = salesManagement.AddChild(ERPPermissions.SalesManagement.BasicSetting, L("Permission:BasicSetting"));

        var salesPrice = salesManagementBusinessProcess.AddChild(ERPPermissions.SalesPrice.Default, L("Permission:SalesPrice"));
        var salesOrder = salesManagementBusinessProcess.AddChild(ERPPermissions.SalesOrder.Default, L("Permission:SalesOrder"));
        var shipmentApply = salesManagementBusinessProcess.AddChild(ERPPermissions.ShipmentApply.Default, L("Permission:ShipmentApply"));
        var salesReturnApply = salesManagementBusinessProcess.AddChild(ERPPermissions.SalesReturnApply.Default, L("Permission:SalesReturnApply"));

        var customer = salesManagementBasicSetting.AddChild(ERPPermissions.Customer.Default, L("Permission:Customer"));

        salesPrice.AddChild(ERPPermissions.SalesPrice.Create, L("Permission:Create"));
        salesPrice.AddChild(ERPPermissions.SalesPrice.Update, L("Permission:Update"));
        salesPrice.AddChild(ERPPermissions.SalesPrice.Delete, L("Permission:Delete"));

        salesOrder.AddChild(ERPPermissions.SalesOrder.Create, L("Permission:Create"));
        salesOrder.AddChild(ERPPermissions.SalesOrder.Update, L("Permission:Update"));
        salesOrder.AddChild(ERPPermissions.SalesOrder.Close, L("Permission:Close"));
        salesOrder.AddChild(ERPPermissions.SalesOrder.Confirm, L("Permission:Confirm"));
        salesOrder.AddChild(ERPPermissions.SalesOrder.Delete, L("Permission:Delete"));
        salesOrder.AddChild(ERPPermissions.SalesOrder.CreateMps, L("Permission:CreateMps"));

        shipmentApply.AddChild(ERPPermissions.ShipmentApply.Create, L("Permission:Create"));
        shipmentApply.AddChild(ERPPermissions.ShipmentApply.Update, L("Permission:Update"));
        shipmentApply.AddChild(ERPPermissions.ShipmentApply.Confirm, L("Permission:Confirm"));
        shipmentApply.AddChild(ERPPermissions.ShipmentApply.Delete, L("Permission:Delete"));

        salesReturnApply.AddChild(ERPPermissions.SalesReturnApply.Create, L("Permission:Create"));
        salesReturnApply.AddChild(ERPPermissions.SalesReturnApply.Update, L("Permission:Update"));
        salesReturnApply.AddChild(ERPPermissions.SalesReturnApply.Confirm, L("Permission:Confirm"));
        salesReturnApply.AddChild(ERPPermissions.SalesReturnApply.Delete, L("Permission:Delete"));

        customer.AddChild(ERPPermissions.Customer.Create, L("Permission:Create"));
        customer.AddChild(ERPPermissions.Customer.Update, L("Permission:Update"));
        customer.AddChild(ERPPermissions.Customer.Delete, L("Permission:Delete"));
        #endregion


        #region PurchaseManagement
        var purchaseManagement = myGroup.AddPermission(ERPPermissions.PurchaseManagement.Default, L("Permission:PurchaseManagement"));

        var purchaseManagementBusinessProcess = purchaseManagement.AddChild(ERPPermissions.PurchaseManagement.BusinessProcess, L("Permission:BusinessProcess"));
        var purchaseManagementBasicSetting = purchaseManagement.AddChild(ERPPermissions.PurchaseManagement.BasicSetting, L("Permission:BasicSetting"));


        var purchasePrice = purchaseManagementBusinessProcess.AddChild(ERPPermissions.PurchasePrice.Default, L("Permission:PurchasePrice"));
        var purchaseApply = purchaseManagementBusinessProcess.AddChild(ERPPermissions.PurchaseApply.Default, L("Permission:PurchaseApply"));
        var purchaseOrder = purchaseManagementBusinessProcess.AddChild(ERPPermissions.PurchaseOrder.Default, L("Permission:PurchaseOrder"));
        var arrivalNotice = purchaseManagementBusinessProcess.AddChild(ERPPermissions.ArrivalNotice.Default, L("Permission:ArrivalNotice"));
        var purchaseReturnApply = purchaseManagementBusinessProcess.AddChild(ERPPermissions.PurchaseReturnApply.Default, L("Permission:PurchaseReturnApply"));

        var supplier = purchaseManagementBasicSetting.AddChild(ERPPermissions.Supplier.Default, L("Permission:Supplier"));


        purchasePrice.AddChild(ERPPermissions.PurchasePrice.Create, L("Permission:Create"));
        purchasePrice.AddChild(ERPPermissions.PurchasePrice.Update, L("Permission:Update"));
        purchasePrice.AddChild(ERPPermissions.PurchasePrice.Delete, L("Permission:Delete"));

        purchaseApply.AddChild(ERPPermissions.PurchaseApply.Create, L("Permission:Create"));
        purchaseApply.AddChild(ERPPermissions.PurchaseApply.Update, L("Permission:Update"));
        purchaseApply.AddChild(ERPPermissions.PurchaseApply.Confirm, L("Permission:Confirm"));
        purchaseApply.AddChild(ERPPermissions.PurchaseApply.Accept, L("Permission:Accept"));
        purchaseApply.AddChild(ERPPermissions.PurchaseApply.Delete, L("Permission:Delete"));
        purchaseApply.AddChild(ERPPermissions.PurchaseApply.CreatePurchaseOrder, L("Permission:CreatePurchaseOrder"));


        purchaseOrder.AddChild(ERPPermissions.PurchaseOrder.Create, L("Permission:Create"));
        purchaseOrder.AddChild(ERPPermissions.PurchaseOrder.Update, L("Permission:Update"));
        purchaseOrder.AddChild(ERPPermissions.PurchaseOrder.Confirm, L("Permission:Confirm"));
        purchaseOrder.AddChild(ERPPermissions.PurchaseOrder.Close, L("Permission:Close"));
        purchaseOrder.AddChild(ERPPermissions.PurchaseOrder.Delete, L("Permission:Delete"));

        arrivalNotice.AddChild(ERPPermissions.ArrivalNotice.Create, L("Permission:Create"));
        arrivalNotice.AddChild(ERPPermissions.ArrivalNotice.Update, L("Permission:Update"));
        arrivalNotice.AddChild(ERPPermissions.ArrivalNotice.Confirm, L("Permission:Confirm"));
        arrivalNotice.AddChild(ERPPermissions.ArrivalNotice.Delete, L("Permission:Delete"));

        purchaseReturnApply.AddChild(ERPPermissions.PurchaseReturnApply.Create, L("Permission:Create"));
        purchaseReturnApply.AddChild(ERPPermissions.PurchaseReturnApply.Update, L("Permission:Update"));
        purchaseReturnApply.AddChild(ERPPermissions.PurchaseReturnApply.Confirm, L("Permission:Confirm"));
        purchaseReturnApply.AddChild(ERPPermissions.PurchaseReturnApply.Delete, L("Permission:Delete"));


        supplier.AddChild(ERPPermissions.Supplier.Create, L("Permission:Create"));
        supplier.AddChild(ERPPermissions.Supplier.Update, L("Permission:Update"));
        supplier.AddChild(ERPPermissions.Supplier.Delete, L("Permission:Delete"));

        #endregion

        #region ProductionManagement
        var productionManagement = myGroup.AddPermission(ERPPermissions.ProductionManagement.Default, L("Permission:ProductionManagement"));

        var productionManagementBusinessProcess = productionManagement.AddChild(ERPPermissions.ProductionManagement.BusinessProcess, L("Permission:BusinessProcess"));
        var productionManagementBasicSetting = productionManagement.AddChild(ERPPermissions.ProductionManagement.BasicSetting, L("Permission:BasicSetting"));

        var mps = productionManagementBusinessProcess.AddChild(ERPPermissions.Mps.Default, L("Permission:Mps"));
        var workOrder = productionManagementBusinessProcess.AddChild(ERPPermissions.WorkOrder.Default, L("Permission:WorkOrder"));
        var materialApply = productionManagementBusinessProcess.AddChild(ERPPermissions.MaterialApply.Default, L("Permission:MaterialApply"));
        var workOrderStorageApply = productionManagementBusinessProcess.AddChild(ERPPermissions.WorkOrderStorageApply.Default, L("Permission:WorkOrderStorageApply"));
        var materialReturnApply = productionManagementBusinessProcess.AddChild(ERPPermissions.MaterialReturnApply.Default, L("Permission:MaterialReturnApply"));

        var workshop = productionManagementBasicSetting.AddChild(ERPPermissions.Workshop.Default, L("Permission:Workshop"));
        var bom = productionManagementBasicSetting.AddChild(ERPPermissions.Bom.Default, L("Permission:Bom"));

        mps.AddChild(ERPPermissions.Mps.Create, L("Permission:Create"));
        mps.AddChild(ERPPermissions.Mps.Update, L("Permission:Update"));
        mps.AddChild(ERPPermissions.Mps.Confirm, L("Permission:Confirm"));
        mps.AddChild(ERPPermissions.Mps.Delete, L("Permission:Delete"));
        mps.AddChild(ERPPermissions.Mps.MRP, L("Permission:MRP"));
        mps.AddChild(ERPPermissions.Mps.Profile, L("Permission:Profile"));

        workOrder.AddChild(ERPPermissions.WorkOrder.Create, L("Permission:Create"));
        workOrder.AddChild(ERPPermissions.WorkOrder.Update, L("Permission:Update"));
        workOrder.AddChild(ERPPermissions.WorkOrder.Confirm, L("Permission:Confirm"));
        workOrder.AddChild(ERPPermissions.WorkOrder.Delete, L("Permission:Delete"));

        materialApply.AddChild(ERPPermissions.MaterialApply.Create, L("Permission:Create"));
        materialApply.AddChild(ERPPermissions.MaterialApply.Update, L("Permission:Update"));
        materialApply.AddChild(ERPPermissions.MaterialApply.Confirm, L("Permission:Confirm"));
        materialApply.AddChild(ERPPermissions.MaterialApply.Delete, L("Permission:Delete"));

        workOrderStorageApply.AddChild(ERPPermissions.WorkOrderStorageApply.Create, L("Permission:Create"));
        workOrderStorageApply.AddChild(ERPPermissions.WorkOrderStorageApply.Update, L("Permission:Update"));
        workOrderStorageApply.AddChild(ERPPermissions.WorkOrderStorageApply.Confirm, L("Permission:Confirm"));
        workOrderStorageApply.AddChild(ERPPermissions.WorkOrderStorageApply.Delete, L("Permission:Delete"));


        materialReturnApply.AddChild(ERPPermissions.MaterialReturnApply.Create, L("Permission:Create"));
        materialReturnApply.AddChild(ERPPermissions.MaterialReturnApply.Update, L("Permission:Update"));
        materialReturnApply.AddChild(ERPPermissions.MaterialReturnApply.Confirm, L("Permission:Confirm"));
        materialReturnApply.AddChild(ERPPermissions.MaterialReturnApply.Delete, L("Permission:Delete"));

        workshop.AddChild(ERPPermissions.Workshop.Create, L("Permission:Create"));
        workshop.AddChild(ERPPermissions.Workshop.Update, L("Permission:Update"));
        workshop.AddChild(ERPPermissions.Workshop.Delete, L("Permission:Delete"));

        bom.AddChild(ERPPermissions.Bom.Create, L("Permission:Create"));
        bom.AddChild(ERPPermissions.Bom.Update, L("Permission:Update"));
        bom.AddChild(ERPPermissions.Bom.Delete, L("Permission:Delete"));
        #endregion

        #region WarehouseManagement

        var warehouseManagement = myGroup.AddPermission(ERPPermissions.WarehouseManagement.Default, L("Permission:WarehouseManagement"));
         
        var warehouseManagementBusinessProcess = warehouseManagement.AddChild(ERPPermissions.WarehouseManagement.BusinessProcess, L("Permission:BusinessProcess"));
        var warehouseManagementReport = warehouseManagement.AddChild(ERPPermissions.WarehouseManagement.Report, L("Permission:Report"));
        var warehouseManagementBasicSetting = warehouseManagement.AddChild(ERPPermissions.WarehouseManagement.BasicSetting, L("Permission:BasicSetting"));

        var salesOut = warehouseManagementBusinessProcess.AddChild(ERPPermissions.SalesOut.Default, L("Permission:SalesOut"));
        var salesReturn = warehouseManagementBusinessProcess.AddChild(ERPPermissions.SalesReturn.Default, L("Permission:SalesReturn"));
        var purchaseStorage = warehouseManagementBusinessProcess.AddChild(ERPPermissions.PurchaseStorage.Default, L("Permission:PurchaseStorage"));
        var purchaseReturn = warehouseManagementBusinessProcess.AddChild(ERPPermissions.PurchaseReturn.Default, L("Permission:PurchaseReturn"));
        var workOrderOut = warehouseManagementBusinessProcess.AddChild(ERPPermissions.WorkOrderOut.Default, L("Permission:WorkOrderOut"));
        var workOrderStorage = warehouseManagementBusinessProcess.AddChild(ERPPermissions.WorkOrderStorage.Default, L("Permission:WorkOrderStorage"));
        var workOrderReturn = warehouseManagementBusinessProcess.AddChild(ERPPermissions.WorkOrderReturn.Default, L("Permission:WorkOrderReturn"));
        var otherStorage = warehouseManagementBusinessProcess.AddChild(ERPPermissions.OtherStorage.Default, L("Permission:OtherStorage"));
        var otherOut = warehouseManagementBusinessProcess.AddChild(ERPPermissions.OtherOut.Default, L("Permission:OtherOut"));
        var inventoryMove = warehouseManagementBusinessProcess.AddChild(ERPPermissions.InventoryMove.Default, L("Permission:InventoryMove"));
        var inventoryCheck = warehouseManagementBusinessProcess.AddChild(ERPPermissions.InventoryCheck.Default, L("Permission:InventoryCheck"));
        var inventoryTransform = warehouseManagementBusinessProcess.AddChild(ERPPermissions.InventoryTransform.Default, L("Permission:InventoryTransform"));

        var inventory = warehouseManagementReport.AddChild(ERPPermissions.Inventory.Default, L("Permission:Inventory"));
        var inventoryLog = warehouseManagementReport.AddChild(ERPPermissions.InventoryLog.Default, L("Permission:InventoryLog"));

        var warehouse = warehouseManagementBasicSetting.AddChild(ERPPermissions.Warehouse.Default, L("Permission:Warehouse"));
        var location = warehouseManagementBasicSetting.AddChild(ERPPermissions.Location.Default, L("Permission:Location"));
        var safetyInventory = warehouseManagementBasicSetting.AddChild(ERPPermissions.SafetyInventory.Default, L("Permission:SafetyInventory"));

        
        salesOut.AddChild(ERPPermissions.SalesOut.Update, L("Permission:Update"));
        salesOut.AddChild(ERPPermissions.SalesOut.Out, L("Permission:Out"));

        salesReturn.AddChild(ERPPermissions.SalesReturn.Update, L("Permission:Update"));
        salesReturn.AddChild(ERPPermissions.SalesReturn.Storage, L("Permission:Storage"));

        purchaseStorage.AddChild(ERPPermissions.PurchaseStorage.Update, L("Permission:Update"));
        purchaseStorage.AddChild(ERPPermissions.PurchaseStorage.Storage, L("Permission:Storage"));

        purchaseReturn.AddChild(ERPPermissions.PurchaseReturn.Update, L("Permission:Update"));
        purchaseReturn.AddChild(ERPPermissions.PurchaseReturn.Out, L("Permission:Out"));


        workOrderOut.AddChild(ERPPermissions.WorkOrderOut.Update, L("Permission:Update"));
        workOrderOut.AddChild(ERPPermissions.WorkOrderOut.Out, L("Permission:Out"));

        workOrderStorage.AddChild(ERPPermissions.WorkOrderStorage.Update, L("Permission:Update"));
        workOrderStorage.AddChild(ERPPermissions.WorkOrderStorage.Storage, L("Permission:Storage"));

        workOrderReturn.AddChild(ERPPermissions.WorkOrderReturn.Update, L("Permission:Update"));
        workOrderReturn.AddChild(ERPPermissions.WorkOrderReturn.Storage, L("Permission:Storage"));


        otherStorage.AddChild(ERPPermissions.OtherStorage.Create, L("Permission:Create"));
        otherStorage.AddChild(ERPPermissions.OtherStorage.Update, L("Permission:Update"));
        otherStorage.AddChild(ERPPermissions.OtherStorage.Storage, L("Permission:Storage"));
        otherStorage.AddChild(ERPPermissions.OtherStorage.Delete, L("Permission:Delete"));

        otherOut.AddChild(ERPPermissions.OtherOut.Create, L("Permission:Create"));
        otherOut.AddChild(ERPPermissions.OtherOut.Update, L("Permission:Update"));
        otherOut.AddChild(ERPPermissions.OtherOut.Out, L("Permission:Out"));
        otherOut.AddChild(ERPPermissions.OtherOut.Delete, L("Permission:Delete"));


        inventoryMove.AddChild(ERPPermissions.InventoryMove.Create, L("Permission:Create"));
        inventoryMove.AddChild(ERPPermissions.InventoryMove.Update, L("Permission:Update"));
        inventoryMove.AddChild(ERPPermissions.InventoryMove.Move, L("Permission:Move"));
        inventoryMove.AddChild(ERPPermissions.InventoryMove.Delete, L("Permission:Delete"));


        inventoryCheck.AddChild(ERPPermissions.InventoryCheck.Create, L("Permission:Create"));
        inventoryCheck.AddChild(ERPPermissions.InventoryCheck.Update, L("Permission:Update"));
        inventoryCheck.AddChild(ERPPermissions.InventoryCheck.Confirm, L("Permission:Confirm"));
        inventoryCheck.AddChild(ERPPermissions.InventoryCheck.Delete, L("Permission:Delete"));


        inventoryTransform.AddChild(ERPPermissions.InventoryTransform.Create, L("Permission:Create"));
        inventoryTransform.AddChild(ERPPermissions.InventoryTransform.Update, L("Permission:Update"));
        inventoryTransform.AddChild(ERPPermissions.InventoryTransform.Confirm, L("Permission:Confirm"));
        inventoryTransform.AddChild(ERPPermissions.InventoryTransform.Delete, L("Permission:Delete"));


        warehouse.AddChild(ERPPermissions.Warehouse.Create, L("Permission:Create"));
        warehouse.AddChild(ERPPermissions.Warehouse.Update, L("Permission:Update"));
        warehouse.AddChild(ERPPermissions.Warehouse.Delete, L("Permission:Delete"));


        location.AddChild(ERPPermissions.Location.Create, L("Permission:Create"));
        location.AddChild(ERPPermissions.Location.Update, L("Permission:Update"));
        location.AddChild(ERPPermissions.Location.Delete, L("Permission:Delete"));

        safetyInventory.AddChild(ERPPermissions.SafetyInventory.Create, L("Permission:Create"));
        safetyInventory.AddChild(ERPPermissions.SafetyInventory.Update, L("Permission:Update"));
        safetyInventory.AddChild(ERPPermissions.SafetyInventory.Delete, L("Permission:Delete"));
        #endregion


        #region QualityManagement
        var qualityManagement = myGroup.AddPermission(ERPPermissions.QualityManagement.Default, L("Permission:QualityManagement"));

        var qualityManagementBusinessProcess = qualityManagement.AddChild(ERPPermissions.QualityManagement.BusinessProcess, L("Permission:BusinessProcess"));

        var arrivalInspection = qualityManagementBusinessProcess.AddChild(ERPPermissions.ArrivalInspection.Default, L("Permission:ArrivalInspection"));
        var processInspection = qualityManagementBusinessProcess.AddChild(ERPPermissions.ProcessInspection.Default, L("Permission:ProcessInspection"));
        var finalInspection = qualityManagementBusinessProcess.AddChild(ERPPermissions.FinalInspection.Default, L("Permission:FinalInspection"));

        
        arrivalInspection.AddChild(ERPPermissions.ArrivalInspection.Update, L("Permission:Update"));
        arrivalInspection.AddChild(ERPPermissions.ArrivalInspection.Confirm, L("Permission:Confirm"));

        processInspection.AddChild(ERPPermissions.ProcessInspection.Update, L("Permission:Update"));
        processInspection.AddChild(ERPPermissions.ProcessInspection.Confirm, L("Permission:Confirm"));


        finalInspection.AddChild(ERPPermissions.FinalInspection.Update, L("Permission:Update"));
        finalInspection.AddChild(ERPPermissions.FinalInspection.Confirm, L("Permission:Confirm"));

        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<ERPResource>(name);
    }
}
