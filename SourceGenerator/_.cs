﻿using System.ComponentModel;

// ReSharper disable once CheckNamespace
namespace System.Runtime.CompilerServices;

public class IsExternalInit;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Field | AttributeTargets.Property,
	AllowMultiple = false, Inherited = false)]
[EditorBrowsable(EditorBrowsableState.Never)]
internal sealed class RequiredMemberAttribute : Attribute;

[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = false)]
internal sealed class CompilerFeatureRequiredAttribute : Attribute
{
	public CompilerFeatureRequiredAttribute(string featureName)
	{
		FeatureName = featureName;
	}

	public string FeatureName { get; }
	public bool IsOptional { get; init; }
	public const string RefStructs = nameof(RefStructs);
	public const string RequiredMembers = nameof(RequiredMembers);
}