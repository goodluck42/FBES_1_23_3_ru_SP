var nums = Enumerable
	.Range(1, 10)
	.Select(_ => Random.Shared.Next(10, 99))
	.ToList();

foreach (var num in nums)
{
	Console.WriteLine(num);
}

Console.WriteLine("----------");

var results = nums.AsParallel().AsOrdered().Select(num =>
{
	// Console.WriteLine($"ThreadId: {Thread.CurrentThread.ManagedThreadId}");
	
	return num * 2;
});

foreach (var result in results)
{
	Console.WriteLine(result);
}


