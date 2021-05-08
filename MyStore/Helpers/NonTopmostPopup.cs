using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interop;

namespace MyStore.Helpers
{
    public class NonTopmostPopup : Popup
    {
        public static readonly DependencyProperty IsTopmostProperty =
            DependencyProperty.Register(
                "IsTopmost", typeof(bool), typeof(NonTopmostPopup),
                new FrameworkPropertyMetadata(false, OnIsTopmostChanged));

        private bool? _appliedTopMost;
        private bool _alreadyLoaded;
        private Window _parentWindow;

        public bool IsTopmost
        {
            get { return (bool)GetValue(IsTopmostProperty); }
            set { SetValue(IsTopmostProperty, value); }
        }

        public NonTopmostPopup()
        {
            Loaded += OnPopupLoaded;
        }


        void OnPopupLoaded(object sender, RoutedEventArgs e)
        {
            if (_alreadyLoaded)
                return;

            _alreadyLoaded = true;

            if (Child != null)
            {
                Child.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(OnChildPreviewMouseLeftButtonDown), true);
            }

            _parentWindow = Window.GetWindow(this);

            if (_parentWindow == null)
                return;

            _parentWindow.Activated += OnParentWindowActivated;
            _parentWindow.Deactivated += OnParentWindowDeactivated;
        }

        void OnParentWindowActivated(object sender, EventArgs e)
        {
            SetTopmostState(true);
        }

        void OnParentWindowDeactivated(object sender, EventArgs e)
        {
            if (IsTopmost == false)
            {
                SetTopmostState(IsTopmost);
            }
        }

        void OnChildPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            SetTopmostState(true);

            if (!_parentWindow.IsActive && IsTopmost == false)
            {
                _parentWindow.Activate();
            }
        }

        private static void OnIsTopmostChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var thisobj = (NonTopmostPopup)obj;

            thisobj.SetTopmostState(thisobj.IsTopmost);
        }

        protected override void OnOpened(EventArgs e)
        {
            SetTopmostState(IsTopmost);
        }

        private void SetTopmostState(bool isTop)
        {
            if (_appliedTopMost.HasValue && _appliedTopMost == isTop)
            {
                return;
            }

            if (Child == null)
                return;

            var hwndSource = (PresentationSource.FromVisual(Child)) as HwndSource;

            if (hwndSource == null)
                return;
            var hwnd = hwndSource.Handle;

            RECT rect;

            if (!GetWindowRect(hwnd, out rect))
                return;


            if (isTop)
            {
                SetWindowPos(hwnd, HWND_TOPMOST, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
            }
            else
            {
                SetWindowPos(hwnd, HWND_BOTTOM, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
                SetWindowPos(hwnd, HWND_TOP, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
                SetWindowPos(hwnd, HWND_NOTOPMOST, rect.Left, rect.Top, (int)Width, (int)Height, TOPMOST_FLAGS);
            }

            _appliedTopMost = isTop;
        }

        #region P/Invoke imports & definitions
        #pragma warning disable 1591
        #pragma warning disable 169 

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT

        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X,
        int Y, int cx, int cy, uint uFlags);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        private const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;

        const UInt32 SWP_FRAMECHANGED = 0x0020;
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;
        const UInt32 SWP_NOSENDCHANGING = 0x0400;

        const UInt32 TOPMOST_FLAGS =
          SWP_NOACTIVATE | SWP_NOOWNERZORDER | SWP_NOSIZE | SWP_NOMOVE | SWP_NOREDRAW | SWP_NOSENDCHANGING;

        #pragma warning restore 1591
        #pragma warning restore 169
        #endregion
    }
}