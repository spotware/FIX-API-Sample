namespace FIX_API_Sample
{
    partial class frmFIXAPISample
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
            this.btnLogon = new System.Windows.Forms.Button();
            this.lblLogonResponce = new System.Windows.Forms.Label();
            this.btnHeartbeat = new System.Windows.Forms.Button();
            this.lblHeartbeatMessage = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMarketDataRequest = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogon
            // 
            this.btnLogon.Location = new System.Drawing.Point(12, 4);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(133, 23);
            this.btnLogon.TabIndex = 0;
            this.btnLogon.Text = "Logon";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // lblLogonResponce
            // 
            this.lblLogonResponce.AutoSize = true;
            this.lblLogonResponce.Location = new System.Drawing.Point(159, 33);
            this.lblLogonResponce.Name = "lblLogonResponce";
            this.lblLogonResponce.Size = new System.Drawing.Size(0, 13);
            this.lblLogonResponce.TabIndex = 1;
            // 
            // btnHeartbeat
            // 
            this.btnHeartbeat.Location = new System.Drawing.Point(12, 33);
            this.btnHeartbeat.Name = "btnHeartbeat";
            this.btnHeartbeat.Size = new System.Drawing.Size(133, 23);
            this.btnHeartbeat.TabIndex = 2;
            this.btnHeartbeat.Text = "Send Test Request";
            this.btnHeartbeat.UseVisualStyleBackColor = true;
            this.btnHeartbeat.Click += new System.EventHandler(this.btnHeartbeat_Click);
            // 
            // lblHeartbeatMessage
            // 
            this.lblHeartbeatMessage.AutoSize = true;
            this.lblHeartbeatMessage.Location = new System.Drawing.Point(12, 33);
            this.lblHeartbeatMessage.Name = "lblHeartbeatMessage";
            this.lblHeartbeatMessage.Size = new System.Drawing.Size(0, 13);
            this.lblHeartbeatMessage.TabIndex = 3;
            // 
            // btnLogout
            // 
            this.btnLogout.Location = new System.Drawing.Point(12, 91);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(133, 23);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMarketDataRequest
            // 
            this.btnMarketDataRequest.Location = new System.Drawing.Point(12, 62);
            this.btnMarketDataRequest.Name = "btnMarketDataRequest";
            this.btnMarketDataRequest.Size = new System.Drawing.Size(133, 23);
            this.btnMarketDataRequest.TabIndex = 5;
            this.btnMarketDataRequest.Text = "Market Data Request";
            this.btnMarketDataRequest.UseVisualStyleBackColor = true;
            this.btnMarketDataRequest.Click += new System.EventHandler(this.btnMarketDataRequest_Click);
            // 
            // frmFIXAPISample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(585, 261);
            this.Controls.Add(this.btnMarketDataRequest);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblHeartbeatMessage);
            this.Controls.Add(this.btnHeartbeat);
            this.Controls.Add(this.lblLogonResponce);
            this.Controls.Add(this.btnLogon);
            this.Name = "frmFIXAPISample";
            this.ShowIcon = false;
            this.Text = "Fix API Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.Label lblLogonResponce;
        private System.Windows.Forms.Button btnHeartbeat;
        private System.Windows.Forms.Label lblHeartbeatMessage;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnMarketDataRequest;
    }
}

