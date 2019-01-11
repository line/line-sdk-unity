using UnityEngine;
using System;
using System.Collections.Generic;

namespace Line.LineSDK {
    public partial class API {

        public static void RefreshAccessToken(Action<Result<AccessToken>> action) {
            var identifier = AddAction(FlattenAction.JsonFlatten<AccessToken>(action));
            NativeInterface.RefreshAccessToken(identifier);
        }
    }

    partial class API {
        internal static Dictionary<String, FlattenAction> actions = new Dictionary<string, FlattenAction>();
        static string AddAction(FlattenAction action) {
            var identifier = Guid.NewGuid().ToString();
            actions.Add(identifier, action);
            return identifier;
        }

        static FlattenAction PopActionFromPayload(CallbackPayload payload) {
            var identifier = payload.Identifier;
            if (identifier == null) {
                return null;
            }
            FlattenAction action = null;
            if (actions.TryGetValue(identifier, out action)) {
                actions.Remove(identifier);
                return action;
            }
            return null;
        }

        internal static void _OnApiOk(string result) {
            var payload = CallbackPayload.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null) {
                action.CallOk(payload.Value);
            }
        }

        internal static void _OnApiError(string result) {
            var payload = CallbackPayload.FromJson(result);
            var action = PopActionFromPayload(payload);
            if (action != null) {
                action.CallError(payload.Value);
            }
        }
    }
}