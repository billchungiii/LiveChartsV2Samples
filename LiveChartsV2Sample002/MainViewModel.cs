using BindingLibrary;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChartsV2Sample002
{
    public class MainViewModel : NotifyPropertyBase
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

        public MainViewModel()
        {

            Series = new ObservableCollection<ISeries>
            {
                new ColumnSeries<int> {Values = new ObservableCollection<int> { 100, 150, 210, 350, 220, 180 } },
            };

            XAxes = new ObservableCollection<Axis>
            {
                new Axis
                {
                    Labels =new ObservableCollection<string> { "魯夫", "索隆" , "香吉士", "娜美", "羅賓", "喬巴" },
                    LabelsPaint = new SolidColorPaint
                    {
                        Color = SKColors.Black ,
                        SKTypeface = SKFontManager.Default.MatchCharacter('繁'),
                        // SKTypeface = SKFontManager.Default.MatchFamily("新細明體"),
                    }
                }
            };

            YAxes = new ObservableCollection<Axis>
            {
                new Axis{ Labeler = (x) => $"{x:C}" },
            };
        }
    }
}
