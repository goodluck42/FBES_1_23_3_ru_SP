using System.Reflection;
using System.Runtime.Loader;

var alc = new AssemblyLoadContext("MyContext", true);
using var fileStream = new FileStream("net8.0/Logger.dll", FileMode.Open, FileAccess.Read);
var assembly = alc.LoadFromStream(fileStream);

alc.Unload();


public sealed class FileLoggerPlugin
{
	private const string LoggerClassName = "FileLogger";
	
	private AssemblyLoadContext _ctx;
	private Assembly? _loggerAssembly;
	private object? _loggerInstance;
	private Type? _loggerType;
	private MethodInfo? _logMethod;
	private MethodInfo? _logAsyncMethod;
	private string _loggerPath;
	
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

		_loggerType = types.FirstOrDefault(t => t.Name == "FileLogger");

		if (_loggerType is null)
		{
			return;
		}
		
		_loggerInstance = Activator.CreateInstance(_loggerType, _loggerPath);

		if (_loggerInstance is null)
		{
			return;
		}
		
		_logMethod = _loggerType.GetMethod("Log", BindingFlags.Public | BindingFlags.Instance);
		_logAsyncMethod = _loggerType.GetMethod("LogAsync", BindingFlags.Public | BindingFlags.Instance);
	}
	
	public void LogInfo()
	{
		
	}
}