using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClangPowerTools.MVVM.ViewModels
{
  class OfflineLoginViewModel: INotifyPropertyChanged
  {
    private string authenticationKey;

    public string AuthenticationKey
    {
      get { return authenticationKey; }
      set
      {
        if(authenticationKey == value) { return; }

        authenticationKey = Regex.Replace(value, "[A-Za-z ]", "");
        authenticationKey = authenticationKey.Length <= 16 ? authenticationKey : authenticationKey.Substring(0, 16);
        authenticationKey= Regex.Replace(authenticationKey, ".{4}", "$0 ");

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AuthenticationKey"));
      }
    }

    public event PropertyChangedEventHandler PropertyChanged;
  }
}
