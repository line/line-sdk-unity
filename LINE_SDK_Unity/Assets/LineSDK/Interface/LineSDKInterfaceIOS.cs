#if UNITY_IOS

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Reflection;

namespace Line.LineSDK {
    internal class NativeInterface {
        static NativeInterface() {
            // Ensure the LineSDK instance existing.
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
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                Debug.LogWarning("[LINE SDK] This RuntimePlatform is not supported. Only iOS and Android is supported.");
                var errorJson = @"{""code"":-1, ""message"":""Platform not supported.""}";
                var result = CallbackPayload.WrapValue(identifier, errorJson);
                LineSDK.Instance.OnApiError(result);
                return true;
            }
            return false;
        }

    }
}


#endif