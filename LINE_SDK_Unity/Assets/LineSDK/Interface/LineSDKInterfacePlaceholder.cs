#if !UNITY_IOS && !UNITY_ANDROID
namespace Line.LineSDK {
    internal class NativeInterface {
        internal static void SetupSDK(string channelId, string universalLinkURL) {}
        internal static void Login(string scope, bool onlyWebLogin, string botPrompt, string identifier) {
            
        }
        internal static void Logout(string identifier) {}
        internal static void RefreshAccessToken(string identifier) {}
        internal static void RevokeAccessToken(string identifier) {}
        internal static void VerifyAccessToken(string identifier) {}
        internal static void GetProfile(string identifier) {}
        internal static void GetBotFriendshipStatus(string identifier) {}
        internal static string GetCurrentAccessToken() { return null; }
    }
}
#endif