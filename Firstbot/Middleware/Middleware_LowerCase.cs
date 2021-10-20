using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Firstbot.Middleware
{
    public class Middleware_LowerCase : IMiddleware
    {
        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                await turnContext.SendActivityAsync($"[Middleware_LowerCase] {turnContext.Activity.Text}/Before");


                turnContext.Activity.Text = turnContext.Activity.Text.ToLower();
                await next(cancellationToken);
                await turnContext.SendActivityAsync($"[Middleware_LowerCase] {turnContext.Activity.Text}/After");

            }
            else
            {
                await next(cancellationToken);
            }
        }
    }
}
