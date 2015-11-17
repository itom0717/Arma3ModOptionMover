using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arma3ModOptionMover
{
    /// <summary>
    /// サーバー設定情報一覧
    /// </summary>
    class ServerSettings : List<ServerSetting>
    {

        /// <summary>
        /// サーバー設定が格納されているフォルダのワイルドカード
        /// </summary>
        private static string TargetFolderNameWildcards = @"@Arma3ModOptionMover*";


        /// <summary>
        /// サーバー設定ファイル名
        /// </summary>
        private static string ServerSettingFileName = @"ServerModSetting.cfg";


        /// <summary>
        /// サーバー一覧取得
        /// </summary>
        public void GetServerSettings()
        {

            //サーバー一覧情報を取得する
            // 一つ上の階層の @Arma3ModOptionMover_XXXXX 内の *.cfg/*.settingを調べて、設定データの場合サーバーリストに追加する。
            string tgtPath = Common.File.CombinePath(Common.File.GetApplicationDirectory(), "..");

            //フォルダを列挙
            List<String> folderList = Common.File.GetFolderList(tgtPath, TargetFolderNameWildcards, false);
            foreach ( string folder in folderList )
            {
                string tgtFilename = Common.File.CombinePath(folder, ServerSettingFileName);
                if ( Common.File.ExistsFile( tgtFilename ) )
                {
                    //サーバー情報取得
                    ServerSetting serverSetting = new ServerSetting();
                    serverSetting.GetServerSetting( tgtFilename );

                    this.Add( serverSetting );
                }
            }


        }
    }
}
