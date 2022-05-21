using Unity.Networking.Transport;
using UnityEngine;

public class NetShoot : NetMessage
{
    public enum ACTION
    {
        EXPLOSIVE = 1,
        HITSCAN = 2,
        MINE = 3
    }

    public int m_Player { get; set; }
    public ACTION m_Action { get; set; }
    public int m_Other { get; set; }
    public float m_Force { get; set; }
    public float m_XPos { get; set; }
    public float m_YPos { get; set; }
    public float m_ZPos { get; set; }
    public float m_XDir { get; set; }
    public float m_YDir { get; set; }
    public float m_ZDir { get; set; }

    public NetShoot()
    {
        Code = ServerOpCode.SHOOT;
    }
    public NetShoot(DataStreamReader a_reader)
    {
        Code = ServerOpCode.SHOOT;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteByte((byte)m_Action);
        a_writer.WriteInt(m_Other);
        a_writer.WriteFloat(m_Force);
        a_writer.WriteFloat(m_XPos);
        a_writer.WriteFloat(m_YPos);
        a_writer.WriteFloat(m_ZPos);
        a_writer.WriteFloat(m_XDir);
        a_writer.WriteFloat(m_YDir);
        a_writer.WriteFloat(m_ZDir);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_Action = (ACTION)a_reader.ReadByte();
        m_Other = a_reader.ReadInt();
        m_Force = a_reader.ReadFloat();
        m_XPos = a_reader.ReadFloat();
        m_YPos = a_reader.ReadFloat();
        m_ZPos = a_reader.ReadFloat();
        m_XDir = a_reader.ReadFloat();
        m_YDir = a_reader.ReadFloat();
        m_ZDir = a_reader.ReadFloat();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_SHOOT?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_SHOOT?.Invoke(this, a_connection);
    }
}