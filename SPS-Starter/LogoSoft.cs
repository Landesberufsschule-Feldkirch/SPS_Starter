using System;
using System.IO;
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
using System.Diagnostics;
using System.Windows.Threading;


namespace SPS_Starter
{

    public partial class MainWindow
    {
        public void Projekte_Logo8_Lesen()
        {
            // Zuerst die Listen löschen
            StackPanel_Logo8_PLC.Children.Clear();
            StackPanel_Logo8_PLC_Bugs.Children.Clear();

            Button_Logo8_Liste.Add(Button_Starten_Logo8_PLC);
            Button_Logo8_Liste.Add(Button_Starten_Logo8_PLC_Bugs);

            // Name Komplett, kurz, Sprache, Anfang
            List<Tuple<string, string, string>> TupleList_Logo8_PLC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_Logo8_BUG = new List<Tuple<string, string, string>>();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string Sprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;

                if (OrdnerName.Contains("FUP"))
                {
                    if (Checkbox_Logo8_FUP.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }
                    Sprache = "FUP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("FUP");
                }
                if (OrdnerName.Contains("KOP"))
                {
                    if (Checkbox_Logo8_KOP.IsChecked.Value)
                    {
                        Anzeigen = true;
                    }
                    Sprache = "KOP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("KOP");
                }

                if (Anzeigen)
                {
                    if (d.Name.Contains("PLC"))
                    {
                        // nur PLC und sonst nichts
                        Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                        TupleList_Logo8_PLC.Add(TplEintrag);
                    }
                    else
                    {
                        // Es gibt momentan noch keine Gruppe bei den Bugs
                        Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), Sprache, OrdnerName);
                        TupleList_Logo8_BUG.Add(TplEintrag);
                    }
                }


            } // Ende foreach

            TupleList_Logo8_PLC.Sort();
            TupleList_Logo8_BUG.Sort();

            Logo8_TabMitInhaltFuellen(TupleList_Logo8_PLC, StackPanel_Logo8_PLC);
            Logo8_TabMitInhaltFuellen(TupleList_Logo8_BUG, StackPanel_Logo8_PLC_Bugs);

            Anzeige_Logo8_Aktualisieren = true;

        }

        private void Logo8_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            foreach (Tuple<string, string, string> Projekt in Projekte)
            {
                RadioButton rdo = new RadioButton();
                rdo.GroupName = "Logo8!";
                rdo.VerticalAlignment = VerticalAlignment.Top;
                rdo.Checked += new RoutedEventHandler(Logo8_radioButton_Checked);
                rdo.FontSize = 14;

                // nur PLC und sonst nichts
                rdo.Content = Projekt.Item1 + " (" + Projekt.Item2 + ")";
                rdo.Name = Projekt.Item3;
                StackPanel.Children.Add(rdo);
            }
        }


        private void Logo8_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);

            DarstellungAendernListe(Button_Logo8_Liste, true, Colors.Green, "Logo Projekt starten");
            Projekt_Logo8_Name = rb.Name;

            string HtmlSeite = "";

            string DateiName = ParentDirectory.FullName + "\\" + rb.Name + "\\index.html";
            if (File.Exists(DateiName))
            {
                HtmlSeite = System.IO.File.ReadAllText(DateiName);
            }
            else
            {
                HtmlSeite = LeereHtmlSeite;
            }

            Web_Logo8_PLC.NavigateToString(LeereHtmlSeite);
            Web_Logo8_PLC_Bugs.NavigateToString(LeereHtmlSeite);

            if (rb.Name.Contains("PLC"))
            {
                Web_Logo8_PLC.NavigateToString(HtmlSeite);
            }
            else
            {
                if (rb.Name.Contains("BUG")) Web_Logo8_PLC_Bugs.NavigateToString(HtmlSeite);
            }
        }

        private void Logo8_ProjektStarten(object sender, RoutedEventArgs e)
        {

            Button someButton = sender as Button;
   
               if( someButton.Content == "Logo Projekt starten")
                {
                    someButton.Content = "uups";

                }
         
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);
            string sourceDirectory = ParentDirectory.FullName + "\\" + Projekt_Logo8_Name;

            try
            {
                DarstellungAendernListe(Button_Logo8_Liste, true, Colors.Yellow, "Ordner " + Projekt_Logo8_Ziel + " löschen");
                if (System.IO.Directory.Exists(Projekt_Logo8_Ziel)) System.IO.Directory.Delete(Projekt_Logo8_Ziel, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 2 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_Logo8_Liste, true, Colors.Yellow, "Ordner " + Projekt_Logo8_Ziel + " erstellen");
                System.IO.Directory.CreateDirectory(Projekt_Logo8_Ziel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 3 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_Logo8_Liste, true, Colors.Yellow, "Alle Dateien kopieren");
                Copy(sourceDirectory, Projekt_Logo8_Ziel);
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 4 caught.", exp);
            }

            try
            {
                DarstellungAendernListe(Button_Logo8_Liste, true, Colors.LawnGreen, "Projekt mit Logo8! öffnen");
                Process proc = new Process();
                proc.StartInfo.FileName = Projekt_Logo8_Ziel + "\\start.cmd";
                proc.StartInfo.WorkingDirectory = Projekt_Logo8_Ziel;
                proc.Start();
            }
            catch (Exception exp)
            {
                Console.WriteLine("{0} Exception 5 caught.", exp);
            }


        }

        private void Button_Starten_Logo8_PLC_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Checkbox_Logo8_KOP_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
        }
        private void Checkbox_Logo8_FUP_Checked(object sender, RoutedEventArgs e)
        {
            if (Anzeige_Logo8_Aktualisieren) Projekte_Logo8_Lesen();
        }

    }
}