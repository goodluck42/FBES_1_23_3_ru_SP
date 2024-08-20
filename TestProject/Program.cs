namespace TestProject;

public class Program
{
	static void Main(string[] args)
	{
		using var file = File.CreateText("data.txt");
		
		int i = 0;
		foreach (var arg in args)
		{
			
			file.WriteLine($"{i++}: {arg}");
		}
	}
}