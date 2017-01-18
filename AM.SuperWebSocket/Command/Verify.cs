using SuperSocket.WebSocket.SubProtocol;
using Newtonsoft.Json;

namespace AM.SuperWebSocket.Command
{
    public class Verify : SubCommandBase<AMSession>
    {
        public override void ExecuteCommand(AMSession session, SubRequestInfo requestInfo)
        {
            if (!string.IsNullOrEmpty(requestInfo.Body))
            {
                VerifyIdentity verify = JsonConvert.DeserializeObject<VerifyIdentity>(requestInfo.Body);
                if (string.IsNullOrEmpty(verify.identity) && string.IsNullOrEmpty(verify.type))
                {
                    session.TrySend(JsonConvert.SerializeObject(new { type = "error", message = "请输入身份验证信息" }));
                }
                else
                {
                    session.Type = verify.type;
                    if (!string.IsNullOrEmpty(verify.identity))
                        session.Identity = verify.identity;
                    else
                        session.Identity = session.SessionID;
                    session.TrySend(JsonConvert.SerializeObject(new { type = "success", message = session.SessionID }));
                    session.TrySend(JsonConvert.SerializeObject(new { type = "notice", message = "身份验证成功。类别是：" + session.Type + "，身份是：" + session.Identity }));
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
        public string identity { get; set; }
        public string type { get; set; }
    }
}
