using System.Data;
using Dapper;
using LinkShorter.Repository.Entities;
using LinkShorter.Repository.Interfaces;

namespace LinkShorter.Repository.Repositories;

public class LinkShortRepository(IDbConnection dbConnection) : ILinkShortRepository
{
    public async Task<bool> CheckIfShortLinkExists(string hashlink)
    {
        var query = $@"
    select exists(
        select 1 from links 
                 where shortlink  = '{hashlink}')";

        var result = await dbConnection.QueryFirstOrDefaultAsync<bool>(query);

        return result;
    }

    public async Task<LinkShortEntity?> GetByFullLink(string fullLink)
    {
        var query = @"select * from links where fulllink = @fullLink";

        var result = await dbConnection.QueryFirstOrDefaultAsync<LinkShortEntity>(query, new { fulllink = fullLink });

        return result;
    }

    public async Task<LinkShortEntity?> GetByShortLink(string shortLink)
    {
        var query = "select * from links where shortlink = @shortLink";

        var result = await dbConnection.QueryFirstOrDefaultAsync<LinkShortEntity?>(query, new { shortLink });

        return result;
    }

    public async Task Create(LinkShortEntity entity)
    {
        var query = $@"
insert into links (shortlink, fulllink) VALUES
('{entity.ShortLink}','{entity.FullLink}')";

        await dbConnection.ExecuteAsync(query);
    }
}