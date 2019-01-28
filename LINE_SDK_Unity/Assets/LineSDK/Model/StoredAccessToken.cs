
using System;
using UnityEngine;

namespace Line.LineSDK {
    /// <summary>
    /// Represents the access token stored on the device.
    /// </summary>
    [Serializable]
    public class StoredAccessToken {
        [SerializeField]
        private string access_token;
        [SerializeField]
        private long expires_in;
        
        /// <summary>
        /// The value of the access token.
        /// </summary>
        public string Value { get { return access_token; } }
        
        /// <summary>
        /// Amount of time in seconds until the access token expires. Be careful that this value is stored when the 
        /// token was created and it will not get updated. To know the latest `ExpiresIn` for a token, 
        /// call `API.VerifyAccessToken` instead.
        /// </summary>
        public long ExpiresIn { get { return expires_in; } }
    }
}