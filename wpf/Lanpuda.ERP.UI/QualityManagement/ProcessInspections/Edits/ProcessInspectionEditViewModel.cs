using DevExpress.Mvvm.DataAnnotations;
using Lanpuda.Client.Mvvm;
using Lanpuda.ERP.QualityManagement.ProcessInspections.Dtos;
using Lanpuda.ERP.QualityManagement.ProcessInspections.Edits;
using Lanpuda.ERP.QualityManagement.ProcessInspections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lanpuda.ERP.QualityManagement.ProcessInspections.Edits
{
    public class ProcessInspectionEditViewModel : EditViewModelBase<ProcessInspectionEditModel>
    {
        private readonly IProcessInspectionAppService _processInspectionAppService;
        public Func<Task>? OnCloseCallbackAsync { get; set; }


        public ProcessInspectionEditViewModel(IProcessInspectionAppService processInspectionAppService)
        {
            _processInspectionAppService = processInspectionAppService;
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
                    var result = await _processInspectionAppService.GetAsync(id);
                    this.Model.Number = result.Number;
                    this.Model.BadQuantity = result.BadQuantity;
                    this.Model.Description = result.Description;
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
            else
            {
                this.Model.Number = "系统自动生成";
            }

        }


        [AsyncCommand]
        public async Task SaveAsync()
        {
            await UpdateAsync();
            if (this.OnCloseCallbackAsync != null)
            {
                await OnCloseCallbackAsync();
            }
            this.Close();
        }

        public bool CanSaveAsync()
        {
            bool hasError = Model.HasErrors();
            return !hasError;
        }


        private async Task UpdateAsync()
        {
            try
            {
                this.IsLoading = true;
                ProcessInspectionUpdateDto dto = new ProcessInspectionUpdateDto();
                dto.BadQuantity = Model.BadQuantity;
                dto.Description = Model.Description;
                if (Model.Id == null)
                {
                    throw new ArgumentNullException("", "Id不能为空");
                }
                await _processInspectionAppService.UpdateAsync((Guid)Model.Id, dto);
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
    }
}
