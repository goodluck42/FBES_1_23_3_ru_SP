internal class Program
{
	static async Task Main(string[] args)
	{
		Console.WriteLine($"ThreadId (Main): {Thread.CurrentThread.ManagedThreadId}");

		var manualResetEvent = new ManualResetEventSlim(false);

		// #1
		// await Counter(10); // -> thread start -> join
		// await Counter(5); // -> thread start -> join
		// await Counter(3); // -> thread start -> join

		// #2

		// var task1 = Counter2(10, "cnt1"); // -> thread start
		// var task2 = Counter2(5, "cnt2"); // -> thread start
		// var task3 = Counter2(3, "cnt3"); // -> thread start
		//
		// await task1; // -> join
		// await task2; // -> join
		// await task3; // -> join

		// #3

		// await Counter2(10); // -> thread start -> join
		// await Counter2(5); // -> thread start -> join
		// await Counter2(3); // -> thread start -> join

		// #4

		// var task1 = Counter(10, "cnt1"); // -> thread start
		// var task2 = Counter(5, "cnt2"); // -> thread start
		// var task3 = Counter(3, "cnt3"); // -> thread start
		//
		// await task1; // -> join
		// await task2; // -> join
		// await task3; // -> join

		// #5
		// await Counter(10, "cnt1");
		// await Counter(5, "cnt2");
		// await Counter(3, "cnt3");
		// await Counter(9, "cnt4");

		// Fire and forget
		// _ = Counter(10);

		// Console.WriteLine($"ThreadId (AfterMain): {Thread.CurrentThread.ManagedThreadId}");
		////////////////

		var task1 = Counter(10, "cnt1");
		var task2 = Counter(5, "cnt2");
		var task3 = Counter(3, "cnt3");
		
		await task1;
		Console.WriteLine($"ThreadId (AfterTask1): {Thread.CurrentThread.ManagedThreadId}");
		await task2;
		Console.WriteLine($"ThreadId (AfterTask2): {Thread.CurrentThread.ManagedThreadId}");
		await task3;
		Console.WriteLine($"ThreadId (AfterTask3): {Thread.CurrentThread.ManagedThreadId}");

		// await Counter(10, "cnt1");
		// Console.WriteLine($"ThreadId (AfterTask1): {Thread.CurrentThread.ManagedThreadId}");
		// await Counter(5, "cnt2");
		// Console.WriteLine($"ThreadId (AfterTask2): {Thread.CurrentThread.ManagedThreadId}");
		// await Counter(3, "cnt3");
		// Console.WriteLine($"ThreadId (AfterTask3): {Thread.CurrentThread.ManagedThreadId}");

		manualResetEvent.Wait();
		Console.Read();
	}

	private static async Task Counter(int to, string? name = null)
	{
		// async await for input bound
		Console.WriteLine($"ThreadId (Counter): {Thread.CurrentThread.ManagedThreadId}");
		for (int i = 0; i < to; i++)
		{
			await Task.Delay(200);
			Console.WriteLine($"{name ?? string.Empty}: {i}");
		}
	}

	private static Task Counter2(int to, string? name = null)
	{
		// Task.Run for parralelism (CPU bound)
		return Task.Run(async () =>
		{
			Console.WriteLine($"ThreadId (Counter): {Thread.CurrentThread.ManagedThreadId}");
			for (int i = 0; i < to; i++)
			{
				await Task.Delay(100);
				Console.WriteLine($"{name ?? string.Empty}: {i}");
			}
		});
	}
}