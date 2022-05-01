using Unity.Networking.Transport;
using UnityEngine;

public class NetCustomiserUpdate : NetMessage
{
    public int m_Player { get; set; }
    public int m_CarBody { get; set; }
    public int m_CarWheels { get; set; }
    public int m_CarGun { get; set; }

    public NetCustomiserUpdate()
    {
        Code = ServerOpCode.CUSTOMISER_UPDATE;
    }
    public NetCustomiserUpdate(DataStreamReader a_reader)
    {
        Code = ServerOpCode.CUSTOMISER_UPDATE;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteInt(m_CarBody);
        a_writer.WriteInt(m_CarWheels);
        a_writer.WriteInt(m_CarGun);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_CarBody = a_reader.ReadInt();
        m_CarWheels = a_reader.ReadInt();
        m_CarGun = a_reader.ReadInt();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_CUSTOMISER_UPDATE?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_CUSTOMISER_UPDATE?.Invoke(this, a_connection);
    }
}