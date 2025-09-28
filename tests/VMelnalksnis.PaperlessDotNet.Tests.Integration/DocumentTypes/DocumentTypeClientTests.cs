// Copyright 2022 Valters Melnalksnis
// Licensed under the Apache License 2.0.
// See LICENSE file in the project root for full license information.

using System.Linq;
using System.Threading.Tasks;

using VMelnalksnis.PaperlessDotNet.Correspondents;
using VMelnalksnis.PaperlessDotNet.DocumentTypes;

namespace VMelnalksnis.PaperlessDotNet.Tests.Integration.DocumentTypes;

public sealed class DocumentTypeClientTests(PaperlessFixture paperlessFixture) : PaperlessTests(paperlessFixture)
{
	[Test]
	public async Task CreateGetDelete()
	{
		var createdType = await Client.DocumentTypes.Create(new("Receipts")
		{
			Match = "receipts",
			MatchingAlgorithm = MatchingAlgorithm.ExactMatch,
			IsInsensitive = true,
		});

		await Client.DocumentTypes.Create(new("Contracts"));

		var type = (await Client.DocumentTypes.Get(createdType.Id))!;
		var types = await Client.DocumentTypes.GetAll().ToListAsync();
		var filteredTypes = await Client.DocumentTypes
			.GetAll(filter => filter.PageSize == 1, documentType => documentType.DocumentCount)
			.ToListAsync();

		using (new AssertionScope())
		{
			type.Should().BeEquivalentTo(createdType, ExcludingDocumentCount);
			types.Should().HaveCountGreaterThanOrEqualTo(2);
			types.Should().ContainSingle(t => t.Id == type.Id).Which.Should().BeEquivalentTo(type, ExcludingDocumentCount);
			filteredTypes.Should().BeEquivalentTo(types);

			createdType.Name.Should().Be("Receipts");
			createdType.Slug.Should().Be("receipts");
			createdType.Match.Should().Be("receipts");
			createdType.MatchingAlgorithm.Should().Be(MatchingAlgorithm.ExactMatch);
			createdType.IsInsensitive.Should().BeTrue();

			createdType.DocumentCount.Should().Be(null);
			type.DocumentCount.Should().Be(0);
		}

		await Client.DocumentTypes.Delete(createdType.Id);
		types = await Client.DocumentTypes.GetAll().ToListAsync();

		types.Should().NotContainEquivalentOf(createdType);
	}

	private static EquivalencyAssertionOptions<DocumentType> ExcludingDocumentCount(EquivalencyAssertionOptions<DocumentType> options)
	{
		return options.Excluding(type => type.DocumentCount);
	}
}
