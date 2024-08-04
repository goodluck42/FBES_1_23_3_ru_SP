namespace SourceGeneratorSample.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class IndexAttribute : Attribute
{
	public IndexAttribute(bool isClustered = true) {}
}