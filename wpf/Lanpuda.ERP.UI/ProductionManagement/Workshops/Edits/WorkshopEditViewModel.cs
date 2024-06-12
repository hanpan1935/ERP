using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using HandyControl.Controls;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.ProductionManagement.Workshops.Dtos;
using NUglify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.ProductionManagement.Workshops.Edits
{
    public class WorkshopEditViewModel : EditViewModelBase<WorkshopEditModel>
    {
        private readonly IWorkshopAppService _workshopAppService;
        public Func<Task>? RefreshAsync { get; set; }

        public WorkshopEditViewModel(IWorkshopAppService workshopAppService)
        {
            _workshopAppService = workshopAppService;
        }



        [AsyncCommand]
        public async Task InitializeAsync()
        {
            if (Model.Id != null)
            {
                try
                {
                    if (Model.Id == null) return;
                    this.IsLoading = true;
                    Guid id = (Guid)Model.Id;
                    var result = await _workshopAppService.GetAsync(id);
                    this.Model.Name = result.Name;
                    this.Model.Number = result.Number;
                    this.Model.Remark = result.Remark;
                }
                catch (Exception e)
                {
                    HandleException(e);
                    throw;
                }
                finally
                {
                    this.IsLoading = false;
                }
            }

        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            if (Model.Id == null)
            {
                await CreateAsync();
            }
            else
            {
                await UpdateAsync();
            }
        }


        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private async Task CreateAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkshopCreateDto dto = new WorkshopCreateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                await _workshopAppService.CreateAsync(dto);
                if (RefreshAsync != null)
                {
                    await RefreshAsync();
                    this.CurrentWindowService.Close();
                }
            }
            catch (Exception e)
            {
                HandleException(e);
            }
            finally
            {
                this.IsLoading = false;
            }
        }


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                WorkshopUpdateDto dto = new WorkshopUpdateDto();
                dto.Name = Model.Name;
                dto.Number = Model.Number;
                dto.Remark = Model.Remark;
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _workshopAppService.UpdateAsync((Guid)Model.Id, dto);
                if (this.RefreshAsync != null)
                {
                    await RefreshAsync();
                    this.CurrentWindowService.Close();
                }
            }
            catch (Exception e)
            {
                HandleException(e);
                throw;
            }
            finally
            {
                this.IsLoading = false;
            }
        }
    }
}
