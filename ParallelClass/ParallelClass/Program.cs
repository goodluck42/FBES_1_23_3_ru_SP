// int[] arr = new int[32];
//
// var result = Parallel.For(0, arr.Length, (i, state) =>
// {
// 	arr[i] = i * 10;
// 	
// 	state.Stop();
// 	
// 	Console.WriteLine(i);
// });
//
// foreach (var i in arr)
// {
// 	Console.WriteLine(i);
// }

var list = new List<Action>();

for (int i = 0; i < 20; i++)
{
	int copy = i;
	
	list.Add(() =>
	{
		Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}");
		Console.WriteLine(copy);
	});
}

// foreach (var action in list)
// {
// 	action.Invoke();
// }

// await Task.WhenAny(Task.Run(() => {}), Task.Run(() => {}))

Parallel.Invoke([..list]);

// int[] arr = new int[32];
//
// var task = Parallel.ForAsync(0, arr.Length, (i, token) =>
// {
// 	arr[i] = i * 10;
// 	
// 	token.ThrowIfCancellationRequested();
// 	
// 	Console.WriteLine(i);
// 	
// 	return ValueTask.CompletedTask;
// });
//
// foreach (var i in arr)
// {
// 	Console.WriteLine(i);
// }