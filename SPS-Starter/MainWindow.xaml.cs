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

                    case "Checkbox_TiaPortal_FUP":
                        break;
                    case "Checkbox_TiaPortal_KOP":
                        break;
                    case "Checkbox_TiaPortal_SCL":
                        break;


                    case "Checkbox_TwinCAT_AS":
                        break;
                    case "Checkbox_TwinCAT_AWL":
                        break;
                    case "Checkbox_TwinCAT_CFC":
                        break;
                    case "Checkbox_TwinCAT_FUP":
                        break;
                    case "Checkbox_TwinCAT_KOP":
                        break;
                    case "Checkbox_TwinCAT_ST":
                        break;
                }
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is TabControl tabControl)
            {
                if (tabControl.SelectedValue is TabItem item)
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
        }

        private void AnzeigeUpdatenTwinCat()
        {
            //
        }

        private void AnzeigeUpdatenTiaPortal()
        {
            //
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
