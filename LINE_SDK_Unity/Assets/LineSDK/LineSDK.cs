using System;
using UnityEngine;
using System.Collections.Generic;

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

        public void Login(List<string> scopes, Action<Result<LoginResult>> action) {
            Login(scopes, null, action);
        }

        public void Login(List<string> scopes, LoginOption option, Action<Result<LoginResult>> action) {
            API.Login(scopes, option, action);
        }

        public void Logout(Action<Result<Unit>> action) {
            API.Logout(action);
        }

        public AccessToken CurrentAccessToken {
            get {
                var result = NativeInterface.GetCurrentAccessToken();
                if (string.IsNullOrEmpty(result)) { return null; }
                return JsonUtility.FromJson<AccessToken>(result);
            }
        }

        public void OnApiOk(string result) {
            API._OnApiOk(result);
        }

        public void OnApiError(string result) {
            API._OnApiError(result);
        }
    }
}

