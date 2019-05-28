using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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

            //
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_Bugs);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_DT);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_FIO);
            Button_TiaPortal_Liste.Add(Button_Starten_TiaPortal_PLC_HMI);

            // Name Komplett, kurz, Sprache, Anfang
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_FIO = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_HMI = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_DT = new List<Tuple<string, string, string>>();
            List<Tuple<string, string, string>> TupleList_TiaPortal_PLC_BUG = new List<Tuple<string, string, string>>();

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(ProjektOrdner_TiaPortal_Quelle);

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                string OrdnerName = d.Name;
                string ProgrammierSprache = "";
                int StartBezeichnung = 0;
                bool Anzeigen = false;

                if (OrdnerName.Contains("FUP"))
                {
                    if (Checkbox_TiaPortal_FUP.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "FUP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("FUP");
                }
                if (OrdnerName.Contains("KOP"))
                {
                    if (Checkbox_TiaPortal_KOP.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "KOP";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("KOP");
                }
                if (OrdnerName.Contains("SCL"))
                {
                    if (Checkbox_TiaPortal_SCL.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "SCL";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("SCL");
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
                                    // nur PLC und sonst nichts
                                    Tuple<string, string, string> TplEintrag = new Tuple<string, string, string>(OrdnerName.Substring(StartBezeichnung), ProgrammierSprache, OrdnerName);
                                    TupleList_TiaPortal_PLC.Add(TplEintrag);
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

            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC, StackPanel_TiaPortal_PLC);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_FIO, StackPanel_TiaPortal_PLC_FIO);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_HMI, StackPanel_TiaPortal_PLC_HMI);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_DT, StackPanel_TiaPortal_PLC_DT);
            TiaPortal_TabMitInhaltFuellen(TupleList_TiaPortal_PLC_BUG, StackPanel_TiaPortal_PLC_Bugs);

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

            string DateiName = ParentDirectory.FullName + "\\" + Projekt_TiaPortal_Name + "\\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = "<!doctype html>   </html >";

            Web_TiaPortal_PLC.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_FIO.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_HMI.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_DT.NavigateToString(LeereHtmlSeite);
            Web_TiaPortal_PLC_Bugs.NavigateToString(LeereHtmlSeite);

            if (Projekt_TiaPortal_Name.Contains("PLC"))
            {
                if (Projekt_TiaPortal_Name.Contains("DT")) Web_TiaPortal_PLC_DT.NavigateToString(HtmlSeite);
                else
                {
                    if (Projekt_TiaPortal_Name.Contains("HMI")) Web_TiaPortal_PLC_HMI.NavigateToString(HtmlSeite);
                    else
                    {
                        if (Projekt_TiaPortal_Name.Contains("FIO")) Web_TiaPortal_PLC_FIO.NavigateToString(HtmlSeite);
                        else
                        {
                            Web_TiaPortal_PLC.NavigateToString(HtmlSeite);
                        }
                    }
                }
            }
            else
            {
                if (Projekt_TiaPortal_Name.Contains("BUG")) Web_TiaPortal_PLC_Bugs.NavigateToString(HtmlSeite);
                //bei Bug gibt es keine Unterkategorien
            }
        }

    }
}