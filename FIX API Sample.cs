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
    
        private int _pricePort = 5201;
        private int _tradePort = 5202;
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
        TcpClient _priceClient;
        NetworkStream _priceStream;
        TcpClient _tradeClient;
        NetworkStream _tradeStream;
        public frmFIXAPISample()
        {
            InitializeComponent();
            _priceClient = new TcpClient(_host, _pricePort);
            _priceStream = _priceClient.GetStream();
            _tradeClient = new TcpClient(_host, _tradePort);
            _tradeStream = _priceClient.GetStream();
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.LogonMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber, 30, false);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);           
        }
        private string SendPriceMessage(string message)
        {            
            return SendMessage(message, _priceStream);
        }
        private string SendTradeMessage(string message)
        {           
            return SendMessage(message, _tradeStream);
        }
        private string SendMessage(string message, NetworkStream stream)
        {
            var byteArray = Encoding.ASCII.GetBytes(message);
            stream.Write(byteArray, 0, byteArray.Length);
            var buffer = new byte[1024];
            stream.Read(buffer, 0, 1024);
            _messageSequenceNumber++;
            var returnMessage = Encoding.ASCII.GetString(buffer);
            return returnMessage;
        }

        private void btnTestRequest_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.TestRequestMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,_testRequestID);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.LogoutMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber);
            txtMessageSend.Text = message;         
            txtMessageReceived.Text = SendPriceMessage(message);
            _messageSequenceNumber = 1;
        }

        private void btnMarketDataRequest_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.MarketDataRequestMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber, "EURUSD:WDqsoT", 1,0,0,1,1);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnHeartbeat_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.HeartbeatMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnResendRequest_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.ResendMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.RejectMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,0);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnSequenceReset_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.SequenceResetMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,0);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnNewOrderSingle_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.NewOrderSingleMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber,"0",1,Timestamp(),1000,1,"1");
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnOrderStatusRequest_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.OrderStatusRequest(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber,"1");
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private string Timestamp()
        {
            return (DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds.ToString();
        }

        private void btnRequestForPositions_Click(object sender, EventArgs e)
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
            var messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
            var message = messageConstructor.RequestForPositions(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber,"1");
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }
    }
}
