using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

var processes = Process.GetProcesses();

foreach (var process in processes)
{
	try
	{
		Console.WriteLine($"Id = {process.Id}");
		Console.WriteLine($"ProcessName = {process.ProcessName}");
		Console.WriteLine($"MachineName = {process.MachineName}");
		Console.WriteLine($"Threads.Count = {process.Threads.Count}");
		Console.WriteLine($"BasePriority = {process.BasePriority}");
		Console.WriteLine($"PriorityClass = {process.PriorityClass}");
		Console.WriteLine($"ProcessorAffinity = {process.ProcessorAffinity}");
	}
	catch (Win32Exception ex)
	{
		Console.WriteLine("No access");
	}
	
	Console.WriteLine("---------------------------");
	
}

