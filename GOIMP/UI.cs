using ExitGames.Client.Photon;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GOIMP
{
    class UI : MonoBehaviour
    {
        static int state;
        static List<string> messages = new List<string>();
        static string input = "";
        static int region_index;
        static Dictionary<string, string> regions = new Dictionary<string, string>
        {
            { "asia", "Asia" },
            { "au", "Australia" },
            { "cae","Canada, East" },
            { "cn", "Chinese Mainland" },
            { "eu", "Europe" },
            { "in", "India" },
            { "jp", "Japan" },
            { "ru", "Russia" },
            { "rue", "Russia, East" },
            { "za", "South Africa" },
            { "sa", "South America" },
            { "kr", "South Korea" },
            { "us", "USA, East" },
            { "usw", "USA, West" },
        };

        static Rect regions_rect;
        static Rect connect_rect;
        static Rect chat_rect;

        static Vector2 chat_scroll = Vector2.zero;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                if (state < 2)
                {
                    state++;
                    return;
                }
                state = 0;
            }
        }

        void OnGUI()
        {
            regions_rect = new Rect(10, 10, 150, regions.Count * 30);
            connect_rect = new Rect(regions_rect.x + regions_rect.width + 10, 10, 250, 50);
            chat_rect = new Rect(10, Screen.height - chat_rect.height - 10, 400, 300);

            Event e = Event.current;

            if (state == 0)
            {
                GUI.Window(0, regions_rect, DoWindow, $"Region: {regions.Keys.ToArray()[region_index]}");
                GUI.Window(1, connect_rect, DoWindow, $"Connection: {PhotonManager.client.State}");

            }

            if (state == 0 || state == 1)
            {
                if (!PhotonManager.client.InRoom) return;
                GUI.Window(2, chat_rect, DoWindow, $"Chat: {(PhotonManager.client.CurrentRoom.PlayerCount)}");

                if (e.keyCode == KeyCode.Return && input != "")
                {
                    PhotonManager.client.OpRaiseEvent(1, input, RaiseEventOptions.Default, SendOptions.SendReliable);
                    AddToChat(PhotonManager.client.NickName, input);
                    input = "";
                }
            }
        }

        static public void AddToChat(string nickname, string message)
        {
            messages.Add($"[{nickname}] {message}");
            chat_scroll = new Vector2(0, messages.Count * 20);
        }

        void DoWindow(int windowID)
        {
            if (windowID == 0)
            {
                region_index = GUI.SelectionGrid(new Rect(10, 20, regions_rect.width - 20, regions_rect.height - 30), region_index, regions.Values.ToArray(), 1);
            }
            if (windowID == 1)
            {
                if (GUI.Button(new Rect(10, 20, connect_rect.width - 20, 20), $"{(PhotonManager.client.IsConnected ? "Disconnect" : "Connect")}")) {
                    if (!PhotonManager.client.IsConnected)
                    {
                        PhotonManager.Connect(regions.Keys.ToArray()[region_index]);
                    }
                    else
                    {
                        PhotonManager.Disconnect();
                    }
                }
            }
            if (windowID == 2)
            {
                chat_scroll = GUI.BeginScrollView(new Rect(10, 20, chat_rect.width - 20, chat_rect.height - 60), chat_scroll, new Rect(0, 0, chat_rect.width - 40, messages.Count * 20));
                for (int i = 0; i < messages.Count; i++) {
                    GUI.Label(new Rect(0, i * 20, chat_rect.width - 20, 20), messages[i]);
                }
                GUI.EndScrollView();
                input = GUI.TextField(new Rect(10, chat_rect.height - 30, chat_rect.width - 20, 20), input);
            }
        }
    }
}
