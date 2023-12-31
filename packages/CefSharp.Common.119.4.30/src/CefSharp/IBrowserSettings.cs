// Copyright © 2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;

namespace CefSharp
{
    /// <summary>
    /// Interface representing browser initialization settings. 
    /// </summary>
    public interface IBrowserSettings : IDisposable
    {
        /// <summary>
        /// StandardFontFamily
        /// </summary>
        string StandardFontFamily { get; set; }

        /// <summary>
        /// FixedFontFamily
        /// </summary>
        string FixedFontFamily { get; set; }

        /// <summary>
        /// SerifFontFamily
        /// </summary>
        string SerifFontFamily { get; set; }

        /// <summary>
        /// SansSerifFontFamily
        /// </summary>
        string SansSerifFontFamily { get; set; }

        /// <summary>
        /// CursiveFontFamily
        /// </summary>
        string CursiveFontFamily { get; set; }

        /// <summary>
        /// FantasyFontFamily
        /// </summary>
        string FantasyFontFamily { get; set; }

        /// <summary>
        /// DefaultFontSize
        /// </summary>
        int DefaultFontSize { get; set; }

        /// <summary>
        /// DefaultFixedFontSize
        /// </summary>
        int DefaultFixedFontSize { get; set; }

        /// <summary>
        /// MinimumFontSize
        /// </summary>
        int MinimumFontSize { get; set; }

        /// <summary>
        /// MinimumLogicalFontSize
        /// </summary>
        int MinimumLogicalFontSize { get; set; }

        /// <summary>
        /// Default encoding for Web content. If empty "ISO-8859-1" will be used. Also
        /// configurable using the "default-encoding" command-line switch.
        /// </summary>
        string DefaultEncoding { get; set; }

        /// <summary>
        /// Controls the loading of fonts from remote sources. Also configurable using
        /// the "disable-remote-fonts" command-line switch.
        /// </summary>
        CefState RemoteFonts { get; set; }

        /// <summary>
        /// Controls whether JavaScript can be executed. (Used to Enable/Disable javascript)
        /// Also configurable using the "disable-javascript" command-line switch.
        /// </summary>
        CefState Javascript { get; set; }

        /// <summary>
        /// Controls whether JavaScript can be used to close windows that were not
        /// opened via JavaScript. JavaScript can still be used to close windows that
        /// were opened via JavaScript. Also configurable using the
        /// "disable-javascript-close-windows" command-line switch.
        /// </summary>
        CefState JavascriptCloseWindows { get; set; }

        /// <summary>
        /// Controls whether JavaScript can access the clipboard. Also configurable
        /// using the "disable-javascript-access-clipboard" command-line switch.
        /// </summary>
        CefState JavascriptAccessClipboard { get; set; }

        /// <summary>
        /// Controls whether DOM pasting is supported in the editor via
        /// execCommand("paste"). The |javascript_access_clipboard| setting must also
        /// be enabled. Also configurable using the "disable-javascript-dom-paste"
        /// command-line switch.
        /// </summary>
        CefState JavascriptDomPaste { get; set; }

        /// <summary>
        /// Controls whether image URLs will be loaded from the network. A cached image
        /// will still be rendered if requested. Also configurable using the
        /// "disable-image-loading" command-line switch.
        /// </summary>
        CefState ImageLoading { get; set; }

        /// <summary>
        /// Controls whether standalone images will be shrunk to fit the page. Also
        /// configurable using the "image-shrink-standalone-to-fit" command-line
        /// switch.
        /// </summary>
        CefState ImageShrinkStandaloneToFit { get; set; }

        /// <summary>
        /// Controls whether text areas can be resized. Also configurable using the
        /// "disable-text-area-resize" command-line switch.
        /// </summary>
        CefState TextAreaResize { get; set; }

        /// <summary>
        /// Controls whether the tab key can advance focus to links. Also configurable
        /// using the "disable-tab-to-links" command-line switch.
        /// </summary>
        CefState TabToLinks { get; set; }

        /// <summary>
        /// Controls whether local storage can be used. Also configurable using the
        /// "disable-local-storage" command-line switch.
        /// </summary>
        CefState LocalStorage { get; set; }

        /// <summary>
        /// Controls whether databases can be used. Also configurable using the
        /// "disable-databases" command-line switch.
        /// </summary>
        CefState Databases { get; set; }

        /// <summary>
        /// Controls whether WebGL can be used. Note that WebGL requires hardware
        /// support and may not work on all systems even when enabled. Also
        /// configurable using the "disable-webgl" command-line switch.
        /// </summary>
        CefState WebGl { get; set; }

        /// <summary>
        /// Opaque background color used for the browser before a document is loaded
        /// and when no document color is specified. By default the background color
        /// will be the same as CefSettings.BackgroundColor. Only the RGB compontents
        /// of the specified value will be used. The alpha component must greater than
        /// 0 to enable use of the background color but will be otherwise ignored.
        /// </summary>
        uint BackgroundColor { get; set; }

        /// <summary>
        /// The maximum rate in frames per second (fps) that CefRenderHandler::OnPaint
        /// will be called for a windowless browser. The actual fps may be lower if
        /// the browser cannot generate frames at the requested rate. The minimum
        /// value is 1 and the maximum value is 60 (default 30). This value can also be
        /// changed dynamically via IBrowserHost.SetWindowlessFrameRate.
        /// </summary>
        int WindowlessFrameRate { get; set; }

        /// <summary>
        /// Gets a value indicating if the browser settings has been disposed.
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// Gets a value indicating if the browser settings instance was created internally by CefSharp.
        /// Instances created by CefSharp will be Disposed of after use. To control the lifespan yourself
        /// create an set BrowserSettings yourself.
        /// </summary>
        bool AutoDispose { get; }

        /// <summary>
        /// Used internally to get the underlying <see cref="IBrowserSettings"/> instance.
        /// Unlikely you'll use this yourself.
        /// </summary>
        /// <returns>the inner most instance</returns>
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        IBrowserSettings UnWrap();
    }
}
