﻿#pragma checksum "..\..\DataWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "70B92A027033DA4E0ABA1659089250C6437B6BB2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PLWPF;
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


namespace PLWPF {
    
    
    /// <summary>
    /// DataWindow
    /// </summary>
    public partial class DataWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button nanniesButton;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button mothersButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button childsButton;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button contractsButton;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitButton;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DetailButton;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\DataWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock dataTextBlock;
        
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
            System.Uri resourceLocater = new System.Uri("/PLWPF;component/datawindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\DataWindow.xaml"
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
            this.nanniesButton = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\DataWindow.xaml"
            this.nanniesButton.Click += new System.Windows.RoutedEventHandler(this.Click_nannies);
            
            #line default
            #line hidden
            
            #line 21 "..\..\DataWindow.xaml"
            this.nanniesButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.nanniesButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 21 "..\..\DataWindow.xaml"
            this.nanniesButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.nanniesButton_MouseMove);
            
            #line default
            #line hidden
            return;
            case 2:
            this.mothersButton = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\DataWindow.xaml"
            this.mothersButton.Click += new System.Windows.RoutedEventHandler(this.Click_mothers);
            
            #line default
            #line hidden
            
            #line 24 "..\..\DataWindow.xaml"
            this.mothersButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.mothersButton_MouseMove);
            
            #line default
            #line hidden
            
            #line 24 "..\..\DataWindow.xaml"
            this.mothersButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.mothersButton_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.childsButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\DataWindow.xaml"
            this.childsButton.Click += new System.Windows.RoutedEventHandler(this.Click_child);
            
            #line default
            #line hidden
            
            #line 27 "..\..\DataWindow.xaml"
            this.childsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.childsButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 27 "..\..\DataWindow.xaml"
            this.childsButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.childsButton_MouseMove);
            
            #line default
            #line hidden
            return;
            case 4:
            this.contractsButton = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\DataWindow.xaml"
            this.contractsButton.Click += new System.Windows.RoutedEventHandler(this.Click_Contracts);
            
            #line default
            #line hidden
            
            #line 30 "..\..\DataWindow.xaml"
            this.contractsButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.contractsButton_MouseLeave);
            
            #line default
            #line hidden
            
            #line 30 "..\..\DataWindow.xaml"
            this.contractsButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.contractsButton_MouseMove);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ExitButton = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\DataWindow.xaml"
            this.ExitButton.Click += new System.Windows.RoutedEventHandler(this.ClickExit);
            
            #line default
            #line hidden
            
            #line 42 "..\..\DataWindow.xaml"
            this.ExitButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.exit_MouseLeave);
            
            #line default
            #line hidden
            
            #line 42 "..\..\DataWindow.xaml"
            this.ExitButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.exit_MouseMove);
            
            #line default
            #line hidden
            return;
            case 6:
            this.DetailButton = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\DataWindow.xaml"
            this.DetailButton.Click += new System.Windows.RoutedEventHandler(this.Click_details);
            
            #line default
            #line hidden
            
            #line 43 "..\..\DataWindow.xaml"
            this.DetailButton.MouseLeave += new System.Windows.Input.MouseEventHandler(this.detail_MouseLeave);
            
            #line default
            #line hidden
            
            #line 43 "..\..\DataWindow.xaml"
            this.DetailButton.MouseMove += new System.Windows.Input.MouseEventHandler(this.detail_MouseMove);
            
            #line default
            #line hidden
            return;
            case 7:
            this.title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 8:
            this.dataTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

