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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PowerUsageCalc;

namespace WpfPowerCalcApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //        private PowerStation stationModel = new PowerStation();
        private PowerStation stationModel = new PowerStation();

        internal PowerStation StationModel
        {
            get
            {
                return stationModel;
            }            
        }

        public MainWindow()
        {
            InitializeComponent();
            InitlizeListView();
            StationModelChanged();

        }
        void InitlizeListView()
        {
            // Add columns
            var gridView = new GridView();
            this.listView.View = gridView;
            
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Id",
                DisplayMemberBinding = new Binding("Id")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Name",
                DisplayMemberBinding = new Binding("Name"),
                
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Unom",
                DisplayMemberBinding = new Binding("Unom")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "P",
                DisplayMemberBinding = new Binding("Pnom")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "Q",
                DisplayMemberBinding = new Binding("Qnom")
            });
            gridView.Columns.Add(new GridViewColumn
            {
                Header = "I",
                DisplayMemberBinding = new Binding("InomDisp")
                {
                    
                },
                HeaderStringFormat = ""
            });
        }

        void StationModelChanged()
        {
            // отримання фільтрованого списку
            List<PowerItem> items = StationModel.GetItemsByVoltage(220);

            // заповнення візеального списку
            listView.Items.Clear();
            foreach (PowerItem item in items)
            {
                listView.Items.Add(item);
            }

            // оновлення суми
            double sum = StationModel.GetSum(items).Magnitude;
            labelSumTotal.Content = "S = " + sum.ToString("#.0");
        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you want to exit application?", "Power Calc APP", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void itemAddLoad_Click(object sender, RoutedEventArgs e)
        {
            NewItemAddDlg dlg = new NewItemAddDlg();
            dlg.Title = "Add Load";
            bool? dialogResult = dlg.ShowDialog();
            if (dialogResult.Value)
            {
                StationModel.AddItem(new PowerLoad() {
                    Name = dlg.LoadName,
                    Unom = dlg.LoadUnom,
                    Pnom = dlg.LoadSnom
                });
                StationModelChanged();
            }
        }

        private void itemAddCapacitor_Click(object sender, RoutedEventArgs e)
        {
            NewItemAddDlg dlg = new NewItemAddDlg();
            dlg.Title = "Add Capacitor";
            bool? dialogResult = dlg.ShowDialog();
            if (dialogResult.Value)
            {
                StationModel.AddItem(new PowerCapacitor() {
                    Name = dlg.LoadName,
                    Unom = dlg.LoadUnom,
                    Pnom = dlg.LoadSnom
                });
                StationModelChanged();
            }
        }

        private void menuFileNew_Click(object sender, RoutedEventArgs e)
        {
            stationModel = new PowerStation();
            StationModelChanged();
        }
    }
}
