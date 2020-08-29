using System.ComponentModel;
using System.Threading;

namespace SPS_Starter.ViewModel
{
    public class VisuAnzeigen : INotifyPropertyChanged
    {
        private readonly Model.SpsStarter _spsStarter;
        private readonly MainWindow _mainWindow;

        public VisuAnzeigen(MainWindow mw, Model.SpsStarter ka)
        {
            _mainWindow = mw;
            _spsStarter = ka;

            System.Threading.Tasks.Task.Run(VisuAnzeigenTask);
        }

        private void VisuAnzeigenTask()
        {
            while (true)
            {
                Thread.Sleep(10);
            }
            // ReSharper disable once FunctionNeverReturns
        }


        #region iNotifyPeropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;
        // ReSharper disable once UnusedMember.Local
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        #endregion iNotifyPeropertyChanged Members
    }
}