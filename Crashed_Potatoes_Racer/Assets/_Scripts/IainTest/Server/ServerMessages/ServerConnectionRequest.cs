using Unity.Networking.Transport;

public class ServerConnectionRequest : ServerMessage
{
    ////public string m_Password { get; set; }
    //public ServerConnectionRequest()
    //{
    //    Code = OpCode.CONNECTION_REQUEST;
    //}
    //public ServerConnectionRequest(DataStreamReader a_reader)
    //{
    //    Code = OpCode.CONNECTION_REQUEST;
    //    Deserialize(a_reader);
    //}

    //public override void Serialize(ref DataStreamWriter a_writer)
    //{
    //    a_writer.WriteByte((byte)Code);
    //    //a_writer.WriteString(m_Password);
    //}
    //public override void Deserialize(DataStreamReader a_reader)
    //{
    //    //m_Password = a_reader.ReadString().ToString();
    //}

    //public override void RecievedOnClient()
    //{
    //    ServerUtility.C_CONNECTION_REQUEST?.Invoke(this);
    //}
    //public override void RevievedOnServer(NetworkConnection a_connection)
    //{
    //    ServerUtility.S_CONNECTION_REQUEST?.Invoke(this, a_connection);
    //}
}