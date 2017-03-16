using System;
using System.Windows;
using System.Windows.Controls;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Button _newPersonButton;
        public static Person ActivPerson { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            DatenBereitstellen();
            cbVeranstaltungen.ItemsSource = Bibliothek.Veranstaltungen_Alle();
           cbVeranstaltungen.SelectedIndex = 0;

        }
        #region Demodaten
        private void DatenBereitstellen()
        {
            demoDatenVeranstaltungen();
            demoDatenTische();
            demoDatenPersonen();
        }
        private void demoDatenVeranstaltungen()
        {
            Veranstaltung veranstaltung1 = new Veranstaltung();
            veranstaltung1.Name = "Hochzeit";
            veranstaltung1.Datum = new DateTime(2017, 2, 20);
            Bibliothek.Veranstaltung_Neu(veranstaltung1);
        }
        private void demoDatenTische()
        {
            Tisch tisch1 = new Tisch();
            tisch1.MaximaleAnzahlPersonen = 4;
            tisch1.Veranstaltung = Bibliothek.Veranstaltung_nach_ID(1);
            Bibliothek.Tisch_neu(tisch1);
            Tisch tisch2 = new Tisch();
            tisch2.MaximaleAnzahlPersonen = 4;
            tisch2.Veranstaltung = Bibliothek.Veranstaltung_nach_ID(1);
            Bibliothek.Tisch_neu(tisch2);
        }
        private void demoDatenPersonen()
        {
            Person person1 = new Person();
            person1.Anrede = "Herr";
            person1.Firma = "gibb";
            person1.Geburtsdatum = new DateTime(1950, 4, 12);
            person1.Name = "Hauerter Paul";
            person1.Tisch = Bibliothek.Tisch_nach_ID(1);
            Bibliothek.Person_neu(person1);

            Person person2 = new Person();
            person2.Anrede = "Frau";
            person2.Firma = "gibb";
            person2.Geburtsdatum = new DateTime(1945, 12, 3);
            person2.Name = "Hauerter Paula";
            person2.Tisch = Bibliothek.Tisch_nach_ID(1);
            Bibliothek.Person_neu(person2);

            Person person3 = new Person();
            person3.Anrede = "Herr";
            person3.Firma = "gibb";
            person3.Geburtsdatum = new DateTime(1977, 7, 27);
            person3.Name = "Sauber Karl";
            person3.Tisch = Bibliothek.Tisch_nach_ID(1);
            Bibliothek.Person_neu(person3);

            Person person4 = new Person();
            person4.Anrede = "Herr";
            person4.Firma = "tsbe";
            person4.Geburtsdatum = new DateTime(1964, 8, 1);
            person4.Name = "Cuenet Fred";
            person4.Tisch = Bibliothek.Tisch_nach_ID(2);
            Bibliothek.Person_neu(person4);

            Person person5 = new Person
            {
                Anrede = "Herr",
                Firma = "Test Inc.",
                Name = "Fritz Klaus",
                Geburtsdatum = new DateTime(1966, 6, 2),
                Tisch = Bibliothek.Tisch_nach_ID(2)
            };
            Bibliothek.Person_neu(person5);

            Person person6 = new Person
            {
                Anrede = "Herr",
                Firma = "Test Inc.",
                Name = "Hans-Peter Kneubuehl",
                Geburtsdatum = new DateTime(1952, 8, 12),
                Tisch = Bibliothek.Tisch_nach_ID(2)
            };
            Bibliothek.Person_neu(person6);

            Person person7 = new Person
            {
                Anrede = "Frau",
                Firma = "Test Inc.",
                Name = "Ursula Klaus",
                Geburtsdatum = new DateTime(1968, 9, 8),
                Tisch = Bibliothek.Tisch_nach_ID(2)
            };
            Bibliothek.Person_neu(person7);

            Person person8 = new Person
            {
                Anrede = "Frau",
                Firma = "Test Inc.",
                Name = "Petra Kneubuehl",
                Geburtsdatum = new DateTime(1955, 7, 10),
                Tisch = Bibliothek.Tisch_nach_ID(2)
            };
            Bibliothek.Person_neu(person8);

        }
        #endregion

        private void btnAllPersons_Click(object sender, RoutedEventArgs e)
        {
            ShowAllPersons();
        }

        private void btnNewPerson_Click(object sender, RoutedEventArgs e)
        {
            Content.Children.Clear();
            Content.Children.Add(CreatePersonView());
        }
        private void btnTableView_Click(object sender, RoutedEventArgs e)
        {
            ShowTableView();
        }

        private void ShowUpdatePerson(object sender, EventArgs e)
        {
            var p = (Person)((PersonListView)sender).DataGrid.SelectedItem;
            ShowPersonView(p);
        }

        private void SwitchToAllPersons(object sender, EventArgs e)
        {
            ShowAllPersons();
        }

        private void ShowUpdatePersonFromTableView(object sender, EventArgs e)
        {
            var p = ((TischView) sender).ClickedPerson;
            ShowPersonView(p);
        }

        private void ShowPersonView(Person p)
        {
            ActivPerson = p;
            Content.Children.Clear();
            Content.Children.Add(CreatePersonView(p));
            RemoveNewPersonButton();
        }

        private void ShowAllPersons()
        {
            Content.Children.Clear();
            Content.Children.Add(CreatePersonListView());
            if (_newPersonButton == null)
                _newPersonButton = CreateNewPersonButton();
            RemoveNewPersonButton();
            NavigationGrid.Children.Add(_newPersonButton);
        }

        private void RemoveNewPersonButton()
        {
            NavigationGrid.Children.Remove(_newPersonButton);
        }

        private void ShowTableView()
        {
            Content.Children.Clear();
            Content.Children.Add(CreateTischOrdnungsView());
            RemoveNewPersonButton();
        }

        private PersonView CreatePersonView(Person p = null)
        {
            var view = new PersonView
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            view.PersonViewEnded += SwitchToAllPersons;
            view.lblUpdate.Content = "false";

            if (p == null) return view;

            view.txtAnrede.Text = p.Anrede;
            view.txtFirma.Text = p.Firma;
            view.txtName.Text = p.Name;
            view.dpGeburtstag.SelectedDate = p.Geburtsdatum;
            view.lblUpdate.Content = "true";
            view.cbVeranstalltung.SelectedItem = p.Tisch.Veranstaltung;
            view.cbTisch.SelectedItem = p.Tisch;
            view.txtChair.Text = p.Platz == 0 ? "Kein Platz" : p.Platz.ToString();

            return view;
        }
        private PersonListView CreatePersonListView()
        {
            var personList = new PersonListView
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                Name = "plvPersons",
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Margin = new Thickness(0, 0, 0, 0)

            };

            personList.UpdatePerson += ShowUpdatePerson;

            return personList;
        }

        private Button CreateNewPersonButton()
        {
            var bt = new Button
            {
                Name = "btnNewPerson",
                Content = "Neue Person",
                HorizontalAlignment = HorizontalAlignment.Left,
                Margin = new Thickness(10, 96, 0, 0),
                Width = 90,
                VerticalAlignment = VerticalAlignment.Top,
            };

            bt.Click += btnNewPerson_Click;

            return bt;
        }

        private Tischordnung CreateTischOrdnungsView()
        {
            var tischordnung = new Tischordnung(Bibliothek.Veranstaltung_nach_ID(1))
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                Margin = new Thickness(0,0,0,0)
            };

            tischordnung.ChairClicked += ShowUpdatePersonFromTableView;
            return tischordnung;
        }

    }
}
