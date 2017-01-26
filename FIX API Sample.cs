using FIX_API_Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FIX_API_Sample
{
    public partial class frmFIXAPISample : Form
    {
    
        private int _port = 5201;

        //roboforex
        private string _host = "217.79.189.190";
        private string _username = "4064539";
        private string _password = "m3e1d500D";
        private string _senderCompID = "roboforex.4064539";
        private string _senderSubID = "4064539";
        // Test Server
        //private string _host = "proxy-qa.dev.cs.spotwa.re";
        //private string _username = "1012";
        //private string _password = "4";
        //private string _senderCompID = "local.1012";
        //private string _senderSubID = "1012";


        private string _targetCompID = "CSERVER";
        private int _messageSequenceNumber = 1;
        private int _testRequestID = 1;
        TcpClient _client;
        NetworkStream _stream;
        public frmFIXAPISample()
        {
            InitializeComponent();
            _client = new TcpClient(_host, _port);
            _stream = _client.GetStream();
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.LogonMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,0,true);            
            lblLogonResponce.Text = SendMessage(message);
        }

        private string SendMessage(string message)
        {
            var byteArray = Encoding.ASCII.GetBytes(message);
            var client = new TcpClient(_host, _port);
            var stream = client.GetStream();           
            stream.Write(byteArray, 0, byteArray.Length);
            Thread.Sleep(100); 
            var buffer = new byte[1024];
            if(stream.DataAvailable)
                stream.Read(buffer, 0, 1024);
            _messageSequenceNumber++;
            var returnMessage = Encoding.ASCII.GetString(buffer);
            return returnMessage;
        }

        private void btnHeartbeat_Click(object sender, EventArgs e)
        {
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.TestRequestMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,_testRequestID);
            _testRequestID++;
           lblHeartbeatMessage.Text = SendMessage(message);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.LogoutMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber);           
            lblLogonResponce.Text = SendMessage(message);
        }

        private void btnMarketDataRequest_Click(object sender, EventArgs e)
        {
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.MarketDataRequestMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber, "EURUSD:WDqsoT", 1,0,0,1,1);
            lblLogonResponce.Text = SendMessage(message);
        }
    }
}
