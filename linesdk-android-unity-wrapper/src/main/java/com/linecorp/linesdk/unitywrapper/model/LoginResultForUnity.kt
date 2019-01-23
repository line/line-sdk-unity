package com.linecorp.linesdk.unitywrapper.model

import com.google.gson.annotations.SerializedName
import com.linecorp.linesdk.auth.LineLoginResult


data class LoginResultForUnity(
    @SerializedName("access_token")
    val accessToken: AccessTokenForUnity,
    val scope: String,
    val userProfile: UserProfile,
    val friendshipStatusChanged: Boolean
) {
    companion object {
        fun convertLineResult(lineLoginResult: LineLoginResult): LoginResultForUnity? {
            val accessToken = AccessTokenForUnity.convertFromLineLoginResult(
                lineLoginResult
            ) ?: return null
            val lineProfile = lineLoginResult.lineProfile ?: return null
            val scope = lineLoginResult.lineCredential?.scopes?.joinToString(",") {
                    scope -> scope.code } ?: ""
            return LoginResultForUnity(
                accessToken,
                scope,
                UserProfile.convertLineProfile(
                    lineProfile
                ),
                lineLoginResult.friendshipStatusChanged
            )
        }


    }
}
