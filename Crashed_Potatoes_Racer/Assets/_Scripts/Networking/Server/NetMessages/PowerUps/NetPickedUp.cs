using Unity.Networking.Transport;
using UnityEngine;

public class NetPickedUp : NetMessage
{
    public enum ACTION
    {
        DISAPPEAR = 1,
        APPEAR = 2
    }

    public int m_Player { get; set; }
    public int m_PickUp { get; set; }
    public ACTION m_Action { get; set; }

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
        a_writer.WriteInt(m_Player);
        a_writer.WriteInt(m_PickUp);
        a_writer.WriteByte((byte)m_Action);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_PickUp = a_reader.ReadInt();
        m_Action = (ACTION)a_reader.ReadByte();

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