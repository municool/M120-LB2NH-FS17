using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für TischView.xaml
    /// </summary>
    /// 
    /// erstellt am 11.3.2017, Aufwand 150 Min, erstellen des Grundgerüsts + Zeichnen der Stühle (Uhrzeiger)
    public partial class TischView
    {
        public Tisch TischObject { get; }
        public EventHandler ClickOnChair; 
        public Person ClickedPerson { get; private set; }
        
        /// Konstruktor
        public TischView(Tisch t)
        {
            TischObject = t;
            InitializeComponent();
            DrawChairs();
        }

        /// Zeichnet die Stühle mithilfe eines Rotators im 
        /// entspechend kalkuliertem winkel um den Tisch herum
        private void DrawChairs()
        {
            var angle = 360/TischObject.MaximaleAnzahlPersonen;

            var line = new Line
            {
                X1 = 0,
                Y1 = 0,
                Y2 = -100,
                X2 = 0
            };

            var rotatetrans = new RotateTransform(0);
            

            for (int i = 0; i < TischObject.MaximaleAnzahlPersonen; i++)
            {
                line.RenderTransform = rotatetrans;
                var chairPos = rotatetrans.Transform(new Point(line.X2, line.Y2));
                var personName = GetPersonName(i + 1);
                personName = string.IsNullOrEmpty(personName) ? "Nicht besetzt" : personName;
                var chair = new Ellipse
                {
                    Height = 20,
                    Width = 20,
                    Margin = new Thickness(chairPos.X, chairPos.Y, 0, 0),
                    Fill = new SolidColorBrush(Colors.Brown),
                    ToolTip = i+1 + ", " + personName
                };

                chair.MouseUp += Chair_MouseUp;

                main.Children.Add(chair);

                rotatetrans.Angle += angle;
            }

        }


        private void Chair_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var tooltip = ((Ellipse) sender).ToolTip.ToString();
            int chairId;
            var chairIdString = Regex.Match(tooltip, @"\d+").ToString();
            int.TryParse(chairIdString, out chairId);

            var p = GetPersonOnChair(chairId);

            if (p == null) return;

            ClickedPerson = p;
            ClickOnChair?.Invoke(this, e);
        }

        private string GetPersonName(int platz)
        {
           return (from p in TischObject.Personen where p.Platz == platz select p.Name).FirstOrDefault();
        }

        private Person GetPersonOnChair(int chair)
        {
            return (from p in TischObject.Personen where p.Platz == chair select p).FirstOrDefault();
        }
    }
}
