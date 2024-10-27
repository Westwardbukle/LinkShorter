using LinkShorter.BL.Helpers;
using LinkShorter.Domain.Interfaces;
using LinkShorter.Domain.Options;
using LinkShorter.Repository.Entities;
using LinkShorter.Repository.Interfaces;
using Microsoft.Extensions.Options;

namespace LinkShorter.BL.Services;

public class LinkShortService(ILinkShortRepository repository, IOptions<LinksOptions> options) : ILInkShort
{
    public async Task<string> CreateShortLink(string link)
    {
        var linkShortEntity = await repository.GetByFullLink(link);

        if (linkShortEntity is not null) return $"{options.Value.Domain}/{linkShortEntity.ShortLink}";

        var uri = new Uri(link);

        var hashedLink = HashHelper.CreateMd5(uri.AbsoluteUri);

        var verifiedUri = await VerifyLink(hashedLink);

        var entity = new LinkShortEntity
        {
            ShortLink = verifiedUri,
            FullLink = link
        };

        await repository.Create(entity);

        return $"{options.Value.Domain}/{verifiedUri}";
    }

    public async Task<string> GetFullLink(string hashedLink)
    {
        var entity = await repository.GetByShortLink(hashedLink);

        return entity is null ? string.Empty : entity.FullLink;
    }

    private async Task<string> VerifyLink(string hashedPathAndQuery)
    {
        var isExist = await repository.CheckIfShortLinkExists(hashedPathAndQuery);

        if (isExist)
        {
            var newPathAndQuery = hashedPathAndQuery + new Random().Next(1000, 9999);

            var verifiedLink = await VerifyLink(newPathAndQuery);

            return verifiedLink;
        }

        return hashedPathAndQuery;
    }
}