using System;
using UnityEngine;

namespace Line.LineSDK {
    public class AccessTokenVerifyResult {
        [SerializeField]
        private string client_id;
        [SerializeField]
        private string scope;
        [SerializeField]
        private long expires_in;

        public string ChannelId { get { return client_id; } }
        public string Scope { get { return scope; } }
        public long ExpiresIn { get { return expires_in; } }
    }
}