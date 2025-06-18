using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper.Application.Features.ScrapingByOder;

public record SetOrderHandlerRequest(string FilePath);
