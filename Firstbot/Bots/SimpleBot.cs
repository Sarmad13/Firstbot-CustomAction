using Microsoft.Bot.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Firstbot.Bots
{
    public class SimpleBot : IBot
    {
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            await turnContext.SendActivityAsync($"[SimpleBot] {turnContext.Activity.Type}/OnTurn");
        }
    }
}
