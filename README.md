# Emby.Plugins.Gotify
Gotify notifications for your Emby Server, certified working on `4.8.11.0`

## Fresh Installation

For Emby Server 4.8.0.0 and above:
* Download the [latest v2](https://github.com/rootforbid/Emby.Plugins.Gotify/releases/latest) of the DLL file on the release page of this repo
* Put it in the "plugins" folder of your Emby Server and restart Emby Server
* The plugin should now show up in your Emby Server management page under Advanced > Plugins
* Configure the plugin by heading to the new Notification section under User settings
* Click the "+ Add Notification" button and choose "Gotify"
* The rest is self-explanatory

For Emby Server versions older than 4.8.0.0:
* Download the [v1.0.0.0](https://github.com/rootforbid/Emby.Plugins.Gotify/releases/tag/v1.0.0.0) of the DLL file on the release page of this repo
* Put it in the "plugins" folder of your Emby Server and restart Emby Server
* The plugin should now show up in your Emby Server management page under Advanced > Plugins
* Select the plugin and it will take you to its configuration page

## Upgrading
If you are upgrading Emby Server to 4.8+ from 4.7 or below, after the upgrade: 
* Head to the plugin section in your Emby Server management page
* Uninstall your current Gotify plugin by right-clicking it and choosing "Uninstall"
* Stop your Emby Server
* Go to your Emby server plugin folder and double-check "MediaBrowser.Plugins.GotifyNotifications.dll" is not present, if yes, delete it
* Inside the plugin folder, go to its "configurations" sub-folder and delete "MediaBrowser.Plugins.GotifyNotifications.xml"
* Follow instructions under "Fresh Installation" above for Emby Server 4.8.0.0 and above

## Credits

This is just a slight mod of the Pushover plugin for Emby Server written by [LukePulverenti](https://github.com/MediaBrowser/Pushover)
so all thanks to him and the Emby Server team. I take no credits for this.

And of course big thanks to [Gotify](https://gotify.net) as well.
