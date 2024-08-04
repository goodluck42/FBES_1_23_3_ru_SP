namespace SourceGeneratorSample.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class MaxCountAttribute : Attribute
{
	public MaxCountAttribute(int count)
	{
		Count = count;
	}

	public int Count { get; }
}