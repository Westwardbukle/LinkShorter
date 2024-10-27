using LinkShorter.Repository.Entities;

namespace LinkShorter.Repository.Interfaces;

/// <summary>
/// Интерфейс для взаимодействия с репозиторием
/// </summary>
public interface ILinkShortRepository
{
    /// <summary>
    ///     Проверить существование сокращённой ссылки
    /// </summary>
    /// <param name="hashlink">сокращённая ссылка</param>
    /// <returns></returns>
    Task<bool> CheckIfShortLinkExists(string hashlink);

    /// <summary>
    ///     Получение сущности по полной ссылке
    /// </summary>
    /// <param name="fullLink">Полная ссылка</param>
    /// <returns></returns>
    Task<LinkShortEntity?> GetByFullLink(string fullLink);

    /// <summary>
    ///     Получение сущности по короткой ссылке
    /// </summary>
    /// <param name="shortLink">Короткая ссылка</param>
    /// <returns></returns>
    Task<LinkShortEntity?> GetByShortLink(string shortLink);

    /// <summary>
    ///     Создание сущности сокращённой ссылки
    /// </summary>
    /// <param name="entity">Модель в БД</param>
    /// <returns></returns>
    Task Create(LinkShortEntity entity);
}