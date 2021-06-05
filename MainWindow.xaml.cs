using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ErogeImgSave
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                // ウィンドウハンドル
                var handle = new WindowInteropHelper(this).Handle;
                // クリップボード監視
                var clipboardListener = new ClipboardListener(handle);
                // コールバック登録
                clipboardListener.OnCopyListener += OnCopyListener;
                // ウィンドウ隠す
                Hide();
            };
        }

        /// <summary>
        /// クリップボードコールバック
        /// </summary>
        public void OnCopyListener(object sender, EventArgs args)
        {
            ClipboardImgSaveTool.SaveClipboardImage();
        }

        /// <summary>
        /// 保存先フォルダボタン押したとき
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // .NET Core はなんかひと手間いるらしい
            var startInfo = new System.Diagnostics.ProcessStartInfo(ClipboardImgSaveTool.getFolderPath());
            startInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(startInfo);
        }

        /// <summary>
        /// アプリを終了させる
        /// </summary>
        private void MenuItemClickExit(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        /// <summary>
        /// 保存先フォルダを表示させる
        /// </summary>
        private void MenuItemShowSaveFolder(object sender, RoutedEventArgs e)
        {
            // .NET Core はなんかひと手間いるらしい
            var startInfo = new System.Diagnostics.ProcessStartInfo(ClipboardImgSaveTool.getFolderPath());
            startInfo.UseShellExecute = true;
            System.Diagnostics.Process.Start(startInfo);
        }

    }
}
