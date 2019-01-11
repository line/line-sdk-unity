using System;
using UnityEngine;

namespace Line.LineSDK {
    public class LoginResult {
        [SerializeField]
        private AccessToken accessToken;
        [SerializeField]
        private string scope;
        [SerializeField]
        private UserProfile userProfile;
        [SerializeField]
        private bool friendshipStatusChanged;
        
        public AccessToken AccessToken { get { return accessToken; } }
        public string Scope { get { return scope; } }
        public UserProfile UserProfile { get { return userProfile; } }
        public bool IsFriendshipStatusChanged { get { return friendshipStatusChanged; } }
    }
}