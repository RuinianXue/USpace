// Copyright © 2014 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

#pragma once

#include "Stdafx.h"
#include "JavascriptRootObjectWrapper.h"
#include "CefAppUnmanagedWrapper.h"

using namespace System::Threading;

namespace CefSharp
{
    namespace BrowserSubprocess
    {
        void JavascriptRootObjectWrapper::Bind(ICollection<JavascriptObject^>^ objects, const CefRefPtr<CefV8Value>& v8Value)
        {
            if (objects->Count > 0)
            {
                auto saveMethod = gcnew Func<JavascriptAsyncMethodCallback^, int64_t>(this, &JavascriptRootObjectWrapper::SaveMethodCallback);

                for each (JavascriptObject ^ obj in Enumerable::OfType<JavascriptObject^>(objects))
                {
                    if (obj->IsAsync)
                    {
                        auto wrapperObject = gcnew JavascriptAsyncObjectWrapper(_callbackRegistry, saveMethod);
                        wrapperObject->Bind(obj, v8Value);

                        _wrappedAsyncObjects->Add(wrapperObject);
                    }
#ifndef NETCOREAPP
                    else
                    {
                        if (_browserProcess == nullptr)
                        {
                            LOG(ERROR) << StringUtils::ToNative("IBrowserProcess is null, unable to bind object " + obj->JavascriptName).ToString();

                            continue;
                        }

                        auto wrapperObject = gcnew JavascriptObjectWrapper(_browserProcess);
                        wrapperObject->Bind(obj, v8Value, _callbackRegistry);

                        _wrappedObjects->Add(wrapperObject);
                    }
#endif
                }
            }
        }

        JavascriptCallbackRegistry^ JavascriptRootObjectWrapper::CallbackRegistry::get()
        {
            return _callbackRegistry;
        }

        int64_t JavascriptRootObjectWrapper::SaveMethodCallback(JavascriptAsyncMethodCallback^ callback)
        {
            auto callbackId = Interlocked::Increment(_lastCallback);
            _methodCallbacks->Add(callbackId, callback);
            return callbackId;
        }

        bool JavascriptRootObjectWrapper::TryGetAndRemoveMethodCallback(int64_t id, JavascriptAsyncMethodCallback^% callback)
        {
            bool result = false;
            if (result = _methodCallbacks->TryGetValue(id, callback))
            {
                _methodCallbacks->Remove(id);
            }
            return result;
        }
    }
}
