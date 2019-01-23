package com.linecorp.linesdk.unitywrapper

import com.google.gson.Gson
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
    }
}
