using LinkShorter.BL.Services;
using LinkShorter.Domain.Interfaces;
using LinkShorter.Repository.Interfaces;
using LinkShorter.Repository.Repositories;

namespace LinkShorter.Extensions;

public static class RegisterServices
{
    public static void Register(this IServiceCollection services)
    {
        services.AddScoped<ILInkShort, LinkShortService>();
        
        services.AddScoped<ILinkShortRepository, LinkShortRepository>();
    }
}