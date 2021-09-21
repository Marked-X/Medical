
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows.Forms;

namespace Medical
{
    partial class DoctorInfo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        Label label = new Label();
        DateTimePicker calendar = new DateTimePicker();
        DataGridView data = new DataGridView();
        Button backButton = new Button();
        List<Doctor> Doctors;
        Doctor doc;
        TabControl tabControl;

        public DoctorInfo(Doctor doc, List<Doctor> doctors, TabControl tabControl)
        {
            InitializeComponent();
            this.doc = doc;
            Doctors = doctors;
            this.tabControl = tabControl;

            label.Text = doc.name;
            label.Location = new System.Drawing.Point(100, 10);

            calendar.Location = new System.Drawing.Point(200, 10);
            calendar.Value = System.DateTime.Today;

            backButton.Location = new System.Drawing.Point(600, 10);
            backButton.MouseClick += BackButton_MouseClick;
            backButton.Text = "Закрыть вкладку";

            data.Columns.Add("Time", "Время приёма");
            data.Columns.Add("FIO", "ФИО");
            data.Columns.Add("Age", "Возраст");
            data.Columns.Add("Number", "Номер телефона");
            data.Columns.Add("Procedure", "Процедура");
            data.Columns.Add("Price", "Стоимость");

            Date temp = doc.GetDate(System.DateTime.Today);
            if (temp != null)
            {
                for (int i = 0; i < temp.Count(); i++)
                {
                    data.Rows.Add(temp.time[i].ToShortTimeString(), temp.name[i], temp.age[i], temp.phoneNumber[i], temp.procedure[i], temp.price[i]);
                }
            }

            calendar.ValueChanged += Calendar_ValueChanged;
            
            data.AutoResizeColumns();
            data.CellEndEdit += Data_CellEndEdit;
            data.SortCompare += Data_SortCompare;
            data.Location = new System.Drawing.Point(0, 40);
            data.AutoSize = true;

            this.Controls.Add(backButton);
            this.Controls.Add(label);
            this.Controls.Add(calendar);
            this.Controls.Add(data);
        }

        private void Data_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            if (e.Column.Index == 2 || e.Column.Index == 3 || e.Column.Index == 5)
            {
                e.SortResult = int.Parse(e.CellValue1.ToString()).CompareTo(int.Parse(e.CellValue2.ToString()));
                e.Handled = true;
            }
        }

        private void Data_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(doc.GetDate(calendar.Value) == null)
                doc.AddDate(calendar.Value);

            if (e.ColumnIndex == 0)
            {
                DateTime time;
                if(!DateTime.TryParse(data[e.ColumnIndex, e.RowIndex].Value.ToString(), out time))
                    data[e.ColumnIndex, e.RowIndex].Value = DateTime.Now;
            }

            if (e.ColumnIndex == 2 || e.ColumnIndex == 5)
            {
                int i;
                if (!int.TryParse(data[e.ColumnIndex, e.RowIndex].Value.ToString(), out i))
                    data[e.ColumnIndex, e.RowIndex].Value = 0;
            }

            doc.GetDate(calendar.Value).AddCell(e.ColumnIndex, e.RowIndex, data[e.ColumnIndex, e.RowIndex].Value.ToString());
            
            string temp = JsonSerializer.Serialize(Doctors);
            File.WriteAllText("Data.json", temp);
        }

        private void BackButton_MouseClick(object sender, MouseEventArgs e)
        {
            this.Dispose();
            tabControl.SelectedTab.Dispose(); 
        }

        private void Calendar_ValueChanged(object sender, System.EventArgs e)
        {
            data.Rows.Clear();

            Date temp = doc.GetDate(calendar.Value);
            if (temp != null)
            {
                for (int i = 0; i < temp.time.Count; i++)
                {
                    data.Rows.Add(temp.time[i].ToShortTimeString(), temp.name[i], temp.age[i], temp.phoneNumber[i], temp.procedure[i], temp.price[i]);
                }
            }
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // DoctorInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Name = "DoctorInfo";
            this.Size = new System.Drawing.Size(1103, 602);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
