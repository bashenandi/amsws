var host = "127.0.0.1";
var port = "8080";
var server = "ws://" + host + ":" + port;
var ws = null;
var reconnect = 1;
var terminal = null;
try { ws = new WebSocket(server); }
catch (e) { }

ws.onclose = function () {
    if (reconnect <= 5) {
        ws = new WebSocket(server);
        reconnect++;
    }
}

ws.onerror = function () {
    alert("Socket通信故障，请尽联系Gary/Frank。");
}

var sendSWS = function (c) {
    if (ws) {
        ws.send(c);
    }
}

$(function () {
    $("button").each(function (i, e) {
        $(this).click(function () {
            var v = $("#txt").val();
            alert(v);
            sendSWS("message " + v);
        });
    });
})