using SourceGeneratorSample.Attributes;

namespace SourceGeneratorSample.Entities;

[Entity]
public class Account
{
	[PrimaryKey] public int MyId { get; set; }
	[Index] public string Login { get; set; } = string.Empty;
	[MaxCount(128)] public byte[] PasswordHash { get; set; } = [];
}