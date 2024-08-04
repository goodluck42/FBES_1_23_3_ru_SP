using Microsoft.CodeAnalysis;

namespace SourceGenerator;

internal interface IDbContextEntityFilter
{
	Result Filter(GeneratorSyntaxContext context);
}