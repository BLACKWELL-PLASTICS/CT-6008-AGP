//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;
using UnityEngine;

public class NetOtherConnected : NetMessage
{
    public int m_PlayerCount { get; set; }
    public string m_PlayerName { get; set; }
    public int m_CarBody { get; set; }
    public int m_CarWheels { get; set; }
    public int m_CarGun { get; set; }


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
        a_writer.WriteInt(m_CarBody);
        a_writer.WriteInt(m_CarWheels);
        a_writer.WriteInt(m_CarGun);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_PlayerCount = a_reader.ReadInt();
        m_PlayerName = a_reader.ReadString().ToString();
        m_CarBody = a_reader.ReadInt();
        m_CarWheels = a_reader.ReadInt();
        m_CarGun = a_reader.ReadInt();
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