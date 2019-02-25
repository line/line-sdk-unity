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
    internal class FlattenAction {

        private Action<string> successAction;
        private Action<string> failureAction;

        FlattenAction(Action<string> successAction, Action<string> failureAction) {
            this.successAction = successAction;
            this.failureAction = failureAction;
        }

        static internal FlattenAction JsonFlatten<T>(Action<Result<T>> action) {
            var flattenAction = new FlattenAction(
                value => {
                    var result = Result<T>.Ok(JsonUtility.FromJson<T>(value));
                    action.Invoke(result);
                },
                error => {
                    var result = Result<T>.Error(JsonUtility.FromJson<Error>(error));
                    action.Invoke(result);
                }
            );
            return flattenAction;
        }

        static internal FlattenAction UnitFlatten(Action<Result<Unit>> action) {
                var flattenAction = new FlattenAction(
                _ => {
                    var result = Result<Unit>.Ok(Unit.Value);
                    action.Invoke(result);
                },
                error => {
                    var result = Result<Unit>.Error(JsonUtility.FromJson<Error>(error));
                    action.Invoke(result);
                }
            );
            return flattenAction;
        }

        internal void CallOk(string s) {
            successAction.Invoke(s);
        }

        internal void CallError(string s) {
            failureAction.Invoke(s);
        }
    }
}