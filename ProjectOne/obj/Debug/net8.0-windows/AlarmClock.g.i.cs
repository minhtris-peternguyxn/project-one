﻿#pragma checksum "..\..\..\AlarmClock.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "414A021E69C95F3A00003774EA764067D6533343"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using ProjectOne;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace ProjectOne {
    
    
    /// <summary>
    /// AlarmClock
    /// </summary>
    public partial class AlarmClock : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HourTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock HourPlaceholder;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MinutePlaceholder;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MinuteTextBox;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SetAlarmButton;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label CurrentTimeLabel;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label StatusLabel;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HourTextBox_Copy;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\AlarmClock.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox HourTextBox_Copy1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ProjectOne;component/alarmclock.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AlarmClock.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.HourTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\AlarmClock.xaml"
            this.HourTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.HourTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.HourPlaceholder = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.MinutePlaceholder = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.MinuteTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 28 "..\..\..\AlarmClock.xaml"
            this.MinuteTextBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.MinuteTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.SetAlarmButton = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\AlarmClock.xaml"
            this.SetAlarmButton.Click += new System.Windows.RoutedEventHandler(this.SetAlarmButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.CurrentTimeLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.StatusLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.HourTextBox_Copy = ((System.Windows.Controls.TextBox)(target));
            
            #line 34 "..\..\..\AlarmClock.xaml"
            this.HourTextBox_Copy.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.HourTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.HourTextBox_Copy1 = ((System.Windows.Controls.TextBox)(target));
            
            #line 35 "..\..\..\AlarmClock.xaml"
            this.HourTextBox_Copy1.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.HourTextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

