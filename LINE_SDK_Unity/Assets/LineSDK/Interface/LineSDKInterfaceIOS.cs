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

#if UNITY_IOS

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Reflection;

namespace Line.LineSDK {
    internal class NativeInterface {
        static NativeInterface() {
            // Ensure the LineSDK instance exists.
            var _ = LineSDK.Instance;
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_setup(string channelId, string universalLinkURL);
        internal static void SetupSDK(string channelId, string universalLinkURL) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }
            line_sdk_setup(channelId, universalLinkURL);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_login(string scope, bool onlyWebLogin, string botPrompt, string identifier);
        internal static void Login(string scope, bool onlyWebLogin, string botPrompt, string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }
            line_sdk_login(scope, onlyWebLogin, botPrompt, identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_logout(string identifier);
        internal static void Logout(string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }
            line_sdk_logout(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_refreshAccessToken(string identifier);
        internal static void RefreshAccessToken(string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }
            line_sdk_refreshAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_revokeAccessToken(string identifier);
        internal static void RevokeAccessToken(string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }
            line_sdk_revokeAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_verifyAccessToken(string identifier);
        internal static void VerifyAccessToken(string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }
            line_sdk_verifyAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_getProfile(string identifier);
        internal static void GetProfile(string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }
            line_sdk_getProfile(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_getBotFriendshipStatus(string identifier);
        internal static void GetBotFriendshipStatus(string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }
            line_sdk_getBotFriendshipStatus(identifier);
        }

        [DllImport("__Internal")]
        private static extern string line_sdk_getCurrentAccessToken();
        internal static string GetCurrentAccessToken() {
            if (!Application.isPlaying) { return null; }
            if (IsInvalidRuntime(null)) { return null; }
            return line_sdk_getCurrentAccessToken();
        }

        private static bool IsInvalidRuntime(string identifier) {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.IPhonePlayer);
        }
    }
}


#endif
