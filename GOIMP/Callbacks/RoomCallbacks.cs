using System.Collections;
using Photon.Realtime;

namespace GOIMP.Callbacks
{
    class RoomCallbacks : IInRoomCallbacks
    {
        public void OnMasterClientSwitched(Player newMasterClient)
        {
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            Multiplayer.OnJoinPlayer(newPlayer);
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            Multiplayer.OnLeftPlayer(otherPlayer);
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
        }
    }
}
