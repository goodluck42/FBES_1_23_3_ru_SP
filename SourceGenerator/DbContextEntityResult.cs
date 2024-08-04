using System.Collections.Generic;

namespace SourceGenerator;

internal class DbContextEntityResult : Result
{
	public required IEnumerable<string> EntityFullClassNames { get; init; }
}