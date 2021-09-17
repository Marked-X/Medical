using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Medical
{
    public class Doctor
    {
        public string name { get; set; }
        public List<Date> dates { get; set; }
        
        public Doctor(string name)
        {
            this.name = name;
            dates = new List<Date>();
        }
        public void AddDate(DateTime date)
        {
            this.dates.Add(new Date(date));
        }
    }

    public class Date
    {
        public DateTime date { get; set; }
        public List<DateTime> time { get; set; }
        public List<string> name { get; set; }
        public List<int> age { get; set; }
        public List<string> phoneNumber { get; set; }
        public List<string> procedure { get; set; }
        public List<float> price { get; set; }

        public Date(DateTime date) 
        {
            this.date = date;
            time = new List<DateTime>();
            name = new List<string>();
            age = new List<int>();
            phoneNumber = new List<string>();
            procedure = new List<string>();
            price = new List<float>();
        }
        public void AddAppointment(DateTime time, string name, int age, string number, string procedure, float price)
        {
            this.time.Add(time);
            this.name.Add(name);
            this.age.Add(age);
            this.phoneNumber.Add(number);
            this.procedure.Add(procedure);
            this.price.Add(price);
        }
    }
}
