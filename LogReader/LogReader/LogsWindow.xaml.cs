using LogReader.ViewModels;
using System.Windows;

namespace LogReader
{
    public partial class LogsWindow : Window
    {
        public LogsWindow(LogViewModel logViewModel)
        {
            InitializeComponent();
            DataContext = logViewModel;
        }
    }
}
