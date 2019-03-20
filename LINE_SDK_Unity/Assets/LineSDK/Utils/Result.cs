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
    public class Result<T> {

        private T value;
        private Error error;

        private bool success;

        /// <summary>
        /// Whether this result represents a success value or not.
        /// </summary>
        public bool IsSuccess { get { return success; } }
        /// <summary>
        /// Whether this result represents a failure value or not.
        /// </summary>
        public bool IsFailure { get { return !success; } }

        /// <summary>
        /// Creates a new success result with an input value.
        /// </summary>
        /// <param name="value">The value represents a successful result.</param>
        /// <returns>The initialized result with a success value.</returns>
        public static Result<T> Ok(T value) {
            var r = new Result<T>();
            r.value = value;
            r.success = true;
            return r;
        }

        /// <summary>
        /// Creates a new failure result with an input error.
        /// </summary>
        /// <param name="error">The error represents a failed result.</param>
        /// <returns>The initialized result with an error.</returns>
        public static Result<T> Error(Error error) {
            var r = new Result<T>();
            r.error = error;
            r.success = false;
            return r;
        }

        /// <summary>
        /// Matches when the result represents a success value.
        /// </summary>
        /// <param name="onMatched">The action to be called if `this` is a success value.</param>
        public void MatchOk(System.Action<T> onMatched) {
            if (onMatched == null) throw new System.Exception("Match callback is null!");
            if (IsSuccess) { onMatched(value); }
        }

        /// <summary>
        /// Matches when the result represents a failure value.
        /// </summary>
        /// <param name="onMatched">The action to be called if `this` is a failure value.</param>
        public void MatchError(System.Action<Error> onMatched) {
            if (onMatched == null) throw new System.Exception("Match callback is null!");
            if (IsFailure) { onMatched(error); }
        }

        /// <summary>
        /// Matches either when the result represents a success value or a failure value.
        /// </summary>
        /// <param name="onMatchedOk">The action to be called if `this` is a success value.</param>
        /// <param name="onMatchedError">The action to be called if `this` is a failure value.</param>
        public void Match(System.Action<T> onMatchedOk, System.Action<Error> onMatchedError)
        {
            MatchOk(onMatchedOk);
            MatchError(onMatchedError);
        }
    }

    /// <summary>
    /// Represents an empty result value. It represents a success signal without any data.
    /// </summary>
    public class Unit {
        /// <summary>
        /// The only empty value of `Unit`.
        /// </summary>
        public static Unit Value = new Unit();
    }
}
