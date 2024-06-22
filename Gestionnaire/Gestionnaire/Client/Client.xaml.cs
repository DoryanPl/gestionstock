
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Threading;


namespace Gestionnaire.Client
{
    /// <summary>
    /// Logique d'interaction pour Materiel.xaml
    /// </summary>
    public partial class Client : UserControl
    {

        private BDD.GestionStockBDD connectBDD;
        //private DispatcherTimer _timer;


        public Client()
        {
            InitializeComponent();

        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
            AjouterClient.AjouterClient windows = new AjouterClient.AjouterClient();
            windows.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windows.ShowDialog();
        }

        public void ReloadGrid()
        {
            connectBDD = new BDD.GestionStockBDD();
            ListClients.ItemsSource = connectBDD.ListClients();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListClients.SelectedItem;
            if (selectedItem != null)
            {
                var selectedEmploye = (UnClient)selectedItem;

                var id_employe = selectedEmploye.Id_client;

                ModifierClient.ModifierClient WindowModif = new ModifierClient.ModifierClient(id_employe);

                WindowModif.ShowDialog();

                //MessageBox.Show(nom + " " + prix + " "+ Convert.ToString(flashcode) + " " + livrer + " "+ categorie);
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListClients.SelectedItem;

            if (selectedItem != null)
            {
                var selectedEmploye = (UnClient)selectedItem;

                var id_employe = selectedEmploye.Id_client;

                DeleteClient.DeleteClient WindowDelete = new DeleteClient.DeleteClient(id_employe);

                WindowDelete.ShowDialog();
            }
        }


        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(ListClients.ItemsSource);
            view.Filter = item =>
            {
                UnClient data = (UnClient)item;
                foreach (var property in data.GetType().GetProperties())
                {
                    if (property.GetValue(data).ToString().ToLower().Contains(searchText))
                    {
                        return true;
                    }
                }
                return false;
            };

        }

        private void searchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                searchButton_Click(sender, e);
            }
        }


        private void IsConnected()
        {
            if (connectBDD.IsServerConnected() == true)
            {
                ReloadGrid();
                //StartTimer();

            }
            //else
            //{
            //    StopTimer();
            //}
        }


        //private void StartTimer()
        //{
        //    _timer = new DispatcherTimer();
        //    _timer.Interval = TimeSpan.FromSeconds(90);
        //    _timer.Tick += Timer_Tick;
        //    _timer.Start();

        //}

        //private void Timer_Tick(object sender, EventArgs e)
        //{
        //    ReloadGrid();
        //}


        //private void StopTimer()
        //{
        //    if (_timer != null)
        //    {
        //        _timer.Stop();
        //    }
        //}

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //StopTimer();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            IsConnected();
        }

        private void bntDelete_Click(object sender, RoutedEventArgs e)
        {
            List<int> Listid_clients = new List<int>();


            foreach (var item in ListClients.Items)
            {
                var row = ListClients.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox myCheckBox = ListClients.Columns[0].GetCellContent(row) as CheckBox;
                    if (myCheckBox != null && myCheckBox.IsChecked == true)
                    {
                        var selectedClient = (UnClient)item;
                        Listid_clients.Add(selectedClient.Id_client);
                        //MessageBox.Show(selectedMateriel.Id_materiel.ToString());
                    }

                }
            }

            if (Listid_clients.Count != 0)
            {
                DeleteClient.MultipleConfirmDelete WindowDelete = new DeleteClient.MultipleConfirmDelete(Listid_clients);
                WindowDelete.ShowDialog();
            }
            else
            {
                DeleteClient.DeleteClientEmpty WindowDeleteEmpty = new DeleteClient.DeleteClientEmpty();
                WindowDeleteEmpty.ShowDialog();
            }

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            ReloadGrid();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var item in ListClients.Items)
            {
                var row = ListClients.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = ListClients.Columns[0].GetCellContent(row) as CheckBox;
                    if (checkBox != null && checkBox.IsChecked == false)
                    {
                        checkBox.IsChecked = true;
                    }
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListClients.Items)
            {
                var row = ListClients.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = ListClients.Columns[0].GetCellContent(row) as CheckBox;
                    if (checkBox.IsChecked == true)
                    {
                        checkBox.IsChecked = false;
                    }
                }

            }
        }
    }
}
