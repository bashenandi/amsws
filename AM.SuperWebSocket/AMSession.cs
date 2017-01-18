using Newtonsoft.Json;
using SuperSocket.WebSocket;

namespace AM.SuperWebSocket
{
    public class AMSession : WebSocketSession<AMSession>
    {
        public string Identity { get; set; }
        public string Terminal { get; set; }

        protected override void OnSessionStarted()
        {
            this.TrySend(JsonConvert.SerializeObject(new { type = "log", message = this.SessionID }));
        }
    }
}
