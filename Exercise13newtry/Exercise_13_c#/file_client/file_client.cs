using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_client
	{
		/// <summary>
		/// The BUFSIZE.
		/// </summary>
		const int BUFSIZE = 1000;


		/// <summary>
		/// Initializes a new instance of the <see cref="file_client"/> class.
		/// 
		/// file_client metoden opretter en peer-to-peer forbindelse
		/// Sender en forspÃ¸rgsel for en bestemt fil om denne findes pÃ¥ serveren
		/// Modtager filen hvis denne findes eller en besked om at den ikke findes (jvf. protokol beskrivelse)
		/// Lukker alle streams og den modtagede fil
		/// Udskriver en fejl-meddelelse hvis ikke antal argumenter er rigtige
		/// </summary>
		/// <param name='args'>
		/// Filnavn med evtuelle sti.
		/// </param>
	    private file_client(String[] args)
	    {
			Transport transport = new Transport (1000);
			string filename = args [0];
			//filename = "index.jpeg";
			receiveFile (filename, transport);
		

	    }

		/// <summary>
		/// Receives the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='transport'>
		/// Transportlaget
		/// </param>
		private void receiveFile (String fileName, Transport transport)
		{
			long readBytes = 0;

			Byte[] readbuffer = new byte[BUFSIZE];

			// Extract filename and send to server
			Console.WriteLine("Sending filename to server");

			String extractedFileName = LIB.extractFileName (fileName);


			byte[] filenameToSend = Encoding.ASCII.GetBytes(extractedFileName);

			transport.send (filenameToSend, filenameToSend.Length);


			Console.WriteLine("Filename sent to server: " + extractedFileName);
			Console.WriteLine ();

			byte[] fileSizeBuf = new byte[BUFSIZE];
			int receivedByteCount = transport.receive (ref fileSizeBuf);

			int filesize = Int32.Parse (Encoding.ASCII.GetString (fileSizeBuf, 0, receivedByteCount));

			Console.WriteLine ("Received file size: " + filesize);
			Console.WriteLine ();

			Console.WriteLine("Receiving file");
			if (filesize != 0) {
				// Receive file
				FileStream downloadedfile = File.Create (extractedFileName);

				while (filesize > 0) {
					readBytes = transport.receive (ref readbuffer);
					filesize -= (int)readBytes;

					downloadedfile.Write (readbuffer, 0, (int)readBytes);
				}
				Console.WriteLine ("Download complete...");
			} 
			else 
			{
				Console.WriteLine ("Filename: " + extractedFileName + " does not exist"); 
			}
		}



		/// <summary>
		/// The entry point of the program, where the program control starts and ends.
		/// </summary>
		/// <param name='args'>
		/// First argument: Filname
		/// </param>
		public static void Main (string[] args)
		{
			new file_client(args);
		}
	}
}