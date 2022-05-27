//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;
using UnityEngine;

public class NetUnwelcome : NetMessage
{
    public enum REASON
    {
        FULL = 1,
        SERVER_CLOSE = 2
    }

    public REASON m_Reason { get; set; }

    public NetUnwelcome()
    {
        Code = ServerOpCode.UNWELCOME;
    }
    public NetUnwelcome(DataStreamReader a_reader)
    {
        Code = ServerOpCode.UNWELCOME;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteByte((byte)m_Reason);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Reason = (REASON)a_reader.ReadByte();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_UNWELCOME?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_UNWELCOME?.Invoke(this, a_connection);
    }
}