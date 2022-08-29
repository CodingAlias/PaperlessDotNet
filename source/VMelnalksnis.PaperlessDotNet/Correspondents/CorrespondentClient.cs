﻿// Copyright 2022 Valters Melnalksnis
// Licensed under the Apache License 2.0.
// See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using VMelnalksnis.PaperlessDotNet.Serialization;

namespace VMelnalksnis.PaperlessDotNet.Correspondents;

/// <inheritdoc />
public sealed class CorrespondentClient : ICorrespondentClient
{
	private readonly HttpClient _httpClient;
	private readonly PaperlessJsonSerializerContext _context;

	/// <summary>Initializes a new instance of the <see cref="CorrespondentClient"/> class.</summary>
	/// <param name="httpClient">Http client configured for making requests to the Paperless API.</param>
	/// <param name="serializerOptions">Paperless specific instance of <see cref="JsonSerializerOptions"/>.</param>
	public CorrespondentClient(HttpClient httpClient, PaperlessJsonSerializerOptions serializerOptions)
	{
		_httpClient = httpClient;
		_context = serializerOptions.Context;
	}

	/// <inheritdoc />
	public IAsyncEnumerable<Correspondent> GetAll(CancellationToken cancellationToken = default)
	{
		return _httpClient.GetPaginated(
			"/api/correspondents/",
			_context.PaginatedListCorrespondent,
			cancellationToken);
	}

	/// <inheritdoc />
	public IAsyncEnumerable<Correspondent> GetAll(int pageSize, CancellationToken cancellationToken = default)
	{
		return _httpClient.GetPaginated(
			$"/api/correspondents/?page_size={pageSize}",
			_context.PaginatedListCorrespondent,
			cancellationToken);
	}

	/// <inheritdoc />
	public Task<Correspondent?> Get(int id, CancellationToken cancellationToken = default)
	{
		return _httpClient.GetFromJsonAsync(
			$"/api/correspondents/{id}/",
			_context.Correspondent,
			cancellationToken);
	}
}
