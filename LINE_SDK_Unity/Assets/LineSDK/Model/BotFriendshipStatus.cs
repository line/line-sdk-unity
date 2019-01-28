using System;
using UnityEngine;

namespace Line.LineSDK {
    /// <summary>
    /// Represents a response to a request for getting the friendship status of 
    /// the user and the bot linked to your LINE Login channel.
    /// </summary>
    [Serializable]
    public class BotFriendshipStatus {
        [SerializeField]
        private bool friendFlag;

        /// <summary>
        /// Indicates the friendship status.
        /// </summary>
        /// <value>
        /// `true` if the bot is a friend of the user and the user has not blocked the bot. 
        /// `false` if the bot is not a friend of the user or the user has blocked the bot.
        /// </value>
        public bool IsFriend { get { return friendFlag; } }
    }
}