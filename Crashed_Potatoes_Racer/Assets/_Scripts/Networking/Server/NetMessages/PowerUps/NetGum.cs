//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;
using UnityEngine;

public class NetGum : NetMessage
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
    public float m_XRot { get; set; }
    public float m_YRot { get; set; }
    public float m_ZRot { get; set; }
    public float m_WRot { get; set; }

    public NetGum()
    {
        Code = ServerOpCode.GUM;
    }
    public NetGum(DataStreamReader a_reader)
    {
        Code = ServerOpCode.GUM;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteByte((byte)m_Action);
        a_writer.WriteFloat(m_XPos);
        a_writer.WriteFloat(m_YPos);
        a_writer.WriteFloat(m_ZPos);
        a_writer.WriteFloat(m_XRot);
        a_writer.WriteFloat(m_YRot);
        a_writer.WriteFloat(m_ZRot);
        a_writer.WriteFloat(m_WRot);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_Action = (ACTION)a_reader.ReadByte();
        m_XPos = a_reader.ReadFloat();
        m_YPos = a_reader.ReadFloat();
        m_ZPos = a_reader.ReadFloat();
        m_XRot = a_reader.ReadFloat();
        m_YRot = a_reader.ReadFloat();
        m_ZRot = a_reader.ReadFloat();
        m_WRot = a_reader.ReadFloat();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_GUM?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_GUM?.Invoke(this, a_connection);
    }
}