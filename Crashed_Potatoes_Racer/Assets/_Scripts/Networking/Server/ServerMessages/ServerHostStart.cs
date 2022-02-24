using Unity.Networking.Transport;

public class ServerHostStart : ServerMessage
{
    public string m_ServerIP { get; set; }
    public string m_ServerName { get; set; }
    //public int m_ReqPassword { get; set; }
    public ServerHostStart()
    {
        Code = OpCode.SERVER_START;
    }
    public ServerHostStart(DataStreamReader a_reader)
    {
        Code = OpCode.SERVER_START;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteString(m_ServerIP);
        a_writer.WriteString(m_ServerName);
        //a_writer.WriteInt(m_ReqPassword);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_ServerIP = a_reader.ReadString().ToString();
        m_ServerName = a_reader.ReadString().ToString();
        //m_ReqPassword = a_reader.ReadInt();
    }

    public override void RecievedOnClient()
    {
        ServerUtility.C_SERVER_START?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        ServerUtility.S_SERVER_START?.Invoke(this, a_connection);
    }
}