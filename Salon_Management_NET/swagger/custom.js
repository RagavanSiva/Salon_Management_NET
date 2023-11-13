window.onload = function () {
    // Get the Swagger UI OAuth2 plugin
    var oauthPlugin = window.ui.dom.plugins["oas3/oauth2"];

    // Override the `authorize` function to add the Authorization header
    oauthPlugin.authorize = function (data) {
        var token = data.token;
        if (token) {
            // Add the Authorization header to all API requests
            window.ui.preauthorizeApiKey("Bearer " + token);
        }
    };
};
