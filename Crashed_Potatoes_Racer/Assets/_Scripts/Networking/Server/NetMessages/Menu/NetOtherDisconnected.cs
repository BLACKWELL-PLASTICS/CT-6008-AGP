//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;
using UnityEngine;

public class NetOtherDisconnected : NetMessage
{
    public int m_PlayerNum { get; set; }

    public NetOtherDisconnected()
    {
        Code = ServerOpCode.OTHER_DISCONNECTED;
    }
    public NetOtherDisconnected(DataStreamReader a_reader)
    {
        Code = ServerOpCode.OTHER_DISCONNECTED;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_PlayerNum);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_PlayerNum = a_reader.ReadInt();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_OTHER_DISCONNECTED?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_OTHER_DISCONNECTED?.Invoke(this, a_connection);
    }
}