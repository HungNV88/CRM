namespace TamoCRM.Window.TestService
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.txtXml = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnMissCall = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnUpdateIncommingTCL = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtXml
            // 
            this.txtXml.Location = new System.Drawing.Point(24, 13);
            this.txtXml.Multiline = true;
            this.txtXml.Name = "txtXml";
            this.txtXml.Size = new System.Drawing.Size(523, 283);
            this.txtXml.TabIndex = 0;
            this.txtXml.Text = resources.GetString("txtXml.Text");
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(554, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Incoming";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnMissCall
            // 
            this.btnMissCall.Location = new System.Drawing.Point(554, 43);
            this.btnMissCall.Name = "btnMissCall";
            this.btnMissCall.Size = new System.Drawing.Size(75, 23);
            this.btnMissCall.TabIndex = 2;
            this.btnMissCall.Text = "Miss call";
            this.btnMissCall.UseVisualStyleBackColor = true;
            this.btnMissCall.Click += new System.EventHandler(this.btnMissCall_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(554, 72);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 51);
            this.button1.TabIndex = 3;
            this.button1.Text = "Update Connect Status";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnUpdateIncommingTCL
            // 
            this.btnUpdateIncommingTCL.Location = new System.Drawing.Point(554, 129);
            this.btnUpdateIncommingTCL.Name = "btnUpdateIncommingTCL";
            this.btnUpdateIncommingTCL.Size = new System.Drawing.Size(75, 51);
            this.btnUpdateIncommingTCL.TabIndex = 4;
            this.btnUpdateIncommingTCL.Text = "Update Incomming TCL";
            this.btnUpdateIncommingTCL.UseVisualStyleBackColor = true;
            this.btnUpdateIncommingTCL.Click += new System.EventHandler(this.btnUpdateIncommingTCL_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 308);
            this.Controls.Add(this.btnUpdateIncommingTCL);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnMissCall);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtXml);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtXml;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnMissCall;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnUpdateIncommingTCL;
    }
}

