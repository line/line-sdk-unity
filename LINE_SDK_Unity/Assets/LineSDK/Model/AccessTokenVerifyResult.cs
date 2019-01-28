using System;
using UnityEngine;

namespace Line.LineSDK {
    /// <summary>
    /// Represents a response to the token verification API.
    /// </summary>
    public class AccessTokenVerifyResult {
        [SerializeField]
        private string client_id;
        [SerializeField]
        private string scope;
        [SerializeField]
        private long expires_in;

        /// <summary>
        /// The channel ID bound to the access token.
        /// </summary>
        public string ChannelId { get { return client_id; } }

        /// <summary>
        /// Scope string of the access token.
        /// </summary>
        /// <value></value>
        public string Scope { get { return scope; } }
        
        /// <summary>
        /// The amount of time until the access token expires.
        /// </summary>
        /// <value></value>
        public long ExpiresIn { get { return expires_in; } }
    }
}