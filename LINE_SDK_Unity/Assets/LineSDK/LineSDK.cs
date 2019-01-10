using UnityEngine;

namespace Line.LineSDK {
    public class LineSDK: MonoBehaviour {
        static LineSDK instance;

        public string channelID;
        public string universalLinkURL;
 
        void Awake() {
            if (instance == null) {
                instance = this;
            } else if (instance != this) {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
            SetupSDK();
        }

        public static LineSDK Instance {
            get {
                if (instance == null) {
                    GameObject go = new GameObject("LineSDK");
                    instance = go.AddComponent<LineSDK>();
                }
                return instance;
            }
        }

        void SetupSDK() {
            if (string.IsNullOrEmpty(channelID)) {
                throw new System.Exception("LINE SDK channel ID is not set.");
            }
            NativeInterface.SetupSDK(channelID, universalLinkURL);
        }

        public void OnApiOk(string result) {
            API._OnApiOk(result);
        }

        public void OnApiError(string result) {
            API._OnApiError(result);
        }
    }
}

