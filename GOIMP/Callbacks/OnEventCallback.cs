using ExitGames.Client.Photon;
using Photon.Realtime;

namespace GOIMP.Callbacks
{
    class OnEventCallback : IOnEventCallback
    {
        public void OnEvent(EventData photonEvent)
        {
            Multiplayer.OnEvent(photonEvent);
        }
    }
}
