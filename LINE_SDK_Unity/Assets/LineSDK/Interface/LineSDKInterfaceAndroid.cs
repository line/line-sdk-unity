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
            // Ensure the LineSDK instance exists.
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
