// Copyright 2022 Valters Melnalksnis
// Licensed under the Apache License 2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace VMelnalksnis.PaperlessDotNet.DocumentTypes;

/// <summary>Paperless API client for working with document types.</summary>
public interface IDocumentTypeClient
{
	/// <summary>Gets all document types.</summary>
	/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	/// <returns>An enumerable which will asynchronously iterate over all available pages of document types.</returns>
	IAsyncEnumerable<DocumentType> GetAll(CancellationToken cancellationToken = default);

	/// <summary>Gets all document types.</summary>
	/// <param name="pageSize">The number of document types to get in a single request.</param>
	/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	/// <returns>An enumerable which will asynchronously iterate over all available pages of document types.</returns>
	IAsyncEnumerable<DocumentType> GetAll(int pageSize, CancellationToken cancellationToken = default);

	/// <summary>Gets all document types.</summary>
	/// <param name="filter">Expression for filtering the results.</param>
	/// <param name="orderBy">Expression for selecting the field by which to order the results.</param>
	/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	/// <returns>An enumerable which will asynchronously iterate over all available pages of document types.</returns>
	IAsyncEnumerable<DocumentType> GetAll(
		Expression<Func<DocumentTypeFilter, bool>> filter,
		Expression<Func<DocumentType, object?>>? orderBy = null,
		CancellationToken cancellationToken = default);

	/// <summary>Gets the document type with the specified id.</summary>
	/// <param name="id">The id of the document type to get.</param>
	/// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
	/// <returns>The document type with the specified id if it exists; otherwise <see langword="null"/>.</returns>
	Task<DocumentType?> Get(int id, CancellationToken cancellationToken = default);

	/// <summary>Creates a new document type.</summary>
	/// <param name="type">The document type to create.</param>
	/// <returns>The created document type.</returns>
	Task<DocumentType> Create(DocumentTypeCreation type);

	/// <summary>Deletes the document type with the specified id.</summary>
	/// <param name="id">The id of the document type to delete.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	Task Delete(int id);
}
