using System.Collections.Generic;
using Photon.Realtime;

namespace GOIMP.Callbacks
{
    class ConnectionCallbacks : IConnectionCallbacks
    {
        public void OnConnected()
        {
        }

        public void OnConnectedToMaster()
        {
            Multiplayer.OnConnected();
        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {
        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {
        }

        public void OnDisconnected(DisconnectCause cause)
        {

        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {
        }
    }
}
