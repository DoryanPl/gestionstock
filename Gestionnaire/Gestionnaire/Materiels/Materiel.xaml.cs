
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Threading;


namespace Gestionnaire.Materiels
{
    /// <summary>
    /// Logique d'interaction pour Materiel.xaml
    /// </summary>
    public partial class Materiel : UserControl
    {

        private BDD.GestionStockBDD connectBDD;
        //private DispatcherTimer _timer;

        public Materiel()
        {
            InitializeComponent();

        }

        private void btnAjouter_Click(object sender, RoutedEventArgs e)
        {

            AjouterMateriel.AjouterMateriel windowsmateriel = new AjouterMateriel.AjouterMateriel();
            windowsmateriel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            windowsmateriel.ShowDialog();
        }

        public void ReloadGrid()
        {
            connectBDD = new BDD.GestionStockBDD();
            ListMateriels.ItemsSource = connectBDD.ListMateriels();
        }
        
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListMateriels.SelectedItem;
            if (selectedItem != null)
            {
                var selectedMateriel = (UnMateriel)selectedItem;

                var id_materiel = selectedMateriel.Id_materiel;

                ModifierMateriel.ModifierMateriel WindowModif = new ModifierMateriel.ModifierMateriel(id_materiel);

                WindowModif.ShowDialog();

                //MessageBox.Show(nom + " " + prix + " "+ Convert.ToString(flashcode) + " " + livrer + " "+ categorie);
            }
        
        }
        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListMateriels.SelectedItem;

            if (selectedItem != null)
            {
                var selectedMateriel = (UnMateriel)selectedItem;

                var id_materiel = selectedMateriel.Id_materiel;

                DeleteMateriel.ConfirmDelete WindowDelete = new DeleteMateriel.ConfirmDelete(id_materiel);

                WindowDelete.ShowDialog();
            }
        }


        private void FlashCodeButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ListMateriels.SelectedItem;
            if (selectedItem != null)
            {
                var selectedMateriel = (Materiels.UnMateriel)selectedItem;
                var id_materiel = selectedMateriel.Id_materiel;

                FlashCode WindowQRCode = new FlashCode(id_materiel);
                WindowQRCode.ShowDialog();
            }


        }

       
        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();
            ICollectionView view = CollectionViewSource.GetDefaultView(ListMateriels.ItemsSource);
            view.Filter = item =>
            {
                Materiels.UnMateriel data = (Materiels.UnMateriel)item;
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
            List<int> Listid_materiel = new List<int>();


            foreach (var item in ListMateriels.Items)
            {
                var row = ListMateriels.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox myCheckBox = ListMateriels.Columns[0].GetCellContent(row) as CheckBox;
                    if (myCheckBox != null && myCheckBox.IsChecked == true)
                    {
                        var selectedMateriel = (Materiels.UnMateriel)item;
                        Listid_materiel.Add(selectedMateriel.Id_materiel);
                        //MessageBox.Show(selectedMateriel.Id_materiel.ToString());
                    }

                }
            }

            if (Listid_materiel.Count != 0)
            {
                DeleteMateriel.MultipleConfirmDelete WindowDelete = new DeleteMateriel.MultipleConfirmDelete(Listid_materiel);
                WindowDelete.ShowDialog();
            }
            else
            {
                DeleteMateriel.DeleteMaterielEmpty WindowDeleteEmpty = new DeleteMateriel.DeleteMaterielEmpty();
                WindowDeleteEmpty.ShowDialog();
            }

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            ReloadGrid();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            foreach (var item in ListMateriels.Items)
            {
                var row = ListMateriels.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = ListMateriels.Columns[0].GetCellContent(row) as CheckBox;
                    if (checkBox != null && checkBox.IsChecked == false)
                    {
                        checkBox.IsChecked = true;
                    }
                }
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListMateriels.Items)
            {
                var row = ListMateriels.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    var checkBox = ListMateriels.Columns[0].GetCellContent(row) as CheckBox;
                    if (checkBox.IsChecked == true)
                    {
                        checkBox.IsChecked = false;
                    }
                }
            }
        }

        private void btnQrCode_Click(object sender, RoutedEventArgs e)
        {
            List<UnMateriel> Listid_materiel = new List<UnMateriel>();

            foreach (var item in ListMateriels.Items)
            {
                var row = ListMateriels.ItemContainerGenerator.ContainerFromItem(item) as DataGridRow;
                if (row != null)
                {
                    CheckBox myCheckBox = ListMateriels.Columns[0].GetCellContent(row) as CheckBox;
                    if (myCheckBox != null && myCheckBox.IsChecked == true)
                    {
                        var selectedMateriel = (Materiels.UnMateriel)item;
                        BDD.GestionStockBDD connectBDD = new BDD.GestionStockBDD();
                        UnMateriel UnMateriel = connectBDD.GetMateriel(selectedMateriel.Id_materiel);
                        Listid_materiel.Add(UnMateriel);
                        //MessageBox.Show(selectedMateriel.Id_materiel.ToString());
                    }
                    
                }
            }

            if (Listid_materiel.Count != 0)
            {
                MultipleFlashCode qrcode = new MultipleFlashCode(Listid_materiel);
                qrcode.ShowDialog();
            }
            else
            {
                DeleteMateriel.DeleteMaterielEmpty WindowDeleteEmpty = new DeleteMateriel.DeleteMaterielEmpty();
                WindowDeleteEmpty.ShowDialog();
            }
            
        }


    }

}
