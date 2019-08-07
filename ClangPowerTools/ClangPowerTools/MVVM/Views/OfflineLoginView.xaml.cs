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
    // Login button colors
    private readonly string colorBackgroundEnabled = "#FFBF31";
    private readonly string colorForegroundEnabled = "#000000";
    private readonly string colorBackgroundDisabled = "#BBB6C4";
    private readonly string colorForegroundDisabled = "#707079";

    // Validation messages
    private readonly string invalidAuthenticationKey = "The authentication key that you have enterd is not valid.";
    public OfflineLoginView()
    {
      InitializeComponent();
      DataContext = offlineLoginViewModel;
    }

   private void ActivateButton_Click(object sender, RoutedEventArgs e)
    {
      if (string.IsNullOrWhiteSpace(offlineLoginViewModel.AuthenticationKey) )
      {
        InvalidUserTextBlock.Text = invalidAuthenticationKey;
        InvalidUserTextBlock.Visibility = Visibility.Visible;
        return;
      }

      SetLoginButtonState(false, colorBackgroundDisabled, colorForegroundDisabled);

      InvalidUserTextBlock.Text = invalidAuthenticationKey;
      InvalidUserTextBlock.Visibility = Visibility.Hidden;
      bool isAuthenticationKeyValid = VerifyAuthenticationKey();
      if (isAuthenticationKeyValid)
      {
        Close();
      }
      else
      {
        SetLoginButtonState(true, colorBackgroundEnabled, colorForegroundEnabled);
        InvalidUserTextBlock.Text = invalidAuthenticationKey;
        InvalidUserTextBlock.Visibility = Visibility.Visible;
      }
    }

    private bool VerifyAuthenticationKey()
    {
      return false;
    }

    private void SetLoginButtonState(bool isEnabled, string background, string foreground)
    {
      Color colorBackground = (Color)ColorConverter.ConvertFromString(background);
      Color colorForeground = (Color)ColorConverter.ConvertFromString(foreground);

      ActivateButton.IsEnabled = isEnabled;
      ActivateButton.Background = new SolidColorBrush(colorBackground);
      ActivateButton.Foreground = new SolidColorBrush(colorForeground);
    }
  }
}
