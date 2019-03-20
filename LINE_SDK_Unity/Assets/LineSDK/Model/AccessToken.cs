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
    /// Represents an access token which is used to access the LINE Platform. Most API calls
    /// to the LINE Platform require an access token as evidence of successful authorization.
    /// A valid access token is issued after the user grants your app the permissions that
    /// your app requests. An access token is bound to permissions (scopes) that define the
    /// API endpoints that you can access. Choose the permissions for your channel in the
    /// LINE Developers site and set them in the login method used in your app.
    ///
    /// An access token expires after a certain period. `ExpiresIn` specifies the time until
    /// which this access token expires.
    ///
    /// By default, the LINE SDK stores access tokens in a secure place on the device running
    /// your app and obtains authorization when you access the LINE Platform through the
    /// framework request methods.
    ///
    /// Don't try to create an access token yourself. You can get the stored access token with
    /// fewer properties with `LineSDK.Instance.CurrentAccessToken`.
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
        /// Number of seconds until the access token expires.
        /// </summary>
        public long ExpiresIn { get { return expires_in; } }

        /// <summary>
        /// The raw string value of the ID token bound to the access token. The
        /// value exists only if the access token is obtained with the "openID"
        /// permission.
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
        /// The expected authorization type when this token is used in a request
        /// header. Fixed to "Bearer" for now.
        /// </summary>
        public string TokenType { get { return token_type; } }
    }
}
