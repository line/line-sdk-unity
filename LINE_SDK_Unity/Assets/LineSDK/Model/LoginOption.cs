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
        /// Sets this value to `true` if you only want to use web authentication flow.
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