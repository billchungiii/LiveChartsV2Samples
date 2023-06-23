using BindingLibrary;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace LiveChartsV2Sample001
{
    public class MainViewModel : NotifyPropertyBase
    {
        private ObservableCollection<ISeries> _series;

        public ObservableCollection<ISeries> Series
        {
            get => _series;
            set => SetProperty(ref _series, value);
        }

        public MainViewModel()
        {
            // LiveChartsCore.SkiaSharpView.LineSeries

            var items = Enumerable.Repeat(Enumerable.Range(0, 50).OrderBy(d => Guid.NewGuid()), 2).SelectMany(x => x).ToArray();

            Series = new ObservableCollection<ISeries>()
            {
                new LineSeries<int>
                {
                    Values = items,
                    Fill = null
                },

            };

        }
    }
}
