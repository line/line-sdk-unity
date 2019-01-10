using System;
using UnityEngine;

namespace Line.LineSDK {
    [Serializable]
    public class CallbackPayload {
        public string identifier;
        public string value;

        public static CallbackPayload FromJson(string json) {
            return JsonUtility.FromJson<CallbackPayload>(json);
        }
    }
}