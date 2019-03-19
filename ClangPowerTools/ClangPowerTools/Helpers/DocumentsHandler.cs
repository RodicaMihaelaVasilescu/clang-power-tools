﻿using ClangPowerTools.Services;
using EnvDTE;
using Microsoft.VisualStudio.ComponentModelHost;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.TextManager.Interop;
using System.Collections.Generic;
using System.IO;

namespace ClangPowerTools
{
  public class DocumentsHandler
  {
    #region Public Methods

    /// <summary>
    /// Get active documents
    /// </summary>
    /// <returns>Active documents</returns>
    public static Documents GetActiveDocuments()
    {
      return VsServiceProvider.TryGetService(typeof(DTE), out object dte) ? (dte as DTE).Documents : null;
    }

    /// <summary>
    /// Get the active document
    /// </summary>
    /// <returns>Active document</returns>
    public static Document GetActiveDocument()
    {
      return VsServiceProvider.TryGetService(typeof(DTE), out object dte) ? (dte as DTE).ActiveDocument : null;
    }

    /// <summary>
    /// Get the elected items
    /// </summary>
    /// <returns></returns>
    public static SelectedItems GetSelectedItems()
    {
      DTE vsServiceProvider = VsServiceProvider.TryGetService(typeof(DTE), out object dte) ? (dte as DTE) : null;

      return vsServiceProvider.SelectedItems;
    }

    /// <summary>
    /// Save all the active documents
    /// </summary>
    public static void SaveActiveDocuments()
    {
      var activeDocuments = GetActiveDocuments();
      if (null != activeDocuments && 0 < activeDocuments.Count)
        activeDocuments.SaveAll();
    }

    public static ITextBuffer GetDocumentTextBuffer()
    {
      var document = GetActiveDocument();
      var openWindowPath = Path.Combine(document.Path, document.Name);
      return GetBufferAt(openWindowPath);
    }

    #endregion


    #region Private Methods


    private static ITextBuffer GetBufferAt(string filePath)
    {
      var componentModel = (IComponentModel)VsServiceProvider.GetService(typeof(SComponentModel));
      var editorAdapterFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

      IVsTextView view = Vsix.GetVsTextViewFrompPath(filePath);
      IVsTextLines lines;
      if (view.GetBuffer(out lines) == 0)
      {
        var buffer = lines as IVsTextBuffer;
        if (buffer != null)
          return editorAdapterFactoryService.GetDataBuffer(buffer);
      }
      return null;


      //var componentModel = (IComponentModel) VsServiceProvider.GetService(typeof(SComponentModel));
      //var editorAdapterFactoryService = componentModel.GetService<IVsEditorAdaptersFactoryService>();

      //var dte2 = (EnvDTE80.DTE2)Package.GetGlobalService(typeof(SDTE));
      //var sp = (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)dte2;
      //var serviceProvider = new ServiceProvider(sp);

      //IVsUIHierarchy uiHierarchy;
      //uint itemID;
      //IVsWindowFrame windowFrame;

      //if (VsShellUtilities.IsDocumentOpen(
      //  serviceProvider,
      //  filePath,
      //  Guid.Empty,
      //  out uiHierarchy,
      //  out itemID,
      //  out windowFrame))
      //{
      //  IVsTextView view = Vsix.GetVsTextViewFrompPath(filePath) VsShellUtilities.GetTextView(windowFrame);
      //  IVsTextLines lines;
      //  if (view.GetBuffer(out lines) == 0)
      //  {
      //    var buffer = lines as IVsTextBuffer;
      //    if (buffer != null)
      //      return editorAdapterFactoryService.GetDataBuffer(buffer);
      //  }
      //}

      //return null;
    }

    #endregion


  }
}
