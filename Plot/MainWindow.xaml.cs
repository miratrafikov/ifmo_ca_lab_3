using System.Collections.Generic;
using System.Windows;

using OxyPlot;
using OxyPlot.Series;

namespace ShiftCo.ifmo_ca_lab_3.Plot
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(List<(int, int)> points)
        {
            InitializeComponent();
            Model = SetUpModel(points);
            DataContext = this;
        }

        PlotModel SetUpModel(List<(int, int)> points)
        {
            var model = new PlotModel { Title = "Function plot" };
            var series = new LineSeries();

            foreach (var point in points) 
            {
                series.Points.Add(new DataPoint(point.Item1, point.Item2));
            }
            
            model.Series.Add(series);
            return model;
        }

        public PlotModel Model { get; private set; }
    }
}
