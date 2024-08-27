Thread[] threads = [new(ThreadAction), new (ThreadAction)];

foreach (Thread thread in threads)
{
	thread.Start();
}

foreach (Thread thread in threads)
{
	thread.Join();
}

Console.Read();

void ThreadAction()
{
	for (int i = 0; i < 5; i++)
	{
		Console.WriteLine($"ThreadId: [{Thread.CurrentThread.ManagedThreadId}]: {i}");
	}
}


public class MyService
{
	private Thread? _myThread;
	
	public void Init()
	{
		_myThread = new Thread(() => { });
	}
	
	public void DoAction()
	{
		_myThread?.Start();
	}
}