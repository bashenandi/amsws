using SuperSocket.SocketBase;
using SuperSocket.WebSocket;
using System.Linq;
using Newtonsoft.Json;

namespace AM.SuperWebSocket
{
    public class AMAppServer: WebSocketServer<AMSession>
    {
        protected override void OnSessionClosed(AMSession session, CloseReason reason)
        {
            if (session.Terminal == "display")
            {
                if (session.AppServer.GetAllSessions().Any(s => s.Terminal == "control"))
                {
                    var control = session.AppServer.GetSessions(s => s.Terminal == "control").FirstOrDefault();
                    control.TrySend(JsonConvert.SerializeObject(new { type = "error", message = "显示端已下线，请尽快联系Gary/Frank." }));
                }
            }
            base.OnSessionClosed(session, reason);
        }
    }
}
