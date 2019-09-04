using System;
using System.Threading.Tasks;
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
				bus.SubscribeAsync<TextMessage>( "test", message => Task.Factory.StartNew(() =>
                {
                    // Perform some actions here
                    // If there is a exception it will result in a task complete but task faulted which
                    // is dealt with below in the continuation
                }).ContinueWith(task =>
                {
                    if (task.IsCompleted && !task.IsFaulted)
                    {
                        // Everything worked out ok
                        HandleTextMessage(message);
                    }
                    else
                    {
                        // Don't catch this, it is caught further up the hierarchy and results in being sent to the default error queue
                        // on the broker
                        throw new EasyNetQException("Message processing exception - look in the default error queue (broker)");
                    }
                }));

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
