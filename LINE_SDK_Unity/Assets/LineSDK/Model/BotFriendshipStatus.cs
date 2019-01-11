using System;
using UnityEngine;

namespace Line.LineSDK {
    [Serializable]
    public class BotFriendshipStatus {
        [SerializeField]
        private bool friendFlag;

        public bool IsFriend { get { return friendFlag; } }
    }
}