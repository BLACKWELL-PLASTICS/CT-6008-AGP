using Unity.Networking.Transport;

public class NetKeepAlive : NetMessage
{
    public NetKeepAlive()
    {
        Code = ServerOpCode.KEEP_ALIVE;
    }
    public NetKeepAlive(DataStreamReader a_reader)
    {
        Code = ServerOpCode.KEEP_ALIVE;
        Deserialize(a_reader);
    }

    public override void Serialize(ref DataStreamWriter a_writer)
    {
        a_writer.WriteByte((byte)Code);
    }
    public override void Deserialize(DataStreamReader a_reader)
    {

    }

    public override void RecievedOnClient()
    {
        NetUtility.C_KEEP_ALIVE?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_KEEP_ALIVE?.Invoke(this, a_connection);
    }
}