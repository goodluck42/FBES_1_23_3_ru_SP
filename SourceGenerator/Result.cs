using Microsoft.CodeAnalysis.CSharp;

namespace SourceGenerator;

internal class Result
{
	public static implicit operator bool(Result attributeResult)
	{
		return attributeResult.Success;
	}

	public required bool Success { get; init; }

	public static Result Fail()
	{
		return new Result
		{
			Success = false
		};
	}

	public static Result Ok()
	{
		return new Result
		{
			Success = true
		};
	}
}