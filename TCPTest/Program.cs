using System;
using System.Threading;
using System.Threading.Tasks;
using Network;

namespace TCPTest
{
	class Program
	{
		static void Main(string[] args)
		{
			var server = new Server(1234);
			var client = new Client("127.0.0.1", 1234);

			Task.Run(() => server.listen());
			
			client.Connect("asdf");
			client.Connect("a second one");

		}
	}
}