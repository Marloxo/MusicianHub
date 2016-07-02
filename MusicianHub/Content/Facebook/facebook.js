window.fbAsyncInit = function () {
    FB.init({
        appId: '900746463371149',
        xfbml: true,
        version: 'v2.6'
    });
};
(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/sdk.js#xfbml=1&version=v2.6&appId=900746463371149";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));

//<!-- End Of Load Facebook SDK for JavaScript -->