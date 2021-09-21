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
        public Date GetDate(DateTime d)
        {
            foreach(Date date in dates)
            {
                if(date.date.ToShortDateString() == d.ToShortDateString())
                {
                    return date;
                }
            }
            return null;
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
        public List<int> price { get; set; }

        public Date(DateTime date) 
        {
            this.date = date;
            time = new List<DateTime>();
            name = new List<string>();
            age = new List<int>();
            phoneNumber = new List<string>();
            procedure = new List<string>();
            price = new List<int>();
        }
        public void AddAppointment(DateTime time, string name, int age, string number, string procedure, int price)
        {
            this.time.Add(time);
            this.name.Add(name);
            this.age.Add(age);
            this.phoneNumber.Add(number);
            this.procedure.Add(procedure);
            this.price.Add(price);
        }
        public int Count()
        {
            int temp = 0;
            if (time.Count > temp)
                temp = time.Count;
            else if (name.Count > temp)
                temp = name.Count;
            else if (age.Count > temp)
                temp = age.Count;
            else if (phoneNumber.Count > temp)
                temp = phoneNumber.Count;
            else if (procedure.Count > temp)
                temp = procedure.Count;
            else if (price.Count > temp)
                temp = price.Count;
            return temp;
        }
        public void AddCell(int x, int y, string data)
        {
            if (Count() <= y)
            {
                AddEmptyRow();
            }
            switch (x)
            {
                case 0:
                    time[y] = DateTime.Parse(data);
                    break;
                case 1:
                    name[y] = data;
                    break;
                case 2:
                    age[y] = int.Parse(data);
                    break;
                case 3:
                    phoneNumber[y] = data;
                    break;
                case 4:
                    procedure[y] = data;
                    break;
                case 5:
                    price[y] = int.Parse(data);
                    break;
            }
        }
        private void AddEmptyRow()
        {
            time.Add(DateTime.Now);
            name.Add("");
            age.Add(0);
            phoneNumber.Add("");
            procedure.Add("");
            price.Add(0);
        }
    }
}
