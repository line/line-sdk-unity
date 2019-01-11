using System;
using System.Collections.Generic;
using UnityEngine;

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

        public Uri PictureUrl {
            get {
                if (pictureUrl == null) { return null; }
                return new Uri(pictureUrl);
            }
        }

        public Uri PictureUrlLarge {
            get {
                if (pictureUrl == null) { return null; }
                return new Uri(PictureUrl, "/large");
            }
        }

        public Uri PictureUrlSmall {
            get {
                if (pictureUrl == null) { return null; }
                return new Uri(PictureUrl, "/small");
            }
        }
    }
}