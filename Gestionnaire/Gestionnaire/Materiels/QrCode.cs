using System;
using System.Collections.Generic;
using System.Windows;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using QRCoder;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.Windows.Controls;
using Image = iText.Layout.Element.Image;
using VerticalAlignment = iText.Layout.Properties.VerticalAlignment;
using HorizontalAlignment = iText.Layout.Properties.HorizontalAlignment;
using TextAlignment = iText.Layout.Properties.TextAlignment;
using System.Windows.Media.Imaging;


namespace Gestionnaire.Materiels
{
   public class QrCode
    {
        public QrCode()
        {

        }

        private string QrCodeFilePath = Properties.Settings.Default.Path;
        private string link = Properties.Settings.Default.UrlQrCode;

        private string TestPath()
        {
            if (!Directory.Exists(QrCodeFilePath))
            {
                throw new Exception("Définir un dossier pour enregistrer les PDFs");
            }
            else 
            { 
                return QrCodeFilePath;
            }
        }

        public BitmapImage CreateQrcodeBitmapImage(int id_materiel)
        {
            Bitmap qrCodeImage = CreateQrcodeBitmap(id_materiel);

            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream memory = new MemoryStream())
            {
                qrCodeImage.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = memory;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            return bitmapImage;
        }

        private Bitmap CreateQrcodeBitmap(int id_materiel)
        {
            string link = this.link + id_materiel;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            return qrCodeImage;

        }

