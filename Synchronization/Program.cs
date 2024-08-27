//// Semaphore & SemaphoreSlim

// var manualResetEvent = new ManualResetEventSlim(false);
// var semaphore = new SemaphoreSlim(3, 3);
// // ic = 3
// // ic--
// // ic--
// // ic--
// // ic++
// for (int i = 0; i < 100; i++)
// {
// 	ThreadPool.QueueUserWorkItem(_ =>
// 	{
// 		semaphore.Wait();
// 		for (int j = 0; j < 5; j++)
// 		{
// 			Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]: {j}");
// 			Thread.Sleep(Random.Shared.Next(500, 600));
// 		}
// 		semaphore.Release();
// 	});
// }
//
// manualResetEvent.Wait();


//// ManualResetEvent #2
// var manualResetEvent = new ManualResetEventSlim(false);
// var manualResetEvent2 = new ManualResetEventSlim(false);
//
// manualResetEvent.Reset();
// manualResetEvent2.Reset();
//
// ThreadPool.QueueUserWorkItem(_ =>
// {
// 	int i = 0;
// 	while (true)
// 	{
// 		manualResetEvent.Wait();
// 		Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]: {i++}");
// 		Thread.Sleep(1000);
// 	}
// });
//
// ThreadPool.QueueUserWorkItem(_ =>
// {
// 	int i = 0;
// 	while (true)
// 	{
// 		manualResetEvent2.Wait();
// 		Console.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}]: {i++}");
// 		Thread.Sleep(1000);
// 	}
// });
//
//
// while (true)
// {
// 	string? message = Console.ReadLine();
// 	
// 	if (message == "r1")
// 	{
// 		manualResetEvent.Reset();
// 	}
// 	
// 	if (message == "r2")
// 	{
// 		manualResetEvent2.Reset();
// 	}
//
// 	if (message == "s1")
// 	{
// 		manualResetEvent.Set();
// 	}
// 	
// 	if (message == "s2")
// 	{
// 		manualResetEvent2.Set();
// 	}
// }
//// ManualResetEvent #1
// var manualResetEvent = new ManualResetEventSlim(false);
//
// manualResetEvent.Set();
//
// ThreadPool.QueueUserWorkItem(_ =>
// {
// 	int i = 0;
// 	while (manualResetEvent.IsSet)
// 	{
// 		Console.WriteLine(i++);
// 		Thread.Sleep(1000);
// 	}
// });
//
// string? message = Console.ReadLine();
//
// if (message == "r")
// {
// 	manualResetEvent.Reset();
// }

//// List safety and thread safe

// var manualResetEvent = new ManualResetEventSlim(false);
// var list = new List<int>();
// //
// object o = new();
// ThreadPool.QueueUserWorkItem(AddItems);
// ThreadPool.QueueUserWorkItem(AddItems);
// ThreadPool.QueueUserWorkItem(AddItems);
//
// // for (int i = 0; i < 3; i++)
// // {
// // 	Console.WriteLine(i);
// // 	manualResetEvent.Reset(); // o = false
// // 	manualResetEvent.Wait(); // if o == true -> non block; if o == false -> block
// // 	manualResetEvent.Set(); // o = true
// // }
//
// manualResetEvent.Wait();
//
// void AddItems(object? state)
// {
// 	lock (o) // there is error without lock
// 	{
// 		for (int i = 0; i < 1_000_000; i++)
// 		{
// 			list.Add(i);
// 		}
// 	}
// }

