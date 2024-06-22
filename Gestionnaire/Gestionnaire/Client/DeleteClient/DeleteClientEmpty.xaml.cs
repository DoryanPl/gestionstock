
using System.Windows;


namespace Gestionnaire.Client.DeleteClient
{
    /// <summary>
    /// Logique d'interaction pour DeleteMaterielEmpty.xaml
    /// </summary>
    public partial class DeleteClientEmpty : Window
    {
        public DeleteClientEmpty()
        {
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
