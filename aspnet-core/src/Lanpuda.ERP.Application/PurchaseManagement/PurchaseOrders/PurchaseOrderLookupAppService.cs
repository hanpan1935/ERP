//using Lanpuda.ERP.PurchaseManagement.PurchaseOrders.Dtos;
//using Lanpuda.ERP.Utils.UniqueCode;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.Application.Dtos;
//using Volo.Abp.Domain.Entities;
//using Volo.Abp.Domain.Repositories;
//using System.Linq.Dynamic.Core;

//namespace Lanpuda.ERP.PurchaseManagement.PurchaseOrders
//{
//    public class PurchaseOrderLookupAppService : ERPAppService, IPurchaseOrderLookupAppService
//    {
//        private readonly IPurchaseOrderRepository _repository;
//        private readonly IPurchaseOrderDetailRepository _detailRepository;
//        private readonly IUniqueCodeUtils _uniqueCodeUtils;

//        public PurchaseOrderLookupAppService(
//            IPurchaseOrderRepository repository,
//            IPurchaseOrderDetailRepository detailRepository,
//            IUniqueCodeUtils uniqueCodeUtils)
//        {
//            _repository = repository;
//            _detailRepository = detailRepository;
//            _uniqueCodeUtils = uniqueCodeUtils;
//        }

     
//        public async Task<PurchaseOrderDto> GetByNumberAsync(string number)
//        {
//            var queryable = await _repository.WithDetailsAsync();
//            queryable = queryable.Where(x => x.Number == number);

//            var result = await AsyncExecuter.FirstOrDefaultAsync(queryable);
//            if (result == null)
//            {
//                throw new EntityNotFoundException("没有找到单号为：" + number + " 的采购订单！");
//            }

//            var dto = ObjectMapper.Map<PurchaseOrder, PurchaseOrderDto>(result);
//            //TODO 查询已入库数量
//            return dto;
//        }

//        public async Task<List<PurchaseOrderDto>> GetOpenedListBySupplierIdAsync(Guid id)
//        {
//            var queryable = await _repository.WithDetailsAsync();
//            queryable = queryable.Where(
//                m => m.SupplierId == id &&
//                m.CloseStatus == PurchaseOrderCloseStatus.Opened
//                );

//            var result = await AsyncExecuter.ToListAsync(queryable);
//            var dtoList = ObjectMapper.Map<List<PurchaseOrder>, List<PurchaseOrderDto>>(result);
//            return dtoList;
//        }

//        public async Task<PagedResultDto<PurchaseOrderDto>> GetPagedListAsync(PurchaseOrderPagedRequestDto input)
//        {
//            if (string.IsNullOrEmpty(input.Sorting))
//            {
//                input.Sorting = "CreationTime" + " desc";
//            }
//            var query = await _repository.WithDetailsAsync();
//            query = query
//                .WhereIf(!string.IsNullOrEmpty(input.Number), m => m.Number.Contains(input.Number))
//                .WhereIf(input.SupplierId != null, m => m.SupplierId == input.SupplierId)
//                .WhereIf(input.RequiredDateStart != null, m => m.RequiredDate >= input.RequiredDateStart)
//                .WhereIf(input.RequiredDateEnd != null, m => m.RequiredDate <= input.RequiredDateEnd)
//                ;
//            long totalCount = await AsyncExecuter.CountAsync(query);
//            query = query.OrderBy(input.Sorting).Skip(input.SkipCount).Take(input.MaxResultCount);
//            var result = await AsyncExecuter.ToListAsync(query);
//            return new PagedResultDto<PurchaseOrderDto>(totalCount, ObjectMapper.Map<List<PurchaseOrder>, List<PurchaseOrderDto>>(result));
//        }

    
//    }
//}