        private byte[] CreateQrcodeString(int id_materiel)
        {
            string link = this.link + id_materiel;

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(link, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            MemoryStream pngStream = new MemoryStream();
            qrCodeImage.Save(pngStream, System.Drawing.Imaging.ImageFormat.Png);
            byte[] pngBytes = pngStream.ToArray();

            return pngBytes;

        }

        public string namePDF()
        {
            string pdfPath;
            bool name = true;
            int i = 0;

            while (name)
            {
                pdfPath = TestPath() + "\\QrCode_List_" + i + ".pdf";

                if (!File.Exists(pdfPath))
                {
                    return pdfPath;
                }

                i++;
            }

            return null;
        }

        public void ListQrCodePDF(List<UnMateriel> ListeMateriels, bool PrintOrPDF)
        {
            try
            {
                string Path;

                if ((Path=namePDF()) == null)
                {
                    throw new Exception("Echec nom pdf");
                }

                PdfWriter writer = new PdfWriter(Path);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                float centimètre = 28.3464567f; // Sert de variable pour 1 centimètre

                ////// Construction du PDF : //////

                for (int i = 0; i < ListeMateriels.Count; i += 2)
                {
                    UnMateriel materiel = ListeMateriels[i];
                    // Qrcode 1er element ligne
                    byte[] pngBytes = CreateQrcodeString(materiel.Id_materiel);
                    ImageData data = ImageDataFactory.Create(pngBytes);
                    Image img1 = new Image(data);

                    img1.SetWidth(4 * centimètre);
                    img1.SetHeight(4 * centimètre);
                    //

                    // Vérifiez si l'indice suivant est valide avant de récupérer l'élément suivant
                    if (i + 1 < ListeMateriels.Count)
                    {
                        UnMateriel materiel2 = ListeMateriels[i + 1];
                        // Qrcode 1er element ligne
                        byte[] pngBytes2 = CreateQrcodeString(materiel.Id_materiel);
                        ImageData data2 = ImageDataFactory.Create(pngBytes2);
                        Image img2 = new Image(data2);
                        img2.SetWidth(4 * centimètre);
                        img2.SetHeight(4 * centimètre);
                        //


                        // Faites quelque chose avec photo1 et photo2 ici...

                        // Table
                        Table table = new Table(4, false) // (NbrDeColonnes, true pour faire la largeur de la page, false pour l'adapter selon son contenu)
                            .SetHorizontalAlignment(HorizontalAlignment.CENTER)
                            .UseAllAvailableWidth();

                        // Création d'une cellule contenant le QRcode centrée
                        Cell cell11 = new Cell(1, 1)
                           .Add(img1.SetHorizontalAlignment(HorizontalAlignment.CENTER))
                           .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                        //Cellule contenant les informations de la photo
                        Cell cell12 = new Cell(1, 1)
                           .SetTextAlignment(TextAlignment.CENTER)
                           .SetFontSize(13)
                           .SetWidth((float)5 * centimètre).SetHeight((float)4 * centimètre)
                           .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                           .Add(new Paragraph("ID : " + materiel.Id_materiel).SetBold())
                           //.Add(new Paragraph(materiel.caracteristique).SetItalic())
                           .Add(new Paragraph("CAT : " + materiel.Categorie))
                           .Add(new Paragraph("REF : " + materiel.Reference));

                        Cell cell13 = new Cell(1, 1)
                           .Add(img2.SetHorizontalAlignment(HorizontalAlignment.CENTER))
                           .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                        //Cellule contenant les informations de la photo
                        Cell cell14 = new Cell(1, 1)
                           .SetTextAlignment(TextAlignment.CENTER)
                           .SetFontSize(13)
                           .SetWidth((float)5 * centimètre).SetHeight((float)4 * centimètre)
                           .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                           .Add(new Paragraph("ID : " + materiel2.Id_materiel).SetBold())
                           //.Add(new Paragraph(materiel.caracteristique).SetItalic())
                           .Add(new Paragraph("CAT : " + materiel2.Categorie))
                           .Add(new Paragraph("REF : " + materiel2.Reference));

                        table.AddCell(cell11);
                        table.AddCell(cell12);
                        table.AddCell(cell13);
                        table.AddCell(cell14);
                        document.Add(table);

                    }
                    else
                    {
                        // On rentre dans ce else si le nombre de photo dans la base de données est impair.

                        // Table
                        Table table = new Table(2, false); // (NbrDeColonnes, true pour faire la largeur de la page, false pour l'adapter selon son contenu)

                        // Création d'une cellule contenant le QRcode centrée
                        Cell cell11 = new Cell(1, 1)
                           .Add(img1.SetHorizontalAlignment(HorizontalAlignment.CENTER))
                           .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                        //Cellule contenant les informations de la photo
                        Cell cell12 = new Cell(1, 1)
                           .SetTextAlignment(TextAlignment.CENTER)
                           .SetFontSize(13)
                           .SetWidth((float)4.9 * centimètre).SetHeight((float)4 * centimètre)
                           .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                           .Add(new Paragraph("ID : " + materiel.Id_materiel).SetBold())
                           //.Add(new Paragraph(materiel.caracteristique).SetItalic())
                           .Add(new Paragraph("CAT : " + materiel.Categorie))
                           .Add(new Paragraph("REF : " + materiel.Reference));

                        table.AddCell(cell11);
                        table.AddCell(cell12);
                        document.Add(table);
                    }
                }

                document.Close(); // Fermer le PDF une fois nos opérations terminées


                if (File.Exists(Path))
                {
                    if (PrintOrPDF)
                    {
                        openPDF(Path);
                    }
                    else
                    {
                        printPDF(Path);
                    }
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
        }

        public void QrCodePDF(UnMateriel materiel, bool PrintOrPDF)
        {
            try
            {
                string pdfPath = TestPath() + "\\QrCode_id_" + materiel.Id_materiel + ".pdf";

                PdfWriter writer = new PdfWriter(pdfPath);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                float centimètre = 28.3464567f; // Sert de variable pour 1 centimètre

                ////// Construction du PDF : //////

                // Qrcode 1er element ligne
                byte[] pngBytes = CreateQrcodeString(materiel.Id_materiel);
                ImageData data = ImageDataFactory.Create(pngBytes);
                Image img = new Image(data);
                img.SetWidth(4 * centimètre);
                img.SetHeight(4 * centimètre);

                // On rentre dans ce else si le nombre de photo dans la base de données est impair.

                // Table
                Table table = new Table(2, false); // (NbrDeColonnes, true pour faire la largeur de la page, false pour l'adapter selon son contenu)

                // Création d'une cellule contenant le QRcode centrée
                Cell cell11 = new Cell(1, 1)
                   .Add(img.SetHorizontalAlignment(HorizontalAlignment.CENTER))
                   .SetVerticalAlignment(VerticalAlignment.MIDDLE);

                //Cellule contenant les informations de la photo
                Cell cell12 = new Cell(1, 1)
                   .SetTextAlignment(TextAlignment.CENTER)
                   .SetFontSize(13)
                   .SetWidth((float)4.9 * centimètre).SetHeight((float)4 * centimètre)
                   .SetVerticalAlignment(VerticalAlignment.MIDDLE)
                   .Add(new Paragraph("ID : " + materiel.Id_materiel))
                   //.Add(new Paragraph(materiel.caracteristique).SetItalic())
                   .Add(new Paragraph("CAT : " + materiel.Categorie))
                   .Add(new Paragraph("REF : " + materiel.Reference));

                table.AddCell(cell11);
                table.AddCell(cell12);
                document.Add(table);

                document.Close(); // Fermer le PDF une fois nos opérations terminées


                if (File.Exists(pdfPath))
                {
                    if (PrintOrPDF)
                    {
                        openPDF(pdfPath);
                    }
                    else 
                    {
                        printPDF(pdfPath);
                    }
                }
                else
                {
                    MessageBox.Show("Problème création PDF");
                }

                //document.Close(); // Fermer le PDF une fois nos opérations terminées

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void printPDF(string path)
        {

            PrintDialog Dialog = new PrintDialog();

            Dialog.ShowDialog();

            ProcessStartInfo printProcessInfo = new ProcessStartInfo()
            {
                Verb = "print",
                CreateNoWindow = true,
                FileName = path,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process printProcess = new Process();
            printProcess.StartInfo = printProcessInfo;
            printProcess.Start();

            printProcess.WaitForInputIdle();

            Thread.Sleep(3000);

            if (printProcess.CloseMainWindow() == false)
            {
                printProcess.Kill();
            }



            //PrintDialog printDialog = new PrintDialog();

            //// Affiche la boîte de dialogue d'impression
            //if (printDialog.ShowDialog() == true)
            //{
            //    // Obtient l'imprimante sélectionnée
            //    PrintQueue printerQueue = new PrintServer().GetPrintQueue(printDialog.PrinterSettings.PrinterName);

            //    // Imprime le document avec l'imprimante sélectionnée
            //    PrintDocument pd = new PrintDocument();
            //    pd.PrintPage += delegate (object sender, PrintPageEventArgs e)
            //    {
            //        // Dessinez votre document ici
            //    };
            //    pd.PrinterSettings.PrinterName = printerQueue.FullName;
            //    pd.Print();
            //}




        }

        public void openPDF(string path)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = path,
                UseShellExecute = true
            };
            Process.Start(startInfo);
        }



    }
}
