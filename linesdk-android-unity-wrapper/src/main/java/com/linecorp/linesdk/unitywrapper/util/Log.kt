package com.linecorp.linesdk.unitywrapper.util

object Log {

    fun v(tag: String, message: String) {
        runIfDebugBuild { android.util.Log.v(tag, message) }
    }

    fun v(tag: String, message: String, throwable: Throwable) {
        runIfDebugBuild { android.util.Log.v(tag, message, throwable) }
    }

    fun d(tag: String, message: String) {
        runIfDebugBuild { android.util.Log.d(tag, message) }
    }

    fun d(tag: String, message: String, throwable: Throwable) {
        runIfDebugBuild { android.util.Log.d(tag, message, throwable) }
    }

    fun i(tag: String, message: String) {
        runIfDebugBuild { android.util.Log.i(tag, message) }
    }

    fun i(tag: String, message: String, throwable: Throwable) {
        runIfDebugBuild { android.util.Log.i(tag, message, throwable) }
    }

    fun w(tag: String, message: String) {
        runIfDebugBuild { android.util.Log.w(tag, message) }
    }

    fun w(tag: String, message: String, throwable: Throwable) {
        runIfDebugBuild { android.util.Log.w(tag, message, throwable) }
    }

    fun e(tag: String, message: String) {
        runIfDebugBuild { android.util.Log.e(tag, message) }
    }

    fun e(tag: String, message: String, throwable: Throwable) {
        runIfDebugBuild { android.util.Log.e(tag, message, throwable) }
    }

}
