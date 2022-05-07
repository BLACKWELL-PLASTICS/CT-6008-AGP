using Unity.Networking.Transport;
using UnityEngine;

public class NetFinished : NetMessage
{
    public enum ACTION
    {
        INDEVIDUAL = 1,
        ALL = 2
    }

    public int m_Player { get; set; }
    public ACTION m_Action { get; set; }

    public NetFinished()
    {
        Code = ServerOpCode.FINISHED;
    }
    public NetFinished(DataStreamReader a_reader)
    {
        Code = ServerOpCode.FINISHED;
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
        NetUtility.C_FINISHED?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_FINISHED?.Invoke(this, a_connection);
    }
}