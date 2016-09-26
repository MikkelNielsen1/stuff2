using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace tcp
{
	class file_server
	{
		/// <summary>
		/// The PORT
		/// </summary>
		const int PORT = 9000;
		/// <summary>
		/// The BUFSIZE
		/// </summary>
		const int BUFSIZE = 1000;

		private byte[] ipAddress = new byte[] {10, 0, 0, 1};

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// Opretter en socket.
		/// Venter på en connect fra en klient.
		/// Modtager filnavn
		/// Finder filstørrelsen
		/// Kalder metoden sendFile
		/// Lukker socketen og programmet
 		/// </summary>
		private file_server ()
		{
			TcpListener serverSocket = new TcpListener(new IPAddress(ipAddress), PORT);
			serverSocket.Start();
			//Console.WriteLine ("IP addresse: " + host.AddressList[0] + " " + host.AddressList.GetValue(0));
			
			TcpClient newClient = serverSocket.AcceptTcpClient ();
			Console.WriteLine ("Client Accepted...");

			string receivedText = LIB.readTextTCP (newClient.GetStream ());

			Console.WriteLine ("Received Text: " + receivedText);

			/*
			while (true) 
			{
				NetworkStream networkStream = newClient.GetStream ();
				byte[] readBytes = new byte[1000];
				var numberOfBytesRead = 0;
				StringBuilder completeMessage = new StringBuilder();


				do{
					numberOfBytesRead = networkStream.Read(readBytes, 0, readBytes.Length);

					completeMessage.AppendFormat("{0}", Encoding.ASCII.GetString(readBytes, 0, numberOfBytesRead));

				}
				while(networkStream.DataAvailable);

				Console.WriteLine (completeMessage.ToString ());
				sendFile (completeMessage.ToString(), completeMessage.Length,networkStream);
			}
			*/

		}

		/// <summary>
		/// Sends the file.
		/// </summary>
		/// <param name='fileName'>
		/// The filename.
		/// </param>
		/// <param name='fileSize'>
		/// The filesize.
		/// </param>
		/// <param name='io'>
		/// Network stream for writing to the client.
		/// </param>
		private void sendFile (String fileName, long fileSize, NetworkStream io)
		{
			// TO DO Your own code
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine ("Server starts...");
			new file_server();
		}
	}
}
