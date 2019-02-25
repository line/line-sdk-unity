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
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Line.LineSDK {

    /// <summary>
    /// Represents a user profile used in LineSDK.
    /// </summary>
    [Serializable]
    public class UserProfile {
        [SerializeField]
        private string userId;
        [SerializeField]
        private string displayName;
        [SerializeField]
        private string pictureUrl;
        [SerializeField]
        private string statusMessage;

        /// <summary>
        /// The user ID of the current authorized user.
        /// </summary>
        public string UserId { get { return userId; } }

        /// <summary>
        /// The display name of the current authorized user.
        /// </summary>
        public string DisplayName { get { return displayName; } }

        /// <summary>
        /// The status message of the current authorized user. 
        /// Empty or `null` if the user has not set a status message.
        /// </summary>
        public string StatusMessage { get { return statusMessage; } }

        /// <summary>
        /// The profile image URL of the current authorized user. 
        /// Empty or `null` if the user has not set a profile image.
        /// </summary>
        public string PictureUrl { get { return pictureUrl; } }

        /// <summary>
        /// The large profile image URL of the current authorized user. 
        /// Empty or `null` if the user has not set a profile image.
        /// </summary>
        public string PictureUrlLarge {
            get {
                if (pictureUrl == null) { return null; }
                return Path.Combine(pictureUrl, "large");
            }
        }

        /// <summary>
        /// The small profile image URL of the current authorized user. 
        /// Empty or `null` if the user has not set a profile image.
        /// </summary>
        public string PictureUrlSmall {
            get {
                if (pictureUrl == null) { return null; }
                return Path.Combine(pictureUrl, "small");
            }
        }
    }
}