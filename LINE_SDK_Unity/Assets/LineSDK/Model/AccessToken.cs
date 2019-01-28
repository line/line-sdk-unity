
using System;
using UnityEngine;

namespace Line.LineSDK {
    /// <summary>
    /// Represents an access token which is used to access the LINE Platform. Most API calls 
    /// to the LINE Platform require an access token as evidence of successful authorization. 
    /// A valid access token is issued after the user grants your app the permissions that 
    /// your app requests. An access token is bound to permissions (scopes) that define the 
    /// API endpoints that you can access. Choose the permissions for your channel in the 
    /// LINE Developers site and set them in the login method used in your app.
    /// 
    /// An access token will expire after a certain period. `ExpiresIn` contains the duration
    /// to the expiration time when this access token 
    /// 
    /// By default, the LINE SDK stores an access token in a secure place on device for your app and 
    /// obtains authorization when you access the LINE Platform through the framework request methods.
    /// 
    /// Do not try to create an access token yourself. You can get the stored access token with less 
    /// properties by `LineSDK.Instance.CurrentAccessToken`.
    /// </summary>
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

        /// <summary>
        /// The value of the access token.
        /// </summary>
        public string Value { get { return access_token; } }

        /// <summary>
        /// Amount of time in seconds until the access token expires.
        /// </summary>
        public long ExpiresIn { get { return expires_in; } }

        /// <summary>
        /// The raw string value of ID token bound to the access token. The value exists only if the access token
        /// is obtained with the "openID" permission.
        /// </summary>
        public string IdTokenRaw { get { return id_token; } }

        /// <summary>
        /// The refresh token bound to the access token.
        /// </summary>
        public string RefreshToken { get { return refresh_token; } }
        
        /// <summary>
        /// Permissions granted by the user.
        /// </summary>
        public string Scope { get { return scope; } }

        /// <summary>
        /// The expected authorization type when this token used in request header. Fixed to "Bearer" for now.
        /// </summary>
        public string TokenType { get { return token_type; } }
    }
}