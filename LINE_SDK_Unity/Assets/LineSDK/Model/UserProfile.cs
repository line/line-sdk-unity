using System;
using System.Collections.Generic;

namespace Line.LineSDK {
    [Serializable]
    public class UserProfile {
        public string userId;
        public string displayName;
        public string pictureUrl;
        public string statusMessage;

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