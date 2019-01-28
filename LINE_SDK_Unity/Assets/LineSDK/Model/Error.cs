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