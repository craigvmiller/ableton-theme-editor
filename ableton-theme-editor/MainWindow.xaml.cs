using ableton_theme_editor.core;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ableton_theme_editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private ObservableCollection<ViewModel> _data;

        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<ViewModel> Data
        {
            get { return _data; }
            set { _data = value; OnPropertyChanged("Data"); }
        }

        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();

            FileParser fp = new FileParser();
            var props = fp.Open("C:\\Users\\craig\\Downloads\\04Custom.ask");
            Data = new ObservableCollection<ViewModel>();
            foreach (var prop in props.Where(x => x.Colour != null && x.Colour.StartsWith("#")))
            {
                Data.Add(new ViewModel
                {
                    Name = prop.Name,
                    Brush = (SolidColorBrush)new BrushConverter().ConvertFrom(prop.Colour),
                });
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class ViewModel : INotifyPropertyChanged
    {
        private string _name;
        private SolidColorBrush _brush;

        public string Name {
            get { return _name; }
            set { _name = value; OnPropertyRaised("Name"); }
        }
        public SolidColorBrush Brush
        {
            get { return _brush; }
            set { _brush = value; OnPropertyRaised("Brush"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyRaised(string propertyname)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
            }
        }
    }
}
