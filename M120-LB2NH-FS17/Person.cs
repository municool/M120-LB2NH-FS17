using System;

namespace M120_LB2NH_FS17
{
    public class Person
    {
        public Int32 ID { get; set; }
        public String Anrede { get; set; }
        public String Name { get; set; }
        public DateTime Geburtsdatum { get; set; }
        public String Firma { get; set; }
        public Int32 Platz { get; set; }
        public Tisch Tisch { get; set; }
        public Person()
        {

        }
        public override string ToString()
        {
            return ID.ToString();
        }
    }
}
