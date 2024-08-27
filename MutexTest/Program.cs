if (Mutex.TryOpenExisting("MyMutex", out var mutex))
{
	Console.WriteLine("Waiting...");
	mutex.WaitOne();
	Console.WriteLine("Application has completed a task");
}

Console.Read();