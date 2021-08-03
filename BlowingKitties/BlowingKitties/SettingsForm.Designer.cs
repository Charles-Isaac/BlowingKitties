
namespace BlowingKitties
{
    partial class SettingsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudCatPartsCount = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nudExplosionDelay = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudCatPartsCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExplosionDelay)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Cat Parts Count:";
            // 
            // nudCatPartsCount
            // 
            this.nudCatPartsCount.Location = new System.Drawing.Point(12, 31);
            this.nudCatPartsCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudCatPartsCount.Name = "nudCatPartsCount";
            this.nudCatPartsCount.Size = new System.Drawing.Size(120, 23);
            this.nudCatPartsCount.TabIndex = 2;
            this.nudCatPartsCount.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(12, 112);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 3;
            this.btnOk.Text = "OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(93, 112);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // nudExplosionDelay
            // 
            this.nudExplosionDelay.Location = new System.Drawing.Point(12, 83);
            this.nudExplosionDelay.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nudExplosionDelay.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudExplosionDelay.Name = "nudExplosionDelay";
            this.nudExplosionDelay.Size = new System.Drawing.Size(120, 23);
            this.nudExplosionDelay.TabIndex = 6;
            this.nudExplosionDelay.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudExplosionDelay.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Explosion Delay (ms):";
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 151);
            this.Controls.Add(this.nudExplosionDelay);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.nudCatPartsCount);
            this.Controls.Add(this.label1);
            this.Name = "SettingsForm";
            this.Text = "SettingsForm";
            ((System.ComponentModel.ISupportInitialize)(this.nudCatPartsCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudExplosionDelay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudCatPartsCount;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudExplosionDelay;
        private System.Windows.Forms.Label label2;
    }
}