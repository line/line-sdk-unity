using System;
using UnityEngine;

namespace Line.LineSDK {
    public static class Helpers {
        internal static bool IsInvalidRuntime(string identifier, RuntimePlatform platform) {
            if (Application.platform != platform) {
                Debug.LogWarning("[LINE SDK] This RuntimePlatform is not supported. Only iOS and Android is supported.");
                var errorJson = @"{""code"":-1, ""message"":""Platform not supported.""}";
                var result = CallbackPayload.WrapValue(identifier, errorJson);
                LineSDK.Instance.OnApiError(result);
                return true;
            }
            return false;
        }
    }
}

