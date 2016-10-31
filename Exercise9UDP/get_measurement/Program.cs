using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace get_measurement
{
	class MainClass
	{
		const int PORT = 9000;
		
		public static void Main (string[] args)
		{
			// Get arguments from user
			string ipa = args [0];
			string stringtosend = args [1];


			// Create socket to server
			IPAddress ipAddress = IPAddress.Parse(ipa);

			IPEndPoint ep = new IPEndPoint (ipAddress,PORT);

			UdpClient server = new UdpClient ();


			// Convert userinput to byte array + send to server

			Byte[] bytetosend = Encoding.ASCII.GetBytes (stringtosend);

			server.Send(bytetosend,bytetosend.Length,ep);


			// Recieve data and output to console
				
			IPEndPoint serverEP = new IPEndPoint (IPAddress.Any, 0);

			Byte[] receivedData = server.Receive (ref serverEP);

			string result = Encoding.ASCII.GetString (receivedData);

			Console.WriteLine (result);

		}
	}
}
