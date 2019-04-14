namespace imp
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.algorithm = new System.Windows.Forms.CheckedListBox();
            this.arrival_text = new System.Windows.Forms.TextBox();
            this.burst_text = new System.Windows.Forms.TextBox();
            this.priority_text = new System.Windows.Forms.TextBox();
            this.quantum_text = new System.Windows.Forms.TextBox();
            this.pre_non = new System.Windows.Forms.CheckedListBox();
            this.average_output = new System.Windows.Forms.Label();
            this.number_process_text = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(814, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // algorithm
            // 
            this.algorithm.FormattingEnabled = true;
            this.algorithm.Items.AddRange(new object[] {
            "FCFS",
            "SJF",
            "Priority",
            "RR"});
            this.algorithm.Location = new System.Drawing.Point(101, 45);
            this.algorithm.Name = "algorithm";
            this.algorithm.Size = new System.Drawing.Size(120, 89);
            this.algorithm.TabIndex = 1;
            this.algorithm.SelectedIndexChanged += new System.EventHandler(this.algorithm_SelectedIndexChanged);
            // 
            // arrival_text
            // 
            this.arrival_text.Location = new System.Drawing.Point(296, 164);
            this.arrival_text.Name = "arrival_text";
            this.arrival_text.Size = new System.Drawing.Size(100, 22);
            this.arrival_text.TabIndex = 2;
            // 
            // burst_text
            // 
            this.burst_text.Location = new System.Drawing.Point(468, 22);
            this.burst_text.Name = "burst_text";
            this.burst_text.Size = new System.Drawing.Size(100, 22);
            this.burst_text.TabIndex = 3;
            // 
            // priority_text
            // 
            this.priority_text.Location = new System.Drawing.Point(468, 154);
            this.priority_text.Name = "priority_text";
            this.priority_text.Size = new System.Drawing.Size(100, 22);
            this.priority_text.TabIndex = 4;
            // 
            // quantum_text
            // 
            this.quantum_text.Location = new System.Drawing.Point(814, 22);
            this.quantum_text.Name = "quantum_text";
            this.quantum_text.Size = new System.Drawing.Size(100, 22);
            this.quantum_text.TabIndex = 5;
            // 
            // pre_non
            // 
            this.pre_non.FormattingEnabled = true;
            this.pre_non.Items.AddRange(new object[] {
            "NON",
            "PREE"});
            this.pre_non.Location = new System.Drawing.Point(805, 98);
            this.pre_non.Name = "pre_non";
            this.pre_non.Size = new System.Drawing.Size(120, 55);
            this.pre_non.TabIndex = 6;
            this.pre_non.SelectedIndexChanged += new System.EventHandler(this.pre_non_SelectedIndexChanged);
            // 
            // average_output
            // 
            this.average_output.AutoSize = true;
            this.average_output.Location = new System.Drawing.Point(396, 415);
            this.average_output.Name = "average_output";
            this.average_output.Size = new System.Drawing.Size(0, 17);
            this.average_output.TabIndex = 7;
            // 
            // number_process_text
            // 
            this.number_process_text.Location = new System.Drawing.Point(306, 61);
            this.number_process_text.Name = "number_process_text";
            this.number_process_text.Size = new System.Drawing.Size(100, 22);
            this.number_process_text.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1678, 465);
            this.Controls.Add(this.number_process_text);
            this.Controls.Add(this.average_output);
            this.Controls.Add(this.pre_non);
            this.Controls.Add(this.quantum_text);
            this.Controls.Add(this.priority_text);
            this.Controls.Add(this.burst_text);
            this.Controls.Add(this.arrival_text);
            this.Controls.Add(this.algorithm);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckedListBox algorithm;
        private System.Windows.Forms.TextBox arrival_text;
        private System.Windows.Forms.TextBox burst_text;
        private System.Windows.Forms.TextBox priority_text;
        private System.Windows.Forms.TextBox quantum_text;
        private System.Windows.Forms.CheckedListBox pre_non;
        private System.Windows.Forms.Label average_output;
        private System.Windows.Forms.TextBox number_process_text;
    }
}

