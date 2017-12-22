namespace Tutorial3_AcquiringAnImage
{
	partial class UserForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.numericUpDown_NumberImagesToAverage = new System.Windows.Forms.NumericUpDown();
            this.lbl_NumberImagesToAverage = new System.Windows.Forms.Label();
            this.textBox_Filename = new System.Windows.Forms.TextBox();
            this.lbl_Filename = new System.Windows.Forms.Label();
            this.btn_Start = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumberImagesToAverage)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDown_NumberImagesToAverage
            // 
            this.numericUpDown_NumberImagesToAverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown_NumberImagesToAverage.Location = new System.Drawing.Point(182, 36);
            this.numericUpDown_NumberImagesToAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_NumberImagesToAverage.Name = "numericUpDown_NumberImagesToAverage";
            this.numericUpDown_NumberImagesToAverage.Size = new System.Drawing.Size(90, 20);
            this.numericUpDown_NumberImagesToAverage.TabIndex = 0;
            this.numericUpDown_NumberImagesToAverage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.numericUpDown_NumberImagesToAverage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_NumberImagesToAverage.ValueChanged += new System.EventHandler(this.numericUpDown_NumberImagesToAverage_ValueChanged);
            // 
            // lbl_NumberImagesToAverage
            // 
            this.lbl_NumberImagesToAverage.AutoSize = true;
            this.lbl_NumberImagesToAverage.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_NumberImagesToAverage.Location = new System.Drawing.Point(12, 38);
            this.lbl_NumberImagesToAverage.Name = "lbl_NumberImagesToAverage";
            this.lbl_NumberImagesToAverage.Size = new System.Drawing.Size(149, 13);
            this.lbl_NumberImagesToAverage.TabIndex = 1;
            this.lbl_NumberImagesToAverage.Text = "Number of images to average:";
            // 
            // textBox_Filename
            // 
            this.textBox_Filename.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_Filename.Location = new System.Drawing.Point(70, 74);
            this.textBox_Filename.Name = "textBox_Filename";
            this.textBox_Filename.Size = new System.Drawing.Size(202, 20);
            this.textBox_Filename.TabIndex = 2;
            this.textBox_Filename.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // lbl_Filename
            // 
            this.lbl_Filename.AutoSize = true;
            this.lbl_Filename.Location = new System.Drawing.Point(12, 76);
            this.lbl_Filename.Name = "lbl_Filename";
            this.lbl_Filename.Size = new System.Drawing.Size(52, 13);
            this.lbl_Filename.TabIndex = 3;
            this.lbl_Filename.Text = "Filename:";
            // 
            // btn_Start
            // 
            this.btn_Start.Location = new System.Drawing.Point(15, 125);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(257, 86);
            this.btn_Start.TabIndex = 4;
            this.btn_Start.Text = "Capture and Save";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.lbl_Filename);
            this.Controls.Add(this.textBox_Filename);
            this.Controls.Add(this.lbl_NumberImagesToAverage);
            this.Controls.Add(this.numericUpDown_NumberImagesToAverage);
            this.Name = "UserForm";
            this.Text = "Tutorial 3- Acquiring an image";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserForm_FormClosing);
            this.Load += new System.EventHandler(this.UserForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_NumberImagesToAverage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.NumericUpDown numericUpDown_NumberImagesToAverage;
        private System.Windows.Forms.Label lbl_NumberImagesToAverage;
        private System.Windows.Forms.TextBox textBox_Filename;
        private System.Windows.Forms.Label lbl_Filename;
        private System.Windows.Forms.Button btn_Start;
	}
}

