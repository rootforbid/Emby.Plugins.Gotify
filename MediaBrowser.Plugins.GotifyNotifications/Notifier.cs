using System.Collections.Generic;
using MediaBrowser.Common.Net;
using MediaBrowser.Controller.Entities;
using MediaBrowser.Controller.Notifications;
using MediaBrowser.Model.Logging;
using MediaBrowser.Plugins.GotifyNotifications.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediaBrowser.Plugins.GotifyNotifications
{
    public class Notifier : INotificationService
    {
        private readonly ILogger _logger;
        private readonly IHttpClient _httpClient;

        public Notifier(ILogManager logManager, IHttpClient httpClient)
        {
            _logger = logManager.GetLogger(GetType().Name);
            _httpClient = httpClient;
        }

        public bool IsEnabledForUser(User user)
        {
            var options = GetOptions(user);

            return options != null && IsValid(options) && options.Enabled;
        }

        private GotifyOptions GetOptions(User user)
        {
            return Plugin.Instance.Configuration.Options
                .FirstOrDefault(i => string.Equals(i.MediaBrowserUserId, user.Id.ToString("N"), StringComparison.OrdinalIgnoreCase));
        }

        public string Name
        {
            get { return Plugin.Instance.Name; }
        }

        public async Task SendNotification(UserNotification request, CancellationToken cancellationToken)
        {
            var options = GetOptions(request.User);

            var parameters = new Dictionary<string, string>
                {
                    {"token", options.Token},
                    {"url", options.Url},
                };

            // TODO: Improve this with escaping based on what Gotify api requires
            var messageTitle = request.Name.Replace("&", string.Empty);

            if (string.IsNullOrEmpty(request.Description))
                parameters.Add("message", messageTitle);
            else
            {
                parameters.Add("title", messageTitle);
                parameters.Add("message", request.Description);
            }

            _logger.Debug("Gotify to Token : {0} - {1}", options.Token, request.Description);

            var httpRequestOptions = new HttpRequestOptions
            {
                Url = options.Url+"/message?token="+options.Token,
                CancellationToken = cancellationToken
            };

            httpRequestOptions.SetPostData(parameters);

            using (await _httpClient.Post(httpRequestOptions).ConfigureAwait(false))
            {

            }
        }

        private bool IsValid(GotifyOptions options)
        {
            return !string.IsNullOrEmpty(options.Url) &&
                !string.IsNullOrEmpty(options.Token);
        }
    }
}
