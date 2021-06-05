using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ErogeImgSave
{
    /// <summary>
    /// 写真保存関連
    /// </summary>
    class ClipboardImgSaveTool
    {

        /// <summary>
        /// クリップボードの画像を保存する
        /// 
        /// 保存先は各ユーザーの ピクチャ/ErogeImgSave
        /// </summary>
        public static void SaveClipboardImage()
        {
            // ピクチャふぉるだ
            var pictureFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            // ErogeImgSaveフォルダ作成
            var imgFolder = $"{pictureFolder}/ErogeImgSave";
            if (!Directory.Exists(imgFolder))
            {
                Directory.CreateDirectory(imgFolder);
            }
            // 画像ファイル
            var pngFile = $"{imgFolder}/{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.png";
            // クリップボードから取得
            var bitmapSource = Clipboard.GetImage();
            // nullの場合は画像ではない
            if (bitmapSource != null)
            {
                // ファイル作成とStream取得
                using (var fileStream = File.Create(pngFile))
                {
                    var encoder = new PngBitmapEncoder();
                    encoder.Interlace = PngInterlaceOption.On;
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
                    encoder.Save(fileStream);
                }
            }
        }

        /// <summary>
        /// 保存先を返す
        /// </summary>
        /// <returns>写真保存先</returns>
        public static string getFolderPath()
        {
            // ピクチャふぉるだ
            var pictureFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            // ErogeImgSaveフォルダ作成
            var imgFolder = $"{pictureFolder}/ErogeImgSave";
            return imgFolder;
        }

    }
}
