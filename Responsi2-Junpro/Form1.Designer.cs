namespace Responsi2_Junpro
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            Title = new Label();
            Deskription = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            namaInput = new TextBox();
            pilihProyek = new ComboBox();
            statusKontrak = new ComboBox();
            completeInput = new TextBox();
            label7 = new Label();
            label8 = new Label();
            bugInput = new TextBox();
            label5 = new Label();
            label6 = new Label();
            label9 = new Label();
            insertButton = new Button();
            button2 = new Button();
            updateButton = new Button();
            deleteButton = new Button();
            label10 = new Label();
            dataGridView1 = new DataGridView();
            nama_developer = new DataGridViewTextBoxColumn();
            nama_proyek = new DataGridViewTextBoxColumn();
            status_kontrak = new DataGridViewTextBoxColumn();
            fitur_selesai = new DataGridViewTextBoxColumn();
            jumlah_bug = new DataGridViewTextBoxColumn();
            skor_total = new DataGridViewTextBoxColumn();
            total_gaji = new DataGridViewTextBoxColumn();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // Title
            // 
            Title.AutoSize = true;
            Title.Font = new Font("Rethink Sans ExtraBold", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Title.Location = new Point(320, 26);
            Title.Name = "Title";
            Title.Size = new Size(136, 28);
            Title.TabIndex = 0;
            Title.Text = "DTETI Tracker";
            Title.Click += label1_Click;
            // 
            // Deskription
            // 
            Deskription.AutoSize = true;
            Deskription.Font = new Font("Rethink Sans", 11.2499981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Deskription.Location = new Point(257, 51);
            Deskription.Name = "Deskription";
            Deskription.Size = new Size(270, 20);
            Deskription.TabIndex = 1;
            Deskription.Text = "Developer Team Performance Tracker";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Rethink Sans", 12.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(59, 113);
            label1.Name = "label1";
            label1.Size = new Size(153, 22);
            label1.TabIndex = 2;
            label1.Text = "DATA DEVELOPER";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Rethink Sans Medium", 11.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(59, 138);
            label2.Name = "label2";
            label2.Size = new Size(141, 20);
            label2.TabIndex = 3;
            label2.Text = "Nama Developer    :";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Rethink Sans Medium", 11.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(60, 165);
            label3.Name = "label3";
            label3.Size = new Size(140, 20);
            label3.TabIndex = 4;
            label3.Text = "Pilih Proyek             :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Rethink Sans Medium", 11.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(61, 192);
            label4.Name = "label4";
            label4.Size = new Size(139, 20);
            label4.TabIndex = 5;
            label4.Text = "Status Kontrak      :";
            // 
            // namaInput
            // 
            namaInput.Location = new Point(205, 138);
            namaInput.Name = "namaInput";
            namaInput.Size = new Size(251, 23);
            namaInput.TabIndex = 6;
            // 
            // pilihProyek
            // 
            pilihProyek.FormattingEnabled = true;
            pilihProyek.Location = new Point(206, 165);
            pilihProyek.Name = "pilihProyek";
            pilihProyek.Size = new Size(252, 23);
            pilihProyek.TabIndex = 7;
            // 
            // statusKontrak
            // 
            statusKontrak.FormattingEnabled = true;
            statusKontrak.Location = new Point(206, 192);
            statusKontrak.Name = "statusKontrak";
            statusKontrak.Size = new Size(252, 23);
            statusKontrak.TabIndex = 8;
            // 
            // completeInput
            // 
            completeInput.Location = new Point(205, 256);
            completeInput.Name = "completeInput";
            completeInput.Size = new Size(179, 23);
            completeInput.TabIndex = 13;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Rethink Sans Medium", 11.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(59, 256);
            label7.Name = "label7";
            label7.Size = new Size(141, 20);
            label7.TabIndex = 10;
            label7.Text = "Fitur Selesai            :";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Rethink Sans", 12.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(59, 231);
            label8.Name = "label8";
            label8.Size = new Size(124, 22);
            label8.TabIndex = 9;
            label8.Text = "DATA KINERJA";
            // 
            // bugInput
            // 
            bugInput.Location = new Point(205, 282);
            bugInput.Name = "bugInput";
            bugInput.Size = new Size(179, 23);
            bugInput.TabIndex = 15;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Rethink Sans Medium", 11.2499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(59, 282);
            label5.Name = "label5";
            label5.Size = new Size(141, 20);
            label5.TabIndex = 14;
            label5.Text = "Jumlah Bug            :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Rethink Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.Location = new Point(401, 256);
            label6.Name = "label6";
            label6.Size = new Size(173, 16);
            label6.TabIndex = 16;
            label6.Text = "(Jumlah fitur yang dikerjakan)";
            label6.Click += label6_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Rethink Sans", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.Location = new Point(401, 282);
            label9.Name = "label9";
            label9.Size = new Size(174, 16);
            label9.TabIndex = 17;
            label9.Text = "(Jumlah bug yang ditemukan)";
            // 
            // insertButton
            // 
            insertButton.BackColor = Color.LightGreen;
            insertButton.Location = new Point(61, 325);
            insertButton.Name = "insertButton";
            insertButton.Size = new Size(139, 41);
            insertButton.TabIndex = 18;
            insertButton.Text = "INSERT";
            insertButton.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.Location = new Point(718, 453);
            button2.Name = "button2";
            button2.Size = new Size(8, 8);
            button2.TabIndex = 19;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            // 
            // updateButton
            // 
            updateButton.BackColor = Color.Khaki;
            updateButton.Location = new Point(246, 325);
            updateButton.Name = "updateButton";
            updateButton.Size = new Size(138, 41);
            updateButton.TabIndex = 20;
            updateButton.Text = "UPDATE";
            updateButton.UseVisualStyleBackColor = false;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.LightCoral;
            deleteButton.Location = new Point(436, 325);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(138, 41);
            deleteButton.TabIndex = 21;
            deleteButton.Text = "DELETE";
            deleteButton.UseVisualStyleBackColor = false;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Rethink Sans", 12.7499981F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.Location = new Point(61, 397);
            label10.Name = "label10";
            label10.Size = new Size(177, 22);
            label10.TabIndex = 22;
            label10.Text = "DATA PERFORMA TIM";
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { nama_developer, nama_proyek, status_kontrak, fitur_selesai, jumlah_bug, skor_total, total_gaji });
            dataGridView1.Location = new Point(61, 439);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.Size = new Size(744, 150);
            dataGridView1.TabIndex = 23;
            // 
            // nama_developer
            // 
            nama_developer.HeaderText = "Nama";
            nama_developer.Name = "nama_developer";
            // 
            // nama_proyek
            // 
            nama_proyek.HeaderText = "Proyek";
            nama_proyek.Name = "nama_proyek";
            // 
            // status_kontrak
            // 
            status_kontrak.HeaderText = "Status";
            status_kontrak.Name = "status_kontrak";
            // 
            // fitur_selesai
            // 
            fitur_selesai.HeaderText = "Fitur";
            fitur_selesai.Name = "fitur_selesai";
            // 
            // jumlah_bug
            // 
            jumlah_bug.HeaderText = "Bug";
            jumlah_bug.Name = "jumlah_bug";
            // 
            // skor_total
            // 
            skor_total.HeaderText = "Skor";
            skor_total.Name = "skor_total";
            // 
            // total_gaji
            // 
            total_gaji.HeaderText = "TOTAL GAJI";
            total_gaji.Name = "total_gaji";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = (Image)resources.GetObject("pictureBox1.BackgroundImage");
            pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox1.Location = new Point(61, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(151, 77);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 24;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(870, 677);
            Controls.Add(pictureBox1);
            Controls.Add(dataGridView1);
            Controls.Add(label10);
            Controls.Add(deleteButton);
            Controls.Add(updateButton);
            Controls.Add(button2);
            Controls.Add(insertButton);
            Controls.Add(label9);
            Controls.Add(label6);
            Controls.Add(bugInput);
            Controls.Add(label5);
            Controls.Add(completeInput);
            Controls.Add(label7);
            Controls.Add(label8);
            Controls.Add(statusKontrak);
            Controls.Add(pilihProyek);
            Controls.Add(namaInput);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Deskription);
            Controls.Add(Title);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label Title;
        private Label Deskription;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox namaInput;
        private ComboBox pilihProyek;
        private ComboBox statusKontrak;
        private TextBox completeInput;
        private Label label7;
        private Label label8;
        private TextBox bugInput;
        private Label label5;
        private Label label6;
        private Label label9;
        private Button insertButton;
        private Button button2;
        private Button updateButton;
        private Button deleteButton;
        private Label label10;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn nama_developer;
        private DataGridViewTextBoxColumn nama_proyek;
        private DataGridViewTextBoxColumn status_kontrak;
        private DataGridViewTextBoxColumn fitur_selesai;
        private DataGridViewTextBoxColumn jumlah_bug;
        private DataGridViewTextBoxColumn skor_total;
        private DataGridViewTextBoxColumn total_gaji;
        private PictureBox pictureBox1;
    }
}
