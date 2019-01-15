using System;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace Line.LineSDK {
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

        public string UserId { get { return userId; } }
        public string DisplayName { get { return displayName; } }
        public string StatusMessage { get { return statusMessage; } }

        public string PictureUrl {
            get {
                if (pictureUrl == null) { return null; }
                return pictureUrl;
            }
        }

        public string PictureUrlLarge {
            get {
                if (pictureUrl == null) { return null; }
                return Path.Combine(pictureUrl, "large");
            }
        }

        public string PictureUrlSmall {
            get {
                if (pictureUrl == null) { return null; }
                return Path.Combine(pictureUrl, "small");
            }
        }
    }
}