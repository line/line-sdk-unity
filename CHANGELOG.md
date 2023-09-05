# Change Log

## 1.3.0

### Added

* Add a way to set `IDTokenNonce` in `LoginOption`. This allows you to implement your customize nonce verification against your server. [#57](https://github.com/line/line-sdk-unity/pull/57)

### Fixed

* Fix a build issue. Now this package supports starting from Unity 2020.3.15, since any older version of Unity was shipping with an incompatible version of Gradle. [#54](https://github.com/line/line-sdk-unity/pull/54), [#55](https://github.com/line/line-sdk-unity/pull/55)
* Some terminology that is used in API references for legal purposes.

## 1.2.2

### Fixed

* Upgrade the Android native package dependency version and the repo. This fixes a build issue of using deprecated repo for Android. [#51](https://github.com/line/line-sdk-unity/issues/51)

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