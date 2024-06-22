
using System.Collections.Generic;
using System.Windows;


namespace Gestionnaire.Materiels
{
    /// <summary>
    /// Logique d'interaction pour FlashCode.xaml
    /// </summary>
    public partial class MultipleFlashCode : Window
    {
        private List<UnMateriel> Listid_materiel;
        private string QrCodeFilePath = Properties.Settings.Default.Path;

        public MultipleFlashCode(List<UnMateriel> Listid_materiel)
        {
            InitializeComponent();
            this.Listid_materiel = Listid_materiel;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            QrCode qrcode = new QrCode();
            qrcode.ListQrCodePDF(Listid_materiel, false);
        }

        private void btnOpenPDF_Click(object sender, RoutedEventArgs e)
        {
            QrCode qrcode = new QrCode();
            qrcode.ListQrCodePDF(Listid_materiel, true);
        }

 
    }
}
