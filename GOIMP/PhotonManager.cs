using UnityEngine;
using Photon.Realtime;

namespace GOIMP
{
    class PhotonManager : MonoBehaviour
    {
        public static LoadBalancingClient client = new LoadBalancingClient();

        void Start()
        {
            client.LoadBalancingPeer.DisconnectTimeout = 86400000;
            client.NickName = Steamworks.SteamFriends.GetPersonaName();
            client.AddCallbackTarget(new Callbacks.ConnectionCallbacks());
            client.AddCallbackTarget(new Callbacks.MatchmakingCallbacks());
            client.AddCallbackTarget(new Callbacks.RoomCallbacks());
            client.AddCallbackTarget(new Callbacks.OnEventCallback());
        }

        void OnDisable()
        {
            Disconnect();
        }

        void Update()
        {
            client.Service();
        }

        static public void Connect(string region)
        {
            client.ConnectUsingSettings(new AppSettings() { AppIdRealtime = "?", FixedRegion = region });
        }

        static public void Disconnect()
        {
            client.Disconnect();
        }
    }
}
