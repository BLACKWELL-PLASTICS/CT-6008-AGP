//////////////////////////////////////////////////
/// Created: 14/02/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited: 14/02/2022                    ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;

public class ServerListRequest : ServerMessage
{
    public string m_ServerIP { get; set; }
    public string m_ServerName { get; set; }
    public int m_level { get; set; }
    public ServerListRequest()
    {
        Code = OpCode.LIST_REQUEST;
    }
    public ServerListRequest(DataStreamReader a_reader)
    {
        Code = OpCode.LIST_REQUEST;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteString(m_ServerIP);
        a_writer.WriteString(m_ServerName);
        a_writer.WriteInt(m_level);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_ServerIP = a_reader.ReadString().ToString();
        m_ServerName = a_reader.ReadString().ToString();
        m_level = a_reader.ReadInt();
    }

    public override void RecievedOnClient()
    {
        ServerUtility.C_LIST_REQUEST?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        ServerUtility.S_LIST_REQUEST?.Invoke(this, a_connection);
    }
}