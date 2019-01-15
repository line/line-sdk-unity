using UnityEngine;
using System;
using System.Collections.Generic;

namespace Line.LineSDK {
    public partial class LineAPI {

        public static void RefreshAccessToken(Action<Result<AccessToken>> action) {
            var identifier = AddAction(FlattenAction.JsonFlatten<AccessToken>(action));
            NativeInterface.RefreshAccessToken(identifier);
        }

        public static void RevokeAccessToken(Action<Result<Unit>> action) {
            var identifier = AddAction(FlattenAction.UnitFlatten(action));
            NativeInterface.RevokeAccessToken(identifier);
        }

        public static void VerifyAccessToken(Action<Result<AccessTokenVerifyResult>> action) {
            var identifier = AddAction(FlattenAction.JsonFlatten<AccessTokenVerifyResult>(action));
            NativeInterface.VerifyAccessToken(identifier);
        }

        public static void GetProfile(Action<Result<UserProfile>> action) {
            var identifier = AddAction(FlattenAction.JsonFlatten<UserProfile>(action));
            NativeInterface.GetProfile(identifier);
        }

        public static void GetBotFriendshipStatus(Action<Result<BotFriendshipStatus>> action) {
            var identifier = AddAction(FlattenAction.JsonFlatten<BotFriendshipStatus>(action));
            NativeInterface.GetBotFriendshipStatus(identifier);
        }
    }

    partial class LineAPI {
        internal static void Login(List<string> scopes, LoginOption option, Action<Result<LoginResult>> action) {
            var identifier = AddAction(FlattenAction.JsonFlatten<LoginResult>(action));
            if (scopes == null || scopes.Count == 0) {
                scopes = new List<string> {"profile"};
            }
            
            var onlyWebLogin = false;
            string botPrompt = null;
            if (option != null) {
                onlyWebLogin = option.OnlyWebLogin;
                botPrompt = option.BotPrompt;
            }
            NativeInterface.Login(string.Join(" ", scopes.ToArray()), onlyWebLogin, botPrompt, identifier);
        }

        internal static void Logout(Action<Result<Unit>> action) {
            var identifier = AddAction(FlattenAction.UnitFlatten(action));
            NativeInterface.Logout(identifier);
        }
    }

    partial class LineAPI {
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