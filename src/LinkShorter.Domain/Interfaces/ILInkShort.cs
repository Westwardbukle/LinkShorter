namespace LinkShorter.Domain.Interfaces;

public interface ILInkShort
{
    Task<string> CreateShortLink(string link);

    Task<string> GetFullLink(string hashedLink);
}