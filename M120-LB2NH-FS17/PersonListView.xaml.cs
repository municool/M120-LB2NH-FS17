using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace M120_LB2NH_FS17
{
    /// <summary>
    /// Interaktionslogik für PersonListView.xaml
    /// </summary>
    public partial class PersonListView : UserControl
    {
        private List<Person> _personList;
        public event EventHandler UpdatePerson;

        public PersonListView()
        {
            InitializeComponent();
            SetPersonList();
            RefreshListView();
        }

        private void SetPersonList()
        {
            _personList = Bibliothek.Person_Alle();
        }

        public void RefreshListView()
        {
            DataGrid.ItemsSource = _personList;
        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            UpdatePerson?.Invoke(this, e);
        }
    }
}
