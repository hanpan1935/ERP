using AutoMapper;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.BasicData.Products.Dtos;
using Lanpuda.ERP.ProductionManagement.Boms.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.ProductionManagement.Mpses.Edits;
using Lanpuda.ERP.ProductionManagement.WorkOrders;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Edits;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies;
using Lanpuda.ERP.PurchaseManagement.PurchaseReturnApplies.Dtos;
using Lanpuda.ERP.SalesManagement.Customers.Dtos;
using Lanpuda.ERP.SalesManagement.Customers.Edits;
using Lanpuda.ERP.SalesManagement.SalesPrices.Dtos;
using Lanpuda.ERP.SalesManagement.SalesPrices.Edits;

namespace Lanpuda.ERP
{
    public class ERPUIProfile : Profile
    {
        public ERPUIProfile()
        {
            //CreateMap<ArrivalNoticeDto, ArrivalNoticePagedModel>();
            //CreateMap<PurchaseReturnApplyDto, PurchaseReturnApplyPagedModel>();
            CreateMap<ProductDto, ProductPagedModel>();
            
            CreateMap<MpsDto, MpsEditModel>();

            CreateMap<BomLookupDto, WorkOrderMaterialModel>();
            CreateMap<WorkOrderDto, WorkOrderPagedModel>();
            

            #region Sales
            CreateMap<CustomerDto,CustomerEditModel>();
            CreateMap<CustomerEditModel, CustomerUpdateDto>();
            CreateMap<CustomerEditModel, CustomerCreateDto>();


            CreateMap<SalesPriceDto, SalesPriceEditModel>();
            CreateMap<SalesPriceEditModel, SalesPriceUpdateDto>();
            CreateMap<SalesPriceEditModel, SalesPriceCreateDto>();

            CreateMap<SalesPriceDetailDto, SalesPriceDetailEditModel>();
            CreateMap<SalesPriceDetailEditModel, SalesPriceDetailCreateDto>();
            CreateMap<SalesPriceDetailEditModel, SalesPriceDetailUpdateDto>();
            #endregion

        }
    }
}
