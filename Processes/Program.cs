using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

var processes = Process.GetProcesses();

// foreach (var process in processes)
// {
// 	try
// 	{
// 		Console.WriteLine($"Id = {process.Id}");
// 		Console.WriteLine($"ProcessName = {process.ProcessName}");
// 		Console.WriteLine($"MachineName = {process.MachineName}");
// 		Console.WriteLine($"Threads.Count = {process.Threads.Count}");
// 		Console.WriteLine($"BasePriority = {process.BasePriority}");
// 		Console.WriteLine($"PriorityClass = {process.PriorityClass}");
// 		Console.WriteLine($"ProcessorAffinity = {process.ProcessorAffinity}");
// 	}
// 	catch (Win32Exception ex)
// 	{
// 		Console.WriteLine("No access");
// 	}
// 	
// 	Console.WriteLine("---------------------------");
// 	
// }

var teamsProcess = Process.GetProcessById(7540);

teamsProcess.PriorityClass = ProcessPriorityClass.Normal;
// CPU 0 - 1
// CPU 1 - 2
// CPU 2 - 4
// CPU 3 - 8
teamsProcess.ProcessorAffinity = (1 << 14) | (1 << 10);

var timespan = new TimeSpan(days: 0, hours: 0, minutes:0, seconds:0, microseconds:0, milliseconds: Environment.TickCount);

Console.WriteLine(timespan.Days);
Console.WriteLine(timespan.Hours);
Console.WriteLine(timespan.Minutes);
Console.WriteLine($"ProcessorCount: {Environment.ProcessorCount}");
Console.WriteLine($"TEMP: {Environment.GetEnvironmentVariable("TEMP")}");

// Start a process

// Process.Start("notepad.exe");

var myProcessInfo = new ProcessStartInfo();
var myProcess = new Process
{
	StartInfo = myProcessInfo
};
// myProcess.StandardError std::cerr
// myProcess.StandardInput std::cin
// myProcess.StandardOutput std::cout

myProcessInfo.FileName = @"C:\Users\Alex\RiderProjects\FBES_1_23_3_ru_SP\TestProject\bin\Debug\net8.0\TestProject.exe";
myProcessInfo.ArgumentList.Add("data1");
myProcessInfo.ArgumentList.Add("data2");
myProcessInfo.WindowStyle = ProcessWindowStyle.Maximized;
myProcessInfo.RedirectStandardInput = false;
myProcessInfo.RedirectStandardOutput = false;
myProcessInfo.RedirectStandardError = false;

myProcess.Start();

Console.Read();