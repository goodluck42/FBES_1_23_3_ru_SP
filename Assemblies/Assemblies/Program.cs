using System.Reflection;
using System.Runtime.Loader;
using Microsoft.VisualBasic.CompilerServices;

var plugin = new FileLoggerPlugin("MyCtx", "logs.txt");

plugin.LoadAssembly();
plugin.Init();

await plugin.LogAsync(plugin.LogType["Error"], "an error occured!");
await plugin.LogAsync(plugin.LogType["Warning"], "WARNING!");

public sealed class FileLoggerPlugin
{
	private Dictionary<string, object>? _logType;
	
	public Dictionary<string, object> LogType => _logType ??= new();
	
	private const string LoggerClassName = "FileLogger";
	
	private readonly AssemblyLoadContext _ctx;
	private Assembly? _loggerAssembly;
	private object? _loggerInstance;
	private Type? _loggerType;
	private MethodInfo? _logMethod;
	private MethodInfo? _logAsyncMethod;
	private readonly  string _loggerPath;
	
	public FileLoggerPlugin(string contextName, string loggerPath)
	{
		_ctx = new AssemblyLoadContext(contextName, true);
		_loggerPath = loggerPath;
	}
	
	public bool LoadAssembly()
	{
		try
		{
			using var fileStream = new FileStream("net8.0/Logger.dll", FileMode.Open, FileAccess.Read);
			_loggerAssembly = _ctx.LoadFromStream(fileStream);

			return true;
		}
		catch (ArgumentNullException)
		{
			return false;
		}
	}

	public void Init()
	{
		if (_loggerAssembly is null)
		{
			return;
		}

		var types = _loggerAssembly.GetTypes();

		// class FileLogger;
		_loggerType = types.FirstOrDefault(t => t.Name == LoggerClassName);

		if (_loggerType is null)
		{
			return;
		}
		
		// new FileLogger(_loggerType, _loggerPath);
		_loggerInstance = Activator.CreateInstance(_loggerType, _loggerPath);
		
		if (_loggerInstance is null)
		{
			return;
		}
		
		// fileLogger.Log
		_logMethod = _loggerType.GetMethod("Log", BindingFlags.Public | BindingFlags.Instance);
		// fileLogger.LogAsync
		_logAsyncMethod = _loggerType.GetMethod("LogAsync", BindingFlags.Public | BindingFlags.Instance);

		if (_logMethod is null || _logAsyncMethod is null)
		{
			return;
		}
		
		var @enum = types.FirstOrDefault(t => t.IsEnum && t.Name == "LogType");

		if (@enum is null)
		{
			return;
		}

		var enumNames = @enum.GetEnumNames();
		var enumValues = @enum.GetEnumValues();

		int i = 0;
		
		foreach (var enumName in enumNames)
		{
			var value = enumValues.GetValue(i++);

			if (value is null)
			{
				return;
			}
			
			LogType[enumName] = value;
		}
	}
	
	public void Log(object logType, string message)
	{
		if (_logMethod is null || _loggerInstance is null)
		{
			return;
		}
		
		// fileLogger.Log(logType, message);
		_logMethod.Invoke(_loggerInstance, [logType, message]);
	}

	public Task LogAsync(object logType, string message)
	{
		if (_logAsyncMethod is null || _loggerInstance is null)
		{
			return Task.CompletedTask;
		}
		// ! - null forgiving
		// fileLogger.LogAsync(logType, message);
		return (Task)_logAsyncMethod.Invoke(_loggerInstance, [logType, message])!;
	}
}

// Plugin == Assembly
// public abstract class Plugin
// {
// 	public object Invoke(string className, string methodName, params object?[]? args)
// 	{
// 		// ...
// 	}
// }
//
// public sealed class LoggerPlugin : Plugin
// {
// 	public void Log(object logType, string message)
// 	{
// 		
// 	}
// }
//
// public interface IPluginObjectInstance
// {
// 	
// }
//
// public interface IPlugin
// {
// 	IPluginObjectInstance CreateInstance();
// 	void SetProperty(IPluginObjectInstance instance, string propertyName, object propertyValue);
// 	object GetProperty(IPluginObjectInstance instance, string propertyName);
// 	object InvokeMethod(IPluginObjectInstance instance, string methodName, params object?[]? args);
// 	object InvokeStaticMethod(string className, string methodName, params object?[]? args);
// }

