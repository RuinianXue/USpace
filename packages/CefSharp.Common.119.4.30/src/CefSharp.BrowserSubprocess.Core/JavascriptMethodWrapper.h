// Copyright © 2014 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

#pragma once

#include "Stdafx.h"
#include "include/cef_v8.h"

#include "JavascriptMethodHandler.h"

using namespace System::Runtime::Serialization;
using namespace CefSharp::Internals::Wcf;

namespace CefSharp
{
    namespace BrowserSubprocess
    {
        private ref class JavascriptMethodWrapper
        {
        private:
            MCefRefPtr<JavascriptMethodHandler> _javascriptMethodHandler;
            int64_t _ownerId;
            String^ _javascriptMethodName;
            IBrowserProcess^ _browserProcess;

        public:
            JavascriptMethodWrapper(int64_t ownerId, IBrowserProcess^ browserProcess, JavascriptCallbackRegistry^ callbackRegistry)
            {
                _ownerId = ownerId;
                _browserProcess = browserProcess;
                _javascriptMethodHandler = new JavascriptMethodHandler(gcnew Func<array<Object^>^, BrowserProcessResponse^>(this, &JavascriptMethodWrapper::Execute), callbackRegistry);
            }

            !JavascriptMethodWrapper()
            {
                _javascriptMethodHandler = nullptr;
            }

            ~JavascriptMethodWrapper()
            {
                this->!JavascriptMethodWrapper();

                _browserProcess = nullptr;
            }

            void Bind(JavascriptMethod^ javascriptMethod, const CefRefPtr<CefV8Value>& v8Value);
            BrowserProcessResponse^ Execute(array<Object^>^ parameters);
        };
    }
}
