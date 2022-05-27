//////////////////////////////////////////////////
/// Author: Iain Farlow                        ///
/// Created:                                   ///
/// Edited By:                                 ///
/// Last Edited:                               ///
//////////////////////////////////////////////////

using Unity.Networking.Transport;

public class NetStartGame : NetMessage
{
    public NetStartGame()
    {
        Code = ServerOpCode.START_GAME;
    }
    public NetStartGame(DataStreamReader a_reader)
    {
        Code = ServerOpCode.START_GAME;
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
        NetUtility.C_START_GAME?.Invoke(this);
    }
    public override void RevievedOnServer(NetworkConnection a_connection)
    {
        NetUtility.S_START_GAME?.Invoke(this, a_connection);
    }
}