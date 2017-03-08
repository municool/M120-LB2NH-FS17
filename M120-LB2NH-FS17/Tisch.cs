using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M120_LB2NH_FS17
{
    public class Tisch
    {
        public Int32 ID { get; set; }
        public Int32 MaximaleAnzahlPersonen { get; set; }
        public Veranstaltung Veranstaltung { get; set; }
        public List<Person> Personen { get; set; }
        public Tisch()
        {
            Personen = new List<Person>();
        }
        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
