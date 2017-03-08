using System;
using System.Windows;
using System.Windows.Controls;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für PersonView.xaml
    /// </summary>
    public partial class PersonView
    {
        public event EventHandler PersonViewEnded;

        public PersonView()
        {
            InitializeComponent();
            cbVeranstalltung.ItemsSource = Bibliothek.Veranstaltungen_Alle();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var anrede = txtAnrede.Text;
            var nameofPerson = txtName.Text;
            var firma = txtFirma.Text;
            var geburtsdatum = dpGeburtstag.SelectedDate;
            var tisch = (Tisch)cbTisch.SelectionBoxItem;
            var place = txtChair.Text;
            var isUpdate = lblUpdate.Content.Equals("true");

            var person = new Person();

            if (isUpdate)
                person = Bibliothek.Person_nach_ID(MainWindow.ActivPerson.ID);

            if (anrede != "" && firma != "" && nameofPerson != "" && tisch != null && geburtsdatum != null)
            {
                person.Anrede = anrede;
                person.Firma = firma;
                person.Name = nameofPerson;
                person.Geburtsdatum = geburtsdatum.Value;
                person.Tisch = tisch;
            }
            else
            {
                ShowError("Nicht alle Felder wurden korrekt ausgefühlt!");
                return;
            }

            int placeIndex;
            if (int.TryParse(place, out placeIndex) && placeIndex <= tisch.MaximaleAnzahlPersonen)
                person.Platz = placeIndex;
            else
            {
                ShowError("Bitte geben Sie ein Zahl ein die kleiner oder gleich wie die Maximalzahl ist!");
                return;
            }

            if (isUpdate)
                Bibliothek.UpdatePerson(person);
            else
                Bibliothek.Person_neu(person);

            PersonViewEnded?.Invoke(this, e);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            PersonViewEnded?.Invoke(this, e);
        }

        private void cbVeranstalltung_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbTisch.ItemsSource = ((Veranstaltung)((ComboBox)sender).SelectedItem).Tische;

        }

        private void cbTisch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var table = (Tisch)((ComboBox)sender).SelectedItem;
            lblMaxChairs.Content = table.MaximaleAnzahlPersonen.ToString();
        }

        private void ShowError(string message)
        {
            lblErrorBox.Background = System.Windows.Media.Brushes.Red;
            lblErrorBox.Content = message;
            lblErrorBox.Visibility = Visibility.Visible;
        }
    }
}
