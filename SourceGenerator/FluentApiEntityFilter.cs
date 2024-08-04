using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SourceGenerator;

internal class PropertyInfo
{
	public IEnumerable<AttributeData>? Attributes { get; set; }
	public string? Name { get; set; }
}

internal class FluentApiEntityResult : Result
{
	public required IEnumerable<PropertyInfo> PropertyInfos { get; set; }
	public required string FullClassName { get; init; }
}

internal interface IFluentApiEntityFilter
{
	Result Filter(GeneratorSyntaxContext context);
}

internal class FluentApiEntityFilter(string lookupNamespace) : IFluentApiEntityFilter
{
	public Result Filter(GeneratorSyntaxContext context)
	{
		if (context.Node is not ClassDeclarationSyntax classDeclaration)
		{
			return Result.Fail();
		}

		if (context.SemanticModel.GetDeclaredSymbol(classDeclaration) is not ITypeSymbol typeSymbol)
		{
			return Result.Fail();
		}

		if (typeSymbol.ContainingNamespace.ToString() != lookupNamespace)
		{
			return Result.Fail();
		}

		var attributes = typeSymbol.GetAttributes();

		foreach (var attribute in attributes)
		{
			
		}



		return new FluentApiEntityResult
		{
			Success = true,
			FullClassName = typeSymbol.ToString(),
		};
	}
}