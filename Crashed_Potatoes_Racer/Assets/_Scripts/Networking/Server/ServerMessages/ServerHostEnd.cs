//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;

public class ServerHostEnd : ServerMessage
{
    public string m_ServerIP { get; set; }
    public ServerHostEnd()
    {
        Code = OpCode.SERVER_END;
    }
    public ServerHostEnd(DataStreamReader a_reader)
    {
        Code = OpCode.SERVER_END;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
        a_writer.WriteString(m_ServerIP);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {
        m_ServerIP = a_reader.ReadString().ToString();
    }

    public override void RecievedOnClient()
    {
        ServerUtility.C_SERVER_END?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        ServerUtility.S_SERVER_END?.Invoke(this, a_connection);
    }
}