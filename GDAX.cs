using System;
using PureWebSockets;
using System.Net.WebSockets;
using System.Text;
using System.Runtime.InteropServices;

namespace btc_bot
{
    class Program
    {

        private static PureWebSocket _ws;
        
        static void Main(string[] args)
        {
            /*_ws = new PureWebSocket("wss://ws-feed.gdax.com");

            //_ws = new PureWebSocket("wss://ws.blockchain.info/inv");

            _ws.OnStateChanged += Ws_OnStateChanged;
            _ws.OnMessage += Ws_OnMessage;
            _ws.OnClosed += Ws_OnClosed;
            _ws.OnSendFailed += Ws_OnSendFailed;
            _ws.Connect();

            Console.ReadLine();*/

            /*NeuralNetwork nw = new NeuralNetwork(1.25, new int[] { 20*20, 20*10, 10*10, 50, 26}, ActivationFunctions.Activation.SIGMOID, "Latin Alphabet");

            NeuralData outx = new NeuralData(1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0);
            Bitmap bmp = (Bitmap)Bitmap.FromFile("training_data/monospace/A.png");
            
            NeuralData inputData = new NeuralData();

            for(int x = 0; x < bmp.Width; x++)
            {
                for(int y = 0; y < bmp.Height; y++)
                {
                    
                    Color c = bmp.GetPixel(x,y);

                    double val = (255-((c.R + c.G + c.B) / 3))/255.0;

                    //Console.WriteLine($"{x}, {y}: {val}");
                    
                    inputData.Add(val);
                }
            }

            Console.WriteLine(String.Join(", ", inputData.ToArray()));
            
            nw.Train(inputData, outx, 100000);

            NeuralData outp = nw.Run(inputData);

            Console.WriteLine(String.Join(", ", outp.ToArray()));

            File.WriteAllText("test.txt", nw.ToString());

            //nw.SaveImage("test.jpg");

            */

            /*using (var graph = new TFGraph())
            {
                using (var session = new TFSession(graph))
                {
                    

                    var a = graph.Const(2, "a");
                    var b = graph.Const(3, "b");
                    Console.WriteLine("a=2 b=3");

                    var addResults = session.Run(null, new TFOutput[] { graph.Add(a, b) }, new TFTensor[] { }, new TFOutput[] {});

                    Console.WriteLine("a+b={0}", addResults.GetValue(0));

                    var multiplyResults = session.Run(null, new TFOutput[] { graph.Mul(a, b) }, new TFTensor[] {1}, new TFOutput[] {});

                    Console.WriteLine("a*b={0}", multiplyResults.GetValue(0));
                }
            }*/

            

            var value = $"Hello from {TFCore.Version}";

            using (var graph = new TFGraph())
            using (var tensor = TFTensor.CreateString(Encoding.UTF8.GetBytes(value)))
            {
                var output = graph.Const(tensor, tensor.TensorType, "test");
                using (var session = new TFSession(graph))
                {
                    var run = session.GetRunner().Fetch(output).Run();
                    var bytes = run[0];
                    var bytesDest = new byte[(int)bytes.TensorByteSize];
                    Marshal.Copy(bytes.Data, bytesDest, 0, bytesDest.Length);

                    //Encoding.UTF8.GetString(bytesDest).Should().Contain(value);
                }
            }
        }

        private static void Ws_OnStateChanged(WebSocketState newState, WebSocketState prevState)
        {
            if (newState == WebSocketState.Open)
            {
                Send(@"{
                    ""type"": ""subscribe"",
                    ""product_ids"": [
                        ""BTC-USD""
                    ],
                    ""channels"": [
                        ""full""
                    ]
                }");    

                /*
                
                ""heartbeat"",
                {
                    ""name"": ""ticker"",
                    ""product_ids"": [
                        ""BTC-USD""
                    ]
                }
                
                 */

                //Send(@"{""type"": ""subscribe"", ""channels"": [{ ""name"": ""heartbeat"", ""product_ids"": [""ETH-EUR""] }]}");
            }
        }

        private static void Send(string message)
        {
            Console.WriteLine("[<--] " + message);

            _ws.Send(message);
        }

        private static void Ws_OnMessage(string _message)
        {
            Console.WriteLine("[-->] " + _message);

            dynamic message = DynamicJson.Parse(_message);

            /*if (message["event"] == "pusher:connection_established")
            {
                Send("{\"event\":\"pusher:subscribe\",\"data\":{\"channel\":\"live_trades\"}}");
                Send("{\"event\":\"pusher:subscribe\",\"data\":{\"channel\":\"live_orders\"}}");
            }
            else if (message["event"] == "trade")
            {
                Console.WriteLine("Price: " + data["price"]);

                File.AppendAllText("trades.dat", ToUnixTime(DateTime.Now) + " " + data["price"] + "\n");
            }
            else if (message["event"] == "order_changed" || message["event"] == "order_deleted" || message["event"] == "order_created")
            {
                Console.WriteLine("Price: " + data["price"]);

                // Buy
                if (data["order_type"] == 0)
                {
                    File.AppendAllText("order_buys.dat", ToUnixTime(DateTime.Now) + " " + data["price"] + "\n");
                }
                else if (data["order_type"] == 1)
                {
                    File.AppendAllText("order_sells.dat", ToUnixTime(DateTime.Now) + " " + data["price"] + "\n");
                }
                
            }*/
        }
        
        private static void Ws_OnClosed(WebSocketCloseStatus reason)
        {

        }

        private static void Ws_OnSendFailed(string data, Exception exception)
        {

        }

        public static long ToUnixTime(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date.ToUniversalTime() - epoch).TotalMilliseconds);
        }
    }
}
