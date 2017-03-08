using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M120_LB2NH_FS17
{
    public class Veranstaltung
    {
        public Int32 ID { get; set; }
        public String Name { get; set; }
        public DateTime Datum { get; set; }
        public List<Tisch> Tische { get; set; }
        public Veranstaltung()
        {
            Tische = new List<Tisch>();
        }

        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
