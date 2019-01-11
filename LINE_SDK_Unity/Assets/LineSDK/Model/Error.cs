using System;
using UnityEngine;

namespace Line.LineSDK {
    [Serializable]
    public class Error {
        [SerializeField]
        private int code;
        [SerializeField]
        private string message;

        public int Code { get { return code; } }
        public string Message { get { return message; } }

        internal Error(int code, string message) {
            this.code = code;
            this.message = message;
        }
    }
}