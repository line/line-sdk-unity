using System;
using UnityEngine;

namespace Line.LineSDK {
    [Serializable]
    public class CallbackPayload {
        [SerializeField]
        private string identifier;
        [SerializeField]
        private string value;

        public string Identifier { get { return identifier; } }
        public string Value { get { return value; } }

        public static CallbackPayload FromJson(string json) {
            return JsonUtility.FromJson<CallbackPayload>(json);
        }

        CallbackPayload(string identifier, string value) {
            this.identifier = identifier;
            this.value = value;
        }

        string ToJson() {
            return JsonUtility.ToJson(this);
        }

        internal static string WrapValue(string identifier, string value) {
            var payload = new CallbackPayload(identifier, value);
            return payload.ToJson();
        }
    }
}