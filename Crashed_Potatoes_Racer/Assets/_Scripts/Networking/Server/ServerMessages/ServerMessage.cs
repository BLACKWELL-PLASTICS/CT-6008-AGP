//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 15/02/2022                        ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;

public class ServerMessage
{
    //message that each packet is based off of

    public OpCode Code { set; get; }

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
