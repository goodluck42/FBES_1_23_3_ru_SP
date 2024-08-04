namespace SourceGenerator;

internal class AttributeResult
{
	public static implicit operator bool(AttributeResult attributeResult)
	{
		return attributeResult.Success;
	}
	
	public bool Success { get; set; }
}