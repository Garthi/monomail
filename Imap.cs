using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using create;

namespace monomail
{
	public class Imap
	{
		private Socket cSocket;
		private int defaultPort = 143;
		private string defaultHost = "localhost";
		private string result;

		public Imap ()
		{
			this.Connect (defaultHost, defaultPort);
		}

		public Imap (string host)
		{
			this.Connect (host, defaultPort);
		}

		public Imap(string host, int port)
		{
			this.Connect (host, port);
		}

		private void Connect(string host, int port)
		{
			try {
				cSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

				//cSocket.ReceiveTimeout = 15;
				//cSocket.SendTimeout = 15;

				cSocket.Connect (host, port);

				if (cSocket.Connected == false) {
					throw new Exception("Connected fail");
				}

				byte[] bigBuffer = new byte[24];

				int receiveCount = 0;

				//System.Threading.Thread.Sleep(5);
				int BufferSize = 0;

				do {
					receiveCount = cSocket.Receive (bigBuffer);//, SocketFlags.None);

					System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

					BufferSize = cSocket.Available;

					Console.WriteLine ("ReadyForRead: " + BufferSize.ToString());

					string result = enc.GetString(bigBuffer);

					this.result += result;

					//Console.WriteLine (result);

				} while (receiveCount < BufferSize);

				Console.WriteLine(this.result);

				cSocket.Close();

			} catch (Exception ex) {
				Console.WriteLine (ex.ToString());
			}
		}

		public string getResult()
		{
			return this.result;
		}
	}
}

