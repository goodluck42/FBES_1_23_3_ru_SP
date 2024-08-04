using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace SourceGenerator;

internal interface IAttributeFilter
{
	AttributeResult Filter(GeneratorSyntaxContext context);
}

internal class AttributeFilter : IAttributeFilter
{
	public AttributeResult Filter(GeneratorSyntaxContext context)
	{
		
	}
}

internal class SyntaxContextReceiver(IAttributeFilter filter) : ISyntaxContextReceiver
{
	private readonly List<AttributeResult> _results = new();

	public IAttributeFilter AttributeFilter => filter;
	public IEnumerable<AttributeResult> Results => _results;
	
	public void OnVisitSyntaxNode(GeneratorSyntaxContext context)
	{
		var result = AttributeFilter.Filter(context);

		if (result)
		{
			_results.Add(result);
		}
	}
}