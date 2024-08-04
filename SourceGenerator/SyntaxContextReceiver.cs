using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SourceGenerator;

internal class SyntaxContextReceiver(IDbContextEntityFilter filter) : ISyntaxContextReceiver
{
	private readonly List<Result> _results = new();

	public IDbContextEntityFilter DbContextEntityFilter => filter;
	public IEnumerable<Result> Results => _results;
	
	public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
	{
		var result = DbContextEntityFilter.Filter(context);

		if (result)
		{
			_results.Add(result);
		}
	}
}