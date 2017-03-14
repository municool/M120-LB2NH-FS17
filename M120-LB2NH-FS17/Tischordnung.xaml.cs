using System;
using System.Windows;
using System.Xml.Serialization;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für Tischordnung.xaml
    /// </summary>
    public partial class Tischordnung
    {
        private Veranstaltung _currVeranstaltung;
        public EventHandler ChairClicked;
        public Tischordnung(Veranstaltung v)
        {
            _currVeranstaltung = v;
            InitializeComponent();
            cbTische.ItemsSource = new[] {1, 2, 3, 4, 5, 6};
            cbPlaetze.ItemsSource = new[] {1, 2, 3, 4, 5, 6};
            cbTische.SelectedIndex = _currVeranstaltung.Tische.Count - 1;
            cbPlaetze.SelectedIndex = _currVeranstaltung.Tische[0].MaximaleAnzahlPersonen -1;
            InitTablesView();
        }

        private void InitTablesView()
        {
            main.Children.Clear();
            DrawTables();
        }

        private void DrawTables()
        {
            var top = -200;
            var left = -300;

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

        }
    }
}
