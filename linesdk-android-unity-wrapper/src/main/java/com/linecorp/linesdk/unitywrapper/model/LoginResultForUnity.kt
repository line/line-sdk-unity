package com.linecorp.linesdk.unitywrapper.model

import com.linecorp.linesdk.Scope
import com.linecorp.linesdk.auth.LineLoginResult


data class LoginResultForUnity(
    val accessToken: AccessTokenForUnity,
    val scope: String,
    val userProfile: UserProfile?,
    val friendshipStatusChanged: Boolean,
    val IDTokenNonce: String?
) {
    companion object {
        fun convertLineResult(lineLoginResult: LineLoginResult): LoginResultForUnity? {
            val accessToken = AccessTokenForUnity.convertFromLineLoginResult(lineLoginResult) ?: return null
            val lineProfile = lineLoginResult.lineProfile?.let {
                UserProfile.convertLineProfile(it)
            } ?: null
            val scope = lineLoginResult.lineCredential?.scopes?.let {
                Scope.join(it)
            } ?: ""
            return LoginResultForUnity(
                accessToken,
                scope,
                lineProfile,
                lineLoginResult.friendshipStatusChanged,
                lineLoginResult.nonce
            )
        }


    }
}
