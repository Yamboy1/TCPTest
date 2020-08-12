using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TCPTest
{
	public class Server
	{
		private int port;

		public Server(int port)
		{
			this.port = port;
		}

		public void listen()
		{
			TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
			
			listener.Start();
			
			while (true)
			{
				TcpClient client = listener.AcceptTcpClient();

				Byte[] data = new Byte[256];
				NetworkStream stream = client.GetStream();
				StringBuilder builder = new StringBuilder();

				do
				{
					Int32 bytes = stream.Read(data, 0, data.Length);
					builder.AppendFormat("{0}", Encoding.UTF8.GetString(data, 0, bytes));
				} while (stream.DataAvailable);

				string responseData = builder.ToString();

				Console.WriteLine("Listener Received: {0}", responseData);

				data = Encoding.UTF8.GetBytes(responseData);
				stream.Write(data, 0, data.Length);

				Console.WriteLine("Listener Sent: {0}", data);
				
				client.Close();
			}
		}

	}
}