using ClangPowerTools.MVVM.ViewModels;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
      if (string.IsNullOrWhiteSpace(offlineLoginViewModel.AuthenticationKey))
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
        CreateCptjwtFile();
        Close();
      }
      else
      {
        SetLoginButtonState(true, colorBackgroundEnabled, colorForegroundEnabled);
        InvalidUserTextBlock.Text = invalidAuthenticationKey;
        InvalidUserTextBlock.Visibility = Visibility.Visible;
      }
    }

    private void CreateCptjwtFile()
    {
      SettingsPathBuilder settingsPathBuilder = new SettingsPathBuilder();
      string filePath = settingsPathBuilder.GetPath("ctpjwt");
      SaveToken(Guid.NewGuid().ToString());
    }

    private bool VerifyAuthenticationKey()
    {
      if (KeyTextBox.Text.Where(Char.IsLetterOrDigit).Count() != 16)
      {
        return false;
      }

      bool condition1 = KeyTextBox.Text.Substring(0, 4).Select(c => (int)c).Sum() == 400;
      bool condition2 = !KeyTextBox.Text.Substring(5, 4).Any(c => c % 2 != 0);
      bool condition3 = !KeyTextBox.Text.Substring(10, 4).Any(c => c % 2 == 0);
      bool condition4 = KeyTextBox.Text.Substring(15, 4).Select(c => (int)c).Sum() == 400;
      return condition1 && condition2 && condition3 && condition4;
    }
    private void SaveToken(string token)
    {
      SettingsPathBuilder settingsPathBuilder = new SettingsPathBuilder();
      string filePath = settingsPathBuilder.GetPath("ctpjwt");
      DeleteExistingToken(filePath);

      using (StreamWriter streamWriter = new StreamWriter(filePath))
      {
        streamWriter.WriteLine(token);
      }
      File.SetAttributes(filePath, File.GetAttributes(filePath) | FileAttributes.Hidden);
    }

    private void DeleteExistingToken(string filePath)
    {
      if (File.Exists(filePath))
      {
        File.Delete(filePath);
      }
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
