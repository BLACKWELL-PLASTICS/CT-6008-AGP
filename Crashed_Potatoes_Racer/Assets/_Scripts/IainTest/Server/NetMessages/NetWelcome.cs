using Unity.Networking.Transport;
using UnityEngine;

public class NetWelcome : NetMessage
{
    public int m_PlayerCount { get; set; }
    public int m_PlayerNumber { get; set; }
    public string m_PlayerName { get; set; }

    public NetWelcome()
    {
        Code = OpCode.WELCOME;
    }
    public NetWelcome(DataStreamReader a_reader)
    {
        Code = OpCode.WELCOME;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteInt(m_PlayerCount);
        a_writer.WriteInt(m_PlayerNumber);
        a_writer.WriteString(m_PlayerName);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_PlayerCount = a_reader.ReadInt();
        m_PlayerNumber = a_reader.ReadInt();
        m_PlayerName = a_reader.ReadString().ToString();
    }

    public override void RecievedOnClient()
    {
        NetUtility.C_WELCOME?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_WELCOME?.Invoke(this, a_connection);
    }
}