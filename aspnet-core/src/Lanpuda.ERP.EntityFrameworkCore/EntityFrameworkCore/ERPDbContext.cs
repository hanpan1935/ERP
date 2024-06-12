using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.ProductUnits;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.ProductionManagement.MaterialApplies;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.QualityManagement.ArrivalInspections;
using Lanpuda.ERP.QualityManagement.FinalInspections;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.SalesManagement.SalesPrices;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves;
using Lanpuda.ERP.WarehouseManagement.InventoryTransforms;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.OtherOuts;
using Lanpuda.ERP.WarehouseManagement.OtherStorages;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Lanpuda.ERP.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class ERPDbContext :
    AbpDbContext<ERPDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion


    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductUnit> ProductUnits { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<SalesPrice> SalesPrices { get; set; }
    public DbSet<SalesPriceDetail> SalesPriceDetails { get; set; }
    public DbSet<SalesOrder> SalesOrders { get; set; }
    public DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
    public DbSet<ShipmentApply> ShipmentApplies { get; set; }
    public DbSet<ShipmentApplyDetail> ShipmentApplyDetails { get; set; }
    public DbSet<InventoryLog> InventoryLogs { get; set; }
    public DbSet<SalesOut> SalesOuts { get; set; }
    public DbSet<SalesOutDetail> SalesOutDetails { get; set; }
    public DbSet<SalesReturnApply> SalesReturnApplies { get; set; }
    public DbSet<SalesReturnApplyDetail> SalesReturnApplyDetails { get; set; }
    public DbSet<SalesReturn> SalesReturns { get; set; }
    public DbSet<Bom> Boms { get; set; }
    public DbSet<BomDetail> BomDetails { get; set; }
    public DbSet<Workshop> Workshops { get; set; }
    public DbSet<Mps> Mps { get; set; }
    public DbSet<MpsDetail> MpsDetails { get; set; }
    public DbSet<MrpDetail> MrpDetails { get; set; }
    public DbSet<WorkOrder> WorkOrders { get; set; }
    public DbSet<WorkOrderMaterial> WorkOrderMaterials { get; set; }
    public DbSet<MaterialApply> MaterialApplies { get; set; }
    public DbSet<MaterialApplyDetail> MaterialApplyDetails { get; set; }
    public DbSet<MaterialReturnApply> MaterialReturnApplies { get; set; }
    public DbSet<MaterialReturnApplyDetail> MaterialReturnApplyDetails { get; set; }
    public DbSet<PurchasePrice> PurchasePrices { get; set; }
    public DbSet<PurchasePriceDetail> PurchasePriceDetails { get; set; }
    public DbSet<ArrivalNotice> ArrivalNotices { get; set; }
    public DbSet<ArrivalNoticeDetail> ArrivalNoticeDetails { get; set; }
    public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
    public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
    public DbSet<Supplier> Suppliers { get; set; }
    public DbSet<PurchaseReturnApply> PurchaseReturnApplies { get; set; }
    public DbSet<PurchaseReturnApplyDetail> PurchaseReturnApplyDetails { get; set; }
    public DbSet<PurchaseReturn> PurchaseReturns { get; set; }
    public DbSet<PurchaseReturnDetail> PurchaseReturnDetails { get; set; }
    public DbSet<PurchaseStorage> PurchaseStorages { get; set; }
    public DbSet<PurchaseStorageDetail> PurchaseStorageDetails { get; set; }
    public DbSet<WorkOrderOut> WorkOrderOuts { get; set; }
    public DbSet<WorkOrderOutDetail> WorkOrderOutDetails { get; set; }
    public DbSet<WorkOrderReturn> WorkOrderReturns { get; set; }
    public DbSet<WorkOrderReturnDetail> WorkOrderReturnDetails { get; set; }
    public DbSet<WorkOrderStorage> WorkOrderStorages { get; set; }
    public DbSet<WorkOrderStorageDetail> WorkOrderStorageDetails { get; set; }
    public DbSet<SafetyInventory> SafetyInventories { get; set; }
    public DbSet<ArrivalInspection> ArrivalInspections { get; set; }
    public DbSet<WorkOrderStorageApply> WorkOrderStorageApplies { get; set; }
    public DbSet<OtherOut> OtherOuts { get; set; }
    public DbSet<OtherOutDetail> OtherOutDetails { get; set; }
    public DbSet<OtherStorage> OtherStorages { get; set; }
    public DbSet<OtherStorageDetail> OtherStorageDetails { get; set; }
    public DbSet<InventoryMove> InventoryMoves { get; set; }
    public DbSet<InventoryMoveDetail> InventoryMoveDetails { get; set; }
    public DbSet<InventoryCheck> InventoryChecks { get; set; }
    public DbSet<InventoryCheckDetail> InventoryCheckDetails { get; set; }
    public DbSet<InventoryTransform> InventoryTransforms { get; set; }
    public DbSet<InventoryTransformAfterDetail> InventoryTransformAfterDetails { get; set; }
    public DbSet<InventoryTransformBeforeDetail> InventoryTransformBeforeDetails { get; set; }
    public DbSet<ProcessInspection> ProcessInspections { get; set; }
    public DbSet<FinalInspection> FinalInspections { get; set; }
    public DbSet<PurchaseApply> PurchaseApplies { get; set; }
    public DbSet<PurchaseApplyDetail> PurchaseApplyDetails { get; set; }
    public DbSet<SalesReturnDetail> SalesReturnDetails { get; set; }

    public ERPDbContext(DbContextOptions<ERPDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(ERPConsts.DbTablePrefix + "YourEntities", ERPConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        builder.Entity<ProductCategory>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ProductCategories", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Name).IsRequired().HasMaxLength(128).HasComment("");
            b.Property(m => m.Number).HasMaxLength(128).HasComment("");
            b.Property(m => m.Remark).HasMaxLength(128).HasComment("");
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<ProductUnit>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ProductUnits", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Name).IsRequired().HasMaxLength(128).HasComment("");
            b.Property(m => m.Number).HasMaxLength(128).HasComment("");
            b.Property(m => m.Remark).HasMaxLength(128).HasComment("");
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<Product>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Products", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasMaxLength(128).HasComment("");
            b.Property(m => m.ProductCategoryId).HasComment("");
            b.Property(m => m.ProductUnitId).IsRequired().HasComment("");
            b.Property(m => m.Name).IsRequired().HasMaxLength(128).HasComment("");
            b.Property(m => m.Spec).HasMaxLength(128).HasComment("");
            b.Property(m => m.SourceType).HasComment("");
            b.Property(m => m.ProductionBatch).HasComment("");
            b.Property(m => m.DefaultLocationId).HasComment("");
            b.Property(m => m.LeadTime).HasComment("");
            b.Property(m => m.Remark).HasMaxLength(256).HasComment("");

            //
            b.HasOne(m => m.ProductCategory).WithMany().HasForeignKey(m => m.ProductCategoryId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ProductUnit).WithMany().HasForeignKey(m => m.ProductUnitId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.DefaultLocation).WithMany().HasForeignKey(m => m.DefaultLocationId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.DefaultWorkshop).WithMany().HasForeignKey(m => m.DefaultWorkshopId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<Customer>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Customers", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            //b.HasOne(m => m.Area).WithMany().HasForeignKey(m => m.AreaId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<SalesPrice>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesPrices", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Customer).WithMany().HasForeignKey(m => m.CustomerId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.SalesPrice).HasForeignKey(m => m.SalesPriceId);
        });


        builder.Entity<SalesPriceDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesPriceDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            //b.HasOne(m => m.SalesPrice).WithMany(m=>m.Details).HasForeignKey(m => m.SalesPriceId);
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<SalesOrder>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesOrders", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Customer).WithMany().HasForeignKey(m => m.CustomerId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.SalesOrder).HasForeignKey(m => m.SalesOrderId);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<SalesOrderDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesOrderDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            //b.HasOne(m => m.SalesOrder).WithMany().HasForeignKey(m => m.SalesOrderId);
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            //

        });



        builder.Entity<Warehouse>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Warehouses", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).IsRequired().HasMaxLength(128);
            b.Property(m => m.Name).IsRequired().HasMaxLength(128);
            b.Property(m => m.Remark).HasMaxLength(128);
            //
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Locations).WithOne(m => m.Warehouse).HasForeignKey(m => m.WarehouseId).OnDelete(DeleteBehavior.NoAction);
        });



        builder.Entity<Location>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Locations", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).IsRequired().HasMaxLength(128).HasComment("");
            b.Property(m => m.Name).IsRequired().HasMaxLength(128).HasComment("");
            b.Property(m => m.Remark).HasMaxLength(128).HasComment("");
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Warehouse).WithMany(m => m.Locations).HasForeignKey(m => m.WarehouseId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<Inventory>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Inventories", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Quantity).HasComment("");
            b.Property(m => m.Price);
            b.Property(m => m.Batch).HasMaxLength(128);


            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<InventoryLog>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryLogs", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasMaxLength(128).HasComment("");
            b.Property(m => m.ProductId).HasComment("产品");
            b.Property(m => m.LocationId).HasComment("库位");
            b.Property(m => m.LogTime).HasComment("发生时间");
            b.Property(m => m.LogType).HasComment("出入库类型");
            b.Property(m => m.Batch).HasComment("批次");
            //b.Property(m => m.InQuantity).HasComment("入库数量");
            //b.Property(m => m.OutQuantity).HasComment("出库数量");
            b.Property(m => m.AfterQuantity).HasComment("期末数量");
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<ShipmentApply>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ShipmentApplies", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.Address).HasComment("").HasMaxLength(128);
            b.Property(m => m.Consignee).HasComment("").HasMaxLength(128);
            b.Property(m => m.ConsigneeTel).HasComment("").HasMaxLength(128);
            b.Property(m => m.IsConfirmed).HasComment("暂存or确认");
            //
            b.HasOne(m => m.Customer).WithMany().HasForeignKey(m => m.CustomerId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);

            b.HasMany(m => m.Details).WithOne(m => m.ShipmentApply).HasForeignKey(m => m.ShipmentApplyId);

        });



        builder.Entity<ShipmentApplyDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ShipmentApplyDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.ShipmentApplyId).HasComment("所属申请单_Id");
            b.Property(m => m.SalesOrderDetailId).HasComment("");
            b.Property(m => m.Quantity).HasComment("数量");
            //
            b.HasOne(m => m.SalesOrderDetail).WithMany().HasForeignKey(m => m.SalesOrderDetailId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<SalesOut>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesOuts", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.ShipmentApplyDetailId).HasComment("");
            b.Property(m => m.Number).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.SuccessfulTime).HasComment("");
            b.Property(m => m.KeeperUserId).HasComment("");
            b.Property(m => m.Remark).HasComment("").HasMaxLength(128);
            b.Property(m => m.IsSuccessful).HasComment("");
            //1对多
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

            //1对1
            b.HasOne(m => m.ShipmentApplyDetail).WithOne(m => m.SalesOut).HasForeignKey<SalesOut>(m => m.ShipmentApplyDetailId).OnDelete(DeleteBehavior.NoAction);


            //多对1
            b.HasMany(m => m.Details).WithOne(m => m.SalesOut).HasForeignKey(m => m.SalesOutId);

        });


        builder.Entity<SalesOutDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesOutDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(m => m.SalesOutId).HasComment("");
            // b.Property(m => m.ProductId).HasComment("");
            /* Configure more properties here */
            // b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<SalesReturnApply>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesReturnApplies", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasComment("").IsRequired().HasMaxLength(128);
            b.Property(m => m.CustomerId).HasComment("");
            b.Property(m => m.Reason).HasComment("");
            b.Property(m => m.IsProductReturn).HasComment("");
            b.Property(m => m.Description).HasComment("具体描述").HasMaxLength(256);
            b.Property(m => m.IsConfirmed).HasComment("");
            //
            b.HasOne(m => m.Customer).WithMany().HasForeignKey(m => m.CustomerId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.SalesReturnApply).HasForeignKey(m => m.SalesReturnApplyId);

        });


        builder.Entity<SalesReturnApplyDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesReturnApplyDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.SalesReturnApplyId).HasComment("");
            b.Property(m => m.SalesOutDetailId).HasComment("");
            b.Property(m => m.Quantity).HasComment("");
            b.Property(m => m.Description).HasComment("").HasMaxLength(128);
            //
            b.HasOne(m => m.SalesOutDetail).WithMany().HasForeignKey(m => m.SalesOutDetailId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<SalesReturn>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesReturns", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.SalesReturnApplyDetailId).HasComment("");
            b.Property(m => m.Number).HasComment("").HasMaxLength(128);
            b.Property(m => m.SuccessfulTime).HasComment("");
            b.Property(m => m.KeeperUserId).HasComment("");
            b.Property(m => m.Remark).HasComment("").HasMaxLength(256);
            b.Property(m => m.IsSuccessful).HasComment("");
            //1对多
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

            //1对1
            b.HasOne(m => m.SalesReturnApplyDetail).WithOne(m => m.SalesReturn).HasForeignKey<SalesReturn>(m => m.SalesReturnApplyDetailId).OnDelete(DeleteBehavior.NoAction);

            //多对1
            b.HasMany(m => m.Details).WithOne(m => m.SalesReturn).HasForeignKey(m => m.SalesReturnId);
        });


        builder.Entity<SalesReturnDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SalesReturnDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.SalesReturnId).HasComment("");
            //b.Property(m => m.ProductId).HasComment("");
            b.Property(m => m.LocationId).HasComment("");
            b.Property(m => m.Quantity).HasComment("");
            b.Property(m => m.Batch).HasComment("").HasMaxLength(128);
            //
            //b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<Bom>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Boms", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Remark).HasComment("").HasMaxLength(128);
            b.HasOne(m => m.Product).WithOne(m => m.Bom).HasForeignKey<Bom>(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.Bom).HasForeignKey(m => m.BomId);


        });


        builder.Entity<BomDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "BomDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Quantity).HasComment("");
            b.Property(m => m.Remark).HasComment("").HasMaxLength(128);
            //
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<Workshop>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Workshops", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.Name).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.Remark).HasComment("").HasMaxLength(128);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<Mps>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Mps", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.StartDate).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.CompleteDate).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.ProductId).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.Quantity).HasComment("").HasMaxLength(128).IsRequired().HasPrecision(18, 2);
            b.Property(m => m.Remark).HasComment("").HasMaxLength(128);
            //
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId);
            b.HasOne(m => m.ConfirmedUser).WithMany().HasForeignKey(m => m.ConfirmedUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

            b.HasMany(m => m.Details).WithOne(m => m.Mps).HasForeignKey(m => m.MpsId);
            b.HasMany(m => m.MrpDetails).WithOne(m => m.Mps).HasForeignKey(m => m.MpsId);
            //
            b.HasOne(m => m.SalesOrderDetail).WithMany(m => m.MpsList).HasForeignKey(m => m.SalesOrderDetailId).OnDelete(DeleteBehavior.NoAction);

            b.HasOne(m => m.FinalInspection).WithOne(m => m.Mps).HasForeignKey<FinalInspection>(m => m.MpsId);
            b.HasOne(m => m.PurchaseApply).WithOne(m => m.Mps).HasForeignKey<PurchaseApply>(m => m.MpsId).OnDelete(DeleteBehavior.NoAction); ;


            //索引
            b.HasIndex(m => m.Number);
        });


        builder.Entity<MpsDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "MpsDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
        });

        builder.Entity<MrpDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "MrpDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<WorkOrder>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrders", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.Quantity).HasComment("").IsRequired();

            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Workshop).WithMany().HasForeignKey(m => m.WorkshopId).OnDelete(DeleteBehavior.NoAction);
            //list
            b.HasMany(m => m.StandardMaterialDetails).WithOne(m => m.WorkOrder).HasForeignKey(m => m.WorkOrderId);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Mps).WithMany(m => m.WorkOrderDetails).HasForeignKey(m => m.MpsId).OnDelete(DeleteBehavior.NoAction);

            //索引
            b.HasIndex(m => m.Number);
        });


        builder.Entity<WorkOrderMaterial>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderMaterials", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Quantity).HasComment("").IsRequired();
            //
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<MaterialApply>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "MaterialApplies", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).IsRequired().HasComment("");

            b.HasOne(m => m.WorkOrder).WithMany().HasForeignKey(m => m.WorkOrderId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmedUser).WithMany().HasForeignKey(m => m.ConfirmedUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.MaterialApply).HasForeignKey(m => m.MaterialApplyId);

        });


        builder.Entity<MaterialApplyDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "MaterialApplyDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Quantity).HasComment("").IsRequired();
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<MaterialReturnApply>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "MaterialReturnApplies", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            //b.HasOne(m => m.WorkOrder).WithMany().HasForeignKey(m => m.WorkOrderId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmedUser).WithMany().HasForeignKey(m => m.ConfirmedUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.MaterialReturnApply).HasForeignKey(m => m.MaterialReturnApplyId);

        });


        builder.Entity<MaterialReturnApplyDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "MaterialReturnApplyDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Quantity).HasComment("").IsRequired();
            ///
            b.HasOne(m => m.WorkOrderOutDetail).WithMany().HasForeignKey(m => m.WorkOrderOutDetailId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchasePrice>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchasePrices", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasMany(m => m.Details).WithOne(m => m.PurchasePrice).HasForeignKey(m => m.PurchasePriceId);
            b.HasOne(m => m.Supplier).WithMany().HasForeignKey(m => m.SupplierId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchasePriceDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchasePriceDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Price).HasComment("");
            b.Property(m => m.TaxRate).HasComment("");
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
        });

        builder.Entity<ArrivalNotice>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ArrivalNotices", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasComment("").HasMaxLength(128);
            b.Property(m => m.ArrivalTime).HasComment("");
            b.Property(m => m.Remark).HasComment("").HasMaxLength(256);
            b.Property(m => m.IsConfirmed).HasComment("");
            b.Property(m => m.ConfirmedTime).HasComment("");
            //
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.ArrivalNotice).HasForeignKey(m => m.ArrivalNoticeId);
        });


        builder.Entity<ArrivalNoticeDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ArrivalNoticeDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.Property(m => m.ArrivalNoticeId).HasComment("");
            b.Property(m => m.PurchaseOrderDetailId).HasComment("");
            b.Property(m => m.Quantity).HasComment("");
            //
            b.HasOne(m => m.PurchaseOrderDetail).WithMany().HasForeignKey(m => m.PurchaseOrderDetailId).OnDelete(DeleteBehavior.NoAction);

            //一对一 来料检验  //配置为级联删除,删除来料通知会自动删除来料检验单
            b.HasOne(m => m.ArrivalInspection).WithOne(m => m.ArrivalNoticeDetail).HasForeignKey<ArrivalInspection>(m => m.ArrivalNoticeDetailId);



        });


        builder.Entity<PurchaseOrder>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseOrders", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Supplier).WithMany().HasForeignKey(m => m.SupplierId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.PurchaseOrder).HasForeignKey(m => m.PurchaseOrderId);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchaseOrderDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseOrderDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Quantity).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.Price).HasComment("").HasMaxLength(128).IsRequired();
            b.Property(m => m.TaxRate).HasComment("").HasMaxLength(128).IsRequired();
            //
        });


        builder.Entity<Supplier>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "Suppliers", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasComment("");
            b.Property(m => m.FullName).IsRequired().HasMaxLength(128).HasComment("");
            b.Property(m => m.ShortName).IsRequired().HasMaxLength(128).HasComment("");
            b.Property(m => m.FactoryAddress).HasComment("");
            b.Property(m => m.Contact).HasComment("");
            b.Property(m => m.ContactTel).HasComment("");
            b.Property(m => m.OrganizationName).HasComment("");
            b.Property(m => m.TaxNumber).HasComment("");
            b.Property(m => m.BankName).HasComment("");
            b.Property(m => m.AccountNumber).HasComment("");
            b.Property(m => m.TaxAddress).HasComment("");
            b.Property(m => m.TaxTel).HasComment("");
            //
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchaseReturnApply>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseReturnApplies", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.PurchaseReturnApply).HasForeignKey(m => m.PurchaseReturnApplyId);
            //
        });


        builder.Entity<PurchaseReturnApplyDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseReturnApplyDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */

            b.Property(m => m.Quantity).HasComment("").HasMaxLength(128).IsRequired();

            b.HasOne(m => m.PurchaseStorageDetail).WithMany().HasForeignKey(m => m.PurchaseStorageDetailId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<PurchaseReturn>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseReturns", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasMany(m => m.Details).WithOne(m => m.PurchaseReturn).HasForeignKey(m => m.PurchaseReturnId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

            b.HasOne(m => m.PurchaseReturnApplyDetail).WithOne(m => m.PurchaseReturn).HasForeignKey<PurchaseReturn>(m => m.PurchaseReturnApplyDetailId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchaseReturnDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseReturnDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            //b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchaseStorage>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseStorages", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Remark).HasMaxLength(256);
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.PurchaseStorage).HasForeignKey(m => m.PurchaseStorageId);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

            //1 to 1
            b.HasOne(m => m.ArrivalNoticeDetail).WithOne(m => m.PurchaseStorage).HasForeignKey<PurchaseStorage>(m => m.ArrivalNoticeDetailId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<PurchaseStorageDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseStorageDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<WorkOrderOut>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderOuts", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasMany(m => m.Details).WithOne(m => m.WorkOrderOut).HasForeignKey(m => m.WorkOrderOutId).OnDelete(DeleteBehavior.Restrict);
            //
            b.HasOne(m => m.MaterialApplyDetail).WithOne(m => m.WorkOrderOut).HasForeignKey<WorkOrderOut>(m => m.MaterialApplyDetailId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<WorkOrderOutDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderOutDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });



        builder.Entity<WorkOrderReturn>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderReturns", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.WorkOrderReturn).HasForeignKey(m => m.WorkOrderReturnId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

            b.HasOne(m => m.MaterialReturnApplyDetail).WithOne(m => m.WorkOrderReturn).HasForeignKey<WorkOrderReturn>(m => m.MaterialReturnApplyDetailId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<WorkOrderReturnDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderReturnDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<WorkOrderStorage>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderStorages", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.WorkOrderStorage).HasForeignKey(m => m.WorkOrderStorageId);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);

            b.HasOne(m => m.WorkOrderStorageApply).WithOne(m => m.WorkOrderStorage).HasForeignKey<WorkOrderStorage>(m => m.WorkOrderStorageApplyId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<WorkOrderStorageDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderStorageDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<SafetyInventory>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "SafetyInventories", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            b.Property(m => m.MinQuantity);
            b.Property(m => m.MaxQuantity);
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithOne(m => m.SafetyInventory).HasForeignKey<SafetyInventory>(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<ArrivalInspection>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ArrivalInspections", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).HasMaxLength(100).IsRequired();
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<WorkOrderStorageApply>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "WorkOrderStorageApplies", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.WorkOrder).WithMany().HasForeignKey(m => m.WorkOrderId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmedUser).WithMany().HasForeignKey(m => m.ConfirmedUserId).OnDelete(DeleteBehavior.NoAction);

        });


        builder.Entity<OtherOut>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "OtherOuts", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasMany(m => m.Details).WithOne(m => m.OtherOut).HasForeignKey(m => m.OtherOutId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<OtherOutDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "OtherOutDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<OtherStorage>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "OtherStorages", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasMany(m => m.Details).WithOne(m => m.OtherStorage).HasForeignKey(m => m.OtherStorageId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<OtherStorageDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "OtherStorageDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<InventoryMove>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryMoves", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.InventoryMove).HasForeignKey(m => m.InventoryMoveId);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<InventoryMoveDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryMoveDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.OutLocation).WithMany().HasForeignKey(m => m.OutLocationId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.InLocation).WithMany().HasForeignKey(m => m.InLocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<InventoryCheck>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryChecks", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).IsRequired(true);
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Warehouse).WithMany().HasForeignKey(m => m.WarehouseId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.InventoryCheck).HasForeignKey(m => m.InventoryCheckId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<InventoryCheckDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryCheckDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<InventoryTransform>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryTransforms", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.Property(m => m.Number).IsRequired(true);
            b.Property(m => m.Reason).IsRequired(true);
            b.HasOne(m => m.KeeperUser).WithMany().HasForeignKey(m => m.KeeperUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.BeforeDetails).WithOne(m => m.InventoryTransform).HasForeignKey(m => m.InventoryTransformId).OnDelete(DeleteBehavior.Restrict);
            b.HasMany(m => m.AfterDetails).WithOne(m => m.InventoryTransform).HasForeignKey(m => m.InventoryTransformId).OnDelete(DeleteBehavior.Restrict);
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<InventoryTransformAfterDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryTransformAfterDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<InventoryTransformBeforeDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "InventoryTransformBeforeDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Location).WithMany().HasForeignKey(m => m.LocationId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<ProcessInspection>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "ProcessInspections", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */

            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);

            //1对1 工单入库申请
            b.HasOne(m => m.WorkOrderStorageApply).WithOne(m => m.ProcessInspection).HasForeignKey<ProcessInspection>(m => m.WorkOrderStorageApplyId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<FinalInspection>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "FinalInspections", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchaseApply>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseApplies", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Creator).WithMany().HasForeignKey(m => m.CreatorId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.ConfirmeUser).WithMany().HasForeignKey(m => m.ConfirmeUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasOne(m => m.AcceptUser).WithMany().HasForeignKey(m => m.AcceptUserId).OnDelete(DeleteBehavior.NoAction);
            b.HasMany(m => m.Details).WithOne(m => m.PurchaseApply).HasForeignKey(m => m.PurchaseApplyId);
            b.HasOne(m => m.Mps).WithOne(m => m.PurchaseApply).HasForeignKey<PurchaseApply>(m => m.MpsId).OnDelete(DeleteBehavior.NoAction);
        });


        builder.Entity<PurchaseApplyDetail>(b =>
        {
            b.ToTable(ERPConsts.DbTablePrefix + "PurchaseApplyDetails", ERPConsts.DbSchema);
            b.ConfigureByConvention();
            /* Configure more properties here */
            b.HasOne(m => m.Product).WithMany().HasForeignKey(m => m.ProductId).OnDelete(DeleteBehavior.NoAction);
        });


    }
}
