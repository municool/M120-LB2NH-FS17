using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für TischView.xaml
    /// </summary>
    public partial class TischView : UserControl
    {
        private int _distFromTable = 20;
        public TischView()
        {
            InitializeComponent();
            GenerateChairs(2);
        }

        private void GenerateChairs(int amount)
        {
            var centerPoint = tisch.RenderTransformOrigin;
            var counter = amount;
            for (int i = 0; i < amount; i++)
            {
                var pos = GetChairPos(counter, i + 1);

                var chair = new Ellipse
                {
                    Width = 10,
                    Height = 10
                };
                //var location = chair.PointToScreen(new Point(0,0));

                counter--;
            }

        }

        private Point GetChairPos(int amount, int amountused)
        {
            return new Point((tisch.Height / 2) / amount, (tisch.Width / 2) / amountused);
        }
    }
}
