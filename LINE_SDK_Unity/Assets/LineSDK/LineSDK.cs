using System;
using UnityEngine;

namespace Line.LineSDK {
    /// <summary>
    /// Represents a login and token related manager for LINE SDK Login features.
    /// 
    /// You should not create or attach this script to your own game object. Instead, drag a `LineSDK` prefab
    /// to your scene and setup your channel ID there. Then you can use `LineSDK.Instance` to get the component
    /// and call methods on it.
    /// </summary>
    public class LineSDK: MonoBehaviour {
        static LineSDK instance;
        
        /// <summary>
        /// The channel ID for your app.
        /// </summary>
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

        /// <summary>
        /// The shared instance of `LineSDK`. Always use this instance to interact with the login process of the LINE SDK.
        /// </summary>
        /// <value>
        /// The shared instance of `LineSDK`.
        /// </value>
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

        /// <summary>
        /// Logs in to the LINE Platform with the specified scopes.
        /// </summary>
        /// <param name="scopes">
        /// The set of permissions requested by your app. If `null` or empty, "profile" scope will be used.
        /// </param>
        /// <param name="action">
        /// The callback action to be invoked when the login process finishes.
        /// </param>
        public void Login(string[] scopes, Action<Result<LoginResult>> action) {
            Login(scopes, null, action);
        }

        /// <summary>
        /// Logs in to the LINE Platform with the specified scopes and an option.
        /// </summary>
        /// <param name="scopes">
        /// The set of permissions requested by your app. If `null` or empty, "profile" scope will be used.
        /// </param>
        /// <param name="option">
        /// The options used during the login process.
        /// </param>
        /// <param name="action">
        /// The callback action to be invoked when the login process finishes.
        /// </param>
        public void Login(string[] scopes, LoginOption option, Action<Result<LoginResult>> action) {
            LineAPI.Login(scopes, option, action);
        }

        /// <summary>
        /// Logs out the current user by revoking the access token.
        /// </summary>
        /// <param name="action">
        /// The callback action to be invoked when the logout process finishes.
        /// </param>
        public void Logout(Action<Result<Unit>> action) {
            LineAPI.Logout(action);
        }

        /// <summary>
        /// Gets the current access token in use.
        /// </summary>
        /// <value>
        /// A `StoredAccessToken` object which contains the access token value currently in use.
        /// </value>
        public StoredAccessToken CurrentAccessToken {
            get {
                var result = NativeInterface.GetCurrentAccessToken();
                if (string.IsNullOrEmpty(result)) { return null; }
                return JsonUtility.FromJson<StoredAccessToken>(result);
            }
        }

        internal void OnApiOk(string result) {
            LineAPI._OnApiOk(result);
        }

        internal void OnApiError(string result) {
            LineAPI._OnApiError(result);
        }
    }
}

