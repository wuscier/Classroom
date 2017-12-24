using Classroom.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Classroom.Views
{
    /// <summary>
    /// LoginView.xaml 的交互逻辑
    /// </summary>
    public partial class LoginView : Window
    {
        private string _realPassword = string.Empty;
        private string _fakePassword = string.Empty;

        private string _lastEncodedPassword = string.Empty;

        


        public LoginView()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        private void password_TextChanged(object sender, TextChangedEventArgs e)
        {
            Console.WriteLine($"password：{password.Text}");
            Console.WriteLine($"length：{password.Text.Length}");

            if (password.Text != _lastEncodedPassword)
            {
                string realPasswordIncrement = password.Text.Substring(_lastEncodedPassword.Length);

                _realPassword += realPasswordIncrement;
                Console.WriteLine($"real password：{_realPassword}");

                int length = password.Text.Length;
                string encodedPassword = string.Empty;
                for (int i = 0; i < length; i++)
                {
                    encodedPassword += "·";
                }

                _lastEncodedPassword = encodedPassword;
                password.Text = encodedPassword;
                password.SelectionStart = password.Text.Length;
            }
        }

        //private void password_PreviewKeyDown(object sender, KeyEventArgs e)
        //{
        //    Console.WriteLine(e.Key);
        //}

        //private void password_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    Console.WriteLine($"input text：{e.Text}");

        //    string realInputString = e.Text;
        //    _realPassword += realInputString;

        //    string fakeInputString = string.Empty;
        //    for (int i = 0; i < realInputString.Length; i++)
        //    {
        //        fakeInputString += "*";
        //    }

        //    _fakePassword += fakeInputString;

        //    password.Text = _fakePassword;

        //    password.SelectionStart = password.Text.Length;
        //}
    }
}
