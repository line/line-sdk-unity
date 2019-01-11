#if UNITY_IOS

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Reflection;

namespace Line.LineSDK {
    public class NativeInterface {
        static NativeInterface() {
            // Ensure the LineSDK instance existing.
            var _ = LineSDK.Instance;
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_setup(string channelId, string universalLinkURL);
        public static void SetupSDK(string channelId, string universalLinkURL) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_setup(channelId, universalLinkURL);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_refreshAccessToken(string identifier);
        public static void RefreshAccessToken(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_refreshAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_revokeAccessToken(string identifier);
        public static void RevokeAccessToken(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_revokeAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_verifyAccessToken(string identifier);
        public static void VerifyAccessToken(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_verifyAccessToken(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_getProfile(string identifier);
        public static void GetProfile(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_getProfile(identifier);
        }

        [DllImport("__Internal")]
        private static extern void line_sdk_getBotFriendshipStatus(string identifier);
        public static void GetBotFriendshipStatus(string identifier) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_getBotFriendshipStatus(identifier);
        }
    }
}


#endif