using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Scraper.Application.Providers;
using Scraper.Domain.Common;
using Scraper.Domain.Entities;
using System;
using System.Text;

namespace Scraper.Application.Features.ScrapingByOder;

public class SetOrderHandler
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<SetOrderHandler> _logger;

    public SetOrderHandler(
        IOrderRepository orderRepository,
        IUnitOfWork unitOfWork,
        ILogger<SetOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

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
