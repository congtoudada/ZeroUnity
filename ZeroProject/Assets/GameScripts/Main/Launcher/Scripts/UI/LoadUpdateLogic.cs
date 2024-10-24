using System;

namespace GameMain
{
    public class LoadUpdateLogic
    {
        private static LoadUpdateLogic _instance;

        public Action<int> DownloadCompleteAction = null;  //资源下载完成回调
        public Action<float> DownProgressAction = null;  //资源下载进度更新回调
        public Action<bool> UnpackedCompleteAction = null;  //解压缩完成回调
        public Action<float> UnpackedProgressAction = null; //解压缩进度更新回调

        public static LoadUpdateLogic Instance => _instance ??= new LoadUpdateLogic();
    }
}