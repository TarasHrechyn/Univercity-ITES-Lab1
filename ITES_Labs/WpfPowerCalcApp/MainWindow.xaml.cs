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
using ConsolePowerUsageCalcVisual;

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
            StationModelChanged();
        }

        void StationModelChanged()
        {
            List<PowerItem> items = StationModel.ItemsByVoltage(0);
            textFilteredItems.Document.Blocks.Clear();
            
            foreach (PowerItem item in items)
            {
                textFilteredItems.AppendText("\n" + item.ToString());
            }
            double sum = StationModel.GetSum().Magnitude;
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
            Nullable<bool> dialogResult = dlg.ShowDialog();
            if (dialogResult.Value)
            {
                StationModel.Items.Add(new PowerLoad() { Vnom = dlg.Unom, Pnom = dlg.Snom });
                StationModelChanged();
            }
        }

        private void itemAddCapacitor_Click(object sender, RoutedEventArgs e)
        {
            NewItemAddDlg dlg = new NewItemAddDlg();
            dlg.Title = "Add Capacitor";
            Nullable<bool> dialogResult = dlg.ShowDialog();
            if (dialogResult.Value)
            {
                StationModel.Items.Add(new PowerCapacitor() { Vnom = dlg.Unom, Qnom = dlg.Snom });
                StationModelChanged();
            }
        }

        private void textFilteredItems_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void menuFileNew_Click(object sender, RoutedEventArgs e)
        {
            stationModel = new PowerStation();
            StationModelChanged();
        }

        private void menuFileOpen_Click(object sender, RoutedEventArgs e)
        {
            stationModel = PowerStation.LoadFromXML("Data.xml");
            StationModelChanged();
        }

        private void menuFileSave_Click(object sender, RoutedEventArgs e)
        {
            stationModel.SaveToXML("Data.xml");
        }
    }
}
