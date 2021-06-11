package com.linecorp.linesdk.unitywrapper

import com.google.gson.Gson
import com.linecorp.linesdk.LineApiResponse
import com.linecorp.linesdk.auth.LineLoginResult
import com.linecorp.linesdk.unitywrapper.model.ErrorForUnity
import com.unity3d.player.UnityPlayer


data class CallbackPayload(
    val identifier: String,
    val value: String
) {
    fun sendMessageOk() =
        UnityPlayer.UnitySendMessage(
            KEY_LINE_SDK,
            NAME_API_OK,
            generatePayloadJson()
        )

    fun sendMessageError() =
        UnityPlayer.UnitySendMessage(
            KEY_LINE_SDK,
            NAME_API_ERROR,
            generatePayloadJson()
        )

    private fun generatePayloadJson(): String = gson.toJson(this)

    companion object {
        private val gson: Gson = Gson()

        private const val KEY_LINE_SDK: String  = "LineSDK"
        private const val NAME_API_OK: String  = "OnApiOk"
        private const val NAME_API_ERROR: String  = "OnApiError"

        fun <T>sendMessageError(
            identifier: String,
            lineApiResponse: LineApiResponse<T>
        ) {
            val error = getErrorJsonString(lineApiResponse)
            CallbackPayload(identifier, error).sendMessageError()
        }

        fun sendMessageError(
            identifier: String,
            loginResult: LineLoginResult,
            errorString: String
        ) {
            val errorForUnity = ErrorForUnity(loginResult.responseCode.ordinal, errorString)
            CallbackPayload(identifier, gson.toJson(errorForUnity)).sendMessageError()
        }

        private fun <T> getErrorJsonString(lineApiResponse: LineApiResponse<T>): String {
            val error = ErrorForUnity(
                lineApiResponse.responseCode.ordinal,
                lineApiResponse.errorData.message ?: "error"
            )
            return gson.toJson(error)
        }

    }
}
