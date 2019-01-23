package com.linecorp.linesdk.unitywrapper.activity

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import android.util.Log
import com.google.gson.Gson
import com.linecorp.linesdk.LineApiResponseCode
import com.linecorp.linesdk.Scope
import com.linecorp.linesdk.auth.LineAuthenticationParams
import com.linecorp.linesdk.auth.LineLoginApi
import com.linecorp.linesdk.unitywrapper.CallbackPayload
import com.linecorp.linesdk.unitywrapper.model.ErrorForUnity
import com.linecorp.linesdk.unitywrapper.model.LoginResultForUnity

class LineSdkWrapperActivity : Activity() {
    private var onlyWebLogin: Boolean = false
    private lateinit var channelId: String
    private lateinit var identifier: String
    private lateinit var scope: List<Scope>

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        //setContentView(R.layout.activity_line_sdk_wrapper)

        parseIntent()
        startLineSdkLoginActivity()
    }

    private fun parseIntent() {
        identifier = intent.getStringExtra(KEY_IDENTIFIER) ?: ""
        channelId = intent.getStringExtra(KEY_CHANNEL_ID) ?: ""
        onlyWebLogin = intent.getBooleanExtra(KEY_ONLY_WEB_LOGIN, false)

        val scopeString = intent.getStringExtra(KEY_SCOPE) ?: ""
        scope = scopeString.split(",")
            .map { Scope.findScope(it) }
            .filter { it != null }
            .map { it -> it!!}
            .toList()
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        if (requestCode != REQUEST_CODE_LOGIN) return

        if (resultCode != Activity.RESULT_OK || data == null) {
            CallbackPayload(identifier, "login error")
                .sendMessageError()
        }

        val result = LineLoginApi.getLoginResultFromIntent(data)
        Log.d(TAG, "login result:$result")

        when(result.responseCode) {
            LineApiResponseCode.CANCEL -> {
                Log.d(TAG, "login canceled")
                val errorForUnity = ErrorForUnity(
                    result.responseCode.ordinal,
                    "login is canceled."
                )
                CallbackPayload(
                    identifier,
                    gson.toJson(
                        errorForUnity
                    )
                ).sendMessageError()
            }

            LineApiResponseCode.SUCCESS -> {
                Log.d(TAG, "login success")
                val resultJsonString = gson.toJson(
                    LoginResultForUnity.convertLineResult(result))
                CallbackPayload(
                    identifier,
                    resultJsonString
                ).sendMessageOk()
            }

            else -> {
                Log.d(TAG, "login error")
                val errorForUnity = ErrorForUnity(
                    result.responseCode.ordinal,
                    result.errorData.message ?: "login error"
                )
                CallbackPayload(
                    identifier,
                    gson.toJson(
                        errorForUnity
                    )
                )
                    .sendMessageError()
            }
        }

        finish()
    }

    private fun startLineSdkLoginActivity() {
        val lineAuthenticationParams = LineAuthenticationParams.Builder().scopes(scope).build()
        val loginIntent = if (onlyWebLogin) {
            LineLoginApi.getLoginIntentWithoutLineAppAuth(
                this,
                channelId,
                lineAuthenticationParams
            )
        } else {
            LineLoginApi.getLoginIntent(
                this,
                channelId,
                lineAuthenticationParams
            )
        }
        startActivityForResult(loginIntent,
            REQUEST_CODE_LOGIN
        )
    }

    companion object {
        private const val KEY_IDENTIFIER = "identifier"
        private const val KEY_CHANNEL_ID = "channelId"
        private const val KEY_SCOPE = "scope"
        private const val KEY_ONLY_WEB_LOGIN = "onlyWebLogin"
        private const val KEY_BOT_PROMPT = "botPrompt"

        private const val REQUEST_CODE_LOGIN: Int = 1234
        private const val TAG: String = "LineSdkWrapperActivity"
        private val gson: Gson = Gson()

        fun startActivity(
            activity: Activity,
            identifier: String,
            channelId: String,
            scope: String,
            onlyWebLogin: Boolean,
            botPrompt: String
        ) {
            val intent = Intent(activity, LineSdkWrapperActivity::class.java).apply {
                addFlags(Intent.FLAG_ACTIVITY_NO_ANIMATION)
                putExtra(KEY_IDENTIFIER, identifier)
                putExtra(KEY_CHANNEL_ID, channelId)
                putExtra(KEY_SCOPE, scope)
                putExtra(KEY_ONLY_WEB_LOGIN, onlyWebLogin)
                putExtra(KEY_BOT_PROMPT, botPrompt)
            }
            activity.startActivityForResult(intent,
                REQUEST_CODE_LOGIN
            )
        }
    }
}
