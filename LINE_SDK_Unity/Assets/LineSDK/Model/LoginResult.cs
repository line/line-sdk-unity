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
    /// Represents a result of a successful login.
    /// </summary>
    public class LoginResult {
        [SerializeField]
        private AccessToken accessToken;
        [SerializeField]
        private string scope;
        [SerializeField]
        private UserProfile userProfile;
        [SerializeField]
        private bool friendshipStatusChanged;
        [SerializeField]
        private string IDTokenNonce;

        /// <summary>
        /// The access token obtained by the login process.
        /// </summary>
        public AccessToken AccessToken { get { return accessToken; } }

        /// <summary>
        /// The scopes bound to the `AccessToken` object by the authorization process.
        /// </summary>
        public string[] Scopes { get { return scope.Split(' '); } }

        /// <summary>
        /// Contains the user profile including the user ID, display name, and so on.
        /// The value exists only when the "profile" scope is set in the authorization request.
        /// </summary>
        public UserProfile UserProfile { get { return userProfile; } }

        /// <summary>
        /// Indicates that the friendship status between the user and the bot changed during the login.
        /// This value is non-nil only if the `BotPrompt` is specified as part of the option when the
        /// user logs in. For more information, see "Linking a bot with your LINE Login channel" at
        /// https://developers.line.biz/en/docs/line-login/link-a-bot/.
        /// </summary>
        public bool IsFriendshipStatusChanged { get { return friendshipStatusChanged; } }

        /// <summary>
        /// The `nonce` value when requesting an ID token during the login process. Use this value as
        /// a parameter when you verify the ID token against the LINE server. This value is `nil` if
        /// you don't have the `.openID` permission.
        /// </summary>
        public string IdTokenNonce { get { return IDTokenNonce; } }
    }
}
