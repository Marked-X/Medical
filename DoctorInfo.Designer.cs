
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


        public DoctorInfo(Doctor doc)
        {
            InitializeComponent();

            Label label = new Label();
            label.Text = doc.name;
            label.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            DataGridView data = new DataGridView();
            data.Columns.Add("Time", "Время приёма");
            data.Columns.Add("FIO", "ФИО");
            data.Columns.Add("Age", "Возраст");
            data.Columns.Add("Number", "Номер телефона");
            data.Columns.Add("Procedure", "Процедура");
            data.Columns.Add("Price", "Стоимость");

            data.Rows.Add(doc.dates[0].time[0].ToShortTimeString(), doc.dates[0].name[0], doc.dates[0].age[0], doc.dates[0].phoneNumber[0], doc.dates[0].procedure[0], doc.dates[0].price[0]);

            data.Location = new System.Drawing.Point(0, 40);
            data.AutoSize = true;

            this.Controls.Add(label);
            this.Controls.Add(data);
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
            this.Name = "DoctorInfo";
            this.Size = new System.Drawing.Size(1103, 602);
            this.ResumeLayout(false);
        }

        #endregion
    }
}
