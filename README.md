# LINE SDK for Unity

## Overview

This repo contains LINE SDK for Unity. It allows you use LINE Login and LINE API in your Unity games easier.

## Features

The LINE SDK for Unity provides the following features.

### User authentication

This feature allows users to log in to your service with their LINE accounts. With the help of the LINE SDK for Unity, it has never been easier to integrate LINE Login into your app. Your users will automatically log in to your app without entering their LINE credentials if they are already logged in to LINE on their iOS/Android devices. This offers a great way for users to get started with your app without having to go through a registration process.

### Utilizing user data with OpenID support

Once the user authorizes, you can get the user’s LINE profile. You can utilize the user's information registered in LINE without building your user system.

The LINE SDK supports the OpenID Connect 1.0 specification. You can get ID tokens that contain the user’s LINE profile when you retrieve the access token.

## Using the SDK

### Prerequisites

* iOS 10.0 or later as the deployment target.
* Android `minSdkVersion` set to 17 or higher (Android 4.2 or later).
* Unity 2017.4 or later.

To use the LINE SDK with your game, follow the steps below.

* Create a channel. 
* Integrate LINE Login into your Unity project.
* Make API calls from your game using the SDK or from server-side.

For more information, refer to the [LINE SDK for Unity guide](https://developers.line.biz/en/docs/unity-sdk/) on the [LINE Developers site](https://developers.line.biz).

### Trying the starter app

To have a quick look at the features of the LINE SDK, try our starter app by following the steps below:

1. Clone the repository.

    ```git clone https://github.com/line/line-sdk-unity.git```

2. Open the Unity project under "LINE_SDK_Unity" folder.

3. Export the scene to either iOS or Android, and deploy it to your device and run.

> On iOS, you need CocoaPods installed to integrate the SDK to the exported project. For more information, see the ["Setting up project"](https://developers.line.biz/en/docs/unity-sdk/project-setup/) guide.

## Contributing

If you believe you have discovered a vulnerability or have an issue related to security, please **DO NOT** open a public issue. Instead, send us a mail to [dl_oss_dev@linecorp.com](mailto:dl_oss_dev@linecorp.com).

For contributing to this project, please see [CONTRIBUTING.md](https://github.com/line/line-sdk-unity/blob/master/CONTRIBUTING.md).
