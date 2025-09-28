// Copyright 2022 Valters Melnalksnis
// Licensed under the Apache License 2.0.
// See LICENSE file in the project root for full license information.

using JetBrains.Annotations;

using VMelnalksnis.PaperlessDotNet.Filters;

#if NET8_0_OR_GREATER
using System.Diagnostics.CodeAnalysis;
#endif

namespace VMelnalksnis.PaperlessDotNet.DocumentTypes;

/// <summary>Fields by which <see cref="DocumentType"/> can be filtered by.</summary>
[UsedImplicitly(ImplicitUseKindFlags.Access | ImplicitUseKindFlags.Assign, ImplicitUseTargetFlags.Members)]
#if NET8_0_OR_GREATER
[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.All)]
#endif
public sealed class DocumentTypeFilter : PaginatedFilter
{
	/// <inheritdoc cref="DocumentType.Name"/>
	public NameFilter? Name { get; }
}
