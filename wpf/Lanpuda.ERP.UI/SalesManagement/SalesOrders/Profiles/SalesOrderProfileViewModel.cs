using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.Mpses.Edits;
using Lanpuda.ERP.SalesManagement.SalesOrders.CreateMpses;
using Lanpuda.ERP.SalesManagement.SalesOrders.Dtos.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.SalesManagement.SalesOrders.Profiles
{
    public class SalesOrderProfileViewModel : RootViewModelBase
    {
        private readonly ISalesOrderAppService _salesOrderAppService;
        private readonly IServiceProvider _serviceProvider;

        public Guid Id { get; set; }


        public SalesOrderProfileDto Model
        {
            get { return GetValue<SalesOrderProfileDto>(); }
            set { SetValue(value); }
        }

        public SalesOrderProfileDetailDto? SelectedRow
        {
            get { return GetValue<SalesOrderProfileDetailDto?>(); }
            set { SetValue(value); }
        }

        public SalesOrderProfileViewModel(ISalesOrderAppService salesOrderAppService, IServiceProvider serviceProvider)
        {
            _salesOrderAppService = salesOrderAppService;
            Model = new SalesOrderProfileDto();
            _serviceProvider = serviceProvider;
        }


        [AsyncCommand]
        public async Task InitializeAsync()
        {
            await QueryAsync();
        }



        [Command]
        public void Create()
        {
            if (SelectedRow == null)
            {
                return;
            }

            if (this.WindowService != null)
            {
                MpsEditViewModel? mpsEditViewModel = _serviceProvider.GetService<MpsEditViewModel>();
                if (mpsEditViewModel != null)
                {
                    mpsEditViewModel.Model.MpsType = ProductionManagement.Mpses.MpsType.Customer;
                    mpsEditViewModel.Model.ProductId = this.SelectedRow.ProductId;
                    mpsEditViewModel.Model.ProductName = this.SelectedRow.ProductName;
                    mpsEditViewModel.Model.Quantity = this.SelectedRow.Quantity;
                    mpsEditViewModel.Model.StartDate = DateTime.Now;
                    mpsEditViewModel.Model.CompleteDate = SelectedRow.DeliveryDate;
                    mpsEditViewModel.IsCanSelectProduct = false;
                    mpsEditViewModel.OnCloseWindowCallbackAsync = this.QueryAsync;
                    WindowService.Title = "生产计划-新建";
                    WindowService.Show("MpsEditView", mpsEditViewModel);
                }
            }
        }

        private async Task QueryAsync()
        {
            try
            {
                this.IsLoading = true;
                // Model = await this._salesOrderAppService.GetProfileAsync(Id);
                await Task.Delay(100);
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
    }
}
