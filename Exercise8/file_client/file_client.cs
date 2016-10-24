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
	
		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// </summary>
		/// <param name='args'>
		/// The command-line arguments. First ip-adress of the server. Second the filename
		/// </param>
		private file_client (string[] args)
		{
			try
			{
				string ipa = args [0];

				IPAddress ipAddress = IPAddress.Parse(ipa);
				//Console.WriteLine (ipa + ipAddress);
				
			
				TcpClient clientSocket = new TcpClient ();

				clientSocket.Connect(ipAddress,PORT);
				
		

				NetworkStream serverStream = clientSocket.GetStream();

				// Send filename to server
				string filename = args [1];
				LIB.writeTextTCP (serverStream, filename);

				// Extract filename
				string extractedFilename = LIB.extractFileName (filename);

				// Recieve notice if filename exists
				long currentfilesize = LIB.getFileSizeTCP (serverStream);
				//Console.WriteLine (currentfilesize);

				// If filename exists recieve file
				if (currentfilesize != 0) {
					Console.WriteLine ("Filesize: " + currentfilesize);
					Console.WriteLine ("Requested file: " + filename);
					Console.WriteLine ("Extracted file name: " + extractedFilename);
					receiveFile (extractedFilename, serverStream, currentfilesize);
				} else
					Console.WriteLine ("Filename: " + extractedFilename + " does not exist"); 
			}
			catch(ArgumentException e) 
			{
				Console.WriteLine ("ArgumentException: " + e.Message);
			}
			catch(SocketException se)
			{
				Console.WriteLine("SocketException: " + se.Message);
			}
			catch(Exception e)
			{
				Console.WriteLine("Exception: " + e.Message);
			}

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
		private void receiveFile (string fileName, NetworkStream io, long filesize)
		{
			long readBytes = 0;
		
			Byte[] readbuffer = new byte[BUFSIZE];
		

			FileStream downloadedfile = File.Create (fileName);

			while (filesize > 0) 
			{
				readBytes = io.Read (readbuffer, 0, BUFSIZE);
				filesize -= readBytes;

				downloadedfile.Write (readbuffer, 0, (int)readBytes);
			
				Console.WriteLine (filesize);
			}
			Console.WriteLine ("Download complete...");

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
