﻿#pragma checksum "..\..\..\Pages\ContactEditUnit.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "39479EEAC5DC522CAF5886AE5A9D9714F967E161A5648CB558226D0C32E3D982"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Expression.Media;
using HandyControl.Expression.Shapes;
using HandyControl.Interactivity;
using HandyControl.Media.Animation;
using HandyControl.Media.Effects;
using HandyControl.Properties.Langs;
using HandyControl.Themes;
using HandyControl.Tools;
using HandyControl.Tools.Converter;
using HandyControl.Tools.Extension;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using UIDisplay.Pages;


namespace UIDisplay.Pages {
    
    
    /// <summary>
    /// ContactEditUnit
    /// </summary>
    public partial class ContactEditUnit : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 22 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backBtn;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button uploadImgBtn;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button insertBtn;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button updateBtn;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image img;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox nameTextBox;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox phoneTextBox;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\..\Pages\ContactEditUnit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailTextBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/UIDisplay;component/pages/contacteditunit.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Pages\ContactEditUnit.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\Pages\ContactEditUnit.xaml"
            ((UIDisplay.Pages.ContactEditUnit)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.backBtn = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\Pages\ContactEditUnit.xaml"
            this.backBtn.Click += new System.Windows.RoutedEventHandler(this.Btn_Back_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.uploadImgBtn = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\Pages\ContactEditUnit.xaml"
            this.uploadImgBtn.Click += new System.Windows.RoutedEventHandler(this.Btn_UploadImg_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.insertBtn = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\Pages\ContactEditUnit.xaml"
            this.insertBtn.Click += new System.Windows.RoutedEventHandler(this.Btn_Insert_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.updateBtn = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\Pages\ContactEditUnit.xaml"
            this.updateBtn.Click += new System.Windows.RoutedEventHandler(this.Btn_Update_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.img = ((System.Windows.Controls.Image)(target));
            return;
            case 7:
            this.nameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.phoneTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.emailTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

