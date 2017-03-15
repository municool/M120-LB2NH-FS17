using System;
using System.Collections.Generic;
using System.Linq;

namespace M120_LB2NH_FS17
{
    static class Bibliothek
    {
        private static List<Veranstaltung> Veranstaltungen { get; set; }
        private static List<Tisch> Tische { get; set; }
        private static List<Person> Personen { get; set; }
        private static Int32 _idVeranstaltungen = 1;
        private static Int32 _idTische = 1;
        private static Int32 _idPersonen = 1;

        #region Veranstaltung
        public static Int32 Veranstaltung_Neu(Veranstaltung v)
        {
            if (Veranstaltungen == null)
            {
                Veranstaltungen = new List<Veranstaltung>();
            }
            if (v.ID == 0)
            {
                v.ID = _idVeranstaltungen;
                _idVeranstaltungen++;
            }
            Veranstaltungen.Add(v);
            return v.ID;
        }
        public static List<Veranstaltung> Veranstaltungen_Alle()
        {
            return Veranstaltungen;
        }
        public static Veranstaltung Veranstaltung_nach_ID(Int32 id)
        {
            return (from element in Veranstaltungen where element.ID == id select element).FirstOrDefault();
        }
        #endregion
        #region Tisch
        public static Int32 Tisch_neu(Tisch t)
        {
            if (Tische == null)
            {
                Tische = new List<Tisch>();
            }
            if (t.ID == 0)
            {
                t.ID = _idTische;
                _idTische++;
            }

            t.Veranstaltung?.Tische.Add(t);

            Tische.Add(t);
            return t.ID;
        }
        public static List<Tisch> Tische_Alle()
        {
            return Tische;
        }
        public static Tisch Tisch_nach_ID(Int32 id)
        {
            return (from element in Tische where element.ID == id select element).FirstOrDefault();
        }

        public static Person PersonOnChair(Tisch t, int chair)
        {
            return null;
        }
        #endregion
        #region Person
        public static Int32 Person_neu(Person p)
        {
            if (Personen == null)
            {
                Personen = new List<Person>();
            }
            if (p.ID == 0)
            {
                p.ID = _idPersonen;
                _idPersonen++;
            }

            p.Tisch?.Personen.Add(p);

            Personen.Add(p);
            return p.ID;
        }
        public static List<Person> Person_Alle()
        {
            return Personen;
        }
        public static Person Person_nach_ID(Int32 id)
        {
            return (from element in Personen where element.ID == id select element).FirstOrDefault();
        }

        public static void UpdatePerson(Person p)
        {
            var index = Personen.IndexOf(p);

            if (index == 0) return;

            var curPerson = Personen[index];
            if (curPerson.Tisch != p.Tisch)
            {
                p.Tisch.Personen.Add(p);
                curPerson.Tisch.Personen.Remove(curPerson);
            }

            Personen[index] = p;
        }

        #endregion
    }
}
