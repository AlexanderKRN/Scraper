﻿using Microsoft.Extensions.Logging;
using Scraper.Domain.Entities;
using Scraper.Application.Providers;
using Scraper.Application.Features;

namespace Scraper.Infrastructure.Jobs;

/// <summary>
/// Сервис сбора данных
/// </summary>
public class ScrapingJob : IScrapingJob
{
    private readonly IHtmlAgilityProvider _htmlAgility;
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<ScrapingJob> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="htmlAgility"> провйдер библиотеки HtmlAgility </param>
    /// <param name="orderRepository"> провайдер репозитория </param>
    /// <param name="unitOfWork"> провайдер UnitOfWork </param>
    /// <param name="logger"> Регистратор </param>
    public ScrapingJob(
        IHtmlAgilityProvider htmlAgility,
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        ILogger<ScrapingJob> logger)
    {
        _htmlAgility = htmlAgility;
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    /// <summary>
    /// Задача выполнения процесса по Id ордера
    /// </summary>
    /// <param name="id"> Id ордера </param>
    /// <returns></returns>
    public async Task ProcessAsync(Guid id)
    {
        try
        {
            var ct = new CancellationTokenSource().Token;

            _logger.LogInformation("Старт сбора данных по ордеру ID: {id}", id);

            var order = await _orderRepository.GetById(id, ct);
            if (order.IsFailure)
                _logger.LogError("Ошибка доступа к ордеру ID: {id}", id);

            List<ScrapingNotice> notices = [];

            var tasks = new List<Task>();

            foreach (var url in order.Value.Urls)
            {
                tasks.Add(Task.Run(
                    async () =>
                    {
                        #region Получения данных по URL
                        var notice = await _htmlAgility.GetDataByUrl(url, ct);

                        if (notice.IsFailure)
                        {
                            var faultNotice = ScrapingNotice.Create(url, null);

                            faultNotice.Value.ErrorScraping = notice.Error.Message;

                            notices.Add(faultNotice.Value);
                        }

                        if (notice.IsSuccess)
                            notices.Add(notice.Value);
                        #endregion
                    }));
            }

            await Task.WhenAll(tasks);

            foreach (var notice in notices)
            {
                var task = order.Value.AddNotice(notice);
                if (task.IsFailure)
                    _logger.LogInformation(
                        "Ошибка добавления записей к ордеру ID: {id}", id);
            }

            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("Завершение сбора данных по ордеру ID: {id}", id);
        }
        catch (Exception e)
        {
            _logger.LogError("Ошибка сбора данных по ордеру {id}, message: {e}", id, e.Message);
        }
    }
}
