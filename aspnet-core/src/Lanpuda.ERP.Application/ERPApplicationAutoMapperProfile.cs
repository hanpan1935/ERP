using AutoMapper;
using Lanpuda.ERP.BasicData.ProductCategories;
using Lanpuda.ERP.BasicData.ProductCategories.Dtos;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.BasicData.ProductUnits;
using Lanpuda.ERP.BasicData.ProductUnits.Dtos;
using Lanpuda.ERP.ProductionManagement.Boms;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Lanpuda.ERP.ProductionManagement.MaterialApplies;
using Lanpuda.ERP.ProductionManagement.MaterialApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies;
using Lanpuda.ERP.ProductionManagement.MaterialReturnApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies;
using Lanpuda.ERP.ProductionManagement.WorkOrderStorageApplies.Dtos;
using Lanpuda.ERP.ProductionManagement.Workshops;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices;
using Lanpuda.ERP.PurchaseManagement.ArrivalNotices.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders;
using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices;
using Lanpuda.ERP.PurchaseManagement.PurchasePrices.Dtos;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using Lanpuda.ERP.PurchaseManagement.Suppliers;
using Lanpuda.ERP.PurchaseManagement.Suppliers.Dtos;
using Lanpuda.ERP.QualityManagement.ArrivalInspections;
using Lanpuda.ERP.QualityManagement.ArrivalInspections.Dtos;
using Lanpuda.ERP.QualityManagement.FinalInspections;
using Lanpuda.ERP.QualityManagement.FinalInspections.Dtos;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;
using Lanpuda.ERP.SalesManagement.Customers;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles;
using Lanpuda.ERP.SalesManagement.SalesPrices;
using Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies;
using Lanpuda.ERP.SalesManagement.SalesReturnApplies.Dtos;
using Lanpuda.ERP.SalesManagement.ShipmentApplies;
using Lanpuda.ERP.SalesManagement.ShipmentApplies.Dtos;
using Lanpuda.ERP.WarehouseManagement.Inventories;
using Lanpuda.ERP.WarehouseManagement.Inventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks;
using Lanpuda.ERP.WarehouseManagement.InventoryChecks.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves;
using Lanpuda.ERP.WarehouseManagement.InventoryMoves.Dtos;
using Lanpuda.ERP.WarehouseManagement.InventoryTransforms;
using Lanpuda.ERP.WarehouseManagement.InventoryTransforms.Dtos;
using Lanpuda.ERP.WarehouseManagement.Locations;
using Lanpuda.ERP.WarehouseManagement.Locations.Dtos;
using Lanpuda.ERP.WarehouseManagement.OtherOuts;
using Lanpuda.ERP.WarehouseManagement.OtherOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.OtherStorages;
using Lanpuda.ERP.WarehouseManagement.OtherStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns;
using Lanpuda.ERP.WarehouseManagement.PurchaseReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages;
using Lanpuda.ERP.WarehouseManagement.PurchaseStorages.Dtos;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories;
using Lanpuda.ERP.WarehouseManagement.SafetyInventories.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesOuts;
using Lanpuda.ERP.WarehouseManagement.SalesOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.SalesReturns;
using Lanpuda.ERP.WarehouseManagement.SalesReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.Warehouses;
using Lanpuda.ERP.WarehouseManagement.Warehouses.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts;
using Lanpuda.ERP.WarehouseManagement.WorkOrderOuts.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns;
using Lanpuda.ERP.WarehouseManagement.WorkOrderReturns.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies;
using Lanpuda.ERP.PurchaseManagement.PurchaseApplies.Dtos;
using Lanpuda.ERP.WarehouseManagement.WorkOrderStorages.Dtos;
using Volo.Abp.AutoMapper;
using Volo.Abp.Data;
using Lanpuda.ERP.Reports.Dtos.Home;

namespace Lanpuda.ERP;
public class ERPApplicationAutoMapperProfile : Profile
{
    public ERPApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<ProductCategory, ProductCategoryDto>();
        CreateMap<ProductCategoryCreateDto, ProductCategory>(MemberList.Source);
        CreateMap<ProductCategoryUpdateDto, ProductCategory>(MemberList.Source);
        CreateMap<ProductUnit, ProductUnitDto>();
        CreateMap<ProductUnitCreateDto, ProductUnit>(MemberList.Source);
        CreateMap<ProductUnitUpdateDto, ProductUnit>(MemberList.Source);
        CreateMap<Product, ProductDto>()
        .ForMember(m => m.DefaultWarehouseName, x => x.MapFrom(m => m.DefaultLocation.Warehouse.Name))
        .ForMember(m => m.DefaultWarehouseId, x => x.MapFrom(m => m.DefaultLocation.Warehouse.Id))
        ;
        CreateMap<ProductCreateDto, Product>(MemberList.Source);
        CreateMap<ProductUpdateDto, Product>(MemberList.Source);
        CreateMap<Customer, CustomerDto>();
        CreateMap<Customer, CustomerLookupDto>();
        CreateMap<CustomerCreateDto, Customer>(MemberList.Source);
        CreateMap<CustomerUpdateDto, Customer>(MemberList.Source);
        CreateMap<SalesPrice, SalesPriceDto>();
        CreateMap<SalesPriceCreateDto, SalesPrice>(MemberList.Source);
        CreateMap<SalesPriceUpdateDto, SalesPrice>(MemberList.Source);
        CreateMap<SalesPriceDetail, SalesPriceDetailDto>()
        .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name));
        CreateMap<SalesPriceDetailCreateDto, SalesPriceDetail>(MemberList.Source);
        CreateMap<SalesPriceDetailUpdateDto, SalesPriceDetail>(MemberList.Source);
        CreateMap<SalesOrder, SalesOrderDto>();
        CreateMap<SalesOrder, SalesOrderGetDto>();
        CreateMap<SalesOrder, SalesOrderProfileDto>();
        CreateMap<SalesOrderDetail, SalesOrderProfileDetailDto>()
            .ForMember(m => m.SalesOrderDetailId, x => x.MapFrom(m => m.Id))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            .Ignore(m => m.InventoryQuantity)
            .Ignore(m => m.ShipmentApplyDetails)
            .Ignore(m => m.ReturnApplyDetails)
            .Ignore(m => m.OutDetails)
            .Ignore(m => m.ReturnDetails)
            .Ignore(m => m.Mpses)
            ;

        CreateMap<SalesOrderDetail, SalesOrderDetailSelectDto>()
        .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
        ;
        CreateMap<ShipmentApplyDetail, SalesOrderProfileShipmentApplyDetailDto>()
            .ForMember(m => m.ShipmentApplyDetailId, x => x.MapFrom(m => m.Id))
            ;
        CreateMap<SalesReturnApplyDetail, SalesOrderProfileReturnApplyDetailDto>()
            .ForMember(m => m.SalesReturnApplyDetailId, x => x.MapFrom(m => m.Id))
            ;
        CreateMap<SalesOutDetail, SalesOrderProfileOutDetailDto>()
            .ForMember(m => m.SalesOutDetailId, x => x.MapFrom(m => m.Id))
            ;
        CreateMap<SalesReturnDetail, SalesOrderProfileReturnDetailDto>()
            .ForMember(m => m.SalesReturnDetailId, x => x.MapFrom(m => m.Id))
            .ForMember(m => m.SalesRetrunNumber, x => x.MapFrom(m => m.SalesReturn.Number))
            ;
        CreateMap<Mps, SalesOrderProfileMpsDto>()
            .ForMember(m => m.MpsId, x => x.MapFrom(m => m.Id))
            .ForMember(m => m.MpsNumber, x => x.MapFrom(m => m.Number))
            .ForMember(m => m.Quanity, x => x.MapFrom(m => m.Quantity))
            ;
        CreateMap<SalesOrderCreateDto, SalesOrder>(MemberList.Source);
        CreateMap<SalesOrderUpdateDto, SalesOrder>(MemberList.Source);
        CreateMap<SalesOrderDetail, SalesOrderDetailDto>()
        .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
        ;
        CreateMap<SalesOrderDetail, SalesOrderDetailGetDto>()
        .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
        ;
        CreateMap<SalesOrderDetailCreateDto, SalesOrderDetail>(MemberList.Source);
        CreateMap<SalesOrderDetailUpdateDto, SalesOrderDetail>(MemberList.Source);
        CreateMap<Warehouse, WarehouseDto>();
        CreateMap<WarehouseCreateDto, Warehouse>(MemberList.Source);
        CreateMap<WarehouseUpdateDto, Warehouse>(MemberList.Source);
        CreateMap<Location, LocationDto>();
        CreateMap<LocationCreateDto, Location>(MemberList.Source);
        CreateMap<LocationUpdateDto, Location>(MemberList.Source);
        CreateMap<Inventory, InventoryDto>()
        .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
        .ForMember(m => m.LocationName, x => x.MapFrom(m => m.Location.Name))
        .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.Product.Number))
        .ForMember(m => m.ProductName, x => x.MapFrom(m => m.Product.Name))
        .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.Product.Spec))
        .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
        //.ForMember(m => m.CustomerShortName, x => x.MapFrom(m => m.Customer.ShortName))
        //.ForMember(m => m.SupplierShortName, x => x.MapFrom(m => m.Supplier.ShortName))
            ;
        //CreateMap<InventoryCreateDto, Inventory>(MemberList.Source);
        //CreateMap<InventoryUpdateDto, Inventory>(MemberList.Source);
        CreateMap<ShipmentApply, ShipmentApplyDto>();
        CreateMap<ShipmentApplyCreateDto, ShipmentApply>(MemberList.Source);
        CreateMap<ShipmentApplyUpdateDto, ShipmentApply>(MemberList.Source);
        CreateMap<ShipmentApplyDetail, ShipmentApplyDetailDto>()
            .ForMember(m => m.DeliveryDate, x => x.MapFrom(m => m.SalesOrderDetail.DeliveryDate))
            .ForMember(m => m.Requirement, x => x.MapFrom(m => m.SalesOrderDetail.Requirement))
            .ForMember(m => m.OrderQuantity, x => x.MapFrom(m => m.SalesOrderDetail.Quantity))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.SalesOrderDetail.Product.Name))
            .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.SalesOrderDetail.Product.Number))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.SalesOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.SalesOrderDetail.Product.Spec))
            .ForMember(m => m.SalesOrderNumber, x => x.MapFrom(m => m.SalesOrderDetail.SalesOrder.Number))
            ;
        CreateMap<ShipmentApplyDetailCreateDto, ShipmentApplyDetail>(MemberList.Source);
        CreateMap<ShipmentApplyDetailUpdateDto, ShipmentApplyDetail>(MemberList.Source);
        CreateMap<InventoryLog, InventoryLogDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;
        CreateMap<SalesOut, SalesOutDto>()
            .ForMember(m => m.CustomerNumber, x => x.MapFrom(m => m.ShipmentApplyDetail.ShipmentApply.Customer.Number))
            .ForMember(m => m.CustomerFullName, x => x.MapFrom(m => m.ShipmentApplyDetail.ShipmentApply.Customer.FullName))
            .ForMember(m => m.CustomerShortName, x => x.MapFrom(m => m.ShipmentApplyDetail.ShipmentApply.Customer.ShortName))
            .ForMember(m => m.ShipmentApplyNumber, x => x.MapFrom(m => m.ShipmentApplyDetail.ShipmentApply.Number))
            .ForMember(m => m.ShipmentApplyConsignee, x => x.MapFrom(m => m.ShipmentApplyDetail.ShipmentApply.Consignee))
            .ForMember(m => m.ShipmentApplyConsigneeTel, x => x.MapFrom(m => m.ShipmentApplyDetail.ShipmentApply.ConsigneeTel))
            .ForMember(m => m.ShipmentApplyAddress, x => x.MapFrom(m => m.ShipmentApplyDetail.ShipmentApply.Address))

            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.ShipmentApplyDetail.SalesOrderDetail.ProductId))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.ShipmentApplyDetail.SalesOrderDetail.Product.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.ShipmentApplyDetail.SalesOrderDetail.Product.Spec))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.ShipmentApplyDetail.SalesOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ApplyQuantity, x => x.MapFrom(m => m.ShipmentApplyDetail.Quantity))
            ;


        CreateMap<SalesOutDetail, SalesOutDetailSelectDto>()
            .ForMember(m => m.SalesOutNumber, x => x.MapFrom(m => m.SalesOut.Number))
            .ForMember(m => m.CustomerFullName, x => x.MapFrom(m => m.SalesOut.ShipmentApplyDetail.ShipmentApply.Customer.FullName))
            .ForMember(m => m.CustomerShortName, x => x.MapFrom(m => m.SalesOut.ShipmentApplyDetail.ShipmentApply.Customer.ShortName))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Name))
            .ForMember(m => m.Batch, x => x.MapFrom(m => m.Batch))
            .ForMember(m => m.OutQuantity, x => x.MapFrom(m => m.Quantity))
            .ForMember(m => m.SuccessfulTime, x => x.MapFrom(m => m.SalesOut.SuccessfulTime))
            ;


        CreateMap<SalesOutUpdateDto, SalesOut>(MemberList.Source);
        CreateMap<SalesOutDetail, SalesOutDetailDto>()
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.SalesOut.ShipmentApplyDetail.SalesOrderDetail.ProductId))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Name))
            ;


        CreateMap<SalesOutDetailUpdateDto, SalesOutDetail>(MemberList.Source);



        CreateMap<SalesReturnApply, SalesReturnApplyDto>();
        CreateMap<SalesReturnApplyCreateDto, SalesReturnApply>(MemberList.Source);
        CreateMap<SalesReturnApplyUpdateDto, SalesReturnApply>(MemberList.Source);

        CreateMap<SalesReturnApplyDetail, SalesReturnApplyDetailDto>()
           .ForMember(m => m.ProductName, x => x.MapFrom(m => m.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Name))
           .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Number))
           .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.ProductUnit.Name))
           .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Spec))
           .ForMember(m => m.ProductId, x => x.MapFrom(m => m.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.ProductId))
           .ForMember(m => m.Batch, x => x.MapFrom(m => m.SalesOutDetail.Batch))
           .ForMember(m => m.OutQuantity, x => x.MapFrom(m => m.SalesOutDetail.Quantity))
           ;
        CreateMap<SalesReturnApplyDetailCreateDto, SalesReturnApplyDetail>(MemberList.Source);
        CreateMap<SalesReturnApplyDetailUpdateDto, SalesReturnApplyDetail>(MemberList.Source);
        CreateMap<SalesReturn, SalesReturnDto>()
            .ForMember(m => m.ApplyNumber, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesReturnApply.Number))
            .ForMember(m => m.CustomerFullName, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesReturnApply.Customer.FullName))
            .ForMember(m => m.CustomerShortName, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesReturnApply.Customer.ShortName))
            .ForMember(m => m.Reason, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesReturnApply.Reason))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Id))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Spec))
            .ForMember(m => m.ApplyQuantity, x => x.MapFrom(m => m.SalesReturnApplyDetail.Quantity))
            .ForMember(m => m.Batch, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesOutDetail.Batch))
            .ForMember(m => m.ProductDefaultLocationId, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.DefaultLocationId))
            .ForMember(m => m.ProductDefaultWarehouseId, x => x.MapFrom(m => m.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.DefaultLocation.WarehouseId))
            ;

        CreateMap<SalesReturnUpdateDto, SalesReturn>(MemberList.Source);

        CreateMap<SalesReturnDetail, SalesReturnDetailDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.SalesReturn.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Name))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.SalesReturn.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Id))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.SalesReturn.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.Spec))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.SalesReturn.SalesReturnApplyDetail.SalesOutDetail.SalesOut.ShipmentApplyDetail.SalesOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            ;
        CreateMap<SalesReturnDetailUpdateDto, SalesReturnDetail>(MemberList.Source);
        CreateMap<Bom, BomDto>()
             .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;
        CreateMap<BomCreateDto, Bom>(MemberList.Source);
        CreateMap<BomUpdateDto, Bom>(MemberList.Source);
        CreateMap<BomDetail, BomDetailDto>()
             .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;
        CreateMap<BomDetail, BomLookupDto>(MemberList.None)
             .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;

        CreateMap<BomDetailCreateDto, BomDetail>(MemberList.Source);
        CreateMap<BomDetailUpdateDto, BomDetail>(MemberList.Source);
        CreateMap<Workshop, WorkshopDto>();
        CreateMap<WorkshopCreateDto, Workshop>(MemberList.Source);
        CreateMap<WorkshopUpdateDto, Workshop>(MemberList.Source);
        CreateMap<Mps, MpsDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;

        CreateMap<Mps, MpsProfileDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;

        CreateMap<MpsCreateDto, Mps>(MemberList.Source)
            ;
        CreateMap<MpsUpdateDto, Mps>(MemberList.Source);

        CreateMap<WorkOrder, WorkOrderDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.Product.Spec))
            ;

        CreateMap<WorkOrderUpdateDto, WorkOrder>(MemberList.Source);
        CreateMap<WorkOrderMaterial, WorkOrderMaterialDto>()
             .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
             .ForMember(m => m.SourceType, x => x.MapFrom(m => m.Product.SourceType))
             ;

        CreateMap<MaterialApply, MaterialApplyDto>()
            .ForMember(m => m.WorkOrderNumber, x => x.MapFrom(m => m.WorkOrder.Number))
            .ForMember(m => m.MpsNumber, x => x.MapFrom(m => m.WorkOrder.Mps.Number))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.WorkOrder.Product.Name))
            ;


        CreateMap<MaterialApplyCreateDto, MaterialApply>(MemberList.Source);
        CreateMap<MaterialApplyUpdateDto, MaterialApply>(MemberList.Source);
        CreateMap<MaterialApplyDetail, MaterialApplyDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;

        CreateMap<MaterialApplyDetailCreateDto, MaterialApplyDetail>(MemberList.Source);
        CreateMap<MaterialApplyDetailUpdateDto, MaterialApplyDetail>(MemberList.Source);
        CreateMap<MaterialReturnApply, MaterialReturnApplyDto>();
        CreateMap<MaterialReturnApplyCreateDto, MaterialReturnApply>(MemberList.Source);
        CreateMap<MaterialReturnApplyUpdateDto, MaterialReturnApply>(MemberList.Source);
        CreateMap<MaterialReturnApplyDetail, MaterialReturnApplyDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.Name))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.ProductId))
            .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.Number))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.Spec))
            .ForMember(m => m.Quantity, x => x.MapFrom(m => m.Quantity))
            .ForMember(m => m.Batch, x => x.MapFrom(m => m.WorkOrderOutDetail.Batch))
            ;
        CreateMap<MaterialReturnApplyDetailCreateDto, MaterialReturnApplyDetail>(MemberList.Source);
        CreateMap<MaterialReturnApplyDetailUpdateDto, MaterialReturnApplyDetail>(MemberList.Source);
        CreateMap<PurchasePrice, PurchasePriceDto>();
        CreateMap<PurchasePriceCreateDto, PurchasePrice>(MemberList.Source);
        CreateMap<PurchasePriceUpdateDto, PurchasePrice>(MemberList.Source);
        CreateMap<PurchasePriceDetail, PurchasePriceDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;
        CreateMap<PurchasePriceDetailCreateDto, PurchasePriceDetail>(MemberList.Source);
        CreateMap<PurchasePriceDetailUpdateDto, PurchasePriceDetail>(MemberList.Source);
        CreateMap<ArrivalNotice, ArrivalNoticeDto>();   //
        CreateMap<ArrivalNoticeCreateDto, ArrivalNotice>(MemberList.Source);
        CreateMap<ArrivalNoticeUpdateDto, ArrivalNotice>(MemberList.Source);
        CreateMap<ArrivalNoticeDetail, ArrivalNoticeDetailDto>()
            .ForMember(m => m.PurchaseOrderNumber, x => x.MapFrom(m => m.PurchaseOrderDetail.PurchaseOrder.Number))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.PurchaseOrderDetail.ProductId))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.PurchaseOrderDetail.Product.Name))
            .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.PurchaseOrderDetail.Product.Number))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.PurchaseOrderDetail.Product.Spec))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.PurchaseOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.DefaultWarehouseId, x => x.MapFrom(m => m.PurchaseOrderDetail.Product.DefaultLocation.WarehouseId))
            .ForMember(m => m.DefaultLocationId, x => x.MapFrom(m => m.PurchaseOrderDetail.Product.DefaultLocationId))
            ;
        CreateMap<ArrivalNoticeDetailCreateDto, ArrivalNoticeDetail>(MemberList.Source);
        CreateMap<ArrivalNoticeDetailUpdateDto, ArrivalNoticeDetail>(MemberList.Source);
        CreateMap<PurchaseOrder, PurchaseOrderDto>();

        CreateMap<PurchaseOrderCreateDto, PurchaseOrder>(MemberList.Source);
        CreateMap<PurchaseOrderUpdateDto, PurchaseOrder>(MemberList.Source);
        CreateMap<PurchaseOrderDetail, PurchaseOrderDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name));

        CreateMap<PurchaseOrderDetail, PurchaseOrderDetailSelectDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            .ForMember(m => m.SupplierFullName, x => x.MapFrom(m => m.PurchaseOrder.Supplier.FullName))
            .ForMember(m => m.SupplierShortName, x => x.MapFrom(m => m.PurchaseOrder.Supplier.ShortName))
            ;


        CreateMap<PurchaseOrderDetailCreateDto, PurchaseOrderDetail>(MemberList.Source);
        CreateMap<PurchaseOrderDetailUpdateDto, PurchaseOrderDetail>(MemberList.Source);
        CreateMap<Supplier, SupplierDto>();
        CreateMap<Supplier, SupplierLookupDto>();
        CreateMap<SupplierCreateDto, Supplier>(MemberList.Source);
        CreateMap<SupplierUpdateDto, Supplier>(MemberList.Source);
        CreateMap<PurchaseReturnApply, PurchaseReturnApplyDto>()

            ;
        CreateMap<PurchaseReturnApplyCreateDto, PurchaseReturnApply>(MemberList.Source);
        CreateMap<PurchaseReturnApplyUpdateDto, PurchaseReturnApply>(MemberList.Source);
        CreateMap<PurchaseReturnApplyDetail, PurchaseReturnApplyDetailDto>()
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Id))
            .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Number))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Spec))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.Batch, x => x.MapFrom(m => m.PurchaseStorageDetail.Batch))
            .ForMember(m => m.PurchaseStorageNumber, x => x.MapFrom(m => m.PurchaseStorageDetail.PurchaseStorage.Number))
            ;
        CreateMap<PurchaseReturnApplyDetailCreateDto, PurchaseReturnApplyDetail>(MemberList.Source);
        CreateMap<PurchaseReturnApplyDetailUpdateDto, PurchaseReturnApplyDetail>(MemberList.Source);

        CreateMap<PurchaseReturn, PurchaseReturnDto>()
             .ForMember(m => m.PurchaseReturnApplyNumber, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.Number))
             .ForMember(m => m.ReturnReason, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.ReturnReason))
             .ForMember(m => m.SupplierFullName, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.Supplier.FullName))
             .ForMember(m => m.SupplierShortName, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.Supplier.ShortName))
             .ForMember(m => m.SupplierId, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.SupplierId))
             .ForMember(m => m.PurchaseReturnApplyDescription, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseReturnApply.Description))
             .ForMember(m => m.ProductId, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Id))
             .ForMember(m => m.ProductName, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name))
             .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Spec))
             .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.ProductUnit.Name))
             .ForMember(m => m.ApplyQuantity, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.Quantity))
             .ForMember(m => m.Batch, x => x.MapFrom(m => m.PurchaseReturnApplyDetail.PurchaseStorageDetail.Batch))
            ;

        CreateMap<PurchaseReturnUpdateDto, PurchaseReturn>(MemberList.Source);
        CreateMap<PurchaseReturnDetail, PurchaseReturnDetailDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.PurchaseReturn.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Number))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.PurchaseReturn.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Spec))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.PurchaseReturn.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.PurchaseReturn.PurchaseReturnApplyDetail.PurchaseStorageDetail.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.ProductUnit.Name))
            ;
        CreateMap<PurchaseReturnDetailUpdateDto, PurchaseReturnDetail>(MemberList.Source);

        CreateMap<PurchaseStorage, PurchaseStorageDto>()
            .ForMember(m => m.ArrivalNoticeNumber, x => x.MapFrom(m => m.ArrivalNoticeDetail.ArrivalNotice.Number))
            .ForMember(m => m.SupplierNumber, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.PurchaseOrder.Supplier.Number))
            .ForMember(m => m.SupplierShortName, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.PurchaseOrder.Supplier.ShortName))
            .ForMember(m => m.SupplierFullName, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.PurchaseOrder.Supplier.FullName))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Id))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Spec))
            .ForMember(m => m.ProductDefaultWarehouseId, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.DefaultLocation.WarehouseId))
            .ForMember(m => m.ProductDefaultLocationId, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.DefaultLocationId))
            .ForMember(m => m.ApplyQuantity, x => x.MapFrom(m => m.ArrivalNoticeDetail.Quantity))
            ;


        CreateMap<PurchaseStorageUpdateDto, PurchaseStorage>(MemberList.Source);
        CreateMap<PurchaseStorageDetail, PurchaseStorageDetailDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarhouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Number))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Spec))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.ProductUnit.Name))
            ;

        CreateMap<PurchaseStorageDetail, PurchaseStorageDetailSelectDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarhouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Number))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Spec))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.PurchaseStorage.ArrivalNoticeDetail.PurchaseOrderDetail.Product.ProductUnit.Name))
            ;

        CreateMap<PurchaseStorageDetailUpdateDto, PurchaseStorageDetail>(MemberList.Source);

        CreateMap<WorkOrderOut, WorkOrderOutDto>()
            .ForMember(m => m.WorkOrderNumber, x => x.MapFrom(m => m.MaterialApplyDetail.MaterialApply.WorkOrder.Number))
            .ForMember(m => m.MaterialApplyNumber, x => x.MapFrom(m => m.MaterialApplyDetail.MaterialApply.Number))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.MaterialApplyDetail.Product.Id))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.MaterialApplyDetail.Product.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.MaterialApplyDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.MaterialApplyDetail.Product.Spec))
            .ForMember(m => m.ApplyQuantity, x => x.MapFrom(m => m.MaterialApplyDetail.Quantity))
            ;

        CreateMap<WorkOrderOutUpdateDto, WorkOrderOut>(MemberList.Source);

        CreateMap<WorkOrderOutDetail, WorkOrderOutDetailDto>()
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.LocationName, x => x.MapFrom(m => m.Location.Name))
            .ForMember(m => m.LocationId, x => x.MapFrom(m => m.LocationId))
            ;

        CreateMap<WorkOrderOutDetail, WorkOrderOutDetailSelectDto>()
           .ForMember(m => m.ProductId, x => x.MapFrom(m => m.WorkOrderOut.MaterialApplyDetail.Product.Id))
           .ForMember(m => m.ProductName, x => x.MapFrom(m => m.WorkOrderOut.MaterialApplyDetail.Product.Name))
           .ForMember(m => m.ProductNumber, x => x.MapFrom(m => m.WorkOrderOut.MaterialApplyDetail.Product.Number))
           .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.WorkOrderOut.MaterialApplyDetail.Product.Spec))
           .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.WorkOrderOut.MaterialApplyDetail.Product.ProductUnit.Name))
           .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
           .ForMember(m => m.LocationName, x => x.MapFrom(m => m.Location.Name))
           .ForMember(m => m.LocationId, x => x.MapFrom(m => m.LocationId))
           .ForMember(m => m.WorkOrderNumber, x => x.MapFrom(m => m.WorkOrderOut.MaterialApplyDetail.MaterialApply.WorkOrder.Number))
           .ForMember(m => m.WorkOrderOutNumber, x => x.MapFrom(m => m.WorkOrderOut.Number))
           .ForMember(m => m.MaterialApplyNumber, x => x.MapFrom(m => m.WorkOrderOut.MaterialApplyDetail.MaterialApply.Number))
           ;

        //CreateMap<WorkOrderOutDetailCreateDto, WorkOrderOutDetail>(MemberList.Source);
        CreateMap<WorkOrderOutDetailUpdateDto, WorkOrderOutDetail>(MemberList.Source);
        CreateMap<WorkOrderReturn, WorkOrderReturnDto>()
            .ForMember(m => m.MaterialReturnApplyNumber, x => x.MapFrom(m => m.MaterialReturnApplyDetail.MaterialReturnApply.Number))
            .ForMember(m => m.WorkOrderNumber, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.MaterialApply.WorkOrder.Number))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.Id))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.Spec))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ApplyQuantity, x => x.MapFrom(m => m.MaterialReturnApplyDetail.Quantity))
            .ForMember(m => m.Batch, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.Batch))
            .ForMember(m => m.ProductDefaultLocationId, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.DefaultLocationId))
            .ForMember(m => m.ProductDefaultWarehouseId, x => x.MapFrom(m => m.MaterialReturnApplyDetail.WorkOrderOutDetail.WorkOrderOut.MaterialApplyDetail.Product.DefaultLocation.WarehouseId))
            ;


        CreateMap<WorkOrderReturnUpdateDto, WorkOrderReturn>(MemberList.Source);
        CreateMap<WorkOrderReturnDetail, WorkOrderReturnDetailDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.LocationName, x => x.MapFrom(m => m.Location.Name))
            ;

        CreateMap<WorkOrderReturnDetailUpdateDto, WorkOrderReturnDetail>(MemberList.Source);
        CreateMap<WorkOrderStorage, WorkOrderStorageDto>()
            .ForMember(m => m.ApplyNumber, x => x.MapFrom(m => m.WorkOrderStorageApply.Number))
            .ForMember(m => m.WorkOrderNumber, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Number))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.ProductId))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Product.Name))
            .ForMember(m => m.Quantity, x => x.MapFrom(m => m.WorkOrderStorageApply.Quantity))
            .ForMember(m => m.KeeperUserName, x => x.MapFrom(m => m.KeeperUser.Name))
            .ForMember(m => m.KeeperUserSurname, x => x.MapFrom(m => m.KeeperUser.Surname))
            .ForMember(m => m.ProductDefaultLocationId, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Product.DefaultLocationId))
            .ForMember(m => m.ProductDefaultWarehouseId, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Product.DefaultLocation.WarehouseId))
            ;

        CreateMap<WorkOrderStorageUpdateDto, WorkOrderStorage>(MemberList.Source);
        CreateMap<WorkOrderStorageDetail, WorkOrderStorageDetailDto>()
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            .ForMember(m => m.LocationName, x => x.MapFrom(m => m.Location.Name))
            ;


        CreateMap<WorkOrderStorageDetailUpdateDto, WorkOrderStorageDetail>(MemberList.Source);

        CreateMap<SafetyInventory, SafetyInventoryDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name));
        CreateMap<SafetyInventoryCreateDto, SafetyInventory>(MemberList.Source);
        CreateMap<SafetyInventoryUpdateDto, SafetyInventory>(MemberList.Source);

        CreateMap<ArrivalInspection, ArrivalInspectionDto>()
            .ForMember(m => m.ArrivalNoticeNumber, x => x.MapFrom(m => m.ArrivalNoticeDetail.ArrivalNotice.Number))
            .ForMember(m => m.PurchaseStorageNumber, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseStorage.Number))
            .ForMember(m => m.ArrivalNoticeQuantity, x => x.MapFrom(m => m.ArrivalNoticeDetail.Quantity))
            .ForMember(m => m.ProductId, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.ProductId))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Name))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.ProductUnit.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.ArrivalNoticeDetail.PurchaseOrderDetail.Product.Spec))
            ;

        CreateMap<ArrivalInspectionUpdateDto, ArrivalInspection>(MemberList.Source);

        CreateMap<WorkOrderStorageApply, WorkOrderStorageApplyDto>()
            .ForMember(m => m.MpsNumber, x => x.MapFrom(m => m.WorkOrder.Mps.Number))
            .ForMember(m => m.WorkOrderStorageNumber, x => x.MapFrom(m => m.WorkOrderStorage.Number))
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.WorkOrder.Product.Name))
            ;
        CreateMap<WorkOrderStorageApplyCreateDto, WorkOrderStorageApply>(MemberList.Source);
        CreateMap<WorkOrderStorageApplyUpdateDto, WorkOrderStorageApply>(MemberList.Source);

        CreateMap<OtherOut, OtherOutDto>();

        CreateMap<OtherOutCreateDto, OtherOut>(MemberList.Source);
        CreateMap<OtherOutUpdateDto, OtherOut>(MemberList.Source);
        CreateMap<OtherOutDetail, OtherOutDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            ;
        CreateMap<OtherOutDetailCreateDto, OtherOutDetail>(MemberList.Source);
        CreateMap<OtherOutDetailUpdateDto, OtherOutDetail>(MemberList.Source);
        CreateMap<OtherStorage, OtherStorageDto>();
        CreateMap<OtherStorageCreateDto, OtherStorage>(MemberList.Source);
        CreateMap<OtherStorageUpdateDto, OtherStorage>(MemberList.Source);
        CreateMap<OtherStorageDetail, OtherStorageDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            .ForMember(m => m.WarehouseId, x => x.MapFrom(m => m.Location.WarehouseId))
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            ;
        CreateMap<OtherStorageDetailCreateDto, OtherStorageDetail>(MemberList.Source);
        CreateMap<OtherStorageDetailUpdateDto, OtherStorageDetail>(MemberList.Source);
        CreateMap<InventoryMove, InventoryMoveDto>();
        CreateMap<InventoryMoveCreateDto, InventoryMove>(MemberList.Source);
        CreateMap<InventoryMoveUpdateDto, InventoryMove>(MemberList.Source);
        CreateMap<InventoryMoveDetail, InventoryMoveDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            .ForMember(m => m.OutWarehouseName, x => x.MapFrom(m => m.OutLocation.Warehouse.Name))
            .ForMember(m => m.OutLocationName, x => x.MapFrom(m => m.OutLocation.Name))
            .ForMember(m => m.InWarehouseName, x => x.MapFrom(m => m.InLocation.Warehouse.Name))
            .ForMember(m => m.InLocationName, x => x.MapFrom(m => m.InLocation.Name))
            .ForMember(m => m.InWarehouseId, x => x.MapFrom(m => m.InLocation.WarehouseId))
            ;
        CreateMap<InventoryMoveDetailCreateDto, InventoryMoveDetail>(MemberList.Source);
        CreateMap<InventoryMoveDetailUpdateDto, InventoryMoveDetail>(MemberList.Source);
        CreateMap<InventoryCheck, InventoryCheckDto>();
        CreateMap<InventoryCheckCreateDto, InventoryCheck>(MemberList.Source);
        CreateMap<InventoryCheckUpdateDto, InventoryCheck>(MemberList.Source);
        CreateMap<InventoryCheckDetail, InventoryCheckDetailDto>()
            .ForMember(m => m.WarehouseName, x => x.MapFrom(m => m.Location.Warehouse.Name))
            ;
        CreateMap<InventoryCheckDetailCreateDto, InventoryCheckDetail>(MemberList.Source);
        CreateMap<InventoryCheckDetailUpdateDto, InventoryCheckDetail>(MemberList.Source);
        CreateMap<InventoryTransform, InventoryTransformDto>();
        CreateMap<InventoryTransformCreateDto, InventoryTransform>(MemberList.Source);
        CreateMap<InventoryTransformUpdateDto, InventoryTransform>(MemberList.Source);
        CreateMap<InventoryTransformAfterDetail, InventoryTransformAfterDetailDto>();
        CreateMap<InventoryTransformAfterDetailCreateDto, InventoryTransformAfterDetail>(MemberList.Source);
        CreateMap<InventoryTransformAfterDetailUpdateDto, InventoryTransformAfterDetail>(MemberList.Source);
        CreateMap<InventoryTransformBeforeDetail, InventoryTransformBeforeDetailDto>();
        CreateMap<InventoryTransformBeforeDetailCreateDto, InventoryTransformBeforeDetail>(MemberList.Source);
        CreateMap<InventoryTransformBeforeDetailUpdateDto, InventoryTransformBeforeDetail>(MemberList.Source);
        CreateMap<ProcessInspection, ProcessInspectionDto>()
             .ForMember(m => m.WorkOrderNumber, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Number))
             .ForMember(m => m.ProductName, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Product.Name))
             .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Product.Spec))
             .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Product.ProductUnit.Name))
             .ForMember(m => m.WorkOrderQuantity, x => x.MapFrom(m => m.WorkOrderStorageApply.WorkOrder.Quantity))
            ;
        CreateMap<ProcessInspectionCreateDto, ProcessInspection>(MemberList.Source);
        CreateMap<ProcessInspectionUpdateDto, ProcessInspection>(MemberList.Source);
        CreateMap<FinalInspection, FinalInspectionDto>()
            .ForMember(m => m.ProductName, x => x.MapFrom(m => m.Mps.Product.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.Mps.Product.Spec))
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Mps.Product.ProductUnit.Name))
            .ForMember(m => m.MpsQuantity, x => x.MapFrom(m => m.Mps.Quantity))
            ;
        CreateMap<FinalInspectionCreateDto, FinalInspection>(MemberList.Source);
        CreateMap<FinalInspectionUpdateDto, FinalInspection>(MemberList.Source);
        CreateMap<MpsDetail, MpsDetailDto>();
        CreateMap<MpsDetailCreateDto, MpsDetail>(MemberList.Source);
        CreateMap<MpsDetailUpdateDto, MpsDetail>(MemberList.Source);
        CreateMap<PurchaseApply, PurchaseApplyDto>()
            ;

        CreateMap<PurchaseApplyCreateDto, PurchaseApply>(MemberList.Source);
        CreateMap<PurchaseApplyUpdateDto, PurchaseApply>(MemberList.Source);
        CreateMap<PurchaseApplyDetail, PurchaseApplyDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;
        CreateMap<PurchaseApplyDetailCreateDto, PurchaseApplyDetail>(MemberList.Source);
        CreateMap<PurchaseApplyDetailUpdateDto, PurchaseApplyDetail>(MemberList.Source);

        CreateMap<WorkOrder, HomeWorkOrderDto>(MemberList.None)
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            .ForMember(m => m.ProductSpec, x => x.MapFrom(m => m.Product.Spec))
            ; ;
        CreateMap<MrpDetail, MrpDetailDto>()
            .ForMember(m => m.ProductUnitName, x => x.MapFrom(m => m.Product.ProductUnit.Name))
            ;
    }
}
