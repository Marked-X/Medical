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
        public List<Doctor> Doctors { get; set; }
        public DateTime monthList { get; set; }

        public Form1()
        {
            InitializeComponent();

            if (!File.Exists("Data.json"))
            {
                Doctors = new List<Doctor>();
                string temp = JsonSerializer.Serialize(Doctors);
                File.WriteAllText("Data.json", temp);
            }

            FillList();

            listView1.ItemActivate += ListView1_ItemActivate;

            dateTimePicker1.Value = DateTime.Today;
            monthList = DateTime.Today;
            label4.Text = monthList.ToString("MMMM");

            dataGridView1.Columns.Add("Date", "Дата");
            dataGridView1.Columns.Add("Doctors", "Доктора");
            dataGridView1.Columns[0].Width = 40;
            dataGridView1.Columns[0].Resizable = DataGridViewTriState.False;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            RefreshMonthList();

            foreach (Doctor doc in Doctors)
            {
                if (doc.GetDate(dateTimePicker1.Value) != null)
                {
                    listView3.Items.Add(doc.name);
                }
            }
        }

        private void ListView1_ItemActivate(object sender, EventArgs e)
        {
            int temp = listView1.SelectedItems[0].Index;
            foreach (TabPage tab in tabControl1.TabPages)
                if (Doctors[temp].name == tab.Text)
                {
                    tabControl1.SelectedTab = tab;
                    return;
                }
            NewTab(Doctors[temp].name);
        }

        private void NewTab(string temp)
        {
            TabPage tab = new TabPage();
            tab.Text = temp;
            foreach(Doctor doc in Doctors)
            {
                if (doc.name == temp)
                {
                    tab.Controls.Add(new DoctorInfo(doc, Doctors, tabControl1));
                    tabControl1.TabPages.Add(tab);
                    tabControl1.SelectedTab = tabControl1.TabPages[tabControl1.TabCount - 1];
                }
            }
        }

        private void FillList()
        {
            listView1.Items.Clear();
            string temp = File.ReadAllText("Data.json");

            Doctors = JsonSerializer.Deserialize<List<Doctor>>(temp);

            foreach (Doctor doc in Doctors)
                listView1.Items.Add(doc.name);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                label3.Visible = true;
            }
            else
            {
                if (label3.Visible)
                    label3.Visible = false;
                Doctors.Add(new Doctor(textBox1.Text));

                string temp = JsonSerializer.Serialize(Doctors);
                File.WriteAllText("Data.json", temp);

                FillList(); //Cringe? shoud've just added name to list?
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                label6.Text = "Поле имени пусто";
                label6.Visible = true;
            } 
            else
            {
                label6.Visible = false;
                for (int i = 0; i < Doctors.Count; i++)
                {
                    if (Doctors[i].name == textBox2.Text)
                    {
                        for (int k = 0; k < listView3.Items.Count; k++)
                            if (listView3.Items[k].Text == textBox2.Text)
                                listView3.Items.RemoveAt(k);

                        Doctors.RemoveAt(i);

                        label6.Visible = false;

                        string temp = JsonSerializer.Serialize(Doctors);
                        File.WriteAllText("Data.json", temp);



                        FillList();

                        return;
                    }
                }
                label6.Text = "Доктор не найден";
                label6.Visible = true;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            listView3.Items.Clear();
            foreach(Doctor doc in Doctors)
            {
                if (doc.GetDate(dateTimePicker1.Value) != null)
                {
                    listView3.Items.Add(doc.name);
                }
            }
        }

        private void listView3_ItemActivate(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
                if (listView3.SelectedItems[0].Text == tab.Text)
                {
                    tabControl1.SelectedTab = tab;
                    return;
                }
            NewTab(listView3.SelectedItems[0].Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            monthList = monthList.AddMonths(1);
            RefreshMonthList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            monthList = monthList.AddMonths(-1);
            RefreshMonthList();
        }

        private void RefreshMonthList()
        {
            label4.Text = monthList.ToString("MMMM");
            dataGridView1.Rows.Clear();
            bool first = true;
            for (int i = 0; i < DateTime.DaysInMonth(monthList.Year, monthList.Month); i++) {
                dataGridView1.Rows.Add();
                first = true;
                dataGridView1[0, i].Value = i + 1;
                foreach (Doctor doc in Doctors)
                {
                    if (doc.GetDate(new DateTime(monthList.Year, monthList.Month, i + 1)) != null)
                    {
                        if (first)
                        {
                            dataGridView1[1, i].Value += doc.name;
                            first = false;
                        }
                        else
                            dataGridView1[1, i].Value += ", " + doc.name;
                    }
                }
            }
        }
    }
}