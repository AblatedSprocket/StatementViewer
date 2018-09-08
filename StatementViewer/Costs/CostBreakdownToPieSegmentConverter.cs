using CustomPresentationControls.Charts;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Data;

namespace StatementViewer.Costs
{
    public class CostBreakdownToPieSegmentConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CostBreakdown costBreakdown)
            {
                ObservableCollection<PieSegment> data = new ObservableCollection<PieSegment>();
                if (costBreakdown.Mortgage > 0)
                {
                    data.Add(new PieSegment
                    {
                        Name = "Mortgage",
                        Value = (double)costBreakdown.Mortgage,
                        Color = ChartColors.Bergundy
                    });
                }
                if (costBreakdown.Loans > 0)
                {
                    data.Add(new PieSegment
                    {
                        Name = "Loans",
                        Value = (double)costBreakdown.Loans,
                        Color = ChartColors.Orange
                    });
                }
                //if (costBreakdown.Payments > 0)
                //{
                //    data.Add(new PieSegment
                //    {
                //        Name = "Payments",
                //        Value = (double)costBreakdown.Payments,
                //        Color = ChartColors.Periwinkle
                //    });
                //}

                if (costBreakdown.Utilities > 0)
                {
                    data.Add(new PieSegment
                    {
                        Name = "Utilities",
                        Value = (double)costBreakdown.Utilities,
                        Color = ChartColors.Yellow
                    });
                }
                if (costBreakdown.Grocery > 0)
                {
                    data.Add(
                    new PieSegment
                    {
                        Name = "Grocery",
                        Value = (double)costBreakdown.Grocery,
                        Color = ChartColors.Spring
                    });
                }
                if (costBreakdown.Home > 0)
                {
                    data.Add(
                new PieSegment
                {
                    Name = "Home",
                    Value = (double)costBreakdown.Home,
                    Color = ChartColors.Pink
                });
                }
                if (costBreakdown.Auto > 0)
                {
                    data.Add(
                        new PieSegment
                        {
                            Name = "Auto",
                            Value = (double)costBreakdown.Auto,
                            Color = ChartColors.Blue
                        });
                }
                if (costBreakdown.Work > 0)
                {
                    data.Add(
                        new PieSegment
                        {
                            Name = "Work",
                            Value = (double)costBreakdown.Work,
                            Color = ChartColors.Green
                        });
                }
                if (costBreakdown.Dining > 0)
                {
                    data.Add(
                        new PieSegment
                        {
                            Name = "Dining",
                            Value = (double)costBreakdown.Dining,
                            Color = ChartColors.Peach
                        });
                }
                if (costBreakdown.Travel > 0)
                {
                    data.Add(
                        new PieSegment
                        {
                            Name = "Travel",
                            Value = (double)costBreakdown.Travel,
                            Color = ChartColors.SkyBlue
                        });
                }
                if (costBreakdown.Luxury > 0)
                {
                    data.Add(
                        new PieSegment
                        {
                            Name = "Luxury",
                            Value = (double)costBreakdown.Luxury,
                            Color = ChartColors.Grape
                        });
                }
                if (costBreakdown.Misc > 0)
                {
                    data.Add(
                        new PieSegment
                        {
                            Name = "Misc",
                            Value = (double)costBreakdown.Misc,
                            Color = ChartColors.Red
                        });
                }
                return data;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
