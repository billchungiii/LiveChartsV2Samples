using BindingLibrary;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Diagnostics;

namespace LiveChartsV2Sample003
{
    public class ChartCommonViewModel : NotifyPropertyBase
    {
        private ObservableCollection<ISeries> _series;
        private ObservableCollection<Axis> _xAxes;
        private ObservableCollection<Axis> _yAxes;
        public ObservableCollection<ISeries> Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        public ObservableCollection<Axis> XAxes
        {
            get => _xAxes;
            set => SetProperty(ref _xAxes, value);
        }

        public ObservableCollection<Axis> YAxes
        {
            get => _yAxes;
            set => SetProperty(ref _yAxes, value);
        }
        private SolidColorPaint _toolTipTextPaint;
        public SolidColorPaint ToolTipTextPaint
        {
            get => _toolTipTextPaint;
            set => SetProperty(ref _toolTipTextPaint, value);
        }
        public ChartCommonViewModel()
        {
            Series = new ObservableCollection<ISeries>();
            XAxes = new ObservableCollection<Axis>();
            YAxes = new ObservableCollection<Axis>();
        }
    }
    public class PersonViewModel : NotifyPropertyBase
    {
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private int _score;
        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value);
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

        public MainViewModel()
        {
            InitialData();           
            Chart = new ChartCommonViewModel();
            AddSeries();
            AddXAxes();
            AddYAxes();
            CreateTextPaint();
        }
        private void InitialData()
        {
            People = new ObservableCollection<PersonViewModel>()
            {
                new PersonViewModel { Name = "魯夫" , Score = 98},
                new PersonViewModel { Name = "索隆" , Score = 79},
                new PersonViewModel { Name = "香吉士" , Score = 58},
                new PersonViewModel { Name = "娜美" , Score = 82},
                new PersonViewModel { Name = "羅賓" , Score = 100},
                new PersonViewModel { Name = "喬巴" , Score = 43},
            };
        }
        private void CreateTextPaint()
        {
            Chart.ToolTipTextPaint = new SolidColorPaint
            {
                Color = SKColors.Black,
                SKTypeface = SKFontManager.Default.MatchFamily("新細明體"),
            };
        }

        private void AddSeries()
        {
            Chart.Series.Add(
                  new ColumnSeries<PersonViewModel>
                  {
                      Mapping = (person, index) => new Coordinate(index, person.Score),
                      Values = People,
                  });
        }

        private void AddXAxes()
        {
            Chart.XAxes.Add(
                new Axis
                {
                    Labels = new ObservableCollection<string>(People.Select(x => x.Name)),
                    LabelsPaint = new SolidColorPaint
                    {
                        Color = SKColors.Black,
                        SKTypeface = SKFontManager.Default.MatchFamily("新細明體"),
                    },
                });
        }

        private void AddYAxes()
        {
            Chart.YAxes.Add(
                 new Axis
                 {
                     Labeler = value => $"{value} 分",
                     LabelsPaint = new SolidColorPaint
                     {
                         Color = SKColors.Black,
                         SKTypeface = SKFontManager.Default.MatchFamily("新細明體"),
                     },
                 });
        }         

        private ChartCommonViewModel _chart;
        public ChartCommonViewModel Chart
        {
            get => _chart;
            set => SetProperty(ref _chart, value);
        }
    }

}
