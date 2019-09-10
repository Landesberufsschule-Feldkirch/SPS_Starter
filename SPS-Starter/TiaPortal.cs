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
        public void Projekte_TiaPortal_Lesen()
        {

            // Zuerst die Listen löschen
            StackPanel_TiaPortal_PLC.Children.Clear();
            StackPanel_TiaPortal_PLC_Bugs.Children.Clear();
            StackPanel_TiaPortal_PLC_DT.Children.Clear();
            StackPanel_TiaPortal_PLC_FIO.Children.Clear();
            StackPanel_TiaPortal_PLC_HMI.Children.Clear();
            StackPanel_TiaPortal_PLC_Snap7.Children.Clear();

            //
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_Bugs);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_DT);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_FIO);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_HMI);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_Snap7);

            // Name Komplett, kurz, Sprache, Anfang
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_FIO = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_HMI = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_DT = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_BUG = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_Snap7 = new List<Tuple<string, string, string>>();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TiaPortal_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string ProgrammierSprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;


                foreach (var TiaPortalTuple in Coll_Checked_TiaPortal)
                {
                    if (OrdnerName.Contains(TiaPortalTuple.Item1))
                    {
                        if (TiaPortalTuple.Item2.IsChecked.Value) Anzeigen = true;
                        ProgrammierSprache = TiaPortalTuple.Item1;
                        StartBezeichnung = TiaPortalTuple.Item3 + OrdnerName.IndexOf(TiaPortalTuple.Item1);
                    }
                }

                if (Anzeigen)
                {
                    if (d.Name.Contains("PLC"))
                    {
                        if (d.Name.Contains("DT"))
                        {
                            Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                            TupleList_TiaPortal_PLC_DT.Add(TplEintrag);
                        }
                        else
                        {
                            if (d.Name.Contains("HMI"))
                            {
                                Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                TupleList_TiaPortal_PLC_HMI.Add(TplEintrag);
                            }
                            else
                            {
                                if (d.Name.Contains("FIO"))
                                {
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                    TupleList_TiaPortal_PLC_FIO.Add(TplEintrag);
                                }
                                else
                                {
                                    if (d.Name.Contains("Snap7"))
                                    {
                                        Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                        TupleList_TiaPortal_PLC_Snap7.Add(TplEintrag);
                                    }
                                    else
                                    {
                                        // nur PLC und sonst nichts
                                        Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                        TupleList_TiaPortal_PLC.Add(TplEintrag);
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    // Es gibt momentan noch keine Gruppe bei den Bugs
                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                    TupleList_TiaPortal_PLC_BUG.Add(TplEintrag);
                }

            } // Ende foreach

            TupleList_TiaPortal_PLC.Sort();
            TupleList_TiaPortal_PLC_FIO.Sort();
            TupleList_TiaPortal_PLC_HMI.Sort();
            TupleList_TiaPortal_PLC_DT.Sort();
            TupleList_TiaPortal_PLC_BUG.Sort();
            TupleList_TiaPortal_PLC_Snap7.Sort();

            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC, StackPanel_TiaPortal_PLC);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_FIO, StackPanel_TiaPortal_PLC_FIO);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_HMI, StackPanel_TiaPortal_PLC_HMI);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_DT, StackPanel_TiaPortal_PLC_DT);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_BUG, StackPanel_TiaPortal_PLC_Bugs);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_Snap7, StackPanel_TiaPortal_PLC_Snap7);

            Anzeige_TiaPortal_Aktualisieren = true;
        }

        private void TiaPortal_TabMitInhaltFuellen(List<Tuple<string, string, string>> Projekte, System.Windows.Controls.StackPanel StackPanel)
        {
            foreach (Tuple<string, string, string> Projekt in Projekte)
            {
                RadioButton rdo = new RadioButton
                {
                    GroupName = "TIA_PORTAL_V14_SP1",
                    Name = Projekt.Item3,
                    FontSize = 14,
                    VerticalAlignment = VerticalAlignment.Top,
                    Content = Projekt.Item1 + " (" + Projekt.Item2 + ")"
                };
                rdo.Checked += new RoutedEventHandler(TiaPortal_radioButton_Checked);
                StackPanel.Children.Add(rdo);
            }
        }

        private void TiaPortal_radioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            Projekt_TiaPortal_Name = rb.Name;

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TiaPortal_Quelle);

            DarstellungAendernListe(Button_TiaPortal_Liste, true, Colors.Green, "TiaPortal Projekt starten");

            string DateiName = $@"{ParentDirectory.FullName}\{Projekt_TiaPortal_Name}\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = "<!doctype html>   </html >";


            byte[] dataHtmlSeite = Encoding.UTF8.GetBytes(HtmlSeite);
            MemoryStream stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

            byte[] dataLeereHtmlSeite = Encoding.UTF8.GetBytes(LeereHtmlSeite);
            MemoryStream stmLeereHtmlSeite = new MemoryStream(dataLeereHtmlSeite, 0, dataLeereHtmlSeite.Length);

            foreach (var TiaPortalTuple in Coll_Html_TiaPortal)
            {
                if (Projekt_TiaPortal_Name.Contains(TiaPortalTuple.Item1)) TiaPortalTuple.Item2.NavigateToStream(stmHtmlSeite);
                else TiaPortalTuple.Item2.NavigateToStream(stmLeereHtmlSeite);
            }

        }
    }

}