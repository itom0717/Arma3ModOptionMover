using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arma3ModOptionMover
{

    /// <summary>
    /// Mod設定情報
    /// </summary>
    public class ModSetting
    {
        /// <summary>
        /// Remove時の格納パス
        /// </summary>
        private const string RemovePath = "_RemoveFile_";

        /// <summary>
        /// Mod情報
        /// </summary>
        public ModInfomation ModInfomation { get; } = new ModInfomation();

        /// <summary>
        /// MODのフルパス
        /// </summary>
        /// <returns></returns>
        public string ModFullPath { get; set; } = "";

        /// <summary>
        /// addons パス情報
        /// </summary>
        /// <returns></returns>
        public ModPath AddonsPathInfo { get; } = new ModPath();

        /// <summary>
        /// optionals パス情報
        /// </summary>
        /// <returns></returns>
        public ModPath OptionalPathInfo { get; } = new ModPath();

        /// <summary>
        ///  Disable パス情報
        /// </summary>
        /// <returns></returns>
        public ModPath RemovePathInfo { get; } = new ModPath();

        /// <summary>
        /// MOD情報取得
        /// </summary>
        public void GetModInfo()
        {
            try
            {

                // MODのフルパス
                this.ModFullPath = Common.File.CombinePath( Common.File.GetApplicationDirectory() + "..",
                                                            this.ModInfomation.ModName );
                if ( !Common.File.ExistsDirectory( this.ModFullPath ) )
                {
                    //パスが見つからない
                    throw new Exception( String.Format( Resource.TextResource.ErrMsgNotFoundModPath,
                                                        this.ModInfomation.ModName ) );
                }

                // addonsのフルパス
                this.AddonsPathInfo.FullPath = Common.File.CombinePath( this.ModFullPath, "addons" );
                if ( !Common.File.ExistsDirectory( this.AddonsPathInfo.FullPath ) )
                {
                    //パスが見つからない
                    throw new Exception( String.Format( Resource.TextResource.ErrMsgNotFoundModAddonsPath,
                                                        this.ModInfomation.ModName ) );
                }

                // addonsのフルパス
                this.OptionalPathInfo.FullPath = Common.File.CombinePath( this.ModFullPath, this.ModInfomation.OptionalsPath );
                if ( !Common.File.ExistsDirectory( this.OptionalPathInfo.FullPath ) )
                {
                    //パスが見つからない
                    throw new Exception( String.Format( Resource.TextResource.ErrMsgNotFoundModOptionalsPath,
                                                        this.ModInfomation.ModName,
                                                        this.ModInfomation.OptionalsPath ) );
                }



                //Removeパス
                this.RemovePathInfo.FullPath = Common.File.CombinePath( this.ModFullPath, RemovePath );
                //if ( !Common.File.ExistsDirectory( this.RemovePathInfo.FullPath ) )
                //{
                //    //ない場合は作成しておく
                //    Common.File.CreateDirectory( this.RemovePathInfo.FullPath );
                //}


                //各フォルダ内のファイルを調査しておく
                this.AddonsPathInfo.SearchPboFile();
                this.OptionalPathInfo.SearchPboFile();
                this.RemovePathInfo.SearchPboFile();
            }
            catch
            {
                throw;
            }

        }

    }
}
