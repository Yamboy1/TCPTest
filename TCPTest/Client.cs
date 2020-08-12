using System;
using System.Net.Sockets;
using System.Text;

namespace TCPTest
{	
	public class Client
	{
		private readonly string ipAddress;
		private readonly int port;

		public Client(string ipAddress, int port)
		{
			this.ipAddress = ipAddress;
			this.port = port;
		}

		public void Connect(string message)
		{
			TcpClient client = new TcpClient(ipAddress, port);

			Byte[] data = Encoding.UTF8.GetBytes(message);
			NetworkStream stream = client.GetStream();
			stream.Write(data, 0, data.Length);
			Console.WriteLine("Client Sent: {0}", data);
			
			data = new Byte[256];
			StringBuilder builder = new StringBuilder();

			do
			{
				Int32 bytes = stream.Read(data, 0, data.Length);
				builder.AppendFormat("{0}", Encoding.UTF8.GetString(data, 0, bytes));
			} while (stream.DataAvailable);

			String responseData = builder.ToString();
			Console.WriteLine("Client Received: {0}", responseData);

			stream.Close();
			client.Close();
		}
	}
}