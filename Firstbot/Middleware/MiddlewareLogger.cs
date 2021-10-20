using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Firstbot.Middleware
{
    public class MiddlewareLogger : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            await turnContext.SendActivityAsync("Logger Middleware Before", cancellationToken:cancellationToken);
            await next(cancellationToken);
            await turnContext .SendActivityAsync("Logger Middleware After", cancellationToken: cancellationToken);
        }
    }
}
