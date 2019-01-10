#if UNITY_IOS

using UnityEngine;
using System;
using System.Runtime.InteropServices;
using AOT;
using System.Reflection;

namespace Line.LineSDK {
    public class NativeInterface {
        static NativeInterface() {
            var _ = LineSDK.Instance;
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
        private static extern void line_sdk_setup(string channelId, string universalLinkURL);
        public static void SetupSDK(string channelId, string universalLinkURL) {
            if (Application.platform != RuntimePlatform.IPhonePlayer) {
                return;
            }
            line_sdk_setup(channelId, universalLinkURL);
        }
    }
}


#endif