﻿#pragma checksum "..\..\..\View\ClockMainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "4FCA3F1EAE008C39C8319BF40363ABC713D3D2D3B69FBF4591A5577CB2DFE4BB"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
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


namespace TomatoClock {
    
    
    /// <summary>
    /// ClockMainWindow
    /// </summary>
    public partial class ClockMainWindow : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 45 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Viewbox Clock;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel HiddenComponents;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LastTime;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ThisTime;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas Clocking;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Line SecondHand;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.RotateTransform SecondHandRotationTransform;
        
        #line default
        #line hidden
        
        
        #line 109 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button minus;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TimeSet;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock minute;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button plus;
        
        #line default
        #line hidden
        
        
        #line 115 "..\..\..\View\ClockMainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton State;
        
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
            System.Uri resourceLocater = new System.Uri("/TomatoClock;component/view/clockmainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\ClockMainWindow.xaml"
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
            this.Clock = ((System.Windows.Controls.Viewbox)(target));
            
            #line 45 "..\..\..\View\ClockMainWindow.xaml"
            this.Clock.MouseEnter += new System.Windows.Input.MouseEventHandler(this.MouseEnter_ShowInfo);
            
            #line default
            #line hidden
            
            #line 45 "..\..\..\View\ClockMainWindow.xaml"
            this.Clock.MouseLeave += new System.Windows.Input.MouseEventHandler(this.MouseLeave_HideInfo);
            
            #line default
            #line hidden
            return;
            case 2:
            this.HiddenComponents = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.LastTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.ThisTime = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 5:
            this.Clocking = ((System.Windows.Controls.Canvas)(target));
            return;
            case 6:
            this.SecondHand = ((System.Windows.Shapes.Line)(target));
            return;
            case 7:
            this.SecondHandRotationTransform = ((System.Windows.Media.RotateTransform)(target));
            return;
            case 8:
            this.minus = ((System.Windows.Controls.Button)(target));
            
            #line 109 "..\..\..\View\ClockMainWindow.xaml"
            this.minus.Click += new System.Windows.RoutedEventHandler(this.MinusTime);
            
            #line default
            #line hidden
            return;
            case 9:
            this.TimeSet = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.minute = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.plus = ((System.Windows.Controls.Button)(target));
            
            #line 113 "..\..\..\View\ClockMainWindow.xaml"
            this.plus.Click += new System.Windows.RoutedEventHandler(this.PlusTime);
            
            #line default
            #line hidden
            return;
            case 12:
            this.State = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 125 "..\..\..\View\ClockMainWindow.xaml"
            this.State.Checked += new System.Windows.RoutedEventHandler(this.StartCount);
            
            #line default
            #line hidden
            
            #line 126 "..\..\..\View\ClockMainWindow.xaml"
            this.State.Unchecked += new System.Windows.RoutedEventHandler(this.FinishCount);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

