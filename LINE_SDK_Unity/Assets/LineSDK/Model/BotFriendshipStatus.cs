//  Copyright (c) 2019-present, LY Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LY Corporation.
//
//  As with any software that integrates with the LY Corporation platform, your use of this software
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
    /// Represents a response to a request for getting the friendship status of 
    /// the user and the LINE Official Account linked to your LINE Login channel.
    /// </summary>
    [Serializable]
    public class BotFriendshipStatus {
        [SerializeField]
        private bool friendFlag;

        /// <summary>
        /// Indicates the friendship status.
        /// </summary>
        /// <value>
        /// `true` if the LINE Official Account is a friend of the user and the user has not blocked the it. 
        /// `false` if the LINE Official Account is not a friend of the user or the user has blocked the it.
        /// </value>
        public bool IsFriend { get { return friendFlag; } }
    }
}