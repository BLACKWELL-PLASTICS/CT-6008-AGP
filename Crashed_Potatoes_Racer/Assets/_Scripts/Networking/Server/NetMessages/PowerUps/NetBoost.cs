//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;
using UnityEngine;

public class NetBoost : NetMessage
{
    public enum ACTION
    {
        START = 1,
        END = 2,
    }

    public int m_Player { get; set; }
    public int m_CarNum { get; set; }
    public ACTION m_Action { get; set; }

    public NetBoost()
    {
        Code = ServerOpCode.BOOST;
    }
    public NetBoost(DataStreamReader a_reader)
    {
        Code = ServerOpCode.BOOST;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteInt(m_CarNum);
        a_writer.WriteByte((byte)m_Action);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_CarNum = a_reader.ReadInt();
        m_Action = (ACTION)a_reader.ReadByte();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_BOOST?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_BOOST?.Invoke(this, a_connection);
    }
}