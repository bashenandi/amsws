using SuperSocket.WebSocket.SubProtocol;
using Newtonsoft.Json;
using System.Linq;

namespace AM.SuperWebSocket.Command
{
    public class Verify : SubCommandBase<AMSession>
    {
        public override void ExecuteCommand(AMSession session, SubRequestInfo requestInfo)
        {
            if (!string.IsNullOrEmpty(requestInfo.Body))
            {
                VerifyIdentity auth = JsonConvert.DeserializeObject<VerifyIdentity>(requestInfo.Body);
                if (string.IsNullOrEmpty(auth.identity) && string.IsNullOrEmpty(auth.terminal))
                {
                    session.TrySend(JsonConvert.SerializeObject(new { type = "error", message = "请输入身份验证信息" }));
                }
                else
                {
                    session.Terminal = auth.terminal;
                    if (!string.IsNullOrEmpty(auth.identity))
                        session.Identity = auth.identity;
                    else
                        session.Identity = session.SessionID;

                    //表示验证成功，客户端可以记录重连了。
                    session.TrySend(JsonConvert.SerializeObject(new { type = "verifyed", message = session.SessionID }));
                    if (auth.terminal == "display")
                    {
                        if (session.AppServer.GetAllSessions().Any(s => s.Terminal == "control"))
                        {
                            var control = session.AppServer.GetSessions(s => s.Terminal == "control").FirstOrDefault();
                            control.TrySend(JsonConvert.SerializeObject(new { type = "displayed", message = "显示已接入，可以进行控制" }));
                        }
                        
                    }
                    session.TrySend(JsonConvert.SerializeObject(new { type = "log", message = "身份验证成功。终端是：" + session.Terminal + "，身份是：" + session.Identity }));
                }
            }
            else
            {
                session.TrySend(JsonConvert.SerializeObject(new { type = "error", message = "请输入身份验证信息"}));
            }
        }
    }

    class VerifyIdentity
    {
        public string terminal { get; set; }
        public string identity { get; set; }
    }
}
