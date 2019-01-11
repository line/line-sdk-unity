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
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_setup(channelId, universalLinkURL);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_login(string scope, bool onlyWebLogin, string botPrompt, string identifier);
        internal static void Login(string scope, bool onlyWebLogin, string botPrompt, string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_login(scope, onlyWebLogin, botPrompt, identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_refreshAccessToken(string identifier);
        internal static void RefreshAccessToken(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_refreshAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_revokeAccessToken(string identifier);
        internal static void RevokeAccessToken(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_revokeAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_verifyAccessToken(string identifier);
        internal static void VerifyAccessToken(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_verifyAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_getProfile(string identifier);
        internal static void GetProfile(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_getProfile(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_getBotFriendshipStatus(string identifier);
        internal static void GetBotFriendshipStatus(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_getBotFriendshipStatus(identifier);
        }
    }
}


#endif