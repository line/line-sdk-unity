package com.linecorp.linesdk.unitywrapper.model

import com.google.gson.annotations.SerializedName
import com.linecorp.linesdk.auth.LineLoginResult


data class AccessTokenForUnity(
    @SerializedName("access_token")
    val accessToken: String,
    @SerializedName("expires_in")
    val expiresIn: Long
) {
    companion object {
        fun convertFromLineLoginResult(loginResult: LineLoginResult): AccessTokenForUnity? {
            val accessToken = loginResult.lineCredential?.accessToken ?: return null
            return AccessTokenForUnity(
                accessToken.tokenString,
                accessToken.expiresInMillis
            )
        }
    }
}
