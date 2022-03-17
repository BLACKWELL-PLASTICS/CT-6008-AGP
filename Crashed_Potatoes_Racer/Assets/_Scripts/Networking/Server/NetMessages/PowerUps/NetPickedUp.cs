using Unity.Networking.Transport;
using UnityEngine;

public class NetPickedUp : NetMessage
{
    public int m_PickUp { get; set; }

    public NetPickedUp()
    {
        Code = ServerOpCode.PICKED_UP;
    }
    public NetPickedUp(DataStreamReader a_reader)
    {
        Code = ServerOpCode.PICKED_UP;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_PickUp);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_PickUp = a_reader.ReadInt();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_PICKED_UP?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_PCICKED_UP?.Invoke(this, a_connection);
    }
}