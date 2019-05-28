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
        public void Projekte_TwinCAT_Lesen()
        {

            // Zuerst die Listen l�schen
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

                if (OrdnerName.Contains("AS"))
                {
                    if (Checkbox_TwinCAT_AS.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "AS/SFC";
                    StartBezeichnung = 3 + OrdnerName.IndexOf("AS");
                }
                if (OrdnerName.Contains("AWL"))
                {
                    if (Checkbox_TwinCAT_AWL.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "AWL/IL";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("AWL");
                }
                if (OrdnerName.Contains("CFC"))
                {
                    if (Checkbox_TwinCAT_CFC.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "CFC";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("CFC");
                }
                if (OrdnerName.Contains("FUP"))
                {
                    if (Checkbox_TwinCAT_FUP.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "FUP/FBD";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("FUP");
                }
                if (OrdnerName.Contains("KOP"))
                {
                    if (Checkbox_TwinCAT_KOP.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "KOP/LD";
                    StartBezeichnung = 4 + OrdnerName.IndexOf("KOP");
                }
                if (OrdnerName.Contains("ST"))
                {
                    if (Checkbox_TwinCAT_ST.IsChecked.Value) Anzeigen = true;
                    ProgrammierSprache = "ST";
                    StartBezeichnung = 3 + OrdnerName.IndexOf("ST");
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

            string DateiName = ParentDirectory.FullName + "\\" + Projekt_TwinCAT_Name + "\\index.html";

            if (File.Exists(DateiName)) HtmlSeite = System.IO.File.ReadAllText(DateiName);
            else HtmlSeite = "<!doctype html>   </html >";

            Web_TwinCAT_PLC.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_VISU.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_NC.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_DT.NavigateToString(LeereHtmlSeite);
            Web_TwinCAT_PLC_Bugs.NavigateToString(LeereHtmlSeite);

            if (Projekt_TwinCAT_Name.Contains("PLC"))
            {
                if (Projekt_TwinCAT_Name.Contains("DT")) Web_TwinCAT_PLC.NavigateToString(HtmlSeite);
                else
                {
                    if (Projekt_TwinCAT_Name.Contains("VISU")) Web_TwinCAT_PLC_VISU.NavigateToString(HtmlSeite);
                    else
                    {
                        if (Projekt_TwinCAT_Name.Contains("NC")) Web_TwinCAT_PLC_NC.NavigateToString(HtmlSeite);
                        else Web_TwinCAT_PLC.NavigateToString(HtmlSeite);
                    }
                }
            }
            else
            {
                if (Projekt_TwinCAT_Name.Contains("BUG")) Web_TwinCAT_PLC_Bugs.NavigateToString(HtmlSeite);
                //bei Bug gibt es keine Unterkategorien
            }
        }
           
    }
}