using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfRangesControlsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker;

        public MainWindow()
        {
            InitializeComponent();
            calendar.BlackoutDates.Add(new CalendarDateRange
                (
                new DateTime (2024,5,1),
                new DateTime (2024,5,4)
                ));

            calendar.BlackoutDates.Add(new CalendarDateRange
                (
                new DateTime(2024, 5, 9),
                new DateTime(2024, 5, 9)
                ));
            datePicker.BlackoutDates.Add(new CalendarDateRange
                (
                new DateTime(2024, 5, 1),
                new DateTime(2024, 5, 4)
                ));

            worker = new BackgroundWorker();
            worker.DoWork += DoWork;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += Worker_ProgressChanged;

        }

        private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            progress.Value = e.ProgressPercentage;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string str = "";
            foreach(var d in calendar.SelectedDates)
                str += d.ToShortDateString() + "\n";
            MessageBox.Show(str);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sender is Slider slider)
                slider.SelectionEnd = e.NewValue;
        }

        private void btnProgress_Click(object sender, RoutedEventArgs e)
        {
            worker.RunWorkerAsync();
                
        }
        void DoWork(object? sender, DoWorkEventArgs e)
        {
            for (var i = progress.Minimum; i <= progress.Maximum; i++)
            {
                Thread.Sleep(100);
                if (sender is BackgroundWorker worker)
                    worker.ReportProgress((int)i);
            }
        }

        
    }
}