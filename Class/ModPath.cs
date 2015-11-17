using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arma3ModOptionMover
{
    /// <summary>
    /// Modパス情報
    /// </summary>
    public class ModPath
    {
        /// <summary>
        /// 絶対パス
        /// </summary>
        /// <returns></returns>
        public string FullPath { get; set; } = "";

        /// <summary>
        /// パス内の*.pbo
        /// </summary>
        /// <returns></returns>
        public List<string> PboFiles { get; private set; } = null;

        /// <summary>
        /// フォルダのPBOファイルを調査
        /// </summary>
        public void SearchPboFile()
        {
            //ファイル一覧取得
            this.PboFiles = Common.File.GetFileList( this.FullPath, "*.pbo", false );
        }

        /// <summary>
        /// PBOファイルがフォルダ内に存在するか調べる
        /// </summary>
        /// <param name="pboFile"></param>
        /// <returns></returns>
        public bool ExistsPbo( string pboFile )
        {
            bool isExist = false;


            foreach ( string pbo in this.PboFiles )
            {
                string baseFile= Common.File.GetFileName(pbo);

                if ( baseFile.Equals( pboFile, StringComparison.CurrentCultureIgnoreCase ) )
                {
                    //存在する
                    isExist = true;
                    break;
                }
            }
            return isExist;
        }
    }
}
