using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für PersonListView.xaml
    /// </summary>
    /// 
    /// erstellt am 4.3.2017, Aufwand 60 Min, ertellen des Grundgerüsts + der Tabelle mit inhalt
    /// 

    public partial class PersonListView : UserControl
    {
        private List<Person> _personList;
        public event EventHandler UpdatePerson;

        /// Konstruktor
        public PersonListView()
        {
            InitializeComponent();
            SetPersonList();
            RefreshListView();
        }

        /// hohlt alle Personen aus der Bibliothek
        private void SetPersonList()
        {
            _personList = Bibliothek.Person_Alle();
        }

        /// updated die Tabelle
        public void RefreshListView()
        {
            DataGrid.ItemsSource = _personList;
        }

        /// Listener für den Doppelklick auf die Tabelle (startet den eventHandler)
        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdatePerson?.Invoke(this, e);
        }
    }
}
