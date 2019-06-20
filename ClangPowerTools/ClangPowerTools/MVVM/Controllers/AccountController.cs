﻿using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ClangPowerTools.MVVM.WebApi;

namespace ClangPowerTools
{
  public class AccountController
  {
    #region Public Methods

    public async Task<bool> LoginAsync(UserModel userModel)
    {
      StringContent content = new StringContent(SeralizeUserModel(userModel), Encoding.UTF8, "application/json");

      try
      {
        using (HttpResponseMessage result = await ApiUtility.ApiClient.PostAsync(WebApiUrl.loginUrl, content))
        {
          content.Dispose();
          if (result.IsSuccessStatusCode)
          {
            TokenModel tokenModel = await result.Content.ReadAsAsync<TokenModel>();
            ApiUtility.ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenModel.jwt);
            SaveToken(tokenModel.jwt);
            return true;
          }
          else
          {
            return false;
          }
        }
      }
      catch (Exception)
      {
        return false;
      }
    }

    private string SeralizeUserModel(UserModel userModel)
    {
      string jsonObject = JsonConvert.SerializeObject(userModel);
      userModel.Dispose();
      return jsonObject;
    }


    #endregion

    #region Private Methods

    private void SaveToken(string token)
    {
      SettingsPathBuilder settingsPathBuilder = new SettingsPathBuilder();
      string filePath = settingsPathBuilder.GetPath("ctpjwt");

      using (StreamWriter streamWriter = new StreamWriter(filePath))
      {
        streamWriter.WriteLine(token);
      }
      File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
    }

    #endregion

  }
}
