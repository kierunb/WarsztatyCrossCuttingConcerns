using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace WarsztatyCrossCuttingConcerns.ActionFilters
{
    public class MyActionFilter : IActionFilter
    {
        private readonly ILogger<MyActionFilter> logger;
        private Stopwatch stopwatch;

        public MyActionFilter(ILogger<MyActionFilter> logger)
        {
            this.logger = logger;
            stopwatch = new Stopwatch();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            stopwatch.Stop();
            logger.LogInformation($">>> Po wykonaniu metody. Czas wykonania: {stopwatch.ElapsedMilliseconds}");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation(">>> Przed wykonaniem metody");
            stopwatch.Start();

        }
    }
}
