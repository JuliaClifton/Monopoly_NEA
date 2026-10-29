namespace Monopoly
{
    partial class Form3
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
            this.Close = new System.Windows.Forms.Button();
            this.Previous = new System.Windows.Forms.PictureBox();
            this.Next = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Previous)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Next)).BeginInit();
            this.SuspendLayout();
            // 
            // Close
            // 
            this.Close.BackColor = System.Drawing.Color.Red;
            this.Close.Font = new System.Drawing.Font("Arial Rounded MT Bold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Close.Location = new System.Drawing.Point(1454, 12);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(74, 37);
            this.Close.TabIndex = 0;
            this.Close.Text = "X";
            this.Close.UseVisualStyleBackColor = false;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Previous
            // 
            this.Previous.BackColor = System.Drawing.Color.Transparent;
            this.Previous.Image = global::Monopoly.Properties.Resources.Monopoly_Go_Arrow;
            this.Previous.Location = new System.Drawing.Point(275, 140);
            this.Previous.Name = "Previous";
            this.Previous.Size = new System.Drawing.Size(145, 53);
            this.Previous.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Previous.TabIndex = 1;
            this.Previous.TabStop = false;
            this.Previous.Click += new System.EventHandler(this.Previous_Click);
            // 
            // Next
            // 
            this.Next.BackColor = System.Drawing.Color.Transparent;
            this.Next.Image = global::Monopoly.Properties.Resources.Monopoly_Go_Arrow_Right;
            this.Next.Location = new System.Drawing.Point(1119, 140);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(145, 53);
            this.Next.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Next.TabIndex = 2;
            this.Next.TabStop = false;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Monopoly.Properties.Resources.Page1;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1540, 845);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Previous);
            this.Controls.Add(this.Close);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "Form3";
            this.Text = "Form3";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ((System.ComponentModel.ISupportInitialize)(this.Previous)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Next)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.PictureBox Previous;
        private System.Windows.Forms.PictureBox Next;
    }
}