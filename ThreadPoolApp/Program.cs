ThreadPool.QueueUserWorkItem(ThreadAction, 8);
ThreadPool.QueueUserWorkItem(ThreadAction, 16);
ThreadPool.QueueUserWorkItem(ThreadAction, 32);

// new Thread(ThreadAction).Start(8);
// new Thread(ThreadAction).Start(16);
// new Thread(ThreadAction).Start(32);

// Console.Read();


return;

void ThreadAction(object? state)
{
	int iterations = (int)state!;

	for (int i = 0; i < iterations; i++)
	{
		Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]: {i}");
		Thread.Sleep(Random.Shared.Next(50, 100));
	}
}