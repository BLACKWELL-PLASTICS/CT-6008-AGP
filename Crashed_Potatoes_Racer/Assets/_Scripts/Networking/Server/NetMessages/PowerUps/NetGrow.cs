using Unity.Networking.Transport;
using UnityEngine;

public class NetGrow : NetMessage
{
    public enum ACTION
    {
        START = 1,
        END = 2,
    }

    public int m_Player { get; set; }
    public ACTION m_Action { get; set; }

    public NetGrow()
    {
        Code = ServerOpCode.GROW;
    }
    public NetGrow(DataStreamReader a_reader)
    {
        Code = ServerOpCode.GROW;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteByte((byte)m_Action);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_Action = (ACTION)a_reader.ReadByte();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_GROW?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_GROW?.Invoke(this, a_connection);
    }
}