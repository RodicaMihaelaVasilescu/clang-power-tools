﻿using EnvDTE;
using System;
using System.IO;
using System.Reflection;

namespace ClangPowerTools
{
  public class ScriptBuiler
  {
    #region Members

    private string mParameters = string.Empty;

    #endregion

    #region Public Methods

    public string GetScript(IItem aItem, string aFileName)
    {
      string containingDirectoryPath = string.Empty;
      string parentDirectoryPath = string.Empty;
      string script = $"{ScriptConstants.kScriptBeginning} ''{GetScriptPath()}''";

      if (aItem is SelectedProjectItem)
      {
        aFileName = aFileName.Substring(0, aFileName.IndexOf('.'));
        ProjectItem projectItem = aItem.GetObject() as ProjectItem;
        parentDirectoryPath = new DirectoryInfo(projectItem.ContainingProject.FullName).Parent.FullName;
        script = $"{script} -proj {projectItem.ContainingProject.Name} -file {aFileName}";
      }
      else if (aItem is SelectedProject)
      {
        Project project = aItem.GetObject() as Project;
        parentDirectoryPath = new DirectoryInfo(project.FullName).Parent.FullName;
        script = $"{script} -proj {aFileName}";
      }

      return $"{script} {mParameters} -dir ''{parentDirectoryPath}'''";
    }

    public void ConstructParameters(GeneralOptions aGeneralOptions, 
      TidyOptions aTidyPage, string aVsEdition, string aVsVersion)
    {
      mParameters = GetGeneralParameters(aGeneralOptions);

      if (aTidyPage != null)
        mParameters = $"{mParameters} {GetTidyParameters(aTidyPage)}";

      mParameters = $"{mParameters} -vs-ver {aVsVersion} -vs-sku {aVsEdition}";
    }

    #endregion

    #region Get Parameters Helpers

    private string GetScriptPath()
    {
      string assemblyPath = Assembly.GetExecutingAssembly().Location;
      assemblyPath = assemblyPath.Substring(0, assemblyPath.LastIndexOf('\\'));
      return $"{assemblyPath}\\{ScriptConstants.kScriptName}";
    }

    private string GetGeneralParameters(GeneralOptions aGeneralOptions)
    {
      string parameters = string.Empty;

      if (null != aGeneralOptions.ClangFlags && 0 < aGeneralOptions.ClangFlags.Length)
        parameters = $"{parameters} -clang-flags (''{String.Join("'',''", aGeneralOptions.ClangFlags)}'')";
      
      if (aGeneralOptions.Continue)
        parameters = $"{parameters} -continue";
      
      if (null != aGeneralOptions.IncludeDirectories && 0 < aGeneralOptions.IncludeDirectories.Length)
        parameters = $"{parameters} -includeDirs {String.Join(",", aGeneralOptions.IncludeDirectories)}";
      
      if (null != aGeneralOptions.ProjectsToIgnore && 0 < aGeneralOptions.ProjectsToIgnore.Length)
        parameters = $"{parameters} -proj-ignore {String.Join(",", aGeneralOptions.ProjectsToIgnore)}";

      return $"{parameters} {ScriptConstants.kParallel}".Trim(new char[] { ' ', ',' });
    }

    private string GetTidyParameters(TidyOptions aTidyPage)
    {
      string parameters = string.Empty;

      if (aTidyPage.Fix)
        parameters = $"{parameters} -tidy-fix";
      else
        parameters = $"{parameters} -tidy";

      if (null != aTidyPage.TidyFlags && 0 < aTidyPage.TidyFlags.Length)
        parameters = $"{parameters} ''-*,{String.Join(",", aTidyPage.TidyFlags)}''";

      return parameters.Trim(new char[] { ' ', ',' });
    }

    #endregion

  }
}
