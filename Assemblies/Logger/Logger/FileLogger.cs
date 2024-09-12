namespace Logger;

public class FileLogger(string filePath) : ILogger
{
	public string FilePath => filePath;
	
	public void Log(LogType logType, string message)
	{
		File.AppendAllText(FilePath, GetLogText(logType, message));
	}

	public Task LogAsync(LogType logType, string message)
	{
		return File.AppendAllTextAsync(FilePath, GetLogText(logType, message));
	}

	private static string GetLogText(LogType logType, string message)
	{
		return $"[{DateTime.Now}] {logType}: {message}\n";
	}
}