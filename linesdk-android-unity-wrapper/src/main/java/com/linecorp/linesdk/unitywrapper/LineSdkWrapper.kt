package com.linecorp.linesdk.unitywrapper

import com.google.gson.Gson
import com.linecorp.linesdk.Scope
import com.linecorp.linesdk.api.LineApiClient
import com.linecorp.linesdk.api.LineApiClientBuilder
import com.linecorp.linesdk.auth.LineAuthenticationParams
import com.linecorp.linesdk.unitywrapper.CallbackPayload.Companion.sendMessageError
import com.linecorp.linesdk.unitywrapper.activity.LineSdkWrapperActivity
import com.linecorp.linesdk.unitywrapper.model.AccessTokenForUnity
import com.linecorp.linesdk.unitywrapper.model.BotFriendshipStatus
import com.linecorp.linesdk.unitywrapper.model.UserProfile
import com.linecorp.linesdk.unitywrapper.model.VerifyAccessTokenResult
import com.linecorp.linesdk.unitywrapper.util.Log
import com.unity3d.player.UnityPlayer


class LineSdkWrapper {
    private lateinit var lineApiClient: LineApiClient
    private lateinit var channelId: String
    private val gson = Gson()

    fun setupSdk(channelId: String) {
        Log.d(TAG, "setupSdk")

        this.channelId = channelId
        val currentActivity = UnityPlayer.currentActivity
        lineApiClient = LineApiClientBuilder(currentActivity.applicationContext, channelId).build()

    }

    fun login(
        identifier: String,
        scope: String,
        onlyWebLogin: Boolean,
        botPrompt: String?
    ) {
        Log.d(TAG, "login")
        Log.d(TAG, "channelId:$channelId")
        Log.d(TAG, "scope:$scope")

        val currentActivity = UnityPlayer.currentActivity
        LineSdkWrapperActivity.startActivity(
            currentActivity,
            identifier,
            channelId,
            scope,
            onlyWebLogin,
            botPrompt ?: LineAuthenticationParams.BotPrompt.normal.name
        )
    }

    fun getProfile(identifier: String) {
        Log.d(TAG, "getProfile")
        val profile = lineApiClient.profile
        if(!profile.isSuccess) {
            sendMessageError(identifier, profile)
            return
        }

        val profileData = lineApiClient.profile.responseData
        val userProfile = UserProfile.convertLineProfile(profileData)
        val jsonString = gson.toJson(userProfile)
        Log.d(TAG, "getProfile: $jsonString")
        CallbackPayload(identifier, jsonString).sendMessageOk()
    }

    fun getCurrentAccessToken(): String {
        val accessToken = AccessTokenForUnity(
            lineApiClient.currentAccessToken.responseData.tokenString,
            lineApiClient.currentAccessToken.responseData.expiresInMillis
        )
        return gson.toJson(accessToken)
    }

    fun logout(identifier: String) {
        Log.d(TAG, "logout")
        lineApiClient.logout()
        CallbackPayload(identifier, "").sendMessageOk()
    }

    fun getBotFriendshipStatus(identifier: String) {
        Log.d(TAG, "getBotFriendShipStatus")
        val friendshipStatus = lineApiClient.friendshipStatus
        if (!friendshipStatus.isSuccess) {
            sendMessageError(identifier, friendshipStatus)
            return
        }

        val botFriendShipStatus =
            BotFriendshipStatus(lineApiClient.friendshipStatus.responseData.isFriend)
        CallbackPayload(
            identifier,
            gson.toJson(botFriendShipStatus)
        ).sendMessageOk()
    }

    fun refreshAccessToken(identifier: String) {
        Log.d(TAG, "refreshAccessToken")

        val lineApiResponse = lineApiClient.refreshAccessToken()
        if (!lineApiResponse.isSuccess) {
            sendMessageError(identifier, lineApiResponse)
            return
        }

        val accessToken = AccessTokenForUnity(
            lineApiResponse.responseData.tokenString,
            lineApiResponse.responseData.expiresInMillis
        )

        CallbackPayload(
            identifier,
            gson.toJson(accessToken)
        ).sendMessageOk()
    }

    fun verifyAccessToken(identifier: String) {
        Log.d(TAG, "verifyAccessToken")
        val lineApiResponse = lineApiClient.verifyToken()
        if(!lineApiResponse.isSuccess) {
            sendMessageError(identifier, lineApiResponse)
            return
        }

        val lineCredential = lineApiResponse.responseData
        val scopeString = Scope.join(lineCredential.scopes)
        val verifyAccessTokenResult =
            VerifyAccessTokenResult(
                channelId,
                scopeString,
                lineCredential.accessToken.expiresInMillis
            )

        CallbackPayload(
            identifier,
            gson.toJson(verifyAccessTokenResult)
        ).sendMessageOk()
    }

    companion object {
        private const val TAG: String = "LineSdkWrapper"
    }
}
