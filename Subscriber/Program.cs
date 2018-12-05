using System;
using EasyNetQ;
using EasyNetQ.NonGeneric;
using Messages;

namespace Subscriber
{
	internal static class Program
	{
		static void Main()
		{
			using( var bus = RabbitHutch.CreateBus( "host=localhost" ) )
			{
				bus.Subscribe<TextMessage>( "test", HandleTextMessage );

				Console.WriteLine( "Listening for messages. Hit <return> to quit." );
				Console.ReadLine();
			}
		}

		static void HandleTextMessage( TextMessage textMessage )
		{
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine( $"Got message {textMessage.Text}" );
		}
	}
}
