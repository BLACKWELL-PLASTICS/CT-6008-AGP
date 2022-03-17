using Unity.Networking.Transport;
using UnityEngine;

public class NetOtherConnected : NetMessage
{
    public int m_PlayerCount { get; set; }
    public string m_PlayerName { get; set; }

    public NetOtherConnected()
    {
        Code = ServerOpCode.OTHER_CONNECTED;
    }
    public NetOtherConnected(DataStreamReader a_reader)
    {
        Code = ServerOpCode.OTHER_CONNECTED;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_PlayerCount);
        a_writer.WriteString(m_PlayerName);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_PlayerCount = a_reader.ReadInt();
        m_PlayerName = a_reader.ReadString().ToString();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_OTHER_CONNECTED?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_OTHER_CONNECTED?.Invoke(this, a_connection);
    }
}