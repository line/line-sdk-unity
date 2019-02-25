//  Copyright (c) 2019-present, LINE Corporation. All rights reserved.
//
//  You are hereby granted a non-exclusive, worldwide, royalty-free license to use,
//  copy and distribute this software in source code or binary form for use
//  in connection with the web services and APIs provided by LINE Corporation.
//
//  As with any software that integrates with the LINE Corporation platform, your use of this software
//  is subject to the LINE Developers Agreement [http://terms2.line.me/LINE_Developers_Agreement].
//  This copyright notice shall be included in all copies or substantial portions of the software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//  INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//  FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.
//  IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//  DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//  OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using System;
using UnityEngine;

namespace Line.LineSDK {
    [Serializable]
    internal class CallbackPayload {
        [SerializeField]
        private string identifier;
        [SerializeField]
        private string value;

        internal string Identifier { get { return identifier; } }
        internal string Value { get { return value; } }

        internal static CallbackPayload FromJson(string json) {
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