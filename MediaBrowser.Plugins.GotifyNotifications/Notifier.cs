using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Emby.Notifications;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Logging;
using MediaBrowser.Controller;
using System.Web;

namespace MediaBrowser.Plugins.GotifyNotifications
{
    public class Notifier : IUserNotifier
    {
        private ILogger _logger;
        private IServerApplicationHost _appHost;
        private IHttpClient _httpClient;

        public Notifier(ILogger logger, IServerApplicationHost applicationHost, IHttpClient httpClient)
        {
            _logger = logger;
            _appHost = applicationHost;
            _httpClient = httpClient;
        }

        private Plugin Plugin => _appHost.Plugins.OfType<Plugin>().First();

        public string Name => Plugin.StaticName;

        public string Key => "gotifynotifications";

        public string SetupModuleUrl => Plugin.NotificationSetupModuleUrl;

        public async Task SendNotification(InternalNotificationRequest request, CancellationToken cancellationToken)
        {
            var options = request.Configuration.Options;

            options.TryGetValue("Token", out string token);
            options.TryGetValue("ServerUrl", out string serverUrl);
            options.TryGetValue("Priority", out string priority);

            var parameters = new Dictionary<string, string>
                {
                    {"token", token},
                    {"server", serverUrl},
                    {"title", "Emby"}
                };


            if (string.IsNullOrEmpty(request.Title))
                parameters.Add("message", HttpUtility.UrlEncode(request.Description));
            else
            {
                parameters.Add("message", HttpUtility.UrlEncode(request.Title));
            }

            if (!string.IsNullOrEmpty(priority))
                parameters.Add("priority", priority);

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = serverUrl+"/message?token="+token,
                CancellationToken = cancellationToken
            };

            httpRequestOptions.SetPostData(parameters);

            using (await _httpClient.Post(httpRequestOptions).ConfigureAwait(false))
            {

            }
        }
    }
}
