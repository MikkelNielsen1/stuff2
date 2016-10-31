using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.IO;

namespace Exercise9UDP
{
	class MainClass
	{
		const int PORT = 9000;
		public static byte[] ipAddress = new byte[] {10, 0, 0, 1};
		public static byte[] message;
		public static string readFileData;
		public static string messagestring;


		public static void Main (string[] args)
		{
			
			IPEndPoint ep = new IPEndPoint (new IPAddress (ipAddress), PORT);
			UdpClient client = new UdpClient (ep);

			IPEndPoint senderinfo = new IPEndPoint (IPAddress.Any, 0);
			while (true) 
			{
				Byte[] receiveBytes = client.Receive (ref senderinfo); 	


				string receivedData = Encoding.ASCII.GetString(receiveBytes);


				Console.WriteLine (receivedData);
				if (receivedData.ToLower () == "u") 
				{
					readFileData = File.ReadAllText ("/proc/uptime");
					messagestring = "Uptime is " + readFileData;
					message = Encoding.ASCII.GetBytes (messagestring);

						
				} else if (receivedData.ToLower () == "l") 
				{
					readFileData = File.ReadAllText ("/proc/loadavg");
					messagestring = "Load average is " + readFileData;
					message = Encoding.ASCII.GetBytes (messagestring);
				} else 
				{
					messagestring = "Invalid character passed";
					message = Encoding.ASCII.GetBytes (messagestring);
				}

				client.Send (message, message.Length, senderinfo);

			}


		}
	}
}
