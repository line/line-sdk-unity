# Change Log

## 1.4.1

### Fixed

* Upgrade the xcframework to the latest version 5.11.1. This fixes an Apple review issue that does not allow binary containing the bitcode. [#71](https://github.com/line/line-sdk-unity/pull/71)
* Fix an issue that the xcframework is duplicated when exporting on modern Unity versions. [#69](https://github.com/line/line-sdk-unity/issues/69)

## 1.4.0

### Added

* Mark the `LineSDK.SetupSDK` method as public. This adds a way to set the channel ID manually after the instance `Awake`. [#68](https://github.com/line/line-sdk-unity/pull/68)

## 1.3.3

### Fixed

* Upgrade the native LINE iOS SDK for Swift, in which the Privacy Manifest is contained. So LINE SDK for Unity also supports Privacy Manifest. [#66](https://github.com/line/line-sdk-unity/pull/66)

## 1.3.2

### Fixed

* Fix an issue that the ID Token verification on iOS fails if `openid` scope is declared in the login process but the nonce is not set. [#63](https://github.com/line/line-sdk-unity/pull/63)
* Now the `AccessToken.IdTokenRaw` returns the correct raw ID token string on Android. Previously it returns a JSON string that contains the parsed ID token values. The new behavior is now consistent with iOS. [#64](https://github.com/line/line-sdk-unity/pull/64)

## 1.3.1

### Fixed

* Update the license holder's name in all source code files. Now LY Corporation is the license holder of LINE SDK Swift. The license content and terms itself is not changed so you can still use the SDK under the same condition as before. [#59](https://github.com/line/line-sdk-unity/pull/59)
* Increase the minimum deploy version to iOS 13.0 and Android API Level 24 (Android 7.0) to match modern development requirements.

## 1.3.0

### Added

* Add a way to set `IDTokenNonce` in `LoginOption`. This allows you to implement your customize nonce verification against your server. [#57](https://github.com/line/line-sdk-unity/pull/57)

### Fixed

* Fix a build issue. Now this package supports starting from Unity 2020.3.15, since any older version of Unity was shipping with an incompatible version of Gradle. [#54](https://github.com/line/line-sdk-unity/pull/54), [#55](https://github.com/line/line-sdk-unity/pull/55)
* Some terminology that is used in API references for legal purposes.

## 1.2.2

### Fixed

* Upgrade the Android native package dependency version and the repo. This fixes a build issue of using a deprecated repo for Android. [#51](https://github.com/line/line-sdk-unity/issues/51)

## 1.2.1

### Fixed

* An issue that accessing `LineSDK.Instance.CurrentAccessToken` throws an exception on Android in some cases. [#49](https://github.com/line/line-sdk-unity/pull/49)

## 1.2.0

Upgrade to this version of LINE SDK Unity requires full removal of the previous version. Please remove the "LineSDK" folder and LINE SDK files under "Plugins" to perform a clean upgrade. Please follow the [installation guide](https://developers.line.biz/en/docs/unity-sdk/upgrade-guide/) for more information.

### Added

* The `xcframework` way to integrate LINE SDK Unity to the iOS project. Now the package manager is not required anymore and the integration is much easier. [#46](https://github.com/line/line-sdk-unity/pull/46)
* Unity library compatible with Android gradle file. Now the package supports building as a library, which is the default behavior in a newer version of Unity.

### Fixed

* Some internal fixes such as documentation URL.

## 1.1.6

### Fixed

* On Android, now the `BotPrompt` option can pass to the final native activity correctly. [#32](https://github.com/line/line-sdk-unity/issues/32)

## 1.1.5

### Fixed

* Carthage build should now work properly for Xcode 12. [#30](https://github.com/line/line-sdk-unity/pull/30)

## 1.1.4

### Fixed

* Add AndroidX flag to gradle property solve dependency issue.

## 1.1.3

### Fixed

* Updated the template gradle build file to make it compile on Unity 2019.3 or later.

## 1.1.2

### Fixed

* An issue which prevents exporting correct Xcode project on Unity 2019.3.

## 1.1.1

### Fixed

* A script problem that prevents Carthage building with Gem version 3.

## 1.1.0

### Added

* Add `IdTokenNonce` to `LoginResult`. This value can be used against the ID token verification API as a parameter.

## 1.0.2

### Fixed

* An issue causes Android build fails due to Editor integrating in some cases. [#9](https://github.com/line/line-sdk-unity/pull/9)

## 1.0.1

### Fixed

* An issue causes the login page cannot dismiss after screen rotation on Android.

## 1.0.0

* Initial release of LINE SDK for Unity.