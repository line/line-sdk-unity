//  Copyright (c) 2019-present, LINE Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LINE Corporation.
//
//  As with any software that integrates with the LINE Corporation platform, your use of this software
//  is subject to the LINE Developers Agreement [http://terms2.line.me/LINE_Developers_Agreement].
//  This copyright notice shall be included in all copies or substantial portions of the software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
using System;
using UnityEngine;

namespace Line.LineSDK {
    /// <summary>
    /// Represents a login and token related manager for LINE SDK Login features.
    ///
    /// Don't create or attach this script to your own game object. Instead,
    /// drag a `LineSDK` prefab into your scene and set up your channel ID there.
    /// Then you can use `LineSDK.Instance` to get the component and call
    /// methods on it.
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
        /// The shared instance of `LineSDK`. Always use this instance to
        /// interact with the login process of the LINE SDK.
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
        /// The set of permissions requested by your app. If `null` or empty,
        /// the "profile" scope is used.
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
        /// The set of permissions requested by your app. If `null` or empty,
        /// the "profile" scope is used.
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
        /// A `StoredAccessToken` object which contains the access token value
        /// currently in use.
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
