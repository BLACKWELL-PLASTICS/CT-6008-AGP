//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;
using UnityEngine;

public class NetAISpawn : NetMessage
{
    public int m_Player { get; set; }
    public int m_Body { get; set; }
    public int m_Wheels { get; set; }
    public int m_Gun { get; set; }

    public NetAISpawn()
    {
        Code = ServerOpCode.AI_SPAWN;
    }
    public NetAISpawn(DataStreamReader a_reader)
    {
        Code = ServerOpCode.AI_SPAWN;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_Player);
        a_writer.WriteInt(m_Body);
        a_writer.WriteInt(m_Wheels);
        a_writer.WriteInt(m_Gun);

    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_Player = a_reader.ReadInt();
        m_Body = a_reader.ReadInt();
        m_Wheels = a_reader.ReadInt();
        m_Gun = a_reader.ReadInt();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_AI_SPAWN?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_AI_SPAWN?.Invoke(this, a_connection);
    }
}