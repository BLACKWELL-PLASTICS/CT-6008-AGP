using Unity.Networking.Transport;
using UnityEngine;

public class NetMenuCountdown : NetMessage
{
    public enum ACTION
    {
        READY = 1,
        COUNTING = 2,
        GO = 3,
        UNREADY = 4
    }

    public int m_Player { get; set; }
    public ACTION m_Action { get; set; }
    public float m_Count { get; set; }

    public NetMenuCountdown()
    {
        Code = ServerOpCode.MENU_COUNTDOWN;
    }
    public NetMenuCountdown(DataStreamReader a_reader)
    {
        Code = ServerOpCode.MENU_COUNTDOWN;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteByte((byte)m_Action);
        a_writer.WriteFloat(m_Count);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_Action = (ACTION)a_reader.ReadByte();
        m_Count = a_reader.ReadFloat();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_MENU_COUNTDOWN?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_MENU_COUNTDOWN?.Invoke(this, a_connection);
    }
}