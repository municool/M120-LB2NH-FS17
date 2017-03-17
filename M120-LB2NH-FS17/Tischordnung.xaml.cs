using System;
using System.Windows;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für Tischordnung.xaml
    /// </summary>
    /// 
    /// erstellt am 10.3.2017, Aufwand 30 Min, ertellen des Grundgerüsts
    /// bearbeitet am 11.3.2017, Aufwand 120 Min, bearbeiten der Ansicht ertellen des Tisches
    /// bearbeitet am 17.3.2017, Aufwand 60 Min, bearbeiten des Tisches + Ausrichtung

    public partial class Tischordnung
    {
        private Veranstaltung _currVeranstaltung;
        public EventHandler ChairClicked;

        /// Konstruktor
        public Tischordnung(Veranstaltung v)
        {
            _currVeranstaltung = v;
            InitializeComponent();
            cbTische.ItemsSource = new[] { 1, 2, 3, 4, 5, 6 };
            cbPlaetze.ItemsSource = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
            cbTische.SelectedIndex = _currVeranstaltung.Tische.Count - 1;
            cbPlaetze.SelectedIndex = _currVeranstaltung.Tische[0].MaximaleAnzahlPersonen - 1;
            InitTablesView();
        }

        /// löscht alle elemente + Zeichnet die Tische neu
        private void InitTablesView()
        {
            main.Children.Clear();
            DrawTables();
        }

        /// Zeichnet die gewählten Tische neu in einer schräg 
        /// zueinanderliegenden Position
        private void DrawTables()
        {
            var top = -200;
            var left = -500;

            for (int i = 0; i < _currVeranstaltung.Tische.Count; i++)
            {
                var tischView = new TischView(_currVeranstaltung.Tische[i])
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    Margin = new Thickness(left, top, 0, 0)
                };

                tischView.ClickOnChair += ChairClickedForwarder;
                main.Children.Add(tischView);
                top = (i % 2 != 0) ? -200 : 200;
                left += 200;
            }
        }

        private void ChairClickedForwarder(object sender, EventArgs e)
        {
            ChairClicked?.Invoke(sender, e);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var anzahlTische = cbTische.SelectedIndex + 1;
            var maxAnzahlPersonen = cbPlaetze.SelectedIndex + 1;
            var currAnzahlTische = _currVeranstaltung.Tische.Count;

            if (currAnzahlTische < anzahlTische)
            {
                for (int i = 0; i < anzahlTische-currAnzahlTische; i++)
                {
                    var tisch = new Tisch()
                    {
                        ID = 0,
                        MaximaleAnzahlPersonen = maxAnzahlPersonen,
                        Veranstaltung = _currVeranstaltung
                    };
                    Bibliothek.Tisch_neu(tisch);
                }
                
            }

            foreach (var tisch in _currVeranstaltung.Tische)
            {
                tisch.MaximaleAnzahlPersonen = maxAnzahlPersonen;
            }

            InitTablesView();
        }
    }
}
