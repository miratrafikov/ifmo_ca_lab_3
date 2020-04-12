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

        public MainWindow(List<(decimal, decimal)> points)
        {
            InitializeComponent();
            Model = SetUpModel(points);
            DataContext = this;
        }

        PlotModel SetUpModel(List<(decimal, decimal)> points)
        {
            var model = new PlotModel { Title = "Plot" };
            var series = new LineSeries();

            foreach (var point in points) 
            {
                series.Points.Add(new DataPoint((double)point.Item1, (double)point.Item2));
            }
            
            model.Series.Add(series);
            return model;
        }

        public PlotModel Model { get; private set; }
    }
}
