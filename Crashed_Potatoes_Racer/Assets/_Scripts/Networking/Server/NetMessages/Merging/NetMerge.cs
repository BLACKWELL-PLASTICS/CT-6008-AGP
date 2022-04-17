using Unity.Networking.Transport;
using UnityEngine;

public class NetMerge : NetMessage
{
    public enum ACTION
    {
        ACTIVATE = 1,
        DEACTIVATE = 2,
        MERGE = 3,
        DEMERGE = 4,
        DRIVE = 5,
        SHOOT = 6
    }

    public int m_Player { get; set; }
    public ACTION m_Action { get; set; }
    public int m_Other { get; set; }
    public float m_XPos { get; set; }
    public float m_YPos { get; set; }
    public float m_ZPos { get; set; }
    public float m_XRot { get; set; }
    public float m_YRot { get; set; }
    public float m_ZRot { get; set; }
    public float m_WRot { get; set; }
    public float m_secondXRot { get; set; }
    public float m_secondYRot { get; set; }
    public float m_secondZRot { get; set; }
    public float m_secondWRot { get; set; }

    public NetMerge()
    {
        Code = ServerOpCode.MERGE;
    }
    public NetMerge(DataStreamReader a_reader)
    {
        Code = ServerOpCode.MERGE;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteByte((byte)m_Action);
        a_writer.WriteInt(m_Other);
        a_writer.WriteFloat(m_XPos);
        a_writer.WriteFloat(m_YPos);
        a_writer.WriteFloat(m_ZPos);
        a_writer.WriteFloat(m_XRot);
        a_writer.WriteFloat(m_YRot);
        a_writer.WriteFloat(m_ZRot);
        a_writer.WriteFloat(m_WRot);
        a_writer.WriteFloat(m_secondXRot);
        a_writer.WriteFloat(m_secondYRot);
        a_writer.WriteFloat(m_secondZRot);
        a_writer.WriteFloat(m_secondWRot);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_Action = (ACTION)a_reader.ReadByte();
        m_Other = a_reader.ReadInt();
        m_XPos = a_reader.ReadFloat();
        m_YPos = a_reader.ReadFloat();
        m_ZPos = a_reader.ReadFloat();
        m_XRot = a_reader.ReadFloat();
        m_YRot = a_reader.ReadFloat();
        m_ZRot = a_reader.ReadFloat();
        m_WRot = a_reader.ReadFloat();
        m_secondXRot = a_reader.ReadFloat();
        m_secondYRot = a_reader.ReadFloat();
        m_secondZRot = a_reader.ReadFloat();
        m_secondWRot = a_reader.ReadFloat();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_MERGE?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_MERGE?.Invoke(this, a_connection);
    }
}