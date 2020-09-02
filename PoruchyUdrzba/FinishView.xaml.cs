using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using PoruchyUdrzba.Model;
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

namespace PoruchyUdrzba
{
    /// <summary>
    /// Interaction logic for FinishView.xaml
    /// </summary>
    public partial class FinishView : MetroWindow
    {
        public int errorId;
        public FinishView(int id)
        {
            InitializeComponent();
            
            errorId = id;
        }

        private async void BtClose_Click(object sender, RoutedEventArgs e)
        {
            string comment = TbCommnet.Text;
            string name = TbName.Text;

            if (String.IsNullOrEmpty(comment))
            {
                await this.ShowMessageAsync("Chyba", "Prosím doplň popis chyby");
                return;

            }
            if (String.IsNullOrEmpty(name))
            {
                await this.ShowMessageAsync("Chyba", "Prosím doplň jméno");
                return;
            }
            else
            {
                Database db = new Database();
                SendEmail send = new SendEmail();
                db.updateData(errorId, comment, name);
                send.emailClose(errorId);
                Close();
            }
               
        }
    }
}
