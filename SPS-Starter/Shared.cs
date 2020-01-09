using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        private void DarstellungAendernListe(List<Button> btnListe, bool enable, Color Farbe, string text)
        {
            foreach (Button Knopf in btnListe)
            {
                Knopf.IsEnabled = enable;
                Knopf.Background = new SolidColorBrush(Farbe);
                Knopf.Content = text;
                Knopf.Refresh();
            }
        }

        public static void Copy(string sourceDirectory, string targetDirectory)
        {
            DirectoryInfo diSource = new DirectoryInfo(sourceDirectory);
            DirectoryInfo diTarget = new DirectoryInfo(targetDirectory);

            CopyAll(diSource, diTarget);
        }

        public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
        {
            Directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (FileInfo fi in source.GetFiles())
            {
                Console.WriteLine($@"Copying {target.FullName}\{fi.Name}");
                fi.CopyTo(System.IO.Path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (DirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl tabControl = sender as TabControl; // e.Source could have been used instead of sender as well
            TabItem item = tabControl.SelectedValue as TabItem;

            //Button_Starten_Logo8_PLC.Content = "Logo8 Projekt Starten";
            //Button_Starten_TiaPortal_PLC.Content = "TiaPortal Projekt Starten";
            //Button_Starten_TwinCAT_PLC.Content = "TwinCAT Projekt Starten";

            switch (item.Header.ToString())
            {
                case "Logo8":
                    if (gLogo8.AnzeigeAktualisieren) gLogo8.ProjekteLesen();
                    break;

                case "TiaPortal":
                    if (gTiaPortal.AnzeigeAktualisieren) gTiaPortal.ProjekteLesen();
                    break;

                case "TwinCAT":
                    if (gTwinCat.AnzeigeAktualisieren) gTwinCat.ProjekteLesen();
                    break;

                default:
                    break;
            }
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            switch (cb.Name)
            {
                case "Checkbox_Logo8_FUP":
                    if (gLogo8 != null) if (gLogo8.AnzeigeAktualisieren) gLogo8.ProjekteLesen();
                    break;
                case "Checkbox_Logo8_KOP":
                    if (gLogo8 != null) if (gLogo8.AnzeigeAktualisieren) gLogo8.ProjekteLesen();
                    break;

                case "Checkbox_TiaPortal_FUP":
                    if (gTiaPortal != null) if (gTiaPortal.AnzeigeAktualisieren) gTiaPortal.ProjekteLesen();
                    break;
                case "Checkbox_TiaPortal_KOP":
                    if (gTiaPortal != null) if (gTiaPortal.AnzeigeAktualisieren) gTiaPortal.ProjekteLesen();
                    break;
                case "Checkbox_TiaPortal_SCL":
                    if (gTiaPortal != null) if (gTiaPortal.AnzeigeAktualisieren) gTiaPortal.ProjekteLesen();
                    break;


                case "Checkbox_TwinCAT_AS":
                    if (gTwinCat != null) if (gTwinCat.AnzeigeAktualisieren) gTwinCat.ProjekteLesen();
                    break;
                case "Checkbox_TwinCAT_AWL":
                    if (gTwinCat != null) if (gTwinCat.AnzeigeAktualisieren) gTwinCat.ProjekteLesen();
                    break;
                case "Checkbox_TwinCAT_CFC":
                    if (gTwinCat != null) if (gTwinCat.AnzeigeAktualisieren) gTwinCat.ProjekteLesen();
                    break;
                case "Checkbox_TwinCAT_FUP":
                    if (gTwinCat != null) if (gTwinCat.AnzeigeAktualisieren) gTwinCat.ProjekteLesen();
                    break;
                case "Checkbox_TwinCAT_KOP":
                    if (gTwinCat != null) if (gTwinCat.AnzeigeAktualisieren) gTwinCat.ProjekteLesen();
                    break;
                case "Checkbox_TwinCAT_ST":
                    if (gTwinCat != null) if (gTwinCat.AnzeigeAktualisieren) gTwinCat.ProjekteLesen();
                    break;

                default:
                    break;
            }
        }
    }
}