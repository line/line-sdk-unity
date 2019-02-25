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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Line.LineSDK;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainController : MonoBehaviour {

    public Image userIconImage;
    public Text displayNameText;
    public Text statusMessageText;
    public Text rawJsonText;

    public void Login() {
        var scopes = new string[] {"profile"};
        LineSDK.Instance.Login(scopes, result => {
            result.Match(
                value => {
                    StartCoroutine(UpdateProfile(value.UserProfile));
                    UpdateRawSection(value);
                },
                error => {
                    UpdateRawSection(error);
                }
            );
        });
    }

    public void GetProfile() {
        LineAPI.GetProfile(result => {
            result.Match(
                value => {
                    StartCoroutine(UpdateProfile(value));
                    UpdateRawSection(value);
                },
                error => {
                    UpdateRawSection(error);
                }
            );
        });
    }

    public void GetCurrentToken() {
        var currentToken = LineSDK.Instance.CurrentAccessToken;
        UpdateRawSection(currentToken);
    }

    public void VerifyToken() {
        LineAPI.VerifyAccessToken(result => {
            result.Match(
                value => {
                    UpdateRawSection(value);
                },
                error => {
                    UpdateRawSection(error);
                }
            );
        });
    }

    public void RefreshToken() {
        LineAPI.RefreshAccessToken(result => {
            result.Match(
                value => {
                    UpdateRawSection(value);
                },
                error => {
                    UpdateRawSection(error);
                }
            );
        });
    }

    public void GetFriendshipStatus() {
        LineAPI.GetBotFriendshipStatus(result => {
            result.Match(
                value => {
                    UpdateRawSection(value);
                },
                error => {
                    UpdateRawSection(error);
                }
            );
        });
    }

    public void Logout() {
        LineSDK.Instance.Logout(result => {
            result.Match(
                _ => {
                    ResetProfile();
                },
                error => {
                    UpdateRawSection(error);
                }
            );
        });
    }

    IEnumerator UpdateProfile(UserProfile profile) {
        if (profile.PictureUrl != null) {
            var www = UnityWebRequestTexture.GetTexture(profile.PictureUrl);
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError) {
                Debug.LogError(www.error);
            } else {
                var texture = DownloadHandlerTexture.GetContent(www);
                userIconImage.color = Color.white;
                userIconImage.sprite = Sprite.Create(
                    texture, 
                    new Rect(0, 0, texture.width, texture.height), 
                    new Vector2(0, 0));
            }
        } else {
            yield return null;
        }
        displayNameText.text = profile.DisplayName;
        statusMessageText.text = profile.StatusMessage;
    }

    void ResetProfile() {
        userIconImage.color = Color.gray;
        userIconImage.sprite = null;
        displayNameText.text = "Display Name";
        statusMessageText.text = "Status Message";
    }

    void UpdateRawSection(object obj) {
        if (obj == null) {
            rawJsonText.text = "null";
            return;
        } 
        var text = JsonUtility.ToJson(obj);
        if (text == null) {
            rawJsonText.text = "Invalid Object";
            return;
        }
        rawJsonText.text = text;
        var scrollContentTransform = (RectTransform)rawJsonText.gameObject.transform.parent;
        scrollContentTransform.localPosition = Vector3.zero;
    }
}
