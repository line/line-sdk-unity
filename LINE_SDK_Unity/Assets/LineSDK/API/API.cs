using UnityEngine;
using System;
using System.Collections.Generic;

namespace Line.LineSDK {
    public class API {

        static Dictionary<String, FlattenAction> actions;

        public static void RefreshAccessToken(Action<Result<UserProfile>> action) {
            var identifier = Guid.NewGuid().ToString();
            var flattenAction = FlattenAction.JsonFlatten<UserProfile>(action);
            actions.Add(identifier, flattenAction);
        }

        internal static void _OnRefreshAccessTokenOk(string result) {
            // actions.TryGetValue()
        }

        internal static void _OnError(string result) {

        }
    }
}