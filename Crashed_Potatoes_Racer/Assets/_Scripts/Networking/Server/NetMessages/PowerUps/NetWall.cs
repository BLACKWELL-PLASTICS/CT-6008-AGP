using Unity.Networking.Transport;
using UnityEngine;

public class NetWall : NetMessage
{
    public enum ACTION
    {
        //HOLD = 1,
        //THROW = 2,
        LAND = 3
    }

    public int m_Player { get; set; }
    public ACTION m_Action { get; set; }
    public float m_XPos { get; set; }
    public float m_YPos { get; set; }
    public float m_ZPos { get; set; }

    public NetWall()
    {
        Code = ServerOpCode.WALL;
    }
    public NetWall(DataStreamReader a_reader)
    {
        Code = ServerOpCode.WALL;
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
        NetUtility.C_WALL?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_WALL?.Invoke(this, a_connection);
    }
}