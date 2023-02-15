using Application.Common.Interfaces;

namespace WebApi.Services;
public class BackgroundService : IHostedService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<BackgroundService> _logger;
    private Timer _timer;

    public BackgroundService(IServiceScopeFactory scopeFactory, ILogger<BackgroundService> logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        TimeSpan interval = TimeSpan.FromHours(24);
        //calculate time to run the first time & delay to set the timer
        //DateTime.Today gives time of midnight 00.00
        var nextRunTime = DateTime.Today.AddDays(1).AddHours(1);
        var curTime = DateTime.Now;
        var firstInterval = nextRunTime.Subtract(curTime);

        Action action = () =>
        {
            var t1 = Task.Delay(firstInterval);
            t1.Wait();
            //clean expired tokens form black list at expected time
            CleanBlackList(null);
            //now schedule it to be called every 24 hours for future
            // timer repeats call to CleanBlacklist every 24 hours.
            _timer = new Timer(
                CleanBlackList,
                null,
                TimeSpan.Zero,
                interval
            );
        };
        Task.Run(action);

        // starts the timer from when the application is first started
        // _timer = new Timer(
        //             CleanBlackList,
        //             null,
        //             TimeSpan.Zero,
        //             interval
        //         );


        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }
    private async void CleanBlackList(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<IAppDbContext>();
            // var expiredTokens = context.Blacklists.Where(b => b.ExpireDate < DateTime.Now).ToList();
            // context.Blacklists.RemoveRange(expiredTokens);
            await context.SaveChangesAsync(default);

        }

    }
    // public async void FetchCurrency(object state)
    // {
    //     var client = new HttpClient();
    //     var request = new HttpRequestMessage
    //     {
    //         RequestUri = new Uri("https://currency-exchange.p.rapidapi.com/listquotes"),
    //         Headers =
    //                 {
    //                     { "X-RapidAPI-Key", "02f10d766bmsh146e0259d7989dfp130a31jsn9c2154ccfbeb" },
    //                     { "X-RapidAPI-Host", "currency-exchange.p.rapidapi.com" },
    //                 },
    //     };
    //     using (var response = await client.SendAsync(request))
    //     {
    //         response.EnsureSuccessStatusCode();
    //         var body = response.Content;
    //         _logger.LogCritical($"{body}");

    //     }
    // }
}