using Unity.Networking.Transport;

public class NetMessage
{
    public ServerOpCode Code { set; get; }

    public virtual void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
    }
    public virtual void Deserialize(DataStreamReader a_reader)
    {

    }

    public virtual void RecievedOnClient()
    {

    }
    public virtual void RevievedOnServer(NetworkConnection a_connection)
    {

    }
}
