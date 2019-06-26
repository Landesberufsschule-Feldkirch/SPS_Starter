using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Text;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public void Projekte_Logo8_Lesen()
        {
            // Zuerst die Listen löschen
            foreach (var LogoTuple in Coll_Tuple_Logo8)
            {
                LogoTuple.Item3.Children.Clear();
            }

            Button_Logo8_Liste.Add(Button_Starten_Logo8_PLC);
            Button_Logo8_Liste.Add(Button_Starten_Logo8_PLC_Bugs);
            
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string ProgrammierSprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;

                foreach (var LogoTuple in Coll_Checked_Logo8)
                {
                    if (OrdnerName.Contains(LogoTuple.Item1))
                    {
                        if (LogoTuple.Item2.IsChecked.Value) Anzeigen = true;
                        ProgrammierSprache = LogoTuple.Item1;
                        StartBezeichnung = LogoTuple.Item3 + OrdnerName.IndexOf(LogoTuple.Item1);
                    }
                }

                if (Anzeigen)
                {
                    foreach (var LogoTuple in Coll_Tuple_Logo8)
                    {
                        if (d.Name.Contains(LogoTuple.Item1))
                        {
                            // nur PLC und sonst nichts
                            Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                            LogoTuple.Item2.Add(TplEintrag);
                        }
                    }
                }


            } // Ende foreach

            foreach (var LogoTuple in Coll_Tuple_Logo8)
            {
                LogoTuple.Item2.Sort();
                Logo8_TabMitInhaltFuellen(LogoTuple.Item2, LogoTuple.Item3);
            }

            Anzeige_Logo8_Aktualisieren = true;
        }

        private void Logo8_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            foreach (Tuple<string, string, string> Projekt in Projekte)
            {
                RadioButton rdo = new RadioButton
                {
                    GroupName = "Logo8!",
                    Name = Projekt.Item3,
                    FontSize = 14,
                    Content = Projekt.Item1 + " (" + Projekt.Item2 + ")",
                    VerticalAlignment = VerticalAlignment.Top
                };
                rdo.Checked += new RoutedEventHandler(Logo8_radioButton_Checked);
                StackPanel.Children.Add(rdo);
            }
        }

        private void Logo8_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Projekt_Logo8_Name = rb.Name;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_Logo8_Quelle);

            DarstellungAendernListe(Button_Logo8_Liste, true, Colors.Green, "Logo Projekt starten");

            string DateiName = $@"{ParentDirectory.FullName}\{Projekt_Logo8_Name}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = LeereHtmlSeite;

            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(HtmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(LeereHtmlSeite);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var EigenSchaften in Eigenschaften_Logo8)
            {
                if (Projekt_Logo8_Name.Contains(EigenSchaften.getKurzbezeichnung())) EigenSchaften.getBrowserBezeichnung().NavigateToStream(stmHtmlSeite);
                else EigenSchaften.getBrowserBezeichnung().NavigateToStream(stmLeereHtmlSeite);
            }


        }

    }
}