#if UNITY_ANDROID
using UnityEngine;

namespace Line.LineSDK
{
    internal class NativeInterface
    {
        #if UNITY_EDITOR
        static AndroidJavaObject lineSdkWrapper = null;
        #else
        static AndroidJavaObject lineSdkWrapper = new AndroidJavaObject("com.linecorp.linesdk.unitywrapper.LineSdkWrapper");
        #endif

        static NativeInterface()
        {
            // Ensure the LineSDK instance existing.
            var _ = LineSDK.Instance;
        }

        internal static void SetupSDK(string channelId, string universalLinkURL)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            object[] parameters = new object[1];
            parameters[0] = channelId;

            lineSdkWrapper.Call("setupSdk", parameters);
        }

        internal static void Login(string scope, bool onlyWebLogin, string botPrompt, string identifier)
        {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(null)) { return; }

            object[] parameters = new object[4];
            parameters[0] = identifier;
            parameters[1] = scope;
            parameters[2] = onlyWebLogin;
            parameters[3] = botPrompt;

            lineSdkWrapper.Call("login", parameters);
        }

        internal static void Logout(string identifier)
        {
            CallLineSdkWrapperWithIdentifier("logout", identifier);
        }

        internal static void RefreshAccessToken(string identifier) { 
            CallLineSdkWrapperWithIdentifier("refreshAccessToken", identifier);
        }

        internal static void RevokeAccessToken(string identifier) {}

        internal static void VerifyAccessToken(string identifier) {
            CallLineSdkWrapperWithIdentifier("verifyAccessToken", identifier);
        }

        internal static void GetProfile(string identifier) {
            CallLineSdkWrapperWithIdentifier("getProfile", identifier);
        }

        internal static void GetBotFriendshipStatus(string identifier) {
            CallLineSdkWrapperWithIdentifier("getBotFriendshipStatus", identifier);
        }

        internal static string GetCurrentAccessToken()
        {
            if (!Application.isPlaying) { return null; }
            if (IsInvalidRuntime(null)) { return null; }

            return lineSdkWrapper.Call<string>("getCurrentAccessToken");
        }

        private static void CallLineSdkWrapperWithIdentifier(string functionName, string identifier) {
            if (!Application.isPlaying) { return; }
            if (IsInvalidRuntime(identifier)) { return; }

            object[] parameters = new object[1];
            parameters[0] = identifier;
            lineSdkWrapper.Call(functionName, parameters);
        }

        private static bool IsInvalidRuntime(string identifier) {
            return Helpers.IsInvalidRuntime(identifier, RuntimePlatform.Android);
        }
    }
}
#endif