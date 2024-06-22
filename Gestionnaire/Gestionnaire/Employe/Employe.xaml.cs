
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Threading;


namespace Gestionnaire.Employe
{
    /// <summary>
    /// Logique d'interaction pour Materiel.xaml
    /// </summary>
    public partial class Employe : UserControl
    {

        private BDD.GestionStockBDD connectBDD;
        //private DispatcherTimer _timer;


        public Employe()
        {
            InitializeComponent();

        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {
         AjouterEmploye.AjouterEmploye windowsemploye = new AjouterEmploye.AjouterEmploye();
         windowsemploye.WindowStartupLocation = WindowStartupLocation.CenterScreen;
         windowsemploye.ShowDialog();
        }

        public void ReloadGrid()
        {
            connectBDD = new BDD.GestionStockBDD();
            ListEmployes.ItemsSource = connectBDD.ListEmployes();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListEmployes.SelectedItem;
            if (selectedItem != null)
            {
                var selectedEmploye = (UnEmploye)selectedItem;

                var id_employe = selectedEmploye.Id_employe;

                ModifierEmploye.ModifierEmploye WindowModif = new ModifierEmploye.ModifierEmploye(id_employe);

                WindowModif.ShowDialog();

                //MessageBox.Show(nom + " " + prix + " "+ Convert.ToString(flashcode) + " " + livrer + " "+ categorie);
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListEmployes.SelectedItem;

            if (selectedItem != null)
            {
                var selectedEmploye = (UnEmploye)selectedItem;

                var id_employe = selectedEmploye.Id_employe;

                DeleteEmploye.DeleteEmploye WindowDelete = new DeleteEmploye.DeleteEmploye(id_employe);

                WindowDelete.ShowDialog();
            }
        }


        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(ListEmployes.ItemsSource);
            view.Filter = item =>
            {
                UnEmploye data = (UnEmploye)item;
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
               // StartTimer();

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
            List<int> Listid_employes = new List<int>();

            foreach (var item in ListEmployes.Items)
            {
                var row = ListEmployes.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = ListEmployes.Columns[0].GetCellContent(row) as CheckBox;
                    if (checkBox != null && checkBox.IsChecked == true)
                    {
                        var selectedEmploye = (UnEmploye)item;
                        Listid_employes.Add(selectedEmploye.Id_employe);
                    }
                }
            }

            if (Listid_employes.Count != 0)
            {
                DeleteEmploye.MultipleConfirmDelete WindowDelete = new DeleteEmploye.MultipleConfirmDelete(Listid_employes);
                WindowDelete.ShowDialog();
            }
            else
            {
                DeleteEmploye.DeleteEmployeEmpty WindowDeleteEmpty = new DeleteEmploye.DeleteEmployeEmpty();
                WindowDeleteEmpty.ShowDialog();
            }

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            ReloadGrid();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var item in ListEmployes.Items)
            {
                var row = ListEmployes.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = ListEmployes.Columns[0].GetCellContent(row) as CheckBox;
                    if (checkBox != null && checkBox.IsChecked == false)
                    {
                        checkBox.IsChecked = true;
                    }
                }
            }
        }

        private void btnResetPwd_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListEmployes.SelectedItem;
            if (selectedItem != null)
            {
                var selectedEmploye = (UnEmploye)selectedItem;
                var id_employe = selectedEmploye.Id_employe;

                ResetPwd.ResetPwd WindowResetPwd= new ResetPwd.ResetPwd(id_employe);
                WindowResetPwd.ShowDialog();
            }
        }
    }

}
