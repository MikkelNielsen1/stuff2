using System;
using System.Net;
using System.Net.Sockets;

namespace Exercise9UDP
{
	class MainClass
	{
		const int PORT = 9000;
		public byte[] ipAddress = new byte[] {10, 0, 0, 1};
		public byte[] message;


		public static void Main (string[] args)
		{
			
			IPEndPoint ep = new IPEndPoint (new IPAddress (ipAddress), PORT);
			UdpClient client = new UdpClient (ep);

			IPEndPoint senderinfo = new IPEndPoint (IPAddress.Any, PORT);
			while (true) 
			{
				Byte[] receiveBytes = client.Receive (ref senderinfo); 	

				Console.WriteLine (senderinfo.Address + " " + senderinfo.Port);
				string returnData = Encoding.ASCII.GetString(receiveBytes);

				client.Send (message, sizeof(message), senderinfo);
			}


		}
	}
}
