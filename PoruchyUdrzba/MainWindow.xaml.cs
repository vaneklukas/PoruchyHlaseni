using MahApps.Metro.Controls;
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
using System.Threading;
using System.Windows.Threading;
using PoruchyUdrzba.Model;
using MahApps.Metro.Controls.Dialogs;

namespace PoruchyUdrzba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        Database db = new Database();
        public MainWindow()
        {
            InitializeComponent();
            dateTimeLabel.Content = DateTime.Now.ToString();
            getDbData();
        }

        private void dateTimeLabel_Loaded(object sender, RoutedEventArgs e)
        {
            DispatcherTimer dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMinutes(1);
            dt.Tick += Dt_Tick;
            dt.Start();
        }
        
        private void Dt_Tick(object sender, EventArgs e)
        {
            dateTimeLabel.Content = DateTime.Now.ToString();
            getDbData();
        }
        private void getDbData()
        {
            DataGridData lw = new DataGridData();
            List<DataGridData> data = lw.getData();
            OpenTopics.ItemsSource = data;
            SumLabel.Content = data.Count.ToString();
        }
        private async void BtClose_Click(object sender, RoutedEventArgs e)
        {
            DataGridData lw = (DataGridData)OpenTopics.SelectedItem;
            if (lw == null)
            {
                await this.ShowMessageAsync("Chyba", "Prosím vyber chybu");
                return;
            }
            int id = lw.poruchy_Id;
            FinishView fw = new FinishView(id);
            fw.ShowDialog();
            getDbData();

           
        }

        private async void BtTake_Click(object sender, RoutedEventArgs e)
        {
            DataGridData lw = (DataGridData)OpenTopics.SelectedItem;
            if (lw==null)
            {
                await this.ShowMessageAsync("Chyba", "Prosím vyber chybu");
                return;
            }
            int id = lw.poruchy_Id;
            string nameMt = TbName.Text;
            if (String.IsNullOrEmpty(nameMt))
            {
                await this.ShowMessageAsync("Chyba", "Prosím doplň jméno");
                return;
            }
            db.updateTakeOver(id, nameMt);
            TbName.Clear();
            getDbData();
        }

        private void getDbDataHistory()
        {
            DataGridDataOld lw = new DataGridDataOld();
            List<DataGridDataOld> data = lw.getData();
            ClosedTopics.ItemsSource = data;
        }

        private void BtLoadOld_Click(object sender, RoutedEventArgs e)
        {
            getDbDataHistory();
        }
    }
}
