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
			serverSocket.Start ();
			
			TcpClient newClient = serverSocket.AcceptTcpClient ();
			Console.WriteLine ("Client Accepted...");

			//string receivedText = LIB.readTextTCP (newClient.GetStream ());

			//Console.WriteLine ("Received Text: " + receivedText);

			// Recieve filename from client
			NetworkStream networkStream = newClient.GetStream ();
			String filename = LIB.readTextTCP (networkStream);
			//Console.WriteLine(filename);

			// Check to see if filename exists and notify client
			long filesize = LIB.check_File_Exists (filename);
			//Console.WriteLine (filesize);
			string filesizeascii = filesize.ToString();
			LIB.writeTextTCP (networkStream, filesizeascii);

			if (filesize != 0) 
			{
				sendFile (filename, filesize, networkStream);
			}

			newClient.Close ();
			serverSocket.Stop ();
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
			// TO DO Your own cod
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
