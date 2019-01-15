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
        LineSDK.Instance.Login(null, result => {
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
            yield return www.Send();
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
    }
}
