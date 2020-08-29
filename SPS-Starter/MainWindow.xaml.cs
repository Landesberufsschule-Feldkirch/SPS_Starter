using System.Windows;
using System.Windows.Controls;

namespace SPS_Starter
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            var viewModel = new ViewModel.ViewModel(this);

            InitializeComponent();
            DataContext = viewModel;

            AnzeigeUpdatenLogo();
            AnzeigeUpdatenTiaPortal();
            AnzeigeUpdatenTwinCat();
        }

        internal void ProjektStarten(object obj)
        {
            //
        }


        public void TabControlSelectionChanged(object obj)
        {
            //
        }


        private void CheckboxChecked(object sender, RoutedEventArgs e)
        {
            var cb = sender as CheckBox;

            if (cb != null)
                switch (cb.Name)
                {
                    case "CheckboxLogoFup":
                    case "CheckboxLogoKop":
                        AnzeigeUpdatenLogo();
                        break;

                    case "CheckboxTiaPortalFup":
                    case "CheckboxTiaPortalKop": 
                    case "CheckboxTiaPortalScl":
                        AnzeigeUpdatenTiaPortal();
                        break;


                    case "CheckboxTwinCatAs":
                    case "CheckboxTwinCatAwl":
                    case "CheckboxTwinCatCfc":
                    case "CheckboxTwinCatFup":
                    case "CheckboxTwinCatKop":
                    case "CheckboxTwinCatSt":
                        AnzeigeUpdatenTwinCat();
                        break;
                }
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl tabControl && tabControl.SelectedValue is TabItem item)
            {
                switch (item.Header.ToString())
                {
                    case "Logo8":
                        AnzeigeUpdatenLogo();
                        break;

                    case "TiaPortal":
                        AnzeigeUpdatenTiaPortal();
                        break;

                    case "TwinCAT":
                        AnzeigeUpdatenTwinCat();
                        break;
                }
            }
        }

        private void AnzeigeUpdatenTwinCat()
        {
            var als = true;
            var awl = true;
            var cfc = true;
            var fup = true;
            var kop = true;
            var st = true;

            if (CheckboxTwinCatAs != null) als = (bool)CheckboxTwinCatAs.IsChecked;
            if (CheckboxTwinCatAwl != null) awl = (bool)CheckboxTwinCatAwl.IsChecked;
            if (CheckboxTwinCatCfc != null) cfc = (bool)CheckboxTwinCatCfc.IsChecked;
            if (CheckboxTwinCatFup != null) fup = (bool)CheckboxTwinCatFup.IsChecked;
            if (CheckboxTwinCatKop != null) kop = (bool)CheckboxTwinCatKop.IsChecked;
            if (CheckboxTwinCatSt != null) st = (bool)CheckboxTwinCatSt.IsChecked;
        }

        private void AnzeigeUpdatenTiaPortal()
        {
            var fup = true;
            var kop = true;
            var scl = true;

            if (CheckboxTiaPortalFup != null) fup = (bool)CheckboxTiaPortalFup.IsChecked;
            if (CheckboxTiaPortalKop != null) kop = (bool)CheckboxTiaPortalKop.IsChecked;
            if (CheckboxTiaPortalScl != null) scl = (bool)CheckboxTiaPortalScl.IsChecked;
        }

        private void AnzeigeUpdatenLogo()
        {
            var fup = true;
            var kop = true;

            if (CheckboxLogoFup != null) fup = (bool)CheckboxLogoFup.IsChecked;
            if (CheckboxLogoKop != null) kop = (bool)CheckboxLogoKop.IsChecked;
        }
    }
}
