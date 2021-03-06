﻿using Xunit;
using Task = System.Threading.Tasks.Task;

namespace ClangPowerTools.Tests.Settings
{
  [VsTestSettings(UIThread = true)]
  public class GeneralSettingsTests
  {
    [VsFact(Version = "2017-")]
    public async Task ClangGeneralOptionsView_NotNullAsync()
    {
      //Arrange
      await UnitTestUtility.LoadPackageAsync();

      //Act
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      //Assert
      Assert.NotNull(generalSettings);
    }

    [VsFact(Version = "2017-")]
    public async Task CompileFlags_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.ClangFlags = "-Wall";
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();
    
      Assert.Equal(generalSettings.ClangFlags, generalSettingsFromFile.ClangFlags);
    }

    [VsFact(Version = "2017-")]
    public async Task FilesToIgnore_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.FilesToIgnore = "Test.cpp";
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();

      Assert.Equal(generalSettings.FilesToIgnore, generalSettingsFromFile.FilesToIgnore);
    }

    [VsFact(Version = "2017-")]
    public async Task ProjectToIgnore_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.ProjectsToIgnore = "TestProject";
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();

      Assert.Equal(generalSettings.ProjectsToIgnore, generalSettingsFromFile.ProjectsToIgnore);
    }

    [VsFact(Version = "2017-")]
    public async Task AdditionalIncludes_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.AdditionalIncludes = ClangGeneralAdditionalIncludes.SystemIncludeDirectories;
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();

      Assert.Equal(generalSettings.AdditionalIncludes.Value, generalSettingsFromFile.AdditionalIncludes.Value);
    }

    [VsFact(Version = "2017-")]
    public async Task TreatWarningsAsErrors_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.TreatWarningsAsErrors = true;
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();

      Assert.Equal(generalSettings.TreatWarningsAsErrors, generalSettingsFromFile.TreatWarningsAsErrors);
    }

    [VsFact(Version = "2017-")]
    public async Task ContinueOnError_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.Continue = true;
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();

      Assert.Equal(generalSettings.Continue, generalSettingsFromFile.Continue);
    }


    [VsFact(Version = "2017-")]
    public async Task ClangCompileAfterVsCompile_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.ClangCompileAfterVsCompile = true;
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();

      Assert.Equal(generalSettings.ClangCompileAfterVsCompile, generalSettingsFromFile.ClangCompileAfterVsCompile);
    }

    [VsFact(Version = "2017-")]
    public async Task VerboseMode_ChangeValue_CompareViewToFileAsync()
    {
      await UnitTestUtility.LoadPackageAsync();
      SettingsTestUtility.ResetClangGeneralOptionsView();
      ClangGeneralOptionsView generalSettings = SettingsProvider.GeneralSettings;

      generalSettings.VerboseMode = true;
      SettingsTestUtility.SaveGeneralOptions(generalSettings);
      ClangGeneralOptionsView generalSettingsFromFile = SettingsTestUtility.GetClangGeneralOptionsViewFromFile();

      Assert.Equal(generalSettings.VerboseMode, generalSettingsFromFile.VerboseMode);
    }
  }
}
