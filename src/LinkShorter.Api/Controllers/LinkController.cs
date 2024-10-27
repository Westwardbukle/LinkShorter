using System.ComponentModel.DataAnnotations;
using LinkShorter.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LinkShorter.Controllers;

[ApiController]
[Route("[controller]")]
public class LinkController : ControllerBase
{
    private readonly ILInkShort _linkShort;

    public LinkController(ILInkShort linkShort)
    {
        _linkShort = linkShort;
    }

    [HttpPost]
    public async Task<IActionResult> CreateShortLink(
        //[RegularExpression(@"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$")] P.S Да, я специально её оставил
        [Required] [FromQuery] string link)
    {
        var test = Uri.IsWellFormedUriString(link, UriKind.RelativeOrAbsolute);

        if (!test) return BadRequest("Ссылка невалидна");

        var result = await _linkShort.CreateShortLink(link);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetFullLink([FromQuery] [Required] string link)
    {
        var fullLink = await _linkShort.GetFullLink(link);

        if (!fullLink.Any()) return BadRequest("Не удалось получить полную ссылку");

        return Ok(fullLink);
    }
}