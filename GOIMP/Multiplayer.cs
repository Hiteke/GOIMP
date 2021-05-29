using ExitGames.Client.Photon;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Realtime;

namespace GOIMP
{
    class Multiplayer : MonoBehaviour
    {
        public static Dictionary<int, GameObject> balls = new Dictionary<int, GameObject>();

        public static void addPlayer(int id, string nick)
        {
            var ball = new GameObject();
            var label = new GameObject();
            label.AddComponent<MeshRenderer>();
            var text = label.AddComponent<TextMeshPro>();
            text.text = nick;
            text.fontSize = 10;
            text.alignment = TextAlignmentOptions.Center;
            label.transform.position = new Vector3(0, 1, 0);
            ball.AddComponent<SpriteRenderer>().sprite = BallController.ballController.ball;
            label.transform.SetParent(ball.transform);
            ball.transform.position = BallController.ballController.transform.position;
            var trail = ball.AddComponent<TrailRenderer>();
            var trail_ = BallController.ballController.GetComponent<TrailRenderer>();
            trail.material = trail_.material;
            trail.time = trail_.time;
            trail.widthMultiplier = trail_.widthMultiplier;
            balls.Add(id, ball);
        }

        void Update()
        {
            if (!PhotonManager.client.InRoom) return;
            PhotonManager.client.OpRaiseEvent(0, new object[] { BallController.ballController.transform.position.x, BallController.ballController.transform.position.y }, RaiseEventOptions.Default, SendOptions.SendReliable);
        }

        public static void OnEvent(EventData eventData)
        {
            int code = eventData.Code;

            if (code == 0)
            {
                GameObject ball;
                if (balls.TryGetValue(eventData.Sender, out ball))
                {
                    var obj = (object[])eventData.CustomData;
                    ball.transform.position = Vector3.Lerp(ball.transform.position, new Vector3((float)obj.GetValue(0), (float)obj.GetValue(1)), (Time.deltaTime * 1000));
                } else
                {
                    addPlayer(eventData.Sender, PhotonManager.client.CurrentRoom.Players[eventData.Sender].NickName);
                }
            }
            if (code == 1)
            {
                UI.AddToChat(PhotonManager.client.CurrentRoom.Players[eventData.Sender].NickName, (string)eventData.CustomData);
            }
        }

        public static void OnConnected()
        {
            PhotonManager.client.OpJoinOrCreateRoom(new EnterRoomParams() { RoomName = "GOIMP" });
        }

        void OnDisable()
        {
            foreach (var ball in balls.Values)
            {
                Destroy(ball);
            }
            balls.Clear();
        }

        public static void OnRoomLeft()
        {
            foreach (var ball in balls.Values)
            {
                Destroy(ball);
            }
            balls.Clear();
        }

        public static void OnJoinPlayer(Player player)
        {
            UI.AddToChat("Room", $"{player.NickName} joined");
        }

        public static void OnLeftPlayer(Player player)
        {
            GameObject ball;
            balls.TryGetValue(player.ActorNumber, out ball);
            Destroy(ball);
            UI.AddToChat("Room", $"{player.NickName} left");
        }
    }
}
