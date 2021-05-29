using UnityEngine;

namespace GOIMP
{
    class Manager : MonoBehaviour
    {
        void Start()
        { 
            Application.runInBackground = true;
            DontDestroyOnLoad(this);

            gameObject.AddComponent<PhotonManager>();
            gameObject.AddComponent<Multiplayer>();
            gameObject.AddComponent<UI>();
        }

        void Update()
        {
            bool enable = BallController.ballController != null && MainMenuInputController.mainMenuInputController == null;

            if (BallController.ballController != null && MainMenuInputController.mainMenuInputController == null)
            {
                gameObject.GetComponent<PhotonManager>().enabled = true;
                gameObject.GetComponent<Multiplayer>().enabled = true;
                gameObject.GetComponent<UI>().enabled = true;
            }
            if (BallController.ballController == null && MainMenuInputController.mainMenuInputController != null)
            {
                gameObject.GetComponent<PhotonManager>().enabled = false;
                gameObject.GetComponent<Multiplayer>().enabled = false;
                gameObject.GetComponent<UI>().enabled = false;
            }
        }
    }
}
