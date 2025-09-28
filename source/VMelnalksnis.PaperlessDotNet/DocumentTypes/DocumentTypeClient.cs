// Copyright 2022 Valters Melnalksnis
// Licensed under the Apache License 2.0.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using VMelnalksnis.PaperlessDotNet.Filters;
using VMelnalksnis.PaperlessDotNet.Serialization;

namespace VMelnalksnis.PaperlessDotNet.DocumentTypes;

/// <inheritdoc />
public sealed class DocumentTypeClient : IDocumentTypeClient
{
	private readonly HttpClient _httpClient;
	private readonly JsonSerializerOptions _options;

	/// <summary>Initializes a new instance of the <see cref="DocumentTypeClient"/> class.</summary>
	/// <param name="httpClient">Http client configured for making requests to the Paperless API.</param>
	/// <param name="serializerOptions">Paperless specific instance of <see cref="JsonSerializerOptions"/>.</param>
	public DocumentTypeClient(HttpClient httpClient, PaperlessJsonSerializerOptions serializerOptions)
	{
		_httpClient = httpClient;
		_options = serializerOptions.Options;
	}

	/// <inheritdoc />
	public IAsyncEnumerable<DocumentType> GetAll(CancellationToken cancellationToken = default)
	{
		return GetAllCore(Routes.DocumentTypes.Uri(), cancellationToken);
	}

	/// <inheritdoc />
	public IAsyncEnumerable<DocumentType> GetAll(int pageSize, CancellationToken cancellationToken = default)
	{
		return GetAll(filter => filter.PageSize == pageSize, null, cancellationToken);
	}

	/// <inheritdoc />
	public IAsyncEnumerable<DocumentType> GetAll(
		Expression<Func<DocumentTypeFilter, bool>> filter,
		Expression<Func<DocumentType, object?>>? orderBy = null,
		CancellationToken cancellationToken = default)
	{
		return GetAllCore(Routes.DocumentTypes.Uri(filter.GetQueryString(orderBy)), cancellationToken);
	}

	/// <inheritdoc />
	public Task<DocumentType?> Get(int id, CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync(
			Routes.DocumentTypes.IdUri(id),
			_options.GetTypeInfo<DocumentType>(),
			cancellationToken);
	}

	/// <inheritdoc />
	public Task<DocumentType> Create(DocumentTypeCreation type)
	{
		return _httpClient.PostAsJsonAsync(
			Routes.DocumentTypes.Uri(),
			type,
			_options.GetTypeInfo<DocumentTypeCreation>(),
			_options.GetTypeInfo<DocumentType>());
	}

	/// <inheritdoc />
	public async Task Delete(int id)
	{
		using var response = await _httpClient.DeleteAsync(Routes.DocumentTypes.IdUri(id)).ConfigureAwait(false);
		await response.EnsureSuccessStatusCodeAsync().ConfigureAwait(false);
	}

	private IAsyncEnumerable<DocumentType> GetAllCore(Uri requestUri, CancellationToken cancellationToken)
	{
		return _httpClient.GetPaginated(
			requestUri,
			_options.GetTypeInfo<PaginatedList<DocumentType>>(),
			cancellationToken);
	}
}
