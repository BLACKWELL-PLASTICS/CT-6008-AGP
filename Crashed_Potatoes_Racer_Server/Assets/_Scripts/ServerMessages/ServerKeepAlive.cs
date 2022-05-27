//////////////////////////////////////////////////
/// Created: 14/02/2022                        ///
/// Author: Iain Farlow                        ///
/// Edited By:                                 ///
/// Last Edited: 14/02/2022                    ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;

public class ServerKeepAlive : ServerMessage
{
    public ServerKeepAlive()
    {
        Code = OpCode.KEEP_ALIVE;
    }
    public ServerKeepAlive(DataStreamReader a_reader)
    {
        Code = OpCode.KEEP_ALIVE;
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
        ServerUtility.C_KEEP_ALIVE?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        ServerUtility.S_KEEP_ALIVE?.Invoke(this, a_connection);
    }
}