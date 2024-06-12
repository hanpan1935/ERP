using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
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
using Lanpuda.ERP.WarehouseManagement.InventoryChecks;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves;
using Lanpuda.ERP.WarehouseManagement.InventoryTransforms;
using Lanpuda.ERP.WarehouseManagement.OtherOuts;
using Lanpuda.ERP.WarehouseManagement.OtherStorages;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;

namespace Lanpuda.ERP.EntityFrameworkCore;

[DependsOn(
    typeof(ERPDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class ERPEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        ERPEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<ERPDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);

            options.AddRepository<Bom, BomRepository>();
            options.AddRepository<BomDetail, BomDetailRepository>();
            options.AddRepository<Workshop, WorkshopRepository>();
            options.AddRepository<Mps, MpsRepository>();
            options.AddRepository<MrpDetail, MrpDetailRepository>();
            options.AddRepository<WorkOrder, WorkOrderRepository>();
            options.AddRepository<WorkOrderMaterial, WorkOrderMaterialRepository>();
            options.AddRepository<MaterialApply, MaterialApplyRepository>();
            options.AddRepository<MaterialApplyDetail, MaterialApplyDetailRepository>();
            options.AddRepository<MaterialReturnApply, MaterialReturnApplyRepository>();
            options.AddRepository<MaterialReturnApplyDetail, MaterialReturnApplyDetailRepository>();
            options.AddRepository<PurchasePrice, PurchasePriceRepository>();
            options.AddRepository<PurchasePriceDetail, PurchasePriceDetailRepository>();
            options.AddRepository<ArrivalNotice, ArrivalNoticeRepository>();
            options.AddRepository<ArrivalNoticeDetail, ArrivalNoticeDetailRepository>();
            options.AddRepository<PurchaseOrder, PurchaseOrderRepository>();
            options.AddRepository<PurchaseOrderDetail, PurchaseOrderDetailRepository>();
            options.AddRepository<Supplier, SupplierRepository>();
            options.AddRepository<PurchaseReturnApply, PurchaseReturnApplyRepository>();
            options.AddRepository<PurchaseReturnApplyDetail, PurchaseReturnApplyDetailRepository>();
            options.AddRepository<PurchaseReturn, PurchaseReturnRepository>();
            options.AddRepository<PurchaseReturnDetail, PurchaseReturnDetailRepository>();
            options.AddRepository<PurchaseStorage, PurchaseStorageRepository>();
            options.AddRepository<PurchaseStorageDetail, PurchaseStorageDetailRepository>();
            options.AddRepository<WorkOrderOut, WorkOrderOutRepository>();
            options.AddRepository<WorkOrderOutDetail, WorkOrderOutDetailRepository>();
            options.AddRepository<WorkOrderReturn, WorkOrderReturnRepository>();
            options.AddRepository<WorkOrderReturnDetail, WorkOrderReturnDetailRepository>();
            options.AddRepository<WorkOrderStorage, WorkOrderStorageRepository>();
            options.AddRepository<WorkOrderStorageDetail, WorkOrderStorageDetailRepository>();
            options.AddRepository<SafetyInventory, SafetyInventoryRepository>();
            options.AddRepository<ArrivalInspection, ArrivalInspectionRepository>();
            options.AddRepository<WorkOrderStorageApply, WorkOrderStorageApplyRepository>();
            options.AddRepository<OtherOut, OtherOutRepository>();
            options.AddRepository<OtherOutDetail, OtherOutDetailRepository>();
            options.AddRepository<OtherStorage, OtherStorageRepository>();
            options.AddRepository<OtherStorageDetail, OtherStorageDetailRepository>();
            options.AddRepository<InventoryMove, InventoryMoveRepository>();
            options.AddRepository<InventoryMoveDetail, InventoryMoveDetailRepository>();
            options.AddRepository<InventoryCheck, InventoryCheckRepository>();
            options.AddRepository<InventoryCheckDetail, InventoryCheckDetailRepository>();
            options.AddRepository<InventoryTransform, InventoryTransformRepository>();
            options.AddRepository<InventoryTransformAfterDetail, InventoryTransformAfterDetailRepository>();
            options.AddRepository<InventoryTransformBeforeDetail, InventoryTransformBeforeDetailRepository>();
            options.AddRepository<ProcessInspection, ProcessInspectionRepository>();
            options.AddRepository<FinalInspection, FinalInspectionRepository>();
            options.AddRepository<PurchaseApply, PurchaseApplyRepository>();
            options.AddRepository<PurchaseApplyDetail, PurchaseApplyDetailRepository>();
        });

        Configure<AbpDbContextOptions>(options =>
        {
                /* The main point to change your DBMS.
                 * See also ERPMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

    }
}
