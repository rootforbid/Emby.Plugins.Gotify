define(['globalize', 'pluginManager', 'emby-input'], function (globalize, pluginManager) {
    'use strict';

    function EntryEditor() {
    }

    EntryEditor.setObjectValues = function (context, entry) {

        entry.FriendlyName = context.querySelector('.txtFriendlyName').value;
        entry.Options.Token = context.querySelector('.txtToken').value;
        entry.Options.ServerUrl = context.querySelector('.txtServerUrl').value;
    };

    EntryEditor.setFormValues = function (context, entry) {

        context.querySelector('.txtFriendlyName').value = entry.FriendlyName || '';
        context.querySelector('.txtToken').value = entry.Options.Token || '';
        context.querySelector('.txtServerUrl').value = entry.Options.ServerUrl || '';
    };

    EntryEditor.loadTemplate = function (context) {

        return require(['text!' + pluginManager.getConfigurationResourceUrl('gotifyeditortemplate')]).then(function (responses) {

            var template = responses[0];
            context.innerHTML = globalize.translateDocument(template);

            // setup any required event handlers here
        });
    };

    EntryEditor.destroy = function () {

    };

    return EntryEditor;
});