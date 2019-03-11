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

namespace Line.LineSDK {
    /// <summary>
    /// Represents options for logging in to the LINE Platform.
    /// </summary>
    public class LoginOption {
        /// <summary>
        /// Uses the web authentication flow instead of the LINE app-to-app authentication flow.
        /// </summary>
        /// <value>
        /// Set to `true` if you only want to use web authentication flow.
        /// </value>
        public bool OnlyWebLogin { get; set; }

        /// <summary>
        /// Strategy used to show "adding bot as friend" option on the consent screen.
        /// </summary>
        /// <value>
        /// - "normal": Includes an option to add a bot as friend on the consent screen.
        /// - "aggressive": Opens a new screen to add a bot as a friend after the user agrees to the permissions on the consent screen.
        /// </value>
        public string BotPrompt { get; set; }
    }
}
