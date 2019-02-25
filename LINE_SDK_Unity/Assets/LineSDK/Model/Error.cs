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
    /// Represents error happens in Line SDK.
    /// </summary>
    [Serializable]
    public class Error {
        [SerializeField]
        private int code;
        [SerializeField]
        private string message;

        /// <summary>
        /// The error code indicates what kind of error happens.
        /// </summary>
        /// <value>
        /// This value is different depending on running platforms. Refere to Line SDK for Swift 
        /// and Line SDK for Android reference to check the error code for more.
        /// 
        /// - iOS (Swift): https://developers.line.biz/en/reference/ios-sdk-swift/Enums/LineSDKError.html
        /// - Android: https://developers.line.biz/en/reference/android-sdk/reference/com/linecorp/linesdk/LineApiResponseCode.html
        /// </value>
        public int Code { get { return code; } }

        /// <summary>
        /// A human readable error text describes the reason of error.
        /// </summary>
        public string Message { get { return message; } }

        internal Error(int code, string message) {
            this.code = code;
            this.message = message;
        }
    }
}