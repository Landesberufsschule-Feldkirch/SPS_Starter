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
        public void Projekte_TwinCAT_Lesen()
        {

            // Zuerst die Listen löschen
            StackPanel_TwinCAT_PLC.Children.Clear();
            StackPanel_TwinCAT_PLC_Bugs.Children.Clear();
            StackPanel_TwinCAT_PLC_DT.Children.Clear();
            StackPanel_TwinCAT_PLC_NC.Children.Clear();
            StackPanel_TwinCAT_PLC_VISU.Children.Clear();

            //
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_Bugs);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_DT);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_NC);
            Button_TwinCAT_Liste.Add(Button_Starten_TwinCAT_PLC_VISU);


            // Name Komplett, kurz, Sprache, Anfang
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_VISU = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_NC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_BUG = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TwinCAT_PLC_DT = new List<Tuple<string, string, string>>();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TwinCAT_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string ProgrammierSprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;


                foreach (var TwinCATTuple in Coll_Checked_TwinCAT)
                {
                    if (OrdnerName.Contains(TwinCATTuple.Item1))
                    {
                        if (TwinCATTuple.Item2.IsChecked.Value) Anzeigen = true;
                        ProgrammierSprache = TwinCATTuple.Item1;
                        StartBezeichnung = TwinCATTuple.Item3 + OrdnerName.IndexOf(TwinCATTuple.Item1);
                    }
                }

                if (Anzeigen)
                {

                    if (d.Name.Contains("PLC"))
                    {
                        if (d.Name.Contains("DT"))
                        {
                            Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                            TupleList_TwinCAT_PLC_DT.Add(TplEintrag);
                        }
                        else
                        {
                            if (d.Name.Contains("VISU"))
                            {
                                Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                TupleList_TwinCAT_PLC_VISU.Add(TplEintrag);
                            }
                            else
                            {
                                if (d.Name.Contains("NC"))
                                {
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                    TupleList_TwinCAT_PLC_NC.Add(TplEintrag);
                                }
                                else
                                {
                                    // nur PLC und sonst nichts
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                    TupleList_TwinCAT_PLC.Add(TplEintrag);
                                }
                            }
                        }
                    }
                }
                else
                {
                    // Es gibt momentan noch keine Gruppe bei den Bugs
                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                    TupleList_TwinCAT_PLC_BUG.Add(TplEintrag);
                }
            } // Ende foreach

            TupleList_TwinCAT_PLC.Sort();
            TupleList_TwinCAT_PLC_NC.Sort();
            TupleList_TwinCAT_PLC_VISU.Sort();
            TupleList_TwinCAT_PLC_DT.Sort();
            TupleList_TwinCAT_PLC_BUG.Sort();

            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC, StackPanel_TwinCAT_PLC);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_NC, StackPanel_TwinCAT_PLC_NC);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_VISU, StackPanel_TwinCAT_PLC_VISU);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_DT, StackPanel_TwinCAT_PLC_DT);
            TwinCAT_TabMitInhaltFuellen(TupleList_TwinCAT_PLC_BUG, StackPanel_TwinCAT_PLC_Bugs);

            Anzeige_TwinCAT_Aktualisieren = true;
        }

        private void TwinCAT_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            foreach (Tuple<string, string, string> Projekt in Projekte)
            {
                RadioButton rdo = new RadioButton
                {
                    GroupName = "TwinCAT",
                    Name = Projekt.Item3,
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = Projekt.Item1 + " (" + Projekt.Item2 + ")"
                };
                rdo.Checked += new RoutedEventHandler(TwinCAT_radioButton_Checked);

                StackPanel.Children.Add(rdo);
            }
        }

        private void TwinCAT_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Projekt_TwinCAT_Name = rb.Name;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TwinCAT_Quelle);

            DarstellungAendernListe(Button_TwinCAT_Liste, true, Colors.Green, "TwinCAT Projekt starten");

            string DateiName = $@"{ParentDirectory.FullName}\{Projekt_TwinCAT_Name}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = "<!doctype html>   </html >";

            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(HtmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(LeereHtmlSeite);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var TwinCATTuple in Coll_Html_TwinCAT)
            {
                if (Projekt_TwinCAT_Name.Contains(TwinCATTuple.Item1)) TwinCATTuple.Item2.NavigateToStream(stmHtmlSeite);
                else TwinCATTuple.Item2.NavigateToStream(stmLeereHtmlSeite);
            }
        }

    }
}