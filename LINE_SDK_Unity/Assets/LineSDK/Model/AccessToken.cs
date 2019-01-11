
using System;
using UnityEngine;

namespace Line.LineSDK {
    [Serializable]
    public class AccessToken {
        [SerializeField]
        private string access_token;
        [SerializeField]
        private long expires_in;
        [SerializeField]
        private string id_token;
        [SerializeField]
        private string refresh_token;
        [SerializeField]
        private string scope;
        [SerializeField]
        private string token_type;

        public string Value { get { return access_token; } }
        public long ExpiresIn { get { return expires_in; } }
        public string IdToken { get { return id_token; } }
        public string RefreshToken { get { return refresh_token; } }
        public string Scope { get { return scope; } }
        public string TokenType { get { return token_type; } }
    }
}