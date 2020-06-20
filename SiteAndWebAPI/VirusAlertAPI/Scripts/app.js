    function showError(jqXHR) {

        var result = jqXHR.status + ': ' + jqXHR.statusText;
        alert(result);
        var response = jqXHR.responseJSON;
        if (response) {
            if (response.Message) self.errors.push(response.Message);
            if (response.ModelState) {
                var modelState = response.ModelState;
                for (var prop in modelState) {
                    if (modelState.hasOwnProperty(prop)) {
                        var msgArr = modelState[prop]; // expect array here
                        if (msgArr.length) {
                            for (var i = 0; i < msgArr.length; ++i) self.errors.push(msgArr[i]);
                        }
                    }
                }
            }
            if (response.error) self.errors.push(response.error);
            if (response.error_description) self.errors.push(response.error_description);
        }
    }

    register = function () {

        var data = {
            ConfirmPassword: 'a-sA1234567',
            Email: 'СергейСидоров@почта.рус',
            Password: 'a-sA1234567'
        };

        $.ajax({
            type: 'POST',
            url: 'http://localhost/VirusAlertAPI/api/Account/Register',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(data)
        }).done(function (data) {
            this.result("Done!");
        }).fail(showError);
    }

    applogin = function (usr, pass, uri) {

        var loginData = {
            grant_type: 'password',
            username: usr,
            password: pass
        };
        
        $.ajax({
            type: 'POST',
            url: uri,
            data: loginData
        }).done(function (data) {
            window.location.href = '../Personal/Index';
        }).fail(showError);
    }

    logout = function () {
        // Log out from the cookie based logon.
        var token = sessionStorage.getItem(tokenKey);
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            type: 'POST',
            url: '/api/Account/Logout',
            headers: headers
        }).done(function (data) {
            // Successfully logged out. Delete the token.
            self.user('');
            sessionStorage.removeItem(tokenKey);
            window.location.href = '../../Personal/Index';
        }).fail(showError);
    }
