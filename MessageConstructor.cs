using System;
using System.Text;

namespace FIX_API_Library
{
    public class MessageConstructor
    {
        public enum SessionMessageType
        {
            Logon,
            Logout,
            Heartbeat,
            TestRequest,
            Resend,
            Reject,
            SequenceReset
        }

        public enum ApplicationMessageType
        {
            MarketDataRequest,
            MarketDataIncrementalRefresh,
            NewOrderSingle,
            OrderStatusRequest,
            ExecutionReport,
            BusinessMessageReject,
            RequestForPosition,
            PositionReport
        }

        public enum SessionQualifier
        {
            QUOTE,
            TRADE
        }

        private string _host;
        private string _username;
        private string _password;
        private string _senderCompID;
        private string _senderSubID;
        private string _targetCompID;

        public MessageConstructor(string host, string username, string password, string senderCompID, string senderSubID, string targetCompID)
        {
            _host = host;
            _username = username;
            _password = password;
            _senderCompID = senderCompID;
            _senderSubID = senderSubID;
            _targetCompID = targetCompID;
        }

        /// <summary>
        /// Logons the message.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="heartBeatSeconds">The heart beat seconds.</param>
        /// <param name="resetSeqNum">All sides of FIX session should have sequence numbers reset. Valid value
        ///is "Y" = Yes(reset)..</param>
        /// <returns></returns>
        public string LogonMessage(SessionQualifier qualifier, int messageSequenceNumber,
            int heartBeatSeconds, bool resetSeqNum)
        {
            var body = new StringBuilder();
            //Defines a message encryption scheme.Currently, only transportlevel security 
            //is supported.Valid value is "0"(zero) = NONE_OTHER (encryption is not used).
            body.Append("98=0|");
            //Heartbeat interval in seconds.
            //Value is set in the 'config.properties' file (client side) as 'SERVER.POLLING.INTERVAL'.
            //30 seconds is default interval value. If HeartBtInt is set to 0, no heart beat message 
            //is required.
            body.Append("108=" + heartBeatSeconds + "|");
            // All sides of FIX session should have
            //sequence numbers reset. Valid value
            //is "Y" = Yes(reset).
            if (resetSeqNum)
                body.Append("141=Y|");
            //The numeric User ID. User is linked to SenderCompID (#49) value (the
            //user’s organization).
            body.Append("553=" + _username + "|");
            //USer Password
            body.Append("554=" + _password + "|");

            var header = ConstructHeader(qualifier, SessionMessageCode(SessionMessageType.Logon), 
                messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;            
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        public string HeartbeatMessage(SessionQualifier qualifier, int messageSequenceNumber)
        {
            var header = ConstructHeader(qualifier, SessionMessageCode(SessionMessageType.Heartbeat), messageSequenceNumber, string.Empty);
            var trailer = ConstructTrailer(header);
            var headerAndMessageAndTrailer = header + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001"); 
        }

        public string TestRequestMessage(SessionQualifier qualifier, int messageSequenceNumber, int testRequestID)
        {
            var body = new StringBuilder();
            //Heartbeat message ID. TestReqID should be incremental.
            body.Append("112=" + testRequestID + "|");
            var header = ConstructHeader(qualifier, SessionMessageCode(SessionMessageType.TestRequest), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        public string LogoutMessage(SessionQualifier qualifier, int messageSequenceNumber)
        {
            var header = ConstructHeader(qualifier, SessionMessageCode(SessionMessageType.Logout), messageSequenceNumber, string.Empty);
            var trailer = ConstructTrailer(header);
            var headerAndMessageAndTrailer = header + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Resends the message.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">Message sequence number of last record in range to be resent.</param>
        /// <param name="endSequenceNo">The end sequence no.</param>
        /// <returns></returns>
        public string ResendMessage(SessionQualifier qualifier, int messageSequenceNumber, int endSequenceNo)
        {
            var body = new StringBuilder();
            //Message sequence number of last record in range to be resent.
            body.Append("16=" + endSequenceNo + "|");
            var header = ConstructHeader(qualifier, SessionMessageCode(SessionMessageType.Resend), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        public string RejectMessage(SessionQualifier qualifier, int messageSequenceNumber, int rejectSequenceNumber)
        {
            var body = new StringBuilder();
            //Referenced message sequence number.
            body.Append("45=" + rejectSequenceNumber + "|");
            var header = ConstructHeader(qualifier, SessionMessageCode(SessionMessageType.Reject), messageSequenceNumber, string.Empty);
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        public string SequenceResetMessage(SessionQualifier qualifier, int messageSequenceNumber, int rejectSequenceNumber)
        {
            var body = new StringBuilder();
            //New Sequence Number
            body.Append("36=" + rejectSequenceNumber + "|");
            var header = ConstructHeader(qualifier, SessionMessageCode(SessionMessageType.SequenceReset), messageSequenceNumber, string.Empty);
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Constructs a message for requesting market data.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="marketDataRequestID">Unique quote request id. New ID for a new subscription, same one as previously used for subscription removal.</param>
        /// <param name="subscriptionRequestType">1 = Snapshot plus updates (subscribe) 2 = Disable previous snapshot plus update request (unsubscribe).</param>
        /// <param name="marketDepth">Full book will be provided, 0 = Depth subscription, 1 = Spot subscription.</param>
        /// <param name="marketDataEntryType">Always set to 2 (both bid and ask will be sent).</param>
        /// <param name="noRelatedSymbol">Number of symbols requested.</param>
        /// <param name="symbol">Instrument identificators are provided by Spotware.</param>
        /// <returns></returns>
        public string MarketDataRequestMessage(SessionQualifier qualifier, int messageSequenceNumber, string marketDataRequestID, int subscriptionRequestType, int marketDepth, int marketDataEntryType, int noRelatedSymbol, long symbol = 0)
        {
            var body = new StringBuilder();
            //Unique quote request id. New ID for a new subscription, same one as previously used for subscription removal.
            body.Append("262=" + marketDataRequestID + "|");
            //1 = Snapshot plus updates (subscribe) 2 = Disable previous snapshot plus update request (unsubscribe)
            body.Append("263=" + subscriptionRequestType + "|");
            //Full book will be provided, 0 = Depth subscription, 1 = Spot subscription
            body.Append("264=" + marketDepth + "|");
            //Only Incremental refresh is supported.
            body.Append("265=1|");
            //Always set to 2 (both bid and ask will be sent).
            body.Append("267=2|");
            //Always set to 2 (both bid and ask will be sent).
            body.Append("269=0|269=1|");
            //Number of symbols requested.
            body.Append("146=" + noRelatedSymbol + "|");
            //Instrument identificators are provided by Spotware.
            body.Append("55=" + symbol + "|");
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.MarketDataRequest), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Constructs a message for requesting market data snapshot.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="symbol">Instrument identificators are provided by Spotware.</param>
        /// <param name="noMarketDataEntries">Number of entries following.</param>
        /// <param name="entryType">Valid values are: 0 = BID, 1 = OFFER.</param>
        /// <param name="entryPrice">Price of Market Data Entry.</param>
        /// <param name="marketDataRequestID">Unique quote request id. New ID for a new subscription, same one as previously used for subscription removal.</param>
        /// <returns></returns>
        public string MarketDataSnapshotMessage(SessionQualifier qualifier, int messageSequenceNumber, long symbol, string noMarketDataEntries, int entryType, decimal entryPrice, string marketDataRequestID = "")
        {
            var body = new StringBuilder();
            //Unique quote request id. New ID for a new subscription, same one as previously used for subscription removal.
            body.Append("262=" + marketDataRequestID + "|");
            //Instrument identificators are provided by Spotware.
            body.Append("55=" + symbol + "|");
            //Number of entries following.
            body.Append("268=" + noMarketDataEntries + "|");
            //Valid values are: 0 = BID, 1 = OFFER.
            body.Append("269=" + entryType + "|");
            //Price of Market Data Entry.
            body.Append("270=" + entryPrice + "|");
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.MarketDataRequest), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Markets the data incremental refresh message.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="marketDataRequestID">Unique quote request id. New ID for a new subscription, same one as previously used for subscription removal.</param>
        /// <param name="noMarketDataEntries">Number of entries following. This repeating group contains a list of all types of Market Data Entries the requester wants to receive.</param>
        /// <param name="marketDataUpdateAction">Type of Market Data update action. Valid values: 0 = NEW, 2 = DELETE.</param>
        /// <param name="marketDataEntryType">Valid values are: 0 = BID, 1 = OFFER.</param>
        /// <param name="marketDataEntryID">ID of Market Data Entry.</param>
        /// <param name="noRelatedSymbol">The no related symbol.</param>
        /// <param name="marketDataEntryPrice">Conditionally required when MDUpdateAction <279> = New(0).</param>
        /// <param name="marketDataEntrySize">Conditionally required when MDUpdateAction <279> = New(0).</param>
        /// <param name="symbol">/Instrument identificators are provided by Spotware.</param>
        /// <returns></returns>
        public string MarketDataIncrementalRefreshMessage(SessionQualifier qualifier, int messageSequenceNumber, string marketDataRequestID, int noMarketDataEntries, int marketDataUpdateAction, int marketDataEntryType, string marketDataEntryID, int noRelatedSymbol, decimal marketDataEntryPrice = 0, double marketDataEntrySize = 0, long symbol = 0)
        {
            var body = new StringBuilder();
            //Unique quote request id. New ID for a new subscription, same one as previously used for subscription removal.
            body.Append("262=" + marketDataRequestID + "|");
            //Number of entries following. This repeating group contains a list of all types of Market Data Entries the requester wants to receive
            body.Append("268=" + noMarketDataEntries + "|");
            //Type of Market Data update action. Valid values: 0 = NEW, 2 = DELETE
            body.Append("279=" + marketDataUpdateAction + "|");
            //Valid values are: 0 = BID, 1 = OFFER
            body.Append("269=" + marketDataEntryType + "|");
            //ID of Market Data Entry.
            body.Append("278=" + marketDataEntryID + "|");
            //Instrument identificators are provided by Spotware
            body.Append("55=" + symbol + "|");
            if (marketDataEntryPrice > 0)
            {
                //Conditionally required when MDUpdateAction <279> = New(0)
                body.Append("270=" + marketDataEntryPrice + "|");
            }
            if (marketDataEntrySize > 0)
            {
                //Conditionally required when MDUpdateAction <279> = New(0)
                body.Append("271=" + marketDataEntrySize + "|");
            }
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.MarketDataIncrementalRefresh), messageSequenceNumber, string.Empty);
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
            return headerAndMessageAndTrailer;
        }

        /// <summary>
        /// News the order single message.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="orderID">Unique identifier for the order, allocated by the client.</param>
        /// <param name="symbol">The symbol.</param>
        /// <param name="side">1= Buy, 2 = Sell.</param>
        /// <param name="transactTime">Client generated request time.</param>
        /// <param name="orderQuantity">The fixed currency amount.</param>
        /// <param name="orderType">1 = Market, the Order will be processed by 'Immediate Or Cancel'scheme(see TimeInForce(59): IOC);
        /// 2 = Limit, the Order will be processed by 'Good Till Cancel' scheme(see TimeInForce(59): GTC).</param>
        /// <param name="timeInForce">1 = Good Till Cancel (GTC), it will be active only for Limit Orders (see OrdType(40)) ;
        /// 3 = Immediate Or Cancel (IOC), it will be active only for Market Orders(see OrdType(40));
        /// 6 = Good Till Date(GTD), it will be active only if ExpireTime is defined (see ExpireTime(126)).
        /// GTD has a high priority, so if ExpireTime is defined, GTD will be used for the Order processing.</param>
        /// <param name="price">The worst client price that the client will accept.
        /// Required when OrdType = 2, in which case the order will notfill unless this price can be met.</param>
        /// /// <param name="stopPrice">Reserved for future use</param>
        /// <param name="expireTime">  Expire Time in YYYYMMDDHH:MM:SS format.If is assigned then the Order will be processed by 'Good Till Date' scheme
        /// (see TimeInForce: GTD).</param>
        /// <param name="positionID">Position ID, where this order should be placed. If not set, new position will be created, it’s id will be returned in ExecutionReport(8) message.</param>
        /// <returns></returns>
        public string NewOrderSingleMessage(SessionQualifier qualifier, int messageSequenceNumber, string orderID, long symbol, int side, string transactTime, int orderQuantity, int orderType, string timeInForce, decimal price = 0, decimal stopPrice = 0, string expireTime = "", string positionID = "")
        {
            var body = new StringBuilder();
            //Unique identifier for the order, allocated by the client.
            body.Append("11=" + orderID + "|");
            //Instrument identificators are provided by Spotware.
            body.Append("55=" + symbol + "|");
            //1= Buy, 2 = Sell
            body.Append("54=" + side + "|");
            // Client generated request time.
            body.Append("60=" + transactTime + "|");
            //The fixed currency amount.
            body.Append("38=" + orderQuantity + "|");
            //1 = Market, the Order will be processed by 'Immediate Or Cancel'scheme(see TimeInForce(59): IOC);
            //2 = Limit, the Order will be processed by 'Good Till Cancel' scheme(see TimeInForce(59): GTC).
            body.Append("40=" + orderType + "|");
            if (price != 0)
            {
                //Reserved for future use.
                body.Append("44=" + price + "|");
            }
            if (stopPrice != 0)
            {
                //The worst client price that the client will accept.
                //Required when OrdType = 2, in which case the order will notfill unless this price can be met.
                body.Append("99=" + stopPrice + "|");
            }
            // 1 = Good Till Cancel (GTC), it will be active only for Limit Orders (see OrdType(40)) ;
            // 3 = Immediate Or Cancel (IOC), it will be active only for Market Orders(see OrdType(40));
            // 6 = Good Till Date(GTD), it will be active only if ExpireTime is defined (see ExpireTime(126)).
            // GTD has a high priority, so if ExpireTime is defined, GTD will be used for the Order processing.
            body.Append("59=" + timeInForce + "|");
            if (expireTime != string.Empty)
            {
                // Expire Time in YYYYMMDDHH:MM:SS format.If is assigned then the Order will be processed by 'Good Till Date' scheme
                // (see TimeInForce: GTD).
                body.Append("126=" + expireTime + "|");
            }
            if (positionID != string.Empty)
            {
                // Position ID, where this order should be placed. If not set, new position will be created, it’s id will be returned in ExecutionReport(8) message.
                body.Append("721=" + positionID + "|");
            }
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.NewOrderSingle), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Orders the status request.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="orderID">Unique identifier for the order, allocated by the client.</param>
        /// <returns></returns>
        public string OrderStatusRequest(SessionQualifier qualifier, int messageSequenceNumber, string orderID)
        {
            var body = new StringBuilder();
            // Unique identifier for the order, allocated by the client.
            body.Append("11=" + orderID + "|");
            // 1 = Buy; 2 = Sell. There is for the FIX compatibility only, so it will be ignored.
            body.Append("54=1|");
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.OrderStatusRequest), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Executions the report.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="cTraderOrderID">cTrader order id..</param>
        /// <param name="orderStatus"> 0 = New; 1 = Partially filled; 2 = Filled;  8 = Rejected; 4 = Cancelled(When an order is partially filled, "Cancelled" is
        /// returned signifying Tag 151: LeavesQty is cancelled and will not be subsequently filled); C = Expired.</param>
        /// <param name="transactTime">Time the transaction represented by this ExecutionReport occurred message(in UTC).</param>
        /// <param name="symbol">Instrument identificators are provided by Spotware..</param>
        /// <param name="side">1 = Buy; 2 = Sell.</param>
        /// <param name="averagePrice">The price at which the deal was filled.For an IOC or GTD order, this is the VWAP(Volume Weighted Average Price) of the filled order.</param>
        /// <param name="orderQuantity"> The fixed currency amount.</param>
        /// <param name="leavesQuantity">The amount of the order still to be filled.This is a value between 0 (fully filled) and OrderQty (partially filled).</param>
        /// <param name="cumQuantity">The total amount of the order which has been filled.</param>
        /// <param name="orderID"> Unique identifier for the order, allocated by the client.</param>
        /// <param name="orderType"> 1 = Market; 2 = Limit.</param>
        /// <param name="price">If supplied in the NewOrderSingle, it is echoed back in this ExecutionReport.</param>
        /// <param name="stopPrice">If supplied in the NewOrderSingle, it is echoed back in this ExecutionReport.</param>
        /// <param name="timeInForce">U1 = Good Till Cancel (GTC); 3 = Immediate Or Cancel (IOC); 6 = Good Till Date (GTD).</param>
        /// <param name="expireTime"> If supplied in the NewOrderSingle, it is echoed back in this ExecutionReport.</param>
        /// <param name="text">Where possible, message to explain execution report.</param>
        /// <param name="orderRejectionReason">0 = OrdRejReason.BROKER_EXCHANGE_OPTION</param>
        /// <param name="positionID">Position ID.</param>
        /// <returns></returns>
        public string ExecutionReport(SessionQualifier qualifier, int messageSequenceNumber, string cTraderOrderID, string orderStatus, string transactTime, long symbol = 0, int side = 1,
            int averagePrice = 0, int orderQuantity = 0, int leavesQuantity = 0, int cumQuantity = 0, string orderID = "", string orderType = "", int price = 0, int stopPrice = 0,
            string timeInForce = "", string expireTime = "", string text = "", int orderRejectionReason = -1, string positionID = "")
        {
            var body = new StringBuilder();
            // cTrader order id.
            body.Append("37=" + cTraderOrderID + "|");
            // Unique identifier for the order, allocated by the client.
            body.Append("11=" + orderID + "|");
            // Execution Type.
            body.Append("150=F|");
            // 0 = New; 1 = Partially filled; 2 = Filled;  8 = Rejected; 4 = Cancelled(When an order is partially filled, "Cancelled" is
            // returned signifying Tag 151: LeavesQty is cancelled and will not be subsequently filled); C = Expired.
            body.Append("39=" + orderStatus + "|");
            // Instrument identificators are provided by Spotware.
            body.Append("55=" + symbol + "|");
            // 1 = Buy; 2 = Sell.
            body.Append("54=" + side + "|");
            // Time the transaction represented by this ExecutionReport occurred message(in UTC).
            body.Append("60=" + transactTime + "|");
            if (averagePrice > 0)
            {
                // The price at which the deal was filled.For an IOC or GTD order, this is the VWAP(Volume Weighted Average Price) of the filled order.
                body.Append("6=" + averagePrice + "|");
            }
            if (orderQuantity > 0)
            {
                // The fixed currency amount.
                body.Append("38=" + orderQuantity + "|");
            }
            if (leavesQuantity > 0)
            {
                // The amount of the order still to be filled.This is a value between 0 (fully filled) and OrderQty (partially filled).
                body.Append("151=" + leavesQuantity + "|");
            }
            if (cumQuantity > 0)
            {
                // The total amount of the order which has been filled.
                body.Append("14=" + cumQuantity + "|");
            }
            if (orderType != string.Empty)
            {
                // 1 = Market; 2 = Limit.
                body.Append("40=" + orderType + "|");
            }
            if (price > 0)
            {
                // If supplied in the NewOrderSingle, it is echoed back in this ExecutionReport.
                body.Append("44=" + price + "|");
            }
            if (stopPrice > 0)
            {
                // If supplied in the NewOrderSingle, it is echoed back in this ExecutionReport.
                body.Append("99=" + stopPrice + "|");
            }
            if (timeInForce != string.Empty)
            {
                // U1 = Good Till Cancel (GTC); 3 = Immediate Or Cancel (IOC); 6 = Good Till Date (GTD).
                body.Append("59=" + timeInForce + "|");
            }
            if (expireTime != string.Empty)
            {
                // If supplied in the NewOrderSingle, it is echoed back in this ExecutionReport.
                body.Append("126=" + expireTime + "|");
            }
            if (text != string.Empty)
            {
                // Where possible, message to explain execution report.
                body.Append("58=" + text + "|");
            }
            if (orderRejectionReason != -1)
            {
                // 0 = OrdRejReason.BROKER_EXCHANGE_OPTION
                body.Append("103=" + orderRejectionReason + "|");
            }
            if (positionID != string.Empty)
            {
                // Position ID.
                body.Append("721=" + positionID + "|");
            }

            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.OrderStatusRequest), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Businesses the message reject.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="referenceSequenceNum">MsgSeqNum<34> of rejected message.</param>
        /// <param name="referenceMessageType">TThe MsgType<35> of the FIX message being referenced.</param>
        /// <param name="businessRejectRefID">The value of the business-level 'ID' field on the message being referenced.Required unless the corresponding ID field was not specified.</param>
        /// <param name="businessRejectReason">Where possible, message to explain reason for rejection.</param>
        /// <param name="text">Where possible, message to explain reason for rejection.</param>
        /// <returns></returns>
        public string BusinessMessageReject(SessionQualifier qualifier, int messageSequenceNumber, int referenceSequenceNum = 0, string referenceMessageType = "", string businessRejectRefID = "", int businessRejectReason = -1, string text = "")
        {
            var body = new StringBuilder();
            if (referenceSequenceNum != 0)
            {
                // MsgSeqNum<34> of rejected message.
                body.Append("45=" + referenceSequenceNum + "|");
            }
            if (referenceMessageType != string.Empty)
            {
                // The MsgType<35> of the FIX message being referenced.
                body.Append("372=" + referenceMessageType + "|");
            }
            if (businessRejectRefID != string.Empty)
            {
                // The value of the business-level 'ID' field on the message being referenced.Required unless the corresponding ID field was not specified.
                body.Append("379=" + businessRejectRefID + "|");
            }
            if (businessRejectReason != -1)
            {
                // Code to identify reason for a Business Message Reject<j> message. 0 = OTHER.
                body.Append("380=" + businessRejectReason + "|");
            }
            if (text != string.Empty)
            {
                // Where possible, message to explain reason for rejection.
                body.Append("58=" + text + "|");
            }
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.BusinessMessageReject), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Requests for positions.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="requestID">Unique request ID (set by client).</param>
        /// <param name="positionRequestID">Position ID to request. If not set, all open positions will be returned.</param>
        /// <returns></returns>
        public string RequestForPositions(SessionQualifier qualifier, int messageSequenceNumber, string requestID, string positionRequestID = "")
        {
            var body = new StringBuilder();
            // Unique request ID (set by client).
            body.Append("710=" + requestID + "|");
            if (positionRequestID != string.Empty)
            {
                // Position ID to request. If not set, all open positions will be returned.
                body.Append("721=" + positionRequestID + "|");
            }
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.RequestForPosition), messageSequenceNumber, body.ToString());
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Gets the position of reporting.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="requestID">Id of RequestForPositions.</param>
        /// <param name="totalNumberOfPositionReports">Total count of PositionReport’s in sequence when PosReqResult(728) is VALID_REQUEST, otherwise = 0.</param>
        /// <param name="positionRequestResult">0 = Valid Request; 2 = No open positions found that match criteria.</param>
        /// <param name="positionID">Position ID (is not set if PosReqResult(728) is not VALID_REQUEST).</param>
        /// <param name="symbol">The symbol for which the current Position Report is prepared. (is not set if PosReqResult(728) is not VALID_REQUEST).</param>
        /// <param name="noOfPositions">1 when PosReqResult(728) is  VALID_REQUEST, otherwise not set.</param>
        /// <param name="longQuantity">Position’s open volume in case of BUY trade side, = 0 in case of SELL trade side, is not set if PosReqResult(728) is not VALID_REQUEST.</param>
        /// <param name="shortQuantity">Position’s open volume in case of SELL trade side, = 0 in case of BUY trade side, is not set if PosReqResult(728) is not VALID_REQUEST.</param>
        /// <param name="settlementPrice">Average price of the opened volume in the current PositionReport.</param>
        /// <returns></returns>
        public string PositionReport(SessionQualifier qualifier, int messageSequenceNumber, string requestID, string totalNumberOfPositionReports, string positionRequestResult,
            string positionID = "", string symbol = "", string noOfPositions = "", string longQuantity = "", string shortQuantity = "", string settlementPrice = "")
        {
            var body = new StringBuilder();
            // Id of RequestForPositions.
            body.Append("710=" + requestID + "|");
            if (positionID != string.Empty)
            {
                // Position ID (is not set if PosReqResult(728) is not VALID_REQUEST).
                body.Append("721=" + positionID + "|");
            }
            // Total count of PositionReport’s in sequence when PosReqResult(728) is VALID_REQUEST, otherwise = 0.
            body.Append("727=" + totalNumberOfPositionReports + "|");
            // 0 = Valid Request; 2 = No open positions found that match criteria.
            body.Append("728=" + positionRequestResult + "|");

            if (symbol != string.Empty)
            {
                // The symbol for which the current Position Report is prepared. (is not set if PosReqResult(728) is not VALID_REQUEST).
                body.Append("55=" + symbol + "|");
            }
            if (noOfPositions != string.Empty)
            {
                // 1 when PosReqResult(728) is  VALID_REQUEST, otherwise not set.
                body.Append("702=" + noOfPositions + "|");
            }
            if (longQuantity != string.Empty)
            {
                // Position’s open volume in case of BUY trade side, = 0 in case of SELL trade side, is not set if PosReqResult(728) is not VALID_REQUEST.
                body.Append("704=" + requestID + "|");
            }
            if (shortQuantity != string.Empty)
            {
                //Position’s open volume in case of SELL trade side, = 0 in case of BUY trade side, is not set if PosReqResult(728) is not VALID_REQUEST.
                body.Append("705=" + shortQuantity + "|");
            }
            if (settlementPrice != string.Empty)
            {
                // Average price of the opened volume in the current PositionReport.
                body.Append("730=" + settlementPrice + "|");
            }
            var header = ConstructHeader(qualifier, ApplicationMessageCode(ApplicationMessageType.PositionReport), messageSequenceNumber, string.Empty);
            var headerAndBody = header + body;
            var trailer = ConstructTrailer(headerAndBody);
            var headerAndMessageAndTrailer = header + body + trailer;
            return headerAndMessageAndTrailer.Replace("|", "\u0001");
        }

        /// <summary>
        /// Constructs the message header.
        /// </summary>
        /// <param name="qualifier">The session qualifier.</param>
        /// <param name="type">The message type.</param>
        /// <param name="messageSequenceNumber">The message sequence number.</param>
        /// <param name="bodyMessage">The body message.</param>
        /// <returns></returns>
        private string ConstructHeader(SessionQualifier qualifier, string type,
            int messageSequenceNumber, string bodyMessage)
        {
            var header = new StringBuilder();
            // Protocol version. FIX.4.4 (Always unencrypted, must be first field 
            // in message.
            header.Append("8=FIX.4.4|");
            var message = new StringBuilder();
            // Message type. Always unencrypted, must be third field in message.
            message.Append("35=" + type + "|");
            // ID of the trading party in following format: <BrokerUID>.<Trader Login> 
            // where BrokerUID is provided by cTrader and Trader Login is numeric 
            // identifier of the trader account.
            message.Append("49=" + _senderCompID + "|");  
            // Message target. Valid value is "CSERVER"
            message.Append("56=" + _targetCompID + "|");  
            // Additional session qualifier. Possible values are: "QUOTE", "TRADE".
            message.Append("57=" + qualifier.ToString() + "|");  
            // Assigned value used to identify specific message originator.
            message.Append("50=" + _senderSubID + "|");
            // Message Sequence Number
            message.Append("34=" + messageSequenceNumber + "|");
            // Time of message transmission (always expressed in UTC(Universal Time 
            // Coordinated, also known as 'GMT').
            message.Append("52=" + DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss") + "|");
            var length = message.Length + bodyMessage.Length;
            // Message body length. Always unencrypted, must be second field in message.
            header.Append("9=" + length + "|");
            header.Append(message);      
            return header.ToString();
        }

        /// <summary>
        /// Constructs the message trailer.
        /// </summary>
        /// <param name="message">The message trailer.</param>
        /// <returns></returns>
        private string ConstructTrailer(string message)
        {
            //Three byte, simple checksum. Always last field in message; i.e. serves,
            //with the trailing<SOH>, 
            //as the end - of - message delimiter. Always defined as three characters
            //(and always unencrypted).
            var trailer = "10=" + CalculateChecksum(message.Replace("|", "\u0001").ToString()).ToString().PadLeft(3, '0') + "|";
            return trailer;
        }

        /// <summary>
        /// Calculates the checksum.
        /// </summary>
        /// <param name="dataToCalculate">The data to calculate.</param>
        /// <returns></returns>
        private int CalculateChecksum(string dataToCalculate)
        {
            byte[] byteToCalculate = Encoding.ASCII.GetBytes(dataToCalculate);
            int checksum = 0;
            foreach (byte chData in byteToCalculate)
            {
                checksum += chData;
            }
            return checksum % 256;
        }

        /// <summary>
        /// Returns the session the message code.
        /// </summary>
        /// <param name="type">The session message type.</param>
        /// <returns></returns>
        private string SessionMessageCode(SessionMessageType type)
        {
            switch (type)
            {
                case SessionMessageType.Heartbeat:
                    return "0";
                    break;

                case SessionMessageType.Logon:
                    return "A";
                    break;

                case SessionMessageType.Logout:
                    return "5";
                    break;

                case SessionMessageType.Reject:
                    return "3";
                    break;

                case SessionMessageType.Resend:
                    return "2";
                    break;

                case SessionMessageType.SequenceReset:
                    return "4";
                    break;

                case SessionMessageType.TestRequest:
                    return "1";
                    break;

                default:
                    return "0";
            }
        }

        /// <summary>
        /// Returns the application message code.
        /// </summary>
        /// <param name="type">The application message type.</param>
        /// <returns></returns>
        private string ApplicationMessageCode(ApplicationMessageType type)
        {
            switch (type)
            {
                case ApplicationMessageType.MarketDataRequest:
                    return "V";
                    break;

                case ApplicationMessageType.MarketDataIncrementalRefresh:
                    return "X";
                    break;

                case ApplicationMessageType.NewOrderSingle:
                    return "D";
                    break;

                case ApplicationMessageType.OrderStatusRequest:
                    return "H";
                    break;

                case ApplicationMessageType.ExecutionReport:
                    return "8";
                    break;

                case ApplicationMessageType.BusinessMessageReject:
                    return "j";
                    break;

                case ApplicationMessageType.RequestForPosition:
                    return "AN";
                    break;

                case ApplicationMessageType.PositionReport:
                    return "AP";
                    break;

                default:
                    return "0";
            }
        }
    }
}