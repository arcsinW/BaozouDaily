using BaoZouRiBao.Core.Model;
using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace BaoZouRiBao.BaoZouAction
{
    class DocumentAction : DependencyObject, IAction
    {
        #region Properties
        public string DocumentId 
        {
            get { return (string)GetValue(DocumentIdProperty); }
            set { SetValue(DocumentIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DocumentIdProperty =
            DependencyProperty.Register("MyProperty", typeof(string), typeof(string), new PropertyMetadata(0));


        #endregion

        public object Execute(object sender, object parameter)
        {
            
        }
    }
}
