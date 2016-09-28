using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace tcp
{
	class file_client
	{
		/// <summary>
		/// The PORT.
		/// </summary>
		const int PORT = 9000;
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;

		//private byte[] ipAddress = new byte[]{10,0,0,1};
		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments. First ip-adress of the server. Second the filename
		/// </param>
		private file_client (string[] args)
		{
			string ipa = args [1];
			IPAddress ipAddress = IPAddress.Parse(ipa);
			Console.WriteLine (ipa + ipAddress);
			/*long num = Int64.Parse (args [1]);
			ipAddress = new byte[] {(byte) num};

			Console.WriteLine (args[1] + num + ipAddress); */
				
			
			TcpClient clientSocket = new TcpClient ();
			clientSocket.Connect(ipAddress,PORT);

			NetworkStream serverStream = clientSocket.GetStream();

			// Send filename to server
			string filename = "Mikkel er en pickle";
			LIB.writeTextTCP (serverStream, filename);

			// Extract filename
			string extractedFilename = LIB.extractFileName (filename);

			// Recieve notice if filename exists
			string currentfilesize = LIB.readTextTCP (serverStream);
			Console.WriteLine (currentfilesize);

			// If filename exists recieve file
			if (Int32.Parse(currentfilesize) != 0) {
				receiveFile (extractedFilename, serverStream);
			} else
				Console.WriteLine ("Filename: " + extractedFilename + " does not exist"); 

		}

		/// <summary>
		/// Receives the file.
		/// </summary>	
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='io'>
		/// Network stream for reading from the server
		/// </param>
		private void receiveFile (String fileName, NetworkStream io)
		{
			long filesize = LIB.getFileSizeTCP (io);
			// Loopet som indslæser filen til filnavnet Filename
		}

		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments.
		/// </param>
		public static void Main (string[] args)
		{
			Console.WriteLine ("Client starts...");
			new file_client(args);
		}
	}
}
