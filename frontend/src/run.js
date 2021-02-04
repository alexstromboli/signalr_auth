var signalR = require('@microsoft/signalr');

async function setup_messaging (Auth)
{
    var conn = new signalR.HubConnectionBuilder ().withUrl (
        "/api/messages",
        {
            // to use
            // https://medium.com/@tarik.nzl/asp-net-core-and-signalr-authentication-with-the-javascript-client-d46c15584daf
            //accessTokenFactory: () => Auth
        })
        .build ();

    async function start ()
    {
        try
        {
            await conn.start ();
        }
        catch
        {
            setTimeout (start, 5000);
        }
    }

    conn.on ('update_01', data =>
    {
        document.getElementById("print").innerText = data;
    });

    conn.onclose (start);
    start ();
}

(async () =>
{
    var Params = new URLSearchParams(window.location.search);
    var Auth = Params.get('u');
    if (!!Auth)
    {
        document.cookie = "Authentication=" + Auth + "; path=/";
        await setup_messaging (Auth);
    }
})();
