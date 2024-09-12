namespace Logger;

public interface ILogger
{
	void Log(LogType logType, string message);
	Task LogAsync(LogType logType, string message);
}