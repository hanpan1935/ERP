//using Lanpuda.ERP.BasicData.ProductCategories;
//using Lanpuda.ERP.BasicData.Products;
//using Lanpuda.ERP.BasicData.ProductUnits;
//using Lanpuda.ERP.ProductionManagement.Boms;
//using Lanpuda.ERP.ProductionManagement.Workshops;
//using Lanpuda.ERP.PurchaseManagement.Suppliers;
//using Lanpuda.ERP.SalesManagement.Customers;
//using Lanpuda.ERP.WarehouseManagement.Locations;
//using Lanpuda.ERP.WarehouseManagement.Warehouses;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Volo.Abp.Data;
//using Volo.Abp.DependencyInjection;
//using Volo.Abp.Guids;
//using Volo.Abp.Identity;

//namespace Lanpuda.ERP
//{

//    public class ERPDataSeederContributor : IDataSeedContributor, ITransientDependency
//    {
//        private readonly IProductCategoryRepository _productCategoryRepository;
//        private readonly IProductUnitRepository _productUnitRepository;
//        private readonly IProductRepository _productRepository;
//        private readonly ICustomerRepository _customerRepository;
//        private readonly ISupplierRepository _supplierRepository;
//        private readonly IGuidGenerator _guidGenerator;
//        private readonly IIdentityUserRepository _identityUserRepository;
//        private readonly IdentityUserManager _identityUserManager;
//        private readonly IWarehouseRepository _warehouseRepository;
//        private readonly ILocationRepository _locationRepository;
//        private readonly IBomRepository _bomRepository;
//        private readonly IBomDetailRepository _bomDetailRepository;
//        private readonly IWorkshopRepository _workshopRepository;

//        public ERPDataSeederContributor(
//               IGuidGenerator guidGenerator,
//               IProductCategoryRepository productCategoryRepository,
//               IProductUnitRepository productUnitRepository,
//               IProductRepository productRepository,
//               ICustomerRepository customerRepository,
//               ISupplierRepository supplierRepository,
//               IIdentityUserRepository identityUserRepository,
//               IdentityUserManager identityUserManager,
//               IWarehouseRepository warehouseRepository,
//               ILocationRepository locationRepository,
//               IBomRepository bomRepository,
//               IBomDetailRepository bomDetailRepository,
//               IWorkshopRepository workshopRepository
//           )
//        {
//            _productCategoryRepository = productCategoryRepository;
//            _productUnitRepository = productUnitRepository;
//            _productRepository = productRepository;
//            _customerRepository = customerRepository;
//            _supplierRepository = supplierRepository;
//            _identityUserRepository = identityUserRepository;
//            _identityUserManager = identityUserManager;
//            _guidGenerator = guidGenerator;
//            _warehouseRepository = warehouseRepository;
//            _locationRepository = locationRepository;
//            _bomRepository = bomRepository;
//            _bomDetailRepository = bomDetailRepository;
//            _workshopRepository = workshopRepository;
//        }


//        public async Task SeedAsync(DataSeedContext context)
//        {
//            await Task.Delay(100);
//            //await SeedCategoryAndUnitAsync();
//            //await SeedProductAsync();
//            //await SeedCustomerAsync();
//            //await SeedSupplierAsync();
//            //await SeedWorkshopAsync();
//            //await SeedWarehouseAsync();
//            //await SeedLocationAsync();
//        }

//        /// <summary>
//        /// 产品分类和产品单位
//        /// </summary>
//        /// <returns></returns>
//        private async Task SeedCategoryAndUnitAsync()
//        {
//            if (await _productCategoryRepository.GetCountAsync() <= 0)
//            {
//                List<ProductCategory> productCategories = new List<ProductCategory>()
//                {
//                    new ProductCategory(Guid.Parse("02F92C27-CF7C-71E1-67DE-87001A8411F0"),"原材料","C01",""),
//                    new ProductCategory(Guid.Parse("73255068-7128-203B-E418-D76698EE96D5"),"半成品","C02",""),
//                    new ProductCategory(Guid.Parse("927F75FB-E81F-3ABF-0F09-62D8C967989A"),"成品","C03",""),
//                    new ProductCategory(Guid.Parse("EE5F3766-A0B8-A03E-D0B1-5D73B238E4A6"),"辅料","C04",""),
//                    new ProductCategory(Guid.Parse("8AE841B5-F229-12D1-949F-B34EF1DB7180"),"设备备件","C05",""),
//                };
//                await _productCategoryRepository.InsertManyAsync(productCategories, true);
//            }

//            if (await _productUnitRepository.GetCountAsync() <= 0)
//            {
//                List<ProductUnit> productUnits = new List<ProductUnit>()
//                {
//                    new ProductUnit(Guid.Parse("5F1DF78D-D775-D023-00CE-487DBF3B8CDA"),"克",  "U01",""),
//                    new ProductUnit(Guid.Parse("15B91980-2628-7A90-14C0-259AD1F6B8A4"),"千克","U02",""),
//                    new ProductUnit(Guid.Parse("F5299463-ECA1-7749-60FD-BE7F1EF45AD7"),"吨",  "U02",""),
//                    new ProductUnit(Guid.Parse("1AF4C57C-0D23-0384-5C93-3544B9784CEC"),"毫升","U03",""),
//                    new ProductUnit(Guid.Parse("643AAF44-214B-5DFA-9EED-73B805432FF7"),"升",  "U04",""),
//                    new ProductUnit(Guid.Parse("372245BD-0EA4-DFA4-A446-C04DCB188F3B"),"袋",  "U05",""),
//                    new ProductUnit(Guid.Parse("8E9568C3-BD31-4C65-7DC9-E2BFEDF3A7AA"),"个",  "U06",""),
//                    new ProductUnit(Guid.Parse("523FB4CD-5ABE-5DBA-D8EE-BC158A4BE429"),"台",  "U07",""),
//                    new ProductUnit(Guid.Parse("986251C2-1ED2-454B-9A41-6AA96ADCE2B8"),"条",  "U08",""),
//                };
//                await _productUnitRepository.InsertManyAsync(productUnits, true);
//            }

//        }

//        private async Task SeedProductAsync()
//        {
//            List<Product> products = new List<Product>()
//            {
//               new Product(Guid.Parse("623252BD-AE12-4EFD-BDB7-0E72C0E9B782")){ Name = "A" ,ProductCategoryId = Guid.Parse("927F75FB-E81F-3ABF-0F09-62D8C967989A"),ProductUnitId = Guid.Parse("15B91980-2628-7A90-14C0-259AD1F6B8A4"),SourceType = ProductSourceType.Self,    ProductionBatch = null,LeadTime = 1},
//               new Product(Guid.Parse("51D5711E-138C-4EEC-B8C3-8DF430E89F29")){ Name = "B" ,ProductCategoryId = Guid.Parse("927F75FB-E81F-3ABF-0F09-62D8C967989A"),ProductUnitId = Guid.Parse("15B91980-2628-7A90-14C0-259AD1F6B8A4"),SourceType = ProductSourceType.Self,    ProductionBatch = null,LeadTime = 2},
//               new Product(Guid.Parse("D0759968-313B-426E-94A1-76CF14B0ED84")){ Name = "C" ,ProductCategoryId = Guid.Parse("73255068-7128-203B-E418-D76698EE96D5"),ProductUnitId = Guid.Parse("15B91980-2628-7A90-14C0-259AD1F6B8A4"),SourceType = ProductSourceType.Self,    ProductionBatch = null,LeadTime = 3},
//               new Product(Guid.Parse("7378AA2E-61F8-4D07-AEC5-98D0BBAE4D62")){ Name = "D" ,ProductCategoryId = Guid.Parse("73255068-7128-203B-E418-D76698EE96D5"),ProductUnitId = Guid.Parse("15B91980-2628-7A90-14C0-259AD1F6B8A4"),SourceType = ProductSourceType.Purchase,ProductionBatch = null,LeadTime = 1},
//               new Product(Guid.Parse("FE3DE193-A008-4B48-95EE-CE54E3092B96")){ Name = "E" ,ProductCategoryId = Guid.Parse("02F92C27-CF7C-71E1-67DE-87001A8411F0"),ProductUnitId = Guid.Parse("15B91980-2628-7A90-14C0-259AD1F6B8A4"),SourceType = ProductSourceType.Purchase,ProductionBatch = 120,LeadTime = 1},
//               new Product(Guid.Parse("B923CE27-D6FB-4874-A17E-4D22D685D217")){ Name = "F" ,ProductCategoryId = Guid.Parse("02F92C27-CF7C-71E1-67DE-87001A8411F0"),ProductUnitId = Guid.Parse("15B91980-2628-7A90-14C0-259AD1F6B8A4"),SourceType = ProductSourceType.Purchase,ProductionBatch = 50,LeadTime = 1},
//            };

//            await _productRepository.InsertManyAsync(products, true);
//        }

//        private async Task SeedCustomerAsync()
//        {
//            List<Customer> customers = new List<Customer>()
//            {
//                new Customer(Guid.Parse("AECD5A66-F4B6-24C9-E04B-9C39139CD391")){Number = "C20220923" ,ShortName = "客户A",FullName = "XX测试客户A制造有限公司"},
//                new Customer(Guid.Parse("7F1BB1CB-66F4-38C9-D050-FA1782CB96D1")){Number = "C20220924" ,ShortName = "客户B",FullName = "XX测试客户B制造有限公司"},
//                new Customer(Guid.Parse("C44B2395-0A68-649D-AA6C-9A6D1B5C2CAC")){Number = "C20220925" ,ShortName = "客户C",FullName = "XX测试客户C制造有限公司"},
//                new Customer(Guid.Parse("1E6B505B-A9D4-66A6-1958-5AA3FB71D737")){Number = "C20220926" ,ShortName = "客户D",FullName = "XX测试客户D制造有限公司"},
//                new Customer(Guid.Parse("83D22416-D54E-5A2F-73FB-93CA2F73FC33")){Number = "C20220927" ,ShortName = "客户E",FullName = "XX测试客户E制造有限公司"},
//                new Customer(Guid.Parse("E538DE03-CD8C-5CF2-5147-8397C8C5AF66")){Number = "C20220928" ,ShortName = "客户F",FullName = "XX测试客户F制造有限公司"},
//                new Customer(Guid.Parse("B8C6ED2D-7A95-7D88-99A7-85A6F98CAA7F")){Number = "C20220929" ,ShortName = "客户G",FullName = "XX测试客户G制造有限公司"},
//            };

//            await _customerRepository.InsertManyAsync(customers, true);
//        }

//        private async Task SeedSupplierAsync()
//        {
//            List<Supplier> suppliers = new List<Supplier>()
//            {
//                new Supplier(Guid.Parse("7126A780-CC51-B9A5-153E-8CC5F6A30C47")){Number="S20221011",ShortName = "供应商A",FullName = "测试供应商A制造有限公司"},
//                new Supplier(Guid.Parse("BE6DE83A-1953-496E-CD25-C75BFD8E9974")){Number="S20221012",ShortName = "供应商B",FullName = "测试供应商B制造有限公司"},
//                new Supplier(Guid.Parse("D7A11989-4614-CB9B-849A-F2D7BCBA4992")){Number="S20221013",ShortName = "供应商C",FullName = "测试供应商C制造有限公司"},
//                new Supplier(Guid.Parse("E6197BB9-D6C7-175C-9FC9-1BE93A623CAB")){Number="S20221014",ShortName = "供应商D",FullName = "测试供应商D制造有限公司"},
//                new Supplier(Guid.Parse("8265C8A8-9FC9-9124-3157-220545784764")){Number="S20221015",ShortName = "供应商E",FullName = "测试供应商E制造有限公司"},
//                new Supplier(Guid.Parse("D798534C-A96A-72F6-A82F-7DFF15D64384")){Number="S20221016",ShortName = "供应商F",FullName = "测试供应商F制造有限公司"},
//                new Supplier(Guid.Parse("E470F20B-D56B-6AC1-17AD-A103B984CFD0")){Number="S20221017",ShortName = "供应商G",FullName = "测试供应商G制造有限公司"},
//                new Supplier(Guid.Parse("4D0672AB-C647-47C2-69B2-92B9020B6434")){Number="S20221018",ShortName = "供应商H",FullName = "测试供应商H制造有限公司"},
//                new Supplier(Guid.Parse("ED0E1C15-137A-303C-9E44-888B430625DC")){Number="S20221019",ShortName = "供应商I",FullName = "测试供应商I制造有限公司"},
//                new Supplier(Guid.Parse("4CE53E39-097E-E6EA-5C1A-1CABCAA0F422")){Number="S20221020",ShortName = "供应商J",FullName = "测试供应商J制造有限公司"},
//            };
//            await _supplierRepository.InsertManyAsync(suppliers, true);
//        }

//        private async Task SeedWorkshopAsync()
//        {
//            List<Workshop> workshops = new List<Workshop>()
//            {
//                new Workshop(Guid.Parse("E101EFED-59CA-6750-741D-CD91CD31A510")){Name ="密炼车间A",Number = "ML01"},
//                new Workshop(Guid.Parse("E9FDC1EE-7FB6-0186-EB12-35E028E4642D")){Name ="密炼车间B",Number = "ML02"},
//                new Workshop(Guid.Parse("F3C22988-B43D-E2ED-F216-F5A9D05BF0ED")){Name ="部件车间A",Number = "BJ01"},
//                new Workshop(Guid.Parse("82E9D4F5-71A8-D753-CE87-E2C563BA3358")){Name ="部件车间B",Number = "BJ02"},
//                new Workshop(Guid.Parse("4F26CC23-0284-BF68-A635-5B6ACB49B604")){Name ="成型车间A",Number = "CX01"},
//                new Workshop(Guid.Parse("A88A115F-9EE0-655A-428F-3C61A661CE1B")){Name ="成型车间B",Number = "CX02"},
//                new Workshop(Guid.Parse("2C7D2C22-198F-5955-5F9B-6465D70038B7")){Name ="硫化车间A",Number = "LH01"},
//                new Workshop(Guid.Parse("3779EC4E-B41B-9070-6C34-C32222081418")){Name ="硫化车间B",Number = "LH02"},
//            };

//            await _workshopRepository.InsertManyAsync(workshops, true);
//        }

//        private async Task SeedWarehouseAsync()
//        {
//            List<Warehouse> warehouses = new List<Warehouse>()
//            {
//                new Warehouse(Guid.Parse("E5FFE373-369F-1AF0-DBB4-FDD24FBACE08")){Number = "M01",Name = "原材料仓库1"},
//                new Warehouse(Guid.Parse("0998D56A-B74A-C809-205E-F30CB0F1167B")){Number = "M02",Name = "原材料仓库2"},
//                new Warehouse(Guid.Parse("9E4DE050-B13D-9702-469B-F7E6034B2837")){Number = "S21",Name = "半成品库1"},
//                new Warehouse(Guid.Parse("203EF635-6C82-2C86-BE74-15EBB31E2587")){Number = "S22",Name = "半成品库2"},
//                new Warehouse(Guid.Parse("B487202E-D75A-C6DA-4663-B77938332383")){Number = "F01",Name = "成品库1"},
//                new Warehouse(Guid.Parse("B6B503BD-E4FF-7CDA-00F3-8CD3B00CC3CB")){Number = "F02",Name = "成品库2"},
//            };
//            await _warehouseRepository.InsertManyAsync(warehouses, true);
//        }

//        private async Task SeedLocationAsync()
//        {
//            List<Location> locations = new List<Location>()
//            {
//                new Location(Guid.Parse("1B16C2EC-E690-F922-2227-C4A37EE19A71")){WarehouseId = Guid.Parse("E5FFE373-369F-1AF0-DBB4-FDD24FBACE08"),Number ="M01_L01",Name = "库位1",Remark =""},
//                new Location(Guid.Parse("676446D1-FA8D-C226-91D8-3B645025E69F")){WarehouseId = Guid.Parse("0998D56A-B74A-C809-205E-F30CB0F1167B"),Number ="M02_L01",Name = "库位1",Remark =""},
//                new Location(Guid.Parse("4E7EE8A1-68C2-FDE8-80D3-F47CFEA7F7E8")){WarehouseId = Guid.Parse("9E4DE050-B13D-9702-469B-F7E6034B2837"),Number ="S21_L01",Name = "库位1",Remark =""},
//                new Location(Guid.Parse("EFA654D3-2D6A-73F2-FA40-D16BD73F82C3")){WarehouseId = Guid.Parse("203EF635-6C82-2C86-BE74-15EBB31E2587"),Number ="S22_L01",Name = "库位1",Remark =""},
//                new Location(Guid.Parse("BC35B61B-F1B7-5231-011C-34A9B34C91F0")){WarehouseId = Guid.Parse("B487202E-D75A-C6DA-4663-B77938332383"),Number ="F01_L01",Name = "库位1",Remark =""},
//                new Location(Guid.Parse("0C48C23B-B508-9E74-F869-EF4D2279B7D5")){WarehouseId = Guid.Parse("B6B503BD-E4FF-7CDA-00F3-8CD3B00CC3CB"),Number ="F02_L01",Name = "库位1",Remark =""},
//            };

//            await _locationRepository.InsertManyAsync(locations, true);
//        }
//    }
//}
