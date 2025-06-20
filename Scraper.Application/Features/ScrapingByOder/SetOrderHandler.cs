using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Scraper.Application.Providers;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;

namespace Scraper.Application.Features.ScrapingByOder;

/// <summary>
/// Обработка запроса по созданию ордера
/// </summary>
public class SetOrderHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetOrderHandler> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="orderRepository"> Репозиторий ордера </param>
    /// <param name="unitOfWork"> Провайдер UnitOfWork </param>
    /// <param name="logger"> Регистратор </param>
    public SetOrderHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        ILogger<SetOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Обработка запроса по созданию ордера
    /// </summary>
    /// <param name="request"> Данные в запросе </param>
    /// <param name="ct"> Токен отмены </param>
    /// <returns></returns>
    public async Task<Result<Guid, Error>> Handle(
        SetOrderHandlerRequest request,
        CancellationToken ct)
    {
        var urlsFromFile = GetUrls(request.FilePath);
        if (urlsFromFile.IsFailure)
            return urlsFromFile.Error;

        var order = OrderToScrape.Create(urlsFromFile.Value);
        if (order.IsFailure)
            return order.Error;

        await _orderRepository.Add(order.Value, ct);

        await _unitOfWork.SaveChangesAsync(ct);

        _logger.LogInformation("Добавлен ордер, ID: {id}", order.Value.Id);

        return order.Value.Id;
    }

    /// <summary>
    /// Получение перечня URL-адресов из файла
    /// </summary>
    /// <param name="path"> Путь к файлу </param>
    /// <returns></returns>
    private Result<List<string>, Error> GetUrls(string path)
    {
        try
        {
            List<string> urls = [];

            using (StreamReader stream = File.OpenText(path))
            {
                string textLine;

                while ((textLine = stream.ReadLine()) != null)
                    urls.Add(textLine);
            }

            return urls;
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка чтения файла: {0}", e.Message);
            return ErrorList.General.NotFound();
        }
    }
}