//// Mutex
// int counter = 0;
// var mutex = new Mutex(false, "MyMutex");
//
// ThreadPool.QueueUserWorkItem(IncreaseCounter, new
// {
// 	Step = 2,
// 	Count = 200
// });
//
// ThreadPool.QueueUserWorkItem(IncreaseCounter, new
// {
// 	Step = 1,
// 	Count = 100
// });
//
// _ = Console.ReadLine();
// Console.WriteLine("Mutex owned!");
// mutex.WaitOne();
//
//
// var action = Console.ReadLine();
//
// if (action == "release")
// {
// 	mutex.ReleaseMutex();
// 	Console.WriteLine("released");
// }
//
// Console.ReadLine();
//
// void IncreaseCounter(object? state)
// {
// 	Console.WriteLine($"IncreaseCounter: {Thread.CurrentThread.ManagedThreadId}");
// 	dynamic dynamicState = state!;
//
// 	mutex.WaitOne();
// 	Console.WriteLine($"Entered: {Thread.CurrentThread.ManagedThreadId}");
// 	
// 	for (int i = 0; i < dynamicState.Count; i++)
// 	{
// 		counter += dynamicState.Step;
// 	}
//
// 	mutex.ReleaseMutex();
// 	Console.WriteLine($"Exited: {Thread.CurrentThread.ManagedThreadId}");
// 	// o = true;
// }

///// Monitor and lock

// int counter = 0;
// object o = new();
//
// ThreadPool.QueueUserWorkItem(IncreaseCounter, new
// {
// 	Step = 2,
// 	Count = 200
// });
//
// ThreadPool.QueueUserWorkItem(IncreaseCounter, new
// {
// 	Step = 1,
// 	Count = 100
// });
//
// // IncreaseCounter(new
// // {
// // 	Step = 1,
// // 	Count = 100
// // });
//
// // IncreaseCounter(new
// // {
// // 	Step = 2,
// // 	Count = 200
// // });
//
//
//
// Console.Read();
// Console.WriteLine(counter);
// Console.Read();
//
// // o = false
// void IncreaseCounter(object? state)
// {
// 	Console.WriteLine($"IncreaseCounter: {Thread.CurrentThread.ManagedThreadId}");
// 	dynamic dynamicState = state!;
//
// 	Monitor.Enter(o);
// 	// while(!o)
// 	// {
// 	// }
//
// 	// o = false;
// 	Console.WriteLine($"Entered: {Thread.CurrentThread.ManagedThreadId}");
// 	
// 	for (int i = 0; i < dynamicState.Count; i++)
// 	{
// 		counter += dynamicState.Step;
// 	}
//
// 	Monitor.Exit(o);
// 	Console.WriteLine($"Exited: {Thread.CurrentThread.ManagedThreadId}");
// 	// o = true;
// }
//
// // o = false
// void IncreaseCounterWithLock(object? state)
// {
// 	Console.WriteLine($"IncreaseCounter: {Thread.CurrentThread.ManagedThreadId}");
// 	dynamic dynamicState = state!;
//
// 	lock (o)
// 	{
// 		// Monitor.Enter(o);
// 		// while(!o)
// 		// {
// 		// }
//
// 		// o = false;
// 		Console.WriteLine($"Entered: {Thread.CurrentThread.ManagedThreadId}");
// 	
// 		for (int i = 0; i < dynamicState.Count; i++)
// 		{
// 			counter += dynamicState.Step;
// 		}
// 		// Monitor.Exit(o);
// 		// o = true;
// 	}
// 	
// 	Console.WriteLine($"Exited: {Thread.CurrentThread.ManagedThreadId}");
// 	
// }


//// Interlocked
// int counter = 0;
//
// List<Thread> threads = [new(IncreaseCounter), new(IncreaseCounter), new(IncreaseCounter)];
//
// int i = 1;
// foreach (var thread in threads)
// {
// 	thread.Start(1_000_000 * i++);
// }
//
//
// foreach (var thread in threads)
// {
// 	thread.Join();
// }
// Console.WriteLine(counter);
//
// Console.Read();
//
// return;
//
// void IncreaseCounter(object? state)
// {
// 	int iterations = (int)state!;
//
// 	for (int i = 0; i < iterations; i++)
// 	{
// 		Interlocked.Increment(ref counter);
// 	}
// }