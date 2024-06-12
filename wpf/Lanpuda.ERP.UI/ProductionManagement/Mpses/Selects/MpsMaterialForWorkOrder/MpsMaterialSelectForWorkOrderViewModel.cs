//using DevExpress.Mvvm;
//using DevExpress.Mvvm.DataAnnotations;
//using HandyControl.Collections;
//using Lanpuda.Client.Mvvm;
//using Lanpuda.ERP.ProductionManagement.Mpses.Dtos;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Lanpuda.ERP.ProductionManagement.Mpses.Selects.MpsMaterialForWorkOrder
//{
//    public class MpsMaterialSelectForWorkOrderViewModel : RootViewModelBase
//    {
//        protected ICurrentWindowService CurrentWindowService { get { return GetService<ICurrentWindowService>(); } }

//        private IMpsLookupAppService _mpsLookupAppService { get; set; }
//        public List<MpsMaterialDto> RawDatas { get; set; }
//        public Guid MpsId { get; set; }
//        public Action<MpsMaterialDto>? OnSelectedCallback { get; set; }
//        public MpsMaterialDto SelectedRow
//        {
//            get { return GetProperty(() => SelectedRow); }
//            set { SetProperty(() => SelectedRow, value); }
//        }
//        public ManualObservableCollection<MpsMaterialDto> Items
//        {
//            get { return GetProperty(() => Items); }
//            set { SetProperty(() => Items, value); }
//        }

//        public string SearchText
//        {
//            get { return GetProperty(() => SearchText); }
//            set { SetProperty(() => SearchText, value, OnSearchTextChanged); }
//        }

//        public MpsMaterialSelectForWorkOrderViewModel(IMpsLookupAppService mpsLookupAppService)
//        {
//            _mpsLookupAppService = mpsLookupAppService;
//            RawDatas = new List<MpsMaterialDto>();
//            Items = new ManualObservableCollection<MpsMaterialDto>();
//        }

//        [AsyncCommand]
//        public async Task InitializeAsync()
//        {
//            try
//            {
//                this.IsLoading = true;
//                //RawDatas = await _mpsLookupAppService.GetAllMaterialForWorkOrderAsync(MpsId);
//                await Task.Delay(1000);
//                Items.CanNotify = false;
//                Items.Clear();
//                foreach (var item in RawDatas)
//                {
//                    Items.Add(item);
//                }
//                Items.CanNotify = true;
//            }
//            catch (Exception e)
//            {
//                HandleException(e);
//                throw;
//            }
//            finally
//            {
//                this.IsLoading = false;
//            }
//        }

//        [Command]
//        public void Selected()
//        {
//            if (OnSelectedCallback == null)
//            {
//                return;
//            }
//            OnSelectedCallback(SelectedRow);
//            if (CurrentWindowService != null)
//                CurrentWindowService.Close();
//        }


//        private void OnSearchTextChanged()
//        {
//            if (string.IsNullOrEmpty(this.SearchText))
//            {
//                Items.CanNotify = false;
//                Items.Clear();
//                foreach (var item in RawDatas)
//                {
//                    Items.Add(item);
//                }
//                Items.CanNotify = true;
//            }
//            else
//            {
//                Items.CanNotify = false;
//                Items.Clear();
//                var searchResult = RawDatas.Where(m => m.ProductName.Contains(this.SearchText)).ToList();
//                foreach (var item in searchResult)
//                {
//                    Items.Add(item);
//                }
//                Items.CanNotify = true;
//            }
//        }

//    }
//}
