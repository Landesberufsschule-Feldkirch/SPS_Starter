using System;
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
using System.Xml.Linq;

namespace SPS_Starter
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string Ordner_Logo;
        string Ordner_TiaPortal;
        string Ordner_TwinCAT;

        public MainWindow()
        {
            InitializeComponent();
            EinstellungenLesen("Einstellungen.xml");
        }

        public void ProjekteLesen()
        {


            Projekte_Logo_Lesen();
            Projekte_TiaPortal_Lesen();
            Projekte_TwinCAT_Lesen();
        }



        public void EinstellungenLesen(string fileName)
        {
            XDocument doc = XDocument.Load(fileName);

            foreach (XElement el in doc.Root.Elements())
            {
                switch (el.Name.LocalName)
                {
                    case "Logo": Ordner_Logo = el.Value.Trim(); break;
                    case "TiaPortal": Ordner_TiaPortal = el.Value.Trim(); break;
                    case "TwinCAT": Ordner_TwinCAT = el.Value.Trim(); break;
                    default: break;
                }
            }
        }



    }
}
