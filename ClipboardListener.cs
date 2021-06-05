using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace ErogeImgSave
{
    /// <summary>
    /// クリップボードを監視する。Start関数を
    /// </summary>
    class ClipboardListener
    {

        [DllImport("user32.dll")]
        private static extern bool AddClipboardFormatListener(IntPtr hWnd);
        [DllImport("user32.dll")]
        private static extern bool RemoveClipboardFormatListener(IntPtr hWnd);

        // ウィンドウハンドルを控える
        private IntPtr? windowHandle = null;

        /// <summary>
        /// クリップボードへコピーしたら呼ばれるイベント。引数はどっちも空っぽ
        /// </summary>
        public event EventHandler OnCopyListener;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="handle">ウィンドウハンドル</param>
        public ClipboardListener(IntPtr handle)
        {
            windowHandle = handle;
            // WndProc
            HwndSource source = HwndSource.FromHwnd(handle);
            source.AddHook(new HwndSourceHook(WndProc));
            // 監視開始
            Start();
        }

        /// <summary>
        /// クリップボード監視開始メソッド
        /// </summary>
        private void Start()
        {
            AddClipboardFormatListener(windowHandle.Value);
        }

        /// <summary>
        /// クリップボード監視解除メソッド
        /// </summary>
        public void Release()
        {
            RemoveClipboardFormatListener(windowHandle.Value);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // コールバック呼ぶ
            if (msg == 0x031D)
            {
                OnCopyListener.Invoke(null, EventArgs.Empty);
                handled = true;
            }
            return IntPtr.Zero;
        }

    }

}
