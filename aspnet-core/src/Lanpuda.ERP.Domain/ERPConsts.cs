namespace Lanpuda.ERP;

public static class ERPConsts
{
    public const string DbTablePrefix = "App";

    public const string DbSchema = null;

    #region Sales
    public const string SalesOrderPrefix = "SO";
    public const string SalesPricePrefix = "SP";
    public const string ShipmentApplyPrefix = "SA";
    public const string SalesReturnApplyPrefix = "SR";
    #endregion


    #region Purchase  
    public const string ArrivalNoticePrefix = "PN";
    public const string PurchaseApplyPrefix = "PA";
    public const string PurchaseOrderPrefix = "PO";
    public const string PurchasePricePrefix = "PP";
    public const string PurchaseReturnApplyPrefix = "PR";
    #endregion

    #region Production
    public const string MpsPrefix = "MPS";
    public const string WorkOrderPrefix = "WO";
    public const string MaterialApplyPrefix = "MA";
    public const string MaterialReturnApplyPrefix = "MR";
    public const string MrpPrefix = "MRP";
    public const string WorkOrderStorageApplysPrefix = "WA";
    #endregion

    #region Warehouse
    public const string SalesOutPrefix = "ST";
    public const string SalesReturnsPrefix = "SS";

    public const string PurchaseStoragePrefix = "PS";
    public const string PurchaseReturnPrefix = "PT";

    public const string WorkOrderOutPrefix = "WT";
    public const string WorkOrderReturnPrefix = "WR";
    public const string WorkOrderStoragePrefix = "WS";


    public const string OtherStoragePrefix = "OS";
    public const string OtherOutPrefix = "OT";

    public const string InventoryCheckPrefix = "IC";
    public const string InventoryMovePrefix = "IM";
    public const string InventoryTransformPrefix = "IT";

    #endregion



    #region QualityManagement
    public const string ArrivalInspectionPrefix = "AI";
    public const string FinalInspectionPrefix = "FI";
    public const string ProcessInspectionPrefix = "PI";

    #endregion
}
