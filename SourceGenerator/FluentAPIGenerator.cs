using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;

namespace SourceGenerator;

[Generator]
internal class FluentAPIGenerator : ISourceGenerator
{

	private const string DbContextBaseFullName = "Microsoft.EntityFrameworkCore.DbContext";
	
	public void Initialize(GeneratorInitializationContext context)
	{
		context.RegisterForSyntaxNotifications(() => new SyntaxContextReceiver(new DbContextEntityFilter(DbContextBaseFullName)));
	}

	public void Execute(GeneratorExecutionContext context)
	{
		if (context.SyntaxContextReceiver is not SyntaxContextReceiver contextReceiver)
		{
			return;
		}

		var entityClassNames = contextReceiver.Results
			.Where(r => r is DbContextEntityResult)
			.Cast<DbContextEntityResult>()
			.ToArray();
		
		//////////////////////
		var debugResults = contextReceiver.Results
			.Where(r => r is DebugResult)
			.Cast<DebugResult>()
			.ToArray();


		int i = 0;
		foreach (var debugResult in debugResults)
		{
			context.AddSource($"Debug_{i++}.g.cs", $@"
/*
{debugResult.Info}
*/");
		}
	}
}