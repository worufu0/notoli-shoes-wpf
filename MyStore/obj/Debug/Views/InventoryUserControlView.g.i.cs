﻿#pragma checksum "..\..\..\Views\InventoryUserControlView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "0A18E3A7CAD4F0AC5802D2220D3CBF49C234720059F0A6C423B8412BBF68B29E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Core;
using Microsoft.Xaml.Behaviors.Input;
using Microsoft.Xaml.Behaviors.Layout;
using Microsoft.Xaml.Behaviors.Media;
using MyStore.Helpers;
using MyStore.ViewModels;
using MyStore.Views;
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


namespace MyStore.Views {
    
    
    /// <summary>
    /// InventoryUserControlView
    /// </summary>
    public partial class InventoryUserControlView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Views.InventoryUserControlView inventoryUserControl;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox categoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton addCategoryToggleButton;
        
        #line default
        #line hidden
        
        
        #line 81 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NonTopmostPopup addCategoryPopup;
        
        #line default
        #line hidden
        
        
        #line 106 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupAddCategoryTextBox;
        
        #line default
        #line hidden
        
        
        #line 112 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button popupAddCategoryButton;
        
        #line default
        #line hidden
        
        
        #line 136 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton updateCategoryToggleButton;
        
        #line default
        #line hidden
        
        
        #line 144 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NonTopmostPopup updateCategoryPopup;
        
        #line default
        #line hidden
        
        
        #line 168 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupUpdateCategoryTextBox;
        
        #line default
        #line hidden
        
        
        #line 175 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button popupUpdateCategoryButton;
        
        #line default
        #line hidden
        
        
        #line 201 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox searchBox;
        
        #line default
        #line hidden
        
        
        #line 204 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.Effects.DropShadowEffect effect;
        
        #line default
        #line hidden
        
        
        #line 247 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton addProductToggleButton;
        
        #line default
        #line hidden
        
        
        #line 255 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NonTopmostPopup addProductPopup;
        
        #line default
        #line hidden
        
        
        #line 295 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupAddProductNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 308 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupAddProductColorTextBox;
        
        #line default
        #line hidden
        
        
        #line 316 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupAddProductPriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 326 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox popupAddProductCategoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 345 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupAddProductTotalTextBox;
        
        #line default
        #line hidden
        
        
        #line 353 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupAddProductSoldTextBox;
        
        #line default
        #line hidden
        
        
        #line 361 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupAddProductRemainTextBox;
        
        #line default
        #line hidden
        
        
        #line 370 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupAddProductDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 399 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton updateProductToggleButton;
        
        #line default
        #line hidden
        
        
        #line 407 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NonTopmostPopup updateProductPopup;
        
        #line default
        #line hidden
        
        
        #line 447 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupUpdateProductNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 461 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupUpdateProductColorTextBox;
        
        #line default
        #line hidden
        
        
        #line 470 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupUpdateProductPriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 481 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox popupUpdateProductCategoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 500 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupUpdateProductTotalTextBox;
        
        #line default
        #line hidden
        
        
        #line 509 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupUpdateProductSoldTextBox;
        
        #line default
        #line hidden
        
        
        #line 518 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MyStore.Helpers.NumericTextBox popupUpdateProductRemainTextBox;
        
        #line default
        #line hidden
        
        
        #line 528 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox popupUpdateProductDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 563 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView productListView;
        
        #line default
        #line hidden
        
        
        #line 799 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailIdTextBox;
        
        #line default
        #line hidden
        
        
        #line 808 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailSkuTextBox;
        
        #line default
        #line hidden
        
        
        #line 818 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailNameTextBox;
        
        #line default
        #line hidden
        
        
        #line 832 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailColorTextBox;
        
        #line default
        #line hidden
        
        
        #line 841 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailPriceTextBox;
        
        #line default
        #line hidden
        
        
        #line 851 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox detailCategoryComboBox;
        
        #line default
        #line hidden
        
        
        #line 871 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailTotalTextBox;
        
        #line default
        #line hidden
        
        
        #line 880 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailSoldTextBox;
        
        #line default
        #line hidden
        
        
        #line 889 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailRemainTextBox;
        
        #line default
        #line hidden
        
        
        #line 899 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox detailDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 911 "..\..\..\Views\InventoryUserControlView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView imageListView;
        
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
            System.Uri resourceLocater = new System.Uri("/MyStore;component/views/inventoryusercontrolview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\InventoryUserControlView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.inventoryUserControl = ((MyStore.Views.InventoryUserControlView)(target));
            return;
            case 2:
            this.categoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.addCategoryToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 4:
            this.addCategoryPopup = ((MyStore.Helpers.NonTopmostPopup)(target));
            return;
            case 5:
            this.popupAddCategoryTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.popupAddCategoryButton = ((System.Windows.Controls.Button)(target));
            return;
            case 7:
            this.updateCategoryToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 8:
            this.updateCategoryPopup = ((MyStore.Helpers.NonTopmostPopup)(target));
            return;
            case 9:
            this.popupUpdateCategoryTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 10:
            this.popupUpdateCategoryButton = ((System.Windows.Controls.Button)(target));
            return;
            case 11:
            this.searchBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 12:
            this.effect = ((System.Windows.Media.Effects.DropShadowEffect)(target));
            return;
            case 13:
            this.addProductToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 14:
            this.addProductPopup = ((MyStore.Helpers.NonTopmostPopup)(target));
            return;
            case 15:
            this.popupAddProductNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 16:
            this.popupAddProductColorTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 17:
            this.popupAddProductPriceTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 18:
            this.popupAddProductCategoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 19:
            this.popupAddProductTotalTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 20:
            this.popupAddProductSoldTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 21:
            this.popupAddProductRemainTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 22:
            this.popupAddProductDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 23:
            this.updateProductToggleButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            return;
            case 24:
            this.updateProductPopup = ((MyStore.Helpers.NonTopmostPopup)(target));
            return;
            case 25:
            this.popupUpdateProductNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 26:
            this.popupUpdateProductColorTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 27:
            this.popupUpdateProductPriceTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 28:
            this.popupUpdateProductCategoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 29:
            this.popupUpdateProductTotalTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 30:
            this.popupUpdateProductSoldTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 31:
            this.popupUpdateProductRemainTextBox = ((MyStore.Helpers.NumericTextBox)(target));
            return;
            case 32:
            this.popupUpdateProductDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 33:
            this.productListView = ((System.Windows.Controls.ListView)(target));
            return;
            case 34:
            this.detailIdTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 35:
            this.detailSkuTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 36:
            this.detailNameTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 37:
            this.detailColorTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 38:
            this.detailPriceTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 39:
            this.detailCategoryComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 40:
            this.detailTotalTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 41:
            this.detailSoldTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 42:
            this.detailRemainTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 43:
            this.detailDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 44:
            this.imageListView = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

