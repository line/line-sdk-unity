package com.linecorp.linesdk.unitywrapper.util

import com.linecorp.linesdk.unitywrapper.BuildConfig

fun runIfDebugBuild(action: () -> Unit) {
    if (BuildConfig.DEBUG) action()
}
