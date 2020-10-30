using System;
using MahApps.Metro.Controls;
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
using PoruchyHlaseni.Model;
using MahApps.Metro.Controls.Dialogs;

namespace PoruchyHlaseni
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        
        public MainWindow()
        {
            InitializeComponent();
            getStredisko();
            TbName.Focus();
        }
        private void getStredisko()
        {
            //first split button
            Stredisko stredisko = new Stredisko();
            List<Stredisko> list = stredisko.GetStrediska();
            foreach (var item in list)
            {
                SpBtStredisko.Items.Add(item.Name);
            }
        }
        private void getMachines(int i)
        {
            Machine machine = new Machine();
            List<Machine> list = machine.GetMachines();
            foreach (var item in list)
            {
                if (item.stredisko_id == i+1)
                {
                    SpBtMachine.Items.Add(item.machine_name);
                }
            }
        }
        private void getTypes()
        {
            TypeClass typeClass = new TypeClass();
            List<TypeClass> listType = typeClass.GetTypes();
            foreach (var item in listType)
            {
                SpBtType.Items.Add(item.type_name);
            }
        }

        //form validation
        private async void BtFinis_Click(object sender, RoutedEventArgs e)
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
                string dateTime = DateTime.Now.ToString();
                Database database = new Database();
                string[] data = new string[5];
                data[0] = SpBtStredisko.SelectedValue.ToString();
                if (SpBtType.SelectedValue==null)
                {
                    data[1] = "ostatni";
                }
                else
                {
                    data[1] = SpBtType.SelectedValue.ToString();

                }
                if (data[1]=="Budova")
                {
                    data[2] = "budova";
                    data[3] = comment;
                    data[4] = name;
                    
                  bool success=  database.insertData(data,dateTime);
                    if (success)
                    {
                        await this.ShowMessageAsync("Nahlášena porucha:", "Zadal: " + data[4]  + "\r\n" + "Středisko: " + data[0] + "\r\n" + "Typ Poruchy: Závada na budově" + "\r\n" +
                        "Datum a čas nahlášení: " + dateTime + "\r\n" + "Komentář: " + data[3]);
                    }
                    else
                    {
                        await this.ShowMessageAsync("Chyba", "Nepodařilo se nahlásit poruchu!!!!" +"\r\n"+
                            "Zkus to prosím znovu / nebo kontaktuj mistra");
                    }
                }
                if (SpBtStredisko.SelectedValue.ToString()=="Ostatní")
                {
                    data[1] = "ostatni";
                    data[2] = "ostatni";
                    data[3] = comment;
                    data[4] = name;

                    bool success = database.insertData(data, dateTime);
                    if (success)
                    {
                        await this.ShowMessageAsync("Nahlášena porucha:", "Zadal: " + data[4] + "\r\n" + "Středisko: " + data[0] + "\r\n" + "Typ Poruchy: Ostatní" + "\r\n" +
                        "Datum a čas nahlášení: " + dateTime + "\r\n" + "Komentář: " + data[3]);
                    }
                    else
                    {
                        await this.ShowMessageAsync("Chyba", "Nepodařilo se nahlásit poruchu!!!!" + "\r\n" +
                            "Zkus to prosím znovu / nebo kontaktuj mistra");
                    }
                }
                else
                {
                    data[2] = SpBtMachine.SelectedValue.ToString();
                    data[3] = comment;
                    data[4] = name;
                    bool success=database.insertData(data,dateTime);
                    if (success)
                    {
                       await this.ShowMessageAsync("Nahlášena porucha:","Zadal: "+data[4]+"\r\n"+ "Středisko: " + data[0] + "\r\n" + "Stroj: " +
                       data[2] + "\r\n" + "Typ Poruchy: " + data[1] + "\r\n" + "Datum a čas nahlášení: " + dateTime + "\r\n" + "Komentář: " + data[3]);
                    }
                    else
                    {
                        await this.ShowMessageAsync("Chyba", "Nepodařilo se nahlásit poruchu!!!!" + "\r\n" +
                            "Zkus to prosím znovu / nebo kontaktuj mistra");
                    }
                }
                  
                cleanForm();
                
            }
        }

        private async void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var mySettings = new MetroDialogSettings()
            {
                AffirmativeButtonText = "Ano",
                NegativeButtonText = "Ne",
                FirstAuxiliaryButtonText = "Cancel",
                
            };
            MessageDialogResult result = await this.ShowMessageAsync("Smazat?", "Opravdu smazat formulář o poruše?! ",
            MessageDialogStyle.AffirmativeAndNegative, mySettings);

            if (result.ToString()== "Negative")
            {
                return;
            }
            else
            {
                cleanForm();
            }
           
        }
        private void cleanForm()
        {
            refreshForm();
            SpBtStredisko.SelectedIndex = -1;
            TbName.Clear();
            TbName.Focus();
        }
        private void refreshForm()
        {
            BtFinis.Visibility = Visibility.Hidden;
            TbCommnet.Clear();
            TbCommnet.Visibility = Visibility.Hidden;
            LabComment.Visibility = Visibility.Hidden;
            LabMachine.Visibility = Visibility.Hidden;
            LabType.Visibility = Visibility.Hidden;
            SpBtMachine.Items.Clear();
            SpBtMachine.Visibility = Visibility.Hidden;
            SpBtType.Visibility = Visibility.Hidden;
            SpBtType.Items.Clear();
            
        }
        private void SpBtStredisko_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpBtStredisko.SelectedIndex!=-1)
            {
                refreshForm();
                if (SpBtStredisko.SelectedValue.ToString() == "Ostatní")
                {
                    LabComment.Visibility = Visibility.Visible;
                    TbCommnet.Visibility = Visibility.Visible;
                    BtFinis.Visibility = Visibility.Visible;
                }
                else
                {
                    getTypes();
                    LabType.Visibility = Visibility.Visible;
                    SpBtType.Visibility = Visibility.Visible;
                }
               
            }
            
        }

        private  void SpBtType_Selected(object sender, RoutedEventArgs e)
        {
            if (SpBtType.SelectedIndex!=-1)
            {
                if (SpBtType.SelectedValue.ToString() == "Budova" )
                {
                    LabComment.Visibility = Visibility.Visible;
                    TbCommnet.Visibility = Visibility.Visible;
                    BtFinis.Visibility = Visibility.Visible;
                }
                else
                {
                    getMachines(SpBtStredisko.SelectedIndex);
                    LabMachine.Visibility = Visibility.Visible;
                    SpBtMachine.Visibility = Visibility.Visible;
                }
            }
       
        }

        private void SpBtMachine_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SpBtMachine.SelectedIndex!=-1)
            {
                LabComment.Visibility = Visibility.Visible;
                TbCommnet.Visibility = Visibility.Visible;

                BtFinis.Visibility = Visibility.Visible;
            }
            
        }

       
    }
}
