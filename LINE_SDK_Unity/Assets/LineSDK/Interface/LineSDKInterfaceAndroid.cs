#if UNITY_ANDROID
using UnityEngine;

namespace Line.LineSDK
{
    internal class NativeInterface
    {
        static AndroidJavaObject lineSdkWrapper = new AndroidJavaObject("linesdk.linecorp.com.sdknativemodule.LineSdkWrapper");

        static NativeInterface()
        {
            // Ensure the LineSDK instance existing.
            var _ = LineSDK.Instance;
        }

        internal static void SetupSDK(string channelId, string universalLinkURL)
        {
            object[] parameters = new object[1];
            parameters[0] = channelId;

            lineSdkWrapper.Call("setupSdk", parameters);

        }

        internal static void Login(string scope, bool onlyWebLogin, string botPrompt, string identifier)
        {
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
            return lineSdkWrapper.Call<string>("getCurrentAccessToken");
        }

        static void CallLineSdkWrapperWithIdentifier(string functionName, string identifier) {
            object[] parameters = new object[1];
            parameters[0] = identifier;
            lineSdkWrapper.Call(functionName, parameters);
        }
    }
}
#endif