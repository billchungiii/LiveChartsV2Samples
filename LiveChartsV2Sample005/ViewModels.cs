using BindingLibrary;
using LiveChartsCore;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace LiveChartsV2Sample005
{
    public class PersonViewModel : NotifyPropertyBase, IChartEntity
    {
        public PersonViewModel()
        {
            MetaData = new ChartEntityMetaData(OnCoordinateChanged);
            MetaData.EntityIndex = -1;
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _score;
        private Coordinate coordinate;

        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
        }
        public ChartEntityMetaData MetaData
        { get; set; }
        public Coordinate Coordinate
        {
            get => coordinate;
            set => SetProperty(ref coordinate, value);
        }


        protected void OnCoordinateChanged(int index)
        {
            Coordinate = new(index, Score);
        }
    }

    public class MainViewModel : NotifyPropertyBase
    {
        private ObservableCollection<PersonViewModel> _people;
        public ObservableCollection<PersonViewModel> People
        {
            get => _people;
            set => SetProperty(ref _people, value);
        }

        public StackPanel panel { get; set; }
        public ICommand ClickCommand
        {
            get
            {
                return new RelayCommand(async x =>
                {
                    People = new ObservableCollection<PersonViewModel>()
                    {
                        new PersonViewModel { Name = "A", Score = 98 },
                        new PersonViewModel { Name = "B", Score = 68 },
                        new PersonViewModel { Name = "C", Score = 77 },
                    };
                    await Display(0);
                    var Points = new ColumnSeries<PersonViewModel>();
                    Points.Values = People;
                    await Display(1);
                    var Series = new ObservableCollection<ISeries>();
                    Series.Add(Points);
                    await Display(2);
                    var XAxes = new ObservableCollection<Axis>();
                    XAxes.Add(new Axis { Labels = new ObservableCollection<string>(People.Select(x => x.Name)) });
                    await Display(3);
                    var YAxes = new ObservableCollection<Axis>();
                    YAxes.Add(new Axis { Labeler = value => $"{value} S", });
                    await Display(4);
                    var chart = new CartesianChart()
                    {
                        Height = 150,
                        Series = Series,
                        XAxes = XAxes,
                        YAxes = YAxes,
                        Background = new SolidColorBrush(Colors.Yellow)
                    };
                    await Display(5);
                    panel.Children.Add(chart);
                    await Display(6);

                    async Task Display(int testIndex)
                    {
                        await Task.Delay(80);
                        Debug.WriteLine($"({testIndex}) {string.Join(",", People.Select(x => x.MetaData.EntityIndex))}");
                    }
                });
            }
        }
    }
}
