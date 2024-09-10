var cts = new CancellationTokenSource();

Task<int> task = Task.Run(async () =>
{
	int sum = 0;
	
	for (int i = 0; i < 5; i++)
	{
		await Task.Delay(1000);
		
		cts.Token.ThrowIfCancellationRequested();
		
		sum += i;
	}
	
	return sum;
	
}, cts.Token);

_ = Task.Run(async () =>
{
	await Task.Delay(3000);
	
	cts.Cancel();
});


try
{
	int result = await task;

	Console.WriteLine(result);
}
catch (OperationCanceledException ex) 
{
	Console.WriteLine(ex.Message);
}
finally
{
	// cts.Dispose();
}

Console.Read();