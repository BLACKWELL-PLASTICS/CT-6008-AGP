using Unity.Networking.Transport;
using UnityEngine;

public class NetBirdPoop : NetMessage
{
    public int m_Player { get; set; }

    public NetBirdPoop()
    {
        Code = ServerOpCode.BIRD_POOP;
    }
    public NetBirdPoop(DataStreamReader a_reader)
    {
        Code = ServerOpCode.BIRD_POOP;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_BIRD_POOP?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_BIRD_POOP?.Invoke(this, a_connection);
    }
}