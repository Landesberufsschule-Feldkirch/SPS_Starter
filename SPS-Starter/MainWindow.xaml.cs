using SPS_Starter.Model;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public AlleDaten AlleDaten { get; set; }
        public AlleWerte AlleWerte { get; set; }
        public ProjektEigenschaften AktuellesProjekt { get; set; }
        public SpsStarter.Steuerungen AktuelleSteuerung { get; set; }
        public MainWindow()
        {
            AktuelleSteuerung = SpsStarter.Steuerungen.Logo;
            AlleWerte = new AlleWerte();

            var viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = viewModel;

            AlleDaten = new AlleDaten(this);


            AnzeigeUpdatenLogo();
            AnzeigeUpdatenTiaPortal();
            AnzeigeUpdatenTwinCat();
        }

        internal void ProjektStarten(object obj)
        {
            try
            {
                if (Directory.Exists(AktuellesProjekt.ZielOrdner)) Directory.Delete(AktuellesProjekt.ZielOrdner, true);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 2 caught.");
            }
            
            try
            {
                Copy(AktuellesProjekt.QuellOrdner, AktuellesProjekt.ZielOrdner);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 4 caught.");
            }

            try
            {
                var proc = new Process
                {
                    StartInfo =
                    {
                        FileName = AktuellesProjekt.ZielOrdner + "\\start.cmd",
                        WorkingDirectory = AktuellesProjekt.ZielOrdner
                    }
                };
                proc.Start();
            }
            catch (Exception exp)
            {
                Console.WriteLine($"{exp} Exception 5 caught.");
            }
        }




        public void TabControlSelectionChanged(object obj)
        {
           
    //

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
                fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);
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
            if (sender is TabControl tabControl && tabControl.SelectedValue is TabItem item)
            {
                switch (item.Header.ToString())
                {
                    case "Logo8":
                        AktuelleSteuerung = SpsStarter.Steuerungen.Logo;
                        AnzeigeUpdatenLogo();
                        break;

                    case "TiaPortal":
                        AktuelleSteuerung = SpsStarter.Steuerungen.TiaPortal;
                        AnzeigeUpdatenTiaPortal();
                        break;

                    case "TwinCAT":
                        AktuelleSteuerung = SpsStarter.Steuerungen.TwinCat;
                        AnzeigeUpdatenTwinCat();
                        break;
                }
            }
        }


        public void ButtonGeaendert(object obj)
        {
            switch (AktuelleSteuerung)
            {
                case SpsStarter.Steuerungen.Logo:
                    AnzeigeUpdatenLogo();
                    break;

                case SpsStarter.Steuerungen.TiaPortal:
                    AnzeigeUpdatenTiaPortal();
                    break;

                case SpsStarter.Steuerungen.TwinCat:
                    AnzeigeUpdatenTwinCat();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(AktuelleSteuerung), AktuelleSteuerung, "ButtonGeaendert");
            }
        }

        private void AnzeigeUpdatenTwinCat()
        {
            var als = true;
            var awl = true;
            var cfc = true;
            var cpp = true;
            var fup = true;
            var kop = true;
            var st = true;

            if (CheckboxTwinCatAs?.IsChecked != null) als = (bool)CheckboxTwinCatAs.IsChecked;
            if (CheckboxTwinCatAwl?.IsChecked != null) awl = (bool)CheckboxTwinCatAwl.IsChecked;
            if (CheckboxTwinCatCfc?.IsChecked != null) cfc = (bool)CheckboxTwinCatCfc.IsChecked;
            if (CheckboxTwinCatCpp?.IsChecked != null) cpp = (bool)CheckboxTwinCatCpp.IsChecked;
            if (CheckboxTwinCatFup?.IsChecked != null) fup = (bool)CheckboxTwinCatFup.IsChecked;
            if (CheckboxTwinCatKop?.IsChecked != null) kop = (bool)CheckboxTwinCatKop.IsChecked;
            if (CheckboxTwinCatSt?.IsChecked != null) st = (bool)CheckboxTwinCatSt.IsChecked;

            if (AlleDaten.AlleTabEigenschaften != null)
            {
                foreach (var tabEigenschaften in AlleDaten.AlleTabEigenschaften)
                {
                    if (tabEigenschaften.Steuerungen == SpsStarter.Steuerungen.TwinCat)
                    {
                        tabEigenschaften.ProjekteBezeichnung.Clear();
                        tabEigenschaften.StackPanelBezeichnung?.Children?.Clear();

                        foreach (var projektEigenschaften in AlleDaten.AlleProjektEigenschaften)
                        {
                            if (projektEigenschaften.Steuerung == SpsStarter.Steuerungen.TwinCat)
                            {
                                switch (projektEigenschaften.Programmiersprache)
                                {
                                    case SpsStarter.SpsSprachen.As when als:
                                    case SpsStarter.SpsSprachen.Awl when awl:
                                    case SpsStarter.SpsSprachen.Cfc when cfc:
                                    case SpsStarter.SpsSprachen.Cpp when cpp:
                                    case SpsStarter.SpsSprachen.Fup when fup:
                                    case SpsStarter.SpsSprachen.Kop when kop:
                                    case SpsStarter.SpsSprachen.Stl when st:
                                        EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AnzeigeUpdatenTiaPortal()
        {
            var fup = true;
            var kop = true;
            var scl = true;

            if (CheckboxTiaPortalFup?.IsChecked != null) fup = (bool)CheckboxTiaPortalFup.IsChecked;
            if (CheckboxTiaPortalKop?.IsChecked != null) kop = (bool)CheckboxTiaPortalKop.IsChecked;
            if (CheckboxTiaPortalScl?.IsChecked != null) scl = (bool)CheckboxTiaPortalScl.IsChecked;

            if (AlleDaten.AlleTabEigenschaften != null)
            {
                foreach (var tabEigenschaften in AlleDaten.AlleTabEigenschaften)
                {
                    if (tabEigenschaften.Steuerungen == SpsStarter.Steuerungen.TiaPortal)
                    {
                        tabEigenschaften.ProjekteBezeichnung.Clear();
                        tabEigenschaften.StackPanelBezeichnung?.Children.Clear();

                        foreach (var projektEigenschaften in AlleDaten.AlleProjektEigenschaften)
                        {
                            if (projektEigenschaften.Steuerung == SpsStarter.Steuerungen.TiaPortal)
                            {
                                switch (projektEigenschaften.Programmiersprache)
                                {
                                    case SpsStarter.SpsSprachen.Fup when fup:
                                    case SpsStarter.SpsSprachen.Kop when kop:
                                    case SpsStarter.SpsSprachen.Scl when scl:
                                        EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AnzeigeUpdatenLogo()
        {
            var fup = true;
            var kop = true;

            if (CheckboxLogoFup?.IsChecked != null) fup = (bool)CheckboxLogoFup.IsChecked;
            if (CheckboxLogoKop?.IsChecked != null) kop = (bool)CheckboxLogoKop.IsChecked;

            if (AlleDaten.AlleTabEigenschaften != null)
            {
                /*
                var logo = AlleDaten.AlleProjektEigenschaften
                    .Where(x => x.Steuerung == SpsStarter.Steuerungen.Logo)
                    .Where(y => (y.Programmiersprache == SpsStarter.SpsSprachen.Fup &&fup) ||
                                (y.Programmiersprache == SpsStarter.SpsSprachen.Kop && kop))
                    .ToList();

                AlleDaten.AlleTabEigenschaften
                    .Where(x => x.Steuerungen == SpsStarter.Steuerungen.Logo)
                    .ToList()
                    .ForEach(x =>
                    {
                        x.ProjekteBezeichnung.Clear();
                        x.StackPanelBezeichnung?.Children?.Clear();

                    });
                */

                foreach (var tabEigenschaften in AlleDaten.AlleTabEigenschaften)
                {
                    if (tabEigenschaften.Steuerungen == SpsStarter.Steuerungen.Logo)
                    {
                        tabEigenschaften.ProjekteBezeichnung.Clear();
                        tabEigenschaften.StackPanelBezeichnung?.Children.Clear();

                        foreach (var projektEigenschaften in AlleDaten.AlleProjektEigenschaften)
                        {
                            if (projektEigenschaften.Steuerung == SpsStarter.Steuerungen.Logo)
                            {
                                switch (projektEigenschaften.Programmiersprache)
                                {
                                    case SpsStarter.SpsSprachen.Fup when fup:
                                    case SpsStarter.SpsSprachen.Kop when kop:
                                        EinzelnenTabFuellen(tabEigenschaften, projektEigenschaften);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void EinzelnenTabFuellen(TabEigenschaften tabEigenschaften, ProjektEigenschaften projektEigenschaften)
        {

            if (tabEigenschaften.SpsKategorie == projektEigenschaften.SpsKategorie)
            {
                projektEigenschaften.BrowserBezeichnung = tabEigenschaften.BrowserBezeichnung;

                var rdo = new RadioButton
                {
                    GroupName = projektEigenschaften.Steuerung.ToString(),
                    Name = projektEigenschaften.Bezeichnung,
                    FontSize = 14,
                    Content = projektEigenschaften.Bezeichnung + " (" + AlleWerte.AlleProgrammiersprachen[projektEigenschaften.Programmiersprache].Anzeige + ")",
                    VerticalAlignment = VerticalAlignment.Top,
                    Tag = projektEigenschaften
                };

                switch (tabEigenschaften.Steuerungen)
                {
                    case SpsStarter.Steuerungen.Logo:
                    case SpsStarter.Steuerungen.TwinCat:
                    case SpsStarter.Steuerungen.TiaPortal:
                        rdo.Checked += RadioButton_Checked;
                        break;
                }

                tabEigenschaften.StackPanelBezeichnung.Children.Add(rdo);
            }
        }


        public void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

            if (sender is RadioButton rb && rb.Tag is ProjektEigenschaften projektEigenschaften)
            {
                AktuellesProjekt = projektEigenschaften;
                var parentDirectory = new DirectoryInfo(AktuellesProjekt.QuellOrdner);
                var dateiName = $@"{parentDirectory.FullName}\index.html";

                var htmlSeite = File.Exists(dateiName) ? File.ReadAllText(dateiName) : "--??--";

                var dataHtmlSeite = Encoding.UTF8.GetBytes(htmlSeite);
                var stmHtmlSeite = new MemoryStream(dataHtmlSeite, 0, dataHtmlSeite.Length);

                AktuellesProjekt.BrowserBezeichnung.NavigateToStream(stmHtmlSeite);
            }
        }
    }
}
