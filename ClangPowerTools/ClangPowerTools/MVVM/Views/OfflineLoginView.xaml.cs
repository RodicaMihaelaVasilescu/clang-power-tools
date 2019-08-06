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
    public OfflineLoginView()
    {
      InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
    }
    private void SignUpButton_Click(object sender, RoutedEventArgs e)
    {
      Process.Start(new ProcessStartInfo(WebApiUrl.signUpUrl));
    }

  }
}
