using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Medical
{
    public partial class Form1 : Form
    {
        List<Doctor> doctors;
        public Form1()
        {
            doctors = new List<Doctor>();
            string temp = File.ReadAllText("Data.json");

            doctors = JsonSerializer.Deserialize<List<Doctor>>(temp);



            InitializeComponent();

            listView1.Items.Add(doctors[0].name);
            doctors[0].dates[0].
        }

        private void listView1_ItemActivate(object sender, EventArgs e)
        {
            int temp = listView1.SelectedItems[0].Index;

            panel1.Controls.Clear();
            panel1.Controls.Add(new DoctorInfo(doctors[temp]));
        }
    }
}
/*
Doctor doc = new Doctor("Olga");
doc.AddDate(DateTime.Today);
doc.dates[0].AddAppointment(DateTime.Now, "Makar", 64, "+7988456987141", "Idk", 25.99f);

doctors.Add(doc);
string jsonString = JsonSerializer.Serialize(doctors);
File.WriteAllText("Data.json", jsonString);
*/