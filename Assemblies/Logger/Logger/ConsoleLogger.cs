namespace Logger;

public class ConsoleLogger : ILogger
{
	public void Log(LogType logType, string message)
	{
		Console.WriteLine($"[{DateTime.Now}] {logType}: {message}");
	}

	public Task LogAsync(LogType logType, string message)
	{
		return Task.Run(() => Log(logType, message));
	}
}