using System;
using System.IO.Ports;

/// <summary>
/// Link.
/// </summary>
using System.Collections.Generic;


namespace Linklaget
{
	/// <summary>
	/// Link.
	/// </summary>
	public class Link
	{

		/// <summary>
		/// The DELIMITE for slip protocol.
		/// </summary>
		const byte DELIMITER = (byte)'A';
		/// <summary>
		/// The buffer for link.
		/// </summary>
		private byte[] buffer;
		/// <summary>
		/// The serial port.
		/// </summary>
		SerialPort serialPort;

		/// <summary>
		/// Initializes a new instance of the <see cref="link"/> class.
		/// </summary>
		public Link (int BUFSIZE)
		{
			// Create a new SerialPort object with default settings.
			serialPort = new SerialPort("/dev/ttyS1",115200,Parity.None,8,StopBits.One);

			if(!serialPort.IsOpen)
				serialPort.Open();

			buffer = new byte[(BUFSIZE*2)];

			//serialPort.ReadTimeout = 200;
			serialPort.DiscardInBuffer ();
			serialPort.DiscardOutBuffer ();
		}

		/// <summary>
		/// Send the specified buf and size.
		/// </summary>
		/// <param name='buf'>
		/// Buffer.
		/// </param>
		/// <param name='size'>
		/// Size.
		/// </param>
		public void send (byte[] buf, int size)
		{
			

			buffer[0] = Convert.ToByte('A');

			int bufferIndex = 1;
			for (int i = 0; i < size; i++) 
			{
				if (buf [i] == 'A') 
				{
					buffer [bufferIndex] = Convert.ToByte ('B');
					buffer [bufferIndex + 1] = Convert.ToByte ('C');
					Console.WriteLine ("Inserting A " + buf[i] + " with " + buffer [bufferIndex] + buffer [bufferIndex+1]);
					bufferIndex += 2;
				}
				else if(buf[i] == 'B') 
				{
					buffer[bufferIndex] = Convert.ToByte('B');
					buffer [bufferIndex + 1] = Convert.ToByte('D');
					Console.WriteLine ("Inserting B " + buf [i] + " with " + buffer [bufferIndex] + buffer [bufferIndex+1]);
					bufferIndex += 2;
				}
				else
				{
					buffer[bufferIndex] = buf[i];
					Console.WriteLine ("Inserting " + buf [i]);
					bufferIndex++;
				}
			}

			buffer [bufferIndex] = Convert.ToByte ('A');

			serialPort.Write (buffer, 0, bufferIndex+1);

		}

		/// <summary>
		/// Receive the specified buf and size.
		/// </summary>
		/// <param name='buf'>
		/// Buffer.
		/// </param>
		/// <param name='size'>
		/// Size.
		/// </param>
		public int receive (ref byte[] buf)
		{
			bool IsPrevByteB = false;
			bool DetectedA = false;
			int currentByte;
			List<byte> readBytes = new List<byte>();
			Console.Write ("Reading bytes: ");
			while ((currentByte = serialPort.ReadByte ()) != -1) 
			{
				if (currentByte == Convert.ToByte ('A') && !DetectedA) 
				{
					DetectedA = true;
					continue;
				}
				else if (currentByte == Convert.ToByte ('A') && DetectedA)
				{
					break;
				}
				
				if (IsPrevByteB == false) 
				{
					if (currentByte == Convert.ToByte('B')) 
					{
						IsPrevByteB = true;
					} 
					else 
					{
						readBytes.Add(Convert.ToByte((byte)currentByte));
					}


				} 
				else if (IsPrevByteB == true) 
				{
					
					if (currentByte == Convert.ToByte('C')) 
					{
						readBytes.Add (Convert.ToByte ('A'));
						IsPrevByteB = false;
					} 
					else if (currentByte == Convert.ToByte('D')) 
					{
						readBytes.Add (Convert.ToByte ('B'));
						IsPrevByteB = false;
					}
				}


			}
			Console.WriteLine ("Done reading bytes");

			buf = readBytes.ToArray();
			return readBytes.Count;
		}
	}
}
