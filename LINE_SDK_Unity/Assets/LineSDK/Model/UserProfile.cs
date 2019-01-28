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