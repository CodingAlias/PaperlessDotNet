// Copyright 2022 Valters Melnalksnis
// Licensed under the Apache License 2.0.
// See LICENSE file in the project root for full license information.

using VMelnalksnis.PaperlessDotNet.Correspondents;

namespace VMelnalksnis.PaperlessDotNet.DocumentTypes;

/// <summary>Information needed to create a <see cref="DocumentType"/>.</summary>
public sealed class DocumentTypeCreation
{
	/// <summary>Initializes a new instance of the <see cref="DocumentTypeCreation"/> class.</summary>
	/// <param name="name">The name of the document type.</param>
	public DocumentTypeCreation(string name)
	{
		Name = name;
	}

	/// <inheritdoc cref="DocumentType.Name"/>
	public string Name { get; set; }

	/// <inheritdoc cref="DocumentType.Match"/>
	public string? Match { get; set; }

	/// <inheritdoc cref="DocumentType.MatchingAlgorithm"/>
	public MatchingAlgorithm? MatchingAlgorithm { get; set; }

	/// <inheritdoc cref="DocumentType.IsInsensitive"/>
	public bool? IsInsensitive { get; set; }

	/// <inheritdoc cref="DocumentType.Owner"/>
	public int? Owner { get; set; }
}
