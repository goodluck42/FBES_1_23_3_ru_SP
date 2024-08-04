using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.VisualBasic;

namespace SourceGenerator;

internal class DbContextEntityFilter(string dbContextBaseFullName) : IDbContextEntityFilter
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


		if (typeSymbol.BaseType is null)
		{
			return Result.Fail();
		}

		if (typeSymbol.BaseType.ToString() != dbContextBaseFullName)
		{
			return Result.Fail();
		}

		var properties = classDeclaration.ChildNodes().Where(n => n is PropertyDeclarationSyntax)
			.Cast<PropertyDeclarationSyntax>();

		// var semanticModels 

		IPropertySymbol[] propertySymbols = properties
			.Select(p => context.SemanticModel.GetDeclaredSymbol(p))
			.Where(t => t is not null)
			.Cast<IPropertySymbol>()
			.ToArray();

		var listClassNames = new List<string>();

		foreach (var property in propertySymbols)
		{
			var split = property.Type.ToString().Split('<', '>');


			if (split.Length != 3)
			{
				continue;
			}

			listClassNames.Add(split[1]);
		}

		return new DbContextEntityResult
		{
			Success = true,
			EntityFullClassNames = listClassNames
		};

		// return new DbContextEntityResult
		// {
		// 	EntityFullClassName = inf,
		// 	EntityClassName
		// 	Success = true
		// };
	}
}