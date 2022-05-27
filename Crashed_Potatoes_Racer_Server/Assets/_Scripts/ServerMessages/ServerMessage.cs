//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created: 03/02/2022                        ///
/// Edited By:                                 ///
/// Last Edited: 01/03/2022                    ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;

public class ServerMessage
{
    public OpCode Code { set; get; }

    public virtual void Serialize(ref DataStreamWriter a_writer)
    {
        //write in the op code
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
