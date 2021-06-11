package com.linecorp.linesdk.unitywrapper.activity

import android.app.Activity
import android.content.Intent
import android.os.Bundle
import com.google.gson.Gson
import com.linecorp.linesdk.LineApiResponseCode
import com.linecorp.linesdk.Scope
import com.linecorp.linesdk.auth.LineAuthenticationParams
import com.linecorp.linesdk.auth.LineLoginApi
import com.linecorp.linesdk.unitywrapper.CallbackPayload
import com.linecorp.linesdk.unitywrapper.CallbackPayload.Companion.sendMessageError
import com.linecorp.linesdk.unitywrapper.model.ErrorForUnity
import com.linecorp.linesdk.unitywrapper.model.LoginResultForUnity
import com.linecorp.linesdk.unitywrapper.util.Log

class LineSdkWrapperActivity : Activity() {
    private lateinit var identifier: String

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)

        identifier = intent.getStringExtra(KEY_IDENTIFIER) ?: ""
        startLineSdkLoginActivity()
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        if (requestCode != REQUEST_CODE_LOGIN) return

        if (resultCode != Activity.RESULT_OK || data == null) {
            val errorForUnity = ErrorForUnity(-1, "login error")
            CallbackPayload(identifier, gson.toJson(errorForUnity)).sendMessageError()
        }

        val result = LineLoginApi.getLoginResultFromIntent(data)
        Log.d(TAG, "login result:$result")

        when(result.responseCode) {
            LineApiResponseCode.SUCCESS -> {
                Log.d(TAG, "login success")
                val resultJsonString = gson.toJson(LoginResultForUnity.convertLineResult(result))
                CallbackPayload(identifier,resultJsonString).sendMessageOk()
            }
            LineApiResponseCode.CANCEL -> {
                Log.d(TAG, "login canceled")
                sendMessageError(identifier, result, "login is canceled")
            }
            else -> {
                Log.d(TAG, "login error")
                sendMessageError(identifier, result, result.errorData.message ?: "login error")
            }
        }

        finish()
    }

    private fun startLineSdkLoginActivity() {
        val channelId = intent.getStringExtra(KEY_CHANNEL_ID) ?: ""
        val onlyWebLogin = intent.getBooleanExtra(KEY_ONLY_WEB_LOGIN, false)
        val botPrompt = parseBotPrompt(intent.getStringExtra(KEY_BOT_PROMPT))

        val scopeString = intent.getStringExtra(KEY_SCOPE) ?: ""
        Log.d(TAG, scopeString)

        val scope = Scope.parseToList(scopeString)
        Log.d(TAG, scope.toString())

        val builder = LineAuthenticationParams.Builder()
        builder.scopes(scope)
        if (botPrompt != null) {
            builder.botPrompt(botPrompt)
        }

        val lineAuthenticationParams = builder.build()

        val loginIntent = if (onlyWebLogin) {
            LineLoginApi.getLoginIntentWithoutLineAppAuth(this, channelId, lineAuthenticationParams)
        } else {
            LineLoginApi.getLoginIntent(this, channelId, lineAuthenticationParams)
        }

        startActivityForResult(loginIntent, REQUEST_CODE_LOGIN)
    }

    private fun parseBotPrompt(name: String?): LineAuthenticationParams.BotPrompt? {
        val nonNullName = name ?: return null

        return LineAuthenticationParams.BotPrompt.values().firstOrNull {
            it.name.equals(nonNullName, ignoreCase = true)
        }
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