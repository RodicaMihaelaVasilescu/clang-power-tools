using ClangPowerTools.MVVM.ViewModels;
using ClangPowerTools.MVVM.WebApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClangPowerTools.MVVM.Views
{
  /// <summary>
  /// Interaction logic for OfflineLoginView.xaml
  /// </summary>
  public partial class OfflineLoginView : Window
  {
    private OfflineLoginViewModel offlineLoginViewModel = new OfflineLoginViewModel();
    public OfflineLoginView()
    {
      InitializeComponent();
      DataContext = offlineLoginViewModel;
    }

    //private void TextBox_KeyDown(object sender, KeyEventArgs e)
    //{
    //  if (e.Key >= Key.D0 && e.Key <= Key.D9)
    //  {
    //    e.Handled = false;
    //  }
    //  else
    //  {
    //    e.Handled = true;
    //  }
    //}
  }
}
