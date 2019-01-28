using System;
using UnityEngine;

namespace Line.LineSDK {
    /// <summary>
    /// Represents a result of successful login.
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
        /// https://developers.line.me/en/docs/line-login/web/link-a-bot/.
        /// </summary>
        public bool IsFriendshipStatusChanged { get { return friendshipStatusChanged; } }
    }
}