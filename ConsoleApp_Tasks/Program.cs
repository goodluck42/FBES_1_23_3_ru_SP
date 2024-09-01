using System.Diagnostics;
using Stopwatch = ConsoleApp_Tasks.Stopwatch;

internal partial class Program
{
	static void Main(string[] args)
	{
		var stopwatch = new Stopwatch
		{
			Callback = timespan =>
			{
				Console.WriteLine(timespan.TotalMilliseconds);
				Console.Clear();
			},
			Interval = 100
		};
		
		
		stopwatch.Start();

		Console.Read();


		// #1
		// var tasks = new List<Task>();
		// for (int i = 0; i < 100; i++)
		// {
		// 	tasks.Add(DoSomeOperationAsync());
		// }
		//
		// await Task.WhenAny(tasks);
		//
		// Console.Read();
	}

	// static Task DoSomeOperationAsync()
	// {
	// 	// Task.Run()
	// 	return Task.Factory.StartNew(async () =>
	// 	{
	// 		Console.WriteLine($"Task.Run: {Thread.CurrentThread.ManagedThreadId}");
	// 		while (true)
	// 		{
	// 			await Task.Delay(Random.Shared.Next(100, 500));
	// 		}
	// 	}, TaskCreationOptions.LongRunning);
	// }
}