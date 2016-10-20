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

namespace WpfPowerCalcApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class NewItemAddDlg : Window
    {
        public string LoadName
        {
            get { return editorName.Text; }
        }
        public int LoadUnom
        {
            get { return Convert.ToInt32(editorUnom.Text); }
        }

        public double LoadSnom
        {
            get
            {
                try
                {
                    return Convert.ToDouble(editorSnom.Text);
                }
                catch
                {
                    return 0.0;
                }

            }
        }

        public NewItemAddDlg()
        {
            InitializeComponent();
            editorSnom.Text = Convert.ToString(1.0);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        
        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
