using System;
using System.IO;
using System.Text;
using Transportlaget;
using Library;

namespace Application
{
	class file_server
	{
		/// <summary>
		/// The BUFSIZE
		/// </summary>
		private const int BUFSIZE = 1000;
	

		/// <summary>
		/// Initializes a new instance of the <see cref="file_server"/> class.
		/// </summary>
		private file_server ()
		{
			Transport transport = new Transport (BUFSIZE);

			byte[] receiveStuff = new byte[1000];
			transport.receive (ref receiveStuff);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff));


			byte[] receiveStuff1 = new byte[1000];
			transport.receive (ref receiveStuff1);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff1));


			byte[] receiveStuff2 = new byte[1000];
			transport.receive (ref receiveStuff2);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff2));


			byte[] receiveStuff3 = new byte[1000];
			transport.receive (ref receiveStuff3);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff3));


			byte[] receiveStuff4 = new byte[1000];
			transport.receive (ref receiveStuff4);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff4));

			byte[] receiveStuff5 = new byte[1000];
			transport.receive (ref receiveStuff5);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff5));

			byte[] receiveStuff6 = new byte[1000];
			transport.receive (ref receiveStuff6);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff6));

			byte[] receiveStuff7 = new byte[1000];
			transport.receive (ref receiveStuff7);
			Console.WriteLine (Encoding.ASCII.GetString (receiveStuff7));

		
		}

		/// <summary>
		/// Sends the file.
		/// </summary>
		/// <param name='fileName'>
		/// File name.
		/// </param>
		/// <param name='fileSize'>
		/// File size.
		/// </param>
		/// <param name='tl'>
		/// Tl.
		/// </param>
		private void sendFile(String fileName, long fileSize, Transport transport)
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
			new file_server();
		}
	}
}