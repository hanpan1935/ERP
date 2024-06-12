using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.BasicData.Products;
using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
using Lanpuda.ERP.ProductionManagement.WorkOrders.Dtos;
using Lanpuda.ERP.UI.ProductionManagement.Mpses.Profiles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Validation.StringValues;
using static Lanpuda.ERP.Permissions.ERPPermissions;

namespace Lanpuda.ERP.ProductionManagement.Mpses.Profiles
{
    public class MpsProfileViewModel : RootViewModelBase
    {
        private readonly IMpsAppService _mpsAppService;
        public MpsProfileModel Model
        {
            get { return GetProperty(() => Model); }
            set { SetProperty(() => Model, value); }
        }

        public Guid MpsId { get; set; }
        public MpsProfileViewModel(IMpsAppService mpsAppService) 
        {
            _mpsAppService = mpsAppService;
            Model = new MpsProfileModel();
        }

        [AsyncCommand]
        public async Task InitializeAsync()
        {
            try
            {
                this.IsLoading = true;
                var result = await _mpsAppService.GetProfileAsync(MpsId);
                Model.Number = result.Number;
                Model.MpsType = result.MpsType;
                Model.StartDate = result.StartDate;
                Model.CompleteDate = result.CompleteDate;
                Model.ProductId = result.ProductId;
                Model.ProductNumber = result.ProductNumber;
                Model.ProductName = result.ProductName;
                Model.ProductSpec = result.ProductSpec;
                Model.ProductUnitName = result.ProductUnitName;
                Model.Quantity = result.Quantity;
                Model.Remark = result.Remark;
                Model.IsConfirmed = result.IsConfirmed;
                Model.CreatorSurname = result.CreatorSurname;
                Model.CreatorName = result.CreatorName;
                Model.CreationTime = result.CreationTime;
                Model.PurchaseApplyId = result.PurchaseApplyId;
                Model.PurchaseApplyNumber = result.PurchaseApplyNumber;
                Model.Details = new ObservableCollection<MpsDetailDto>(result.Details);
                Model.WorkOrderDetails = new ObservableCollection<WorkOrderDto>(result.WorkOrderDetails);
                Model.MrpDetailsModels = new ObservableCollection<MrpDetailProfileModel>();
                foreach (var mrpDetail in result.MrpDetails)
                {
                    MrpDetailProfileModel mrpDetailProfile = new MrpDetailProfileModel();
                    mrpDetailProfile.ProductId = mrpDetail.ProductId;
                    mrpDetailProfile.RequiredDate = mrpDetail.RequiredDate;
                    mrpDetailProfile.Quantity = mrpDetail.Quantity;
                    mrpDetailProfile.Id = mrpDetail.Id; 

                    var exsitsProduct =  Model.ProductList.Where(m => m.ProductId == mrpDetail.ProductId).FirstOrDefault();
                    if (exsitsProduct != null)
                    {
                        mrpDetailProfile.Product = exsitsProduct;
                    }
                    else
                    {
                        MrpDetailProductProfileModel product = new MrpDetailProductProfileModel();
                        product.ProductId = mrpDetail.ProductId;
                        product.ProductName = mrpDetail.ProductName;
                        product.ProductNumber = mrpDetail.ProductNumber;
                        product.ProductSpec = mrpDetail.ProductSpec;
                        product.ProductUnitName = mrpDetail.ProductUnitName;
                        product.ProductSourceType = mrpDetail.ProductSourceType;
                        product.ProductLeadTime = mrpDetail.ProductLeadTime;
                        Model.ProductList.Add(product);
                        mrpDetailProfile.Product = product;
                    }
                    
                    Model.MrpDetailsModels.Add(mrpDetailProfile);
                }

            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        [AsyncCommand]
        public async Task CreatePurchaseApplyOrWorkOrderAsync(string type)
        {
            try
            {
                this.IsLoading = true;
                CreatePurchaseApplyWorkOrderByMrpInput intput = new CreatePurchaseApplyWorkOrderByMrpInput();
                intput.MpsId = this.MpsId;

                if (type == "purchaseApply")
                {
                    var list = this.Model.MrpDetailsModels.Where(m => m.Product.ProductSourceType != ProductSourceType.Self).ToList();
                    foreach (var item in list)
                    {
                        intput.MrpDetailIdList.Add(item.Id);
                    }
                }
                else if (type == "workOrder")
                {
                    var list = this.Model.MrpDetailsModels.Where(m => m.Product.ProductSourceType == ProductSourceType.Self).ToList();
                    foreach (var item in list)
                    {
                        intput.MrpDetailIdList.Add(item.Id);
                    }
                }
                await _mpsAppService.CreatePurchaseApplyWorkOrderAsync(intput);
                await this.InitializeAsync();
            }
            catch (Exception ex)
            {
                HandleException(ex);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        public bool CanCreatePurchaseApplyOrWorkOrderAsync(string type)
        {
            if (this.Model.IsConfirmed == false)
            {
                return false;
            }

            if (type == "purchaseApply")
            {
                if (!string.IsNullOrEmpty(Model.PurchaseApplyNumber))
                {
                    return false;
                }
            }
            if (type == "workOrder")
            {
                if (Model.WorkOrderDetails.Count >0)
                {
                    return false;
                }
            }
            return true;
        }


        //[AsyncCommand]
        //public async Task CreateByProductIdAsync(Guid productId)
        //{
        //    try
        //    {
        //        this.IsLoading = true;
        //        CreatePurchaseApplyWorkOrderByMrpInput intput = new CreatePurchaseApplyWorkOrderByMrpInput();
        //        intput.MpsId = this.MpsId;

        //        var details = this.Model.MrpDetailsModels.Where(m => m.ProductId == productId);
        //        foreach (var item in details)
        //        {
        //            intput.MrpDetailIdList.Add(item.Id);
        //        }
        //        await _mpsAppService.CreatePurchaseApplyWorkOrderAsync(intput);
        //    }
        //    catch (Exception ex)
        //    {
        //        HandleException(ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        this.IsLoading = false;
        //    }
        //}
    }
}
