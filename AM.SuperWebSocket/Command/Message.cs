using System.Linq;
using SuperSocket.WebSocket.SubProtocol;
using Newtonsoft.Json;

namespace AM.SuperWebSocket.Command
{
    public class Message : SubCommandBase<AMSession>
    {
        public override void ExecuteCommand(AMSession session, SubRequestInfo requestInfo)
        {
            if (!string.IsNullOrEmpty(requestInfo.Body))
            {
                var sessions = session.AppServer.GetSessions(s => s.SessionID != session.SessionID);
                foreach (var s in sessions)
                {
                    s.TrySend(requestInfo.Body);
                }
            }
            else
            {
                session.TrySend(JsonConvert.SerializeObject(new { type = "error", message = "请输入要发送的消息体" }));
            }
        }
    }
}
