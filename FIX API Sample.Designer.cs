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
            this.btnTestRequest = new System.Windows.Forms.Button();
            this.lblHeartbeatMessage = new System.Windows.Forms.Label();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnMarketDataRequest = new System.Windows.Forms.Button();
            this.txtMessageSend = new System.Windows.Forms.TextBox();
            this.lblMessageSend = new System.Windows.Forms.Label();
            this.lblMessageReceived = new System.Windows.Forms.Label();
            this.txtMessageReceived = new System.Windows.Forms.TextBox();
            this.btnHeartbeat = new System.Windows.Forms.Button();
            this.btnResendRequest = new System.Windows.Forms.Button();
            this.btnReject = new System.Windows.Forms.Button();
            this.btnSequenceReset = new System.Windows.Forms.Button();
            this.btnNewOrderSingle = new System.Windows.Forms.Button();
            this.btnOrderStatusRequest = new System.Windows.Forms.Button();
            this.btnRequestForPositions = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLogon
            // 
            this.btnLogon.Location = new System.Drawing.Point(12, 70);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(133, 23);
            this.btnLogon.TabIndex = 0;
            this.btnLogon.Text = "Logon";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new System.EventHandler(this.btnLogon_Click);
            // 
            // btnTestRequest
            // 
            this.btnTestRequest.Location = new System.Drawing.Point(12, 41);
            this.btnTestRequest.Name = "btnTestRequest";
            this.btnTestRequest.Size = new System.Drawing.Size(133, 23);
            this.btnTestRequest.TabIndex = 2;
            this.btnTestRequest.Text = "Send Test Request";
            this.btnTestRequest.UseVisualStyleBackColor = true;
            this.btnTestRequest.Click += new System.EventHandler(this.btnTestRequest_Click);
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
            this.btnLogout.Location = new System.Drawing.Point(12, 99);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(133, 23);
            this.btnLogout.TabIndex = 4;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // btnMarketDataRequest
            // 
            this.btnMarketDataRequest.Location = new System.Drawing.Point(174, 12);
            this.btnMarketDataRequest.Name = "btnMarketDataRequest";
            this.btnMarketDataRequest.Size = new System.Drawing.Size(133, 23);
            this.btnMarketDataRequest.TabIndex = 5;
            this.btnMarketDataRequest.Text = "Market Data Request";
            this.btnMarketDataRequest.UseVisualStyleBackColor = true;
            this.btnMarketDataRequest.Click += new System.EventHandler(this.btnMarketDataRequest_Click);
            // 
            // txtMessageSend
            // 
            this.txtMessageSend.Location = new System.Drawing.Point(131, 284);
            this.txtMessageSend.Name = "txtMessageSend";
            this.txtMessageSend.Size = new System.Drawing.Size(711, 20);
            this.txtMessageSend.TabIndex = 6;
            // 
            // lblMessageSend
            // 
            this.lblMessageSend.AutoSize = true;
            this.lblMessageSend.Location = new System.Drawing.Point(4, 287);
            this.lblMessageSend.Name = "lblMessageSend";
            this.lblMessageSend.Size = new System.Drawing.Size(100, 13);
            this.lblMessageSend.TabIndex = 7;
            this.lblMessageSend.Text = "FIX Message Send:";
            // 
            // lblMessageReceived
            // 
            this.lblMessageReceived.AutoSize = true;
            this.lblMessageReceived.Location = new System.Drawing.Point(4, 314);
            this.lblMessageReceived.Name = "lblMessageReceived";
            this.lblMessageReceived.Size = new System.Drawing.Size(121, 13);
            this.lblMessageReceived.TabIndex = 9;
            this.lblMessageReceived.Text = "FIX Message Received:";
            // 
            // txtMessageReceived
            // 
            this.txtMessageReceived.Location = new System.Drawing.Point(131, 311);
            this.txtMessageReceived.Name = "txtMessageReceived";
            this.txtMessageReceived.Size = new System.Drawing.Size(711, 20);
            this.txtMessageReceived.TabIndex = 8;
            // 
            // btnHeartbeat
            // 
            this.btnHeartbeat.Location = new System.Drawing.Point(12, 12);
            this.btnHeartbeat.Name = "btnHeartbeat";
            this.btnHeartbeat.Size = new System.Drawing.Size(133, 23);
            this.btnHeartbeat.TabIndex = 10;
            this.btnHeartbeat.Text = "Send Heartbeat";
            this.btnHeartbeat.UseVisualStyleBackColor = true;
            this.btnHeartbeat.Click += new System.EventHandler(this.btnHeartbeat_Click);
            // 
            // btnResendRequest
            // 
            this.btnResendRequest.Location = new System.Drawing.Point(12, 128);
            this.btnResendRequest.Name = "btnResendRequest";
            this.btnResendRequest.Size = new System.Drawing.Size(133, 23);
            this.btnResendRequest.TabIndex = 11;
            this.btnResendRequest.Text = "Resend Request";
            this.btnResendRequest.UseVisualStyleBackColor = true;
            this.btnResendRequest.Click += new System.EventHandler(this.btnResendRequest_Click);
            // 
            // btnReject
            // 
            this.btnReject.Location = new System.Drawing.Point(12, 157);
            this.btnReject.Name = "btnReject";
            this.btnReject.Size = new System.Drawing.Size(133, 23);
            this.btnReject.TabIndex = 12;
            this.btnReject.Text = "Reject";
            this.btnReject.UseVisualStyleBackColor = true;
            this.btnReject.Click += new System.EventHandler(this.btnReject_Click);
            // 
            // btnSequenceReset
            // 
            this.btnSequenceReset.Location = new System.Drawing.Point(12, 186);
            this.btnSequenceReset.Name = "btnSequenceReset";
            this.btnSequenceReset.Size = new System.Drawing.Size(133, 23);
            this.btnSequenceReset.TabIndex = 13;
            this.btnSequenceReset.Text = "Sequence Reset";
            this.btnSequenceReset.UseVisualStyleBackColor = true;
            this.btnSequenceReset.Click += new System.EventHandler(this.btnSequenceReset_Click);
            // 
            // btnNewOrderSingle
            // 
            this.btnNewOrderSingle.Location = new System.Drawing.Point(174, 41);
            this.btnNewOrderSingle.Name = "btnNewOrderSingle";
            this.btnNewOrderSingle.Size = new System.Drawing.Size(133, 23);
            this.btnNewOrderSingle.TabIndex = 14;
            this.btnNewOrderSingle.Text = "New Order Single";
            this.btnNewOrderSingle.UseVisualStyleBackColor = true;
            this.btnNewOrderSingle.Click += new System.EventHandler(this.btnNewOrderSingle_Click);
            // 
            // btnOrderStatusRequest
            // 
            this.btnOrderStatusRequest.Location = new System.Drawing.Point(174, 70);
            this.btnOrderStatusRequest.Name = "btnOrderStatusRequest";
            this.btnOrderStatusRequest.Size = new System.Drawing.Size(133, 23);
            this.btnOrderStatusRequest.TabIndex = 15;
            this.btnOrderStatusRequest.Text = "Order Status Request";
            this.btnOrderStatusRequest.UseVisualStyleBackColor = true;
            this.btnOrderStatusRequest.Click += new System.EventHandler(this.btnOrderStatusRequest_Click);
            // 
            // btnRequestForPositions
            // 
            this.btnRequestForPositions.Location = new System.Drawing.Point(174, 99);
            this.btnRequestForPositions.Name = "btnRequestForPositions";
            this.btnRequestForPositions.Size = new System.Drawing.Size(133, 23);
            this.btnRequestForPositions.TabIndex = 16;
            this.btnRequestForPositions.Text = "Request for Positions";
            this.btnRequestForPositions.UseVisualStyleBackColor = true;
            this.btnRequestForPositions.Click += new System.EventHandler(this.btnRequestForPositions_Click);
            // 
            // frmFIXAPISample
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 358);
            this.Controls.Add(this.btnRequestForPositions);
            this.Controls.Add(this.btnOrderStatusRequest);
            this.Controls.Add(this.btnNewOrderSingle);
            this.Controls.Add(this.btnSequenceReset);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnResendRequest);
            this.Controls.Add(this.btnHeartbeat);
            this.Controls.Add(this.lblMessageReceived);
            this.Controls.Add(this.txtMessageReceived);
            this.Controls.Add(this.lblMessageSend);
            this.Controls.Add(this.txtMessageSend);
            this.Controls.Add(this.btnMarketDataRequest);
            this.Controls.Add(this.btnLogout);
            this.Controls.Add(this.lblHeartbeatMessage);
            this.Controls.Add(this.btnTestRequest);
            this.Controls.Add(this.btnLogon);
            this.Name = "frmFIXAPISample";
            this.ShowIcon = false;
            this.Text = "Fix API Sample";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnLogon;
        private System.Windows.Forms.Button btnTestRequest;
        private System.Windows.Forms.Label lblHeartbeatMessage;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnMarketDataRequest;
        private System.Windows.Forms.TextBox txtMessageSend;
        private System.Windows.Forms.Label lblMessageSend;
        private System.Windows.Forms.Label lblMessageReceived;
        private System.Windows.Forms.TextBox txtMessageReceived;
        private System.Windows.Forms.Button btnHeartbeat;
        private System.Windows.Forms.Button btnResendRequest;
        private System.Windows.Forms.Button btnReject;
        private System.Windows.Forms.Button btnSequenceReset;
        private System.Windows.Forms.Button btnNewOrderSingle;
        private System.Windows.Forms.Button btnOrderStatusRequest;
        private System.Windows.Forms.Button btnRequestForPositions;
    }
}

