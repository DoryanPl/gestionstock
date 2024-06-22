
using System.Windows;


namespace Gestionnaire.Materiels
{
    /// <summary>
    /// Logique d'interaction pour FlashCode.xaml
    /// </summary>
    public partial class FlashCode : Window
    {
        private int id_materiel;
       private BDD.GestionStockBDD connectBDD = new BDD.GestionStockBDD();


        public FlashCode(int id_materiel)
        {
            InitializeComponent();
            this.id_materiel = id_materiel;
            QrCode qrcode = new QrCode();
            ImgQrCode.Source = qrcode.CreateQrcodeBitmapImage(id_materiel);
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            UnMateriel UnMateriel = connectBDD.GetMateriel(id_materiel);
            QrCode qrcode = new QrCode();
            qrcode.QrCodePDF(UnMateriel, false);
        }

        private void btnOpenPDF_Click(object sender, RoutedEventArgs e)
        {
            UnMateriel UnMateriel = connectBDD.GetMateriel(id_materiel);
            QrCode qrcode = new QrCode();
            qrcode.QrCodePDF(UnMateriel, true);
        }

        /*private string CreateQrcodeString()
        {
            string link = "http://projet.2brou.fr/material?id=" + id_materiel;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            MemoryStream pngStream = new MemoryStream();
            qrCodeImage.Save(pngStream, ImageFormat.Png);
            byte[] pngBytes = pngStream.ToArray();

            return Convert.ToBase64String(pngBytes);

        }*/

        /*private void QrCodePDF()
        {
            string filePath = QrCodeFilePath + "\\index.html";
            string pdfPath = QrCodeFilePath + "\\QrCode_id_" + id_materiel + ".pdf";

            try
            {
                BDD.GestionStockBDD connectBDD = new BDD.GestionStockBDD();
                UnMateriel UnMateriel = connectBDD.GetMateriel(id_materiel);

                HtmlDocument doc = new HtmlDocument();
                doc.Load(filePath);

                HtmlNode reference = doc.DocumentNode.SelectSingleNode("//p[@class='reference']");
                HtmlNode id = doc.DocumentNode.SelectSingleNode("//p[@class='id']");
                HtmlNode description = doc.DocumentNode.SelectSingleNode("//div[@class='description']/p");
                HtmlNode categorie = doc.DocumentNode.SelectSingleNode("//div[@class='ticket__header']/span[@class='categorie']");
                HtmlNode qrcode = doc.DocumentNode.SelectSingleNode("//img[@class='ticket__barcode']");

                reference.InnerHtml = UnMateriel.Reference.ToString();
                id.InnerHtml = id_materiel.ToString();
                description.InnerHtml = UnMateriel.Caracteristique.ToString();
                qrcode.Attributes["src"].Value = "data:image/bmp;base64," + CreateQrcodeString();
                categorie.InnerHtml = UnMateriel.Categorie.ToString();
                doc.Save(filePath);

                HtmlConverter.ConvertToPdf(new FileStream(filePath, FileMode.Open), new FileStream(pdfPath, FileMode.Create));

                if (File.Exists(pdfPath))
                {
                    printPDF();
                }
                else
                {
                    MessageBox.Show("Problème création PDF");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }*/

        /* public void printPDF()
        {
            string Filepath = QrCodeFilePath + "\\QrCode_id_" + id_materiel + ".pdf";

            PrintDialog Dialog = new PrintDialog();
            
            Dialog.ShowDialog();

            ProcessStartInfo printProcessInfo = new ProcessStartInfo()
            {
                Verb = "print",
                CreateNoWindow = true,
                FileName = Filepath,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process printProcess = new Process();
            printProcess.StartInfo = printProcessInfo;
            printProcess.Start();

            printProcess.WaitForInputIdle();

            Thread.Sleep(3000);

            if (false == printProcess.CloseMainWindow())
            {
                printProcess.Kill();
            }
            
        }*/

        /*public void printPDF2()
        {
            string Filepath = QrCodeFilePath + "\\QrCode_id_" + id_materiel + ".pdf";


            PdfReader reader = new PdfReader(Filepath);

            // Get the number of pages in the PDF document
            int pageCount = reader.NumberOfPages;

            // Create a document object
            Document doc = new Document(reader.GetPageSizeWithRotation(1));

            // Create a PdfWriter object
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream("output.pdf", FileMode.Create));

            // Open the document
            doc.Open();

            // Loop through each page and add it to the document
            for (int i = 1; i <= pageCount; i++)
            {
                doc.NewPage();
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                writer.DirectContent.AddTemplate(page, 0, 0);
            }

            // Close the document
            doc.Close();

            // Print the document using the default printer
            ProcessStartInfo info = new ProcessStartInfo();
            info.Verb = "print";
            info.FileName = "output.pdf";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Normal;

            Process p = new Process();
            p.StartInfo = info;
            p.Start();
        }*/

        /*private void QrCodePng()
        {
            string link = "http://185.200.244.209:443/" + id_materiel;
            string filePath = System.IO.Path.Combine(QrCodeFilePath, "QrCode_id_" + id_materiel + ".png");

            try
            {
                if (!Directory.Exists(QrCodeFilePath))
                {
                    Directory.CreateDirectory(QrCodeFilePath);
                }

                if (!File.Exists(filePath))
                {
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);
                    Bitmap qrCodeImage = qrCode.GetGraphic(20);
                    qrCodeImage.Save(filePath);
                }
                ShowQrCode(id_materiel);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }*/

        /*private void ShowQrCode()
        {
            BitmapImage imageSource = new BitmapImage(new Uri(QrCodeFilePath + "\\QrCode_id_" + id_materiel + ".png"));
            ImgQrCode.Source = imageSource;
        }*/


    }
}
