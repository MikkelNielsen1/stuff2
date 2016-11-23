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
		bool IsPrevByteB = false;
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
			List<byte> bytelist = new List<byte> ();

			bytelist.Insert (0, Convert.ToByte('A'));

			foreach (var item in buf) 
			{
				if (item == 'A') 
				{
					bytelist.Add (Convert.ToByte('B'));
					bytelist.Add (Convert.ToByte('C'));
				}
				else if(item == 'B') 
				{
					bytelist.Add (Convert.ToByte('B'));
					bytelist.Add (Convert.ToByte('D'));
				}
				else
				{
					bytelist.Add (Convert.ToByte(item));
				}
			}

			bytelist.Add (Convert.ToByte('A'));
			byte[] sendBuffer = bytelist.ToArray();

			serialPort.Write (sendBuffer, 0, sendBuffer.Length);

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
			byte currentByte;
			List<byte> readBytes = new List<byte>();

			while ((currentByte = serialPort.ReadByte ()) != -1) 
			{
				if (currentByte == Convert.ToByte('A'))
					continue;
				
				if (IsPrevByteB == false) 
				{
					if (currentByte == Convert.ToByte('B')) 
					{
						IsPrevByteB = true;
					} 
					else 
					{
						readBytes.Add(Convert.ToByte(currentByte));
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
			

			buf = readBytes;
			return readBytes.LastIndexOf - 1;
		}
	}
}
