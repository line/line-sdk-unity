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
        /// String specifying the access token's scope.
        /// </summary>
        /// <value></value>
        public string Scope { get { return scope; } }

        /// <summary>
        /// Number of seconds until the access token expires.
        /// </summary>
        /// <value></value>
        public long ExpiresIn { get { return expires_in; } }
    }
}
