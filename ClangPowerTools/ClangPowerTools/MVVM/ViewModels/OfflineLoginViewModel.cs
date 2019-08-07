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

        var formatedInput = new String(value.Where(Char.IsLetterOrDigit).ToArray());
        formatedInput = formatedInput.Length <= 16 ? formatedInput : formatedInput.Substring(0, 16);
        authenticationKey = Regex.Replace(formatedInput, ".{4}", "$0 ");

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AuthenticationKey"));
      }
    }


    public event PropertyChangedEventHandler PropertyChanged;
  }
}
