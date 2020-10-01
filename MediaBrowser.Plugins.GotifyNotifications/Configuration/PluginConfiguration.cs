using System;
using System.Collections.Generic;
using MediaBrowser.Model.Plugins;

namespace MediaBrowser.Plugins.GotifyNotifications.Configuration
{
    /// <summary>
    /// Class PluginConfiguration
    /// </summary>
    public class PluginConfiguration : BasePluginConfiguration
    {
        public GotifyOptions[] Options { get; set; }

        public PluginConfiguration()
        {
            Options = new GotifyOptions[] { };
        }
    }

    public class GotifyOptions
    {
        public Boolean Enabled { get; set; }
        public String Url { get; set; }
        public String Token { get; set; }
        public string MediaBrowserUserId { get; set; }
    }
}
