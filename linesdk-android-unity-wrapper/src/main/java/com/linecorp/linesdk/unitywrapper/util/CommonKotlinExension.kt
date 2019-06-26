package com.linecorp.linesdk.unitywrapper.util

import com.linecorp.linesdk.unitywrapper.BuildConfig

inline fun runIfDebugBuild(action: () -> Unit) {
    if (BuildConfig.DEBUG) action()
}
