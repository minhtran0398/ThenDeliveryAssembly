﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ThenDelivery.Client.ExtensionMethods;
using ThenDelivery.Shared.Dtos;

namespace ThenDelivery.Client.Components.Merchant
{
   public class MerchantRegisterFormBase : CustomComponentBase<MerchantRegisterFormBase>
   {
      #region Inject

      #endregion

      #region Parameters
      [Parameter] public MerchantDto MerchantModel { get; set; }
      [Parameter] public EventCallback<int> OnSubmitMerchant { get; set; }
      #endregion

      #region Properties
      protected EditContext EditContext { get; set; }
      public List<CityDto> CityList { get; set; }
      public List<DistrictDto> DistrictList { get; set; }
      public List<DistrictDto> CurrentDistrictList { get; set; }
      public List<WardDto> WardList { get; set; }
      public List<WardDto> CurrentWardList { get; set; }
      #endregion

      #region Life Cycle
      protected override void OnInitialized()
      {
         base.OnInitialized();
         EditContext = new EditContext(MerchantModel);
         StateHasChanged();
      }

      protected override async Task OnParametersSetAsync()
      {
         await base.OnParametersSetAsync();

         CityList = await HttpClientServer.CustomGetAsync<List<CityDto>>("api/city");
         DistrictList = await HttpClientServer.CustomGetAsync<List<DistrictDto>>("api/district");
         WardList = await HttpClientServer.CustomGetAsync<List<WardDto>>("api/ward");

         if(CityList != null && DistrictList != null && WardList != null)
         {
            MerchantModel.City = CityList[0];
            CurrentDistrictList = DistrictList.FindAll(e => e.CityCode == MerchantModel.City.CityCode);
            MerchantModel.District = CurrentDistrictList[0];
            CurrentWardList = WardList.FindAll(e => e.DistrictCode == MerchantModel.District.DistrictCode);
            MerchantModel.Ward = CurrentWardList[0];
         }
         await InvokeAsync(StateHasChanged);
      }
      #endregion

      #region Events
      protected async Task HandleOnSubmit()
      {
         if (EditContext.Validate())
         {
            if (MerchantModel.Id == 0)
            {
               var returnedIdString = await HttpClientServer.CustomPostAsync($"api/merchant", MerchantModel);
               if (int.TryParse(returnedIdString, out int returnId) && returnId != -1)
               {
                  await OnSubmitMerchant.InvokeAsync(returnId);
               }
            }
            else if (EditContext.IsModified())
            {
               var response = await HttpClientServer.CustomPutAsync($"api/merchant", MerchantModel);
               await OnSubmitMerchant.InvokeAsync(MerchantModel.Id);
            }
         }
      }

      protected void HandleMerchantNameChanged(string newValue)
      {
         MerchantModel.Name = newValue;
      }

      protected void HandleTaxCodeChanged(string newValue)
      {
         MerchantModel.TaxCode = newValue;
      }

      protected void HandlePhoneNumberChanged(string newValue)
      {
         MerchantModel.PhoneNumber = newValue;
      }

      protected void HandleSearchKeyChanged(string newValue)
      {
         MerchantModel.SearchKey = newValue;
      }

      protected void HandleDescriptionChanged(string newValue)
      {
         MerchantModel.Description = newValue;
      }

      protected async Task HandleOpenTimeChanged(string newTimeString)
      {
         if (string.IsNullOrWhiteSpace(newTimeString) == false)
         {

            MerchantModel.OpenTime.TimeString = newTimeString;
         }
         await InvokeAsync(StateHasChanged);
      }

      protected async Task HandleCloseTimeChanged(string newTimeString)
      {
         if (string.IsNullOrWhiteSpace(newTimeString) == false)
         {
            MerchantModel.CloseTime.TimeString = newTimeString;
         }
         await InvokeAsync(StateHasChanged);
      }

      protected void HandleSelectedCityChanged(CityDto newValue)
      {
         MerchantModel.City = newValue;
         CurrentDistrictList = DistrictList.FindAll(e => e.CityCode == MerchantModel.City.CityCode);
         MerchantModel.District = CurrentDistrictList[0];
         CurrentWardList = WardList.FindAll(e => e.DistrictCode == MerchantModel.District.DistrictCode);
         MerchantModel.Ward = CurrentWardList[0];
         StateHasChanged();
      }

      protected void HandleSelectedDistrictChanged(DistrictDto newValue)
      {
         MerchantModel.District = newValue;
         CurrentWardList = WardList.FindAll(e => e.DistrictCode == MerchantModel.District.DistrictCode);
         MerchantModel.Ward = CurrentWardList[0];
         StateHasChanged();
      }

      protected void HandleSelectedWardChanged(WardDto newValue)
      {
         MerchantModel.Ward = newValue;
      }

      protected void HandleSelectedMerTypeChanged(IEnumerable<MerTypeDto> newValue)
      {
         MerchantModel.MerTypeList.RemoveAll(e => true);
         MerchantModel.MerTypeList.AddRange(newValue);
         StateHasChanged();
      }

      protected void HandleSelectedFeaturedDishChanged(IEnumerable<FeaturedDishDto> newValue)
      {
         MerchantModel.FeaturedDishList.RemoveAll(e => true);
         MerchantModel.FeaturedDishList.AddRange(newValue);
         StateHasChanged();
      }

      protected void HandleHouseNumberChanged(string newValue)
      {
         MerchantModel.HouseNumber = newValue;
      }

      protected void HandleAvatarChanged(string newValue)
      {
         MerchantModel.Avatar = newValue;
      }

      protected void HandleCoverPictureChanged(string newValue)
      {
         MerchantModel.CoverPicture = newValue;
      }

      #endregion

      #region Methods
      protected async Task<IEnumerable<MerTypeDto>> HandleLoadMerchantTypeAsync(CancellationToken _ = default)
      {
         var result = await HttpClientServer.CustomGetAsync<IEnumerable<MerTypeDto>>("api/mertype");
         if (MerchantModel.MerTypeList.Count > 0)
         {
            for (int index = 0; index < MerchantModel.MerTypeList.Count; index++)
            {
               MerchantModel.MerTypeList[index] =
                  result.SingleOrDefault(e => e.Id == MerchantModel.MerTypeList[index].Id);
            }
         }
         return result;
      }

      protected async Task<IEnumerable<FeaturedDishDto>> HandleLoadFeaturedDishCategoryAsync(CancellationToken _ = default)
      {
         var result = await HttpClientServer.CustomGetAsync<IEnumerable<FeaturedDishDto>>("api/featureddish");
         if (MerchantModel.FeaturedDishList.Count > 0)
         {
            for (int index = 0; index < MerchantModel.FeaturedDishList.Count; index++)
            {
               MerchantModel.FeaturedDishList[index] =
                  result.SingleOrDefault(e => e.Id == MerchantModel.FeaturedDishList[index].Id);
            }
         }
         return result;
      }

      //return await HttpClientServer.CustomGetAsync<IEnumerable<CityDto>>("api/city");
      //return await HttpClientServer.CustomGetAsync<IEnumerable<DistrictDto>>("api/district");
      //return await HttpClientServer.CustomGetAsync<IEnumerable<WardDto>>("api/ward");
      #endregion
   }
}
