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
        /// <param name="value">The value represets successful result.</param>
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
        /// <param name="error">The error represets failed result.</param>
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