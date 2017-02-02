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

        private string _host = "80.86.83.6";
        private string _username = "3006156";
        private string _password = "";
        private string _senderCompID = "";
        private string _senderSubID = "";


        private string _targetCompID = "CSERVER";
        private int _messageSequenceNumber = 1;
        private int _testRequestID = 1;
        TcpClient _priceClient;
        NetworkStream _priceStream;
        TcpClient _tradeClient;
        NetworkStream _tradeStream;
        MessageConstructor _messageConstructor;
        public frmFIXAPISample()
        {
            InitializeComponent();
            _priceClient = new TcpClient(_host, _pricePort);           
            _priceStream = _priceClient.GetStream();            
            _tradeClient = new TcpClient(_host, _tradePort);
            _tradeStream = _priceClient.GetStream();
            _messageConstructor = new MessageConstructor(_host, _username, _password, _senderCompID, _senderSubID, _targetCompID);
        }

        private void ClearText()
        {
            txtMessageSend.Text = "";
            txtMessageReceived.Text = "";
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
      
        private void btnLogon_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.LogonMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber, 30, false);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }       

        private void btnTestRequest_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.TestRequestMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,_testRequestID);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.LogoutMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber);
            txtMessageSend.Text = message;         
            txtMessageReceived.Text = SendPriceMessage(message);
            _messageSequenceNumber = 1;
        }

        private void btnMarketDataRequest_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.MarketDataRequestMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber, "EURUSD:WDqsoT", 1,0,0,1,1);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnHeartbeat_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.HeartbeatMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnResendRequest_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.ResendMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,_messageSequenceNumber - 1);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            ClearText();
             var message = _messageConstructor.RejectMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,0);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnSequenceReset_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.SequenceResetMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber,0);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnNewOrderSingle_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.NewOrderSingleMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber,"10",1,1, DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff"), 1000,1, "3");
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnOrderStatusRequest_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.OrderStatusRequest(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber,"10");
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
            ClearText();
            var message = _messageConstructor.RequestForPositions(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber, "876316401");
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnLogonT_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.LogonMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber, 30, false);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnHeartbeatT_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.HeartbeatMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnTestRequestT_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.TestRequestMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber, _testRequestID);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnLogoutT_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.LogoutMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
            _messageSequenceNumber = 1;
        }

        private void btnResendRequestT_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.ResendMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber, _messageSequenceNumber - 1);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnSpotMarketData_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.MarketDataRequestMessage(MessageConstructor.SessionQualifier.QUOTE, _messageSequenceNumber, "EURUSD:WDqsoT", 1, 1, 0, 1, 1);
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendPriceMessage(message);
        }

        private void btnStopOrder_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.NewOrderSingleMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber, "10", 1, 1, DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff"), 1000, 3, "3",0, (decimal)1.08);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }

        private void btnLimitOrder_Click(object sender, EventArgs e)
        {
            ClearText();
            var message = _messageConstructor.NewOrderSingleMessage(MessageConstructor.SessionQualifier.TRADE, _messageSequenceNumber, "10", 1, 1, DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff"), 1000,2, "3", (decimal)1.08);
            _testRequestID++;
            txtMessageSend.Text = message;
            txtMessageReceived.Text = SendTradeMessage(message);
        }
    }
}
