using EasyNetQ;
using Messages;
using System;

namespace Publisher
{
	internal static class Program
	{
		private static void Main()
		{
			using (var bus = RabbitHutch.CreateBus("host=localhost"))
			{
				var input = "";
				Console.WriteLine("Enter a message. 'Quit' to quit.");
				while ((input = Console.ReadLine())?.ToUpper() != "QUIT")
				{
					bus.Publish(new TextMessage { Text = input });
				}
			}
		}
	}
}