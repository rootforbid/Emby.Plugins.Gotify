using System;
using System.Collections.Generic;
using System.Linq;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Net;
using MediaBrowser.Model.Logging;
using MediaBrowser.Model.Services;
using MediaBrowser.Plugins.GotifyNotifications.Configuration;
using System.Threading.Tasks;
using System.Threading;

namespace MediaBrowser.Plugins.GotifyNotifications.Api
{
    [Route("/Notification/Gotify/Test/{UserID}", "POST", Summary = "Tests Gotify")]
    public class TestNotification : IReturnVoid
    {
        [ApiMember(Name = "UserID", Description = "User Id", IsRequired = true, DataType = "string", ParameterType = "path", Verb = "GET")]
        public string UserID { get; set; }
    }

    class ServerApiEndpoints : IService
    {
        private readonly IHttpClient _httpClient;
        private readonly ILogger _logger;

        public ServerApiEndpoints(ILogManager logManager, IHttpClient httpClient)
        {
            _logger = logManager.GetLogger(GetType().Name);
            _httpClient = httpClient;
        }
        private GotifyOptions GetOptions(String userID)
        {
            return Plugin.Instance.Configuration.Options
                .FirstOrDefault(i => string.Equals(i.MediaBrowserUserId, userID, StringComparison.OrdinalIgnoreCase));
        }

        public void Post(TestNotification request)
        {
            var task = PostAsync(request);

            Task.WaitAll(task);
        }

        private async Task PostAsync(TestNotification request)
        {
            var options = GetOptions(request.UserID);

            var parameters = new Dictionary<string, string>
            {
                {"title", "Test Notification" },
                {"message", "This is a test notification from MediaBrowser"}
            };

            _logger.Debug("Gotify <TEST> to {0}", options.Token);

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = options.Url+"/message?token="+options.Token,
                CancellationToken = CancellationToken.None
            };

            httpRequestOptions.SetPostData(parameters);

            using (await _httpClient.Post(httpRequestOptions).ConfigureAwait(false))
            {

            }
        }
    }
}
