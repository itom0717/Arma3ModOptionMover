using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arma3ModOptionMover
{
    /// <summary>
    /// サーバー設定情報
    /// </summary>
    class ServerSetting : List<ModSetting>
    {

        /// <summary>
        /// ダウンロードする場合ファイル名
        /// </summary>
        private const string DownloadTempFilename  ="_DownloadServerModSetting.cfg";


        /// <summary>
        /// サーバー名
        /// </summary>
        public string ServerName
        {
            get {
                if ( this._ServerName.Equals(""))
                {
                    return Resource.TextResource.ServerNoName;
                }
                else
                {
                    return this._ServerName;
                }
            }
            private set {
                this._ServerName = value;
            }
        }
        private string _ServerName = "";


        /// <summary>
        /// サーバー設定情報取得
        /// </summary>
        public void GetServerSetting( string filename )
        {
            try
            {
                this.Clear();

                //Mod情報
                ModSetting modSetting = null;

                //ファイルを調査
                using ( var sr = new System.IO.StreamReader( filename, System.Text.Encoding.UTF8 ) )
                {
                    //内容を一行ずつ読み込む
                    while ( sr.Peek() > -1 )
                    {
                        string line = sr.ReadLine();

                        //;以降を取り除く
                        {
                            var r = new System.Text.RegularExpressions.Regex( @";.*" );
                            line = r.Replace( line, "" );
                        }

                        //先頭と最後の空白を取り除く
                        line = line.Trim();

                        //空欄の場合は次の行へ
                        if ( line.Equals("") )
                        {
                            continue;
                        }


                        //URL(ダウンロード先指定)
                        //これがあれば以下のデータがあっても読み取らず、ファイルをダウンロードしてそちらを使用する
                        {
                            var r = new System.Text.RegularExpressions.Regex( @"^URL\s*=\s*(.+)" );
                            System.Text.RegularExpressions.Match m  = r.Match( line );
                            if ( m.Success )
                            {
                                try
                                {
                                    //ダウンロード

                                    //ファイル名
                                    string downloadTempFilename
                                        = Common.File.CombinePath(Common.File.GetDirectoryName(filename),
                                                                  DownloadTempFilename);

                                    Common.File.DeleteFile( downloadTempFilename );//削除
                                    using ( var wc = new System.Net.WebClient() )
                                    {
                                        wc.DownloadFile( m.Groups[1].ToString() , downloadTempFilename );//ダウンロード
                                        wc.Dispose();
                                    }

                                    //DLできたので、このファイル名でデータ読込
                                    this.GetServerSetting( downloadTempFilename );

                                    //ここで戻る
                                    return;
                                }
                                catch
                                {
                                    //nop
                                    throw;
                                }
                            }
                        }


                        //解析
                        //ServerInfo
                        {
                            var r = new System.Text.RegularExpressions.Regex( @"^ServerInfo\s*=\s*(.+)",
                                                              System.Text.RegularExpressions.RegexOptions.IgnoreCase );
                            System.Text.RegularExpressions.Match m  = r.Match( line );
                            if ( m.Success )
                            {
                                this.ServerName = m.Groups[1].ToString();
                            }
                        }

                        //MOD名
                        {
                            var r = new System.Text.RegularExpressions.Regex( @"^\[(.+)\]$" );
                            System.Text.RegularExpressions.Match m  = r.Match( line );
                            if ( m.Success )
                            {
                                if ( modSetting != null )
                                {
                                    //リストに追加
                                    this.Add( modSetting );
                                }
                                modSetting = new ModSetting();
                                modSetting.ModInfomation.ModName = m.Groups[1].ToString();
                            }
                        }

                        //MODが出てきてなければ次の行へ
                        if ( modSetting == null )
                        {
                            continue;
                        }

                        //optional を解析
                        {
                            var r = new System.Text.RegularExpressions.Regex( @"^optionalsPath\s*=\s*(.+)",
                                                                              System.Text.RegularExpressions.RegexOptions.IgnoreCase );
                            System.Text.RegularExpressions.Match m = r.Match( line );
                            if ( m.Success )
                            {
                                modSetting.ModInfomation.OptionalsPath = m.Groups[1].ToString();
                            }
                        }

                        //追加するファイルを解析
                        {
                            var r = new System.Text.RegularExpressions.Regex( @"^add\s*(.+)",
                                                                              System.Text.RegularExpressions.RegexOptions.IgnoreCase );
                            System.Text.RegularExpressions.Match m = r.Match( line );
                            if ( m.Success )
                            {
                                modSetting.ModInfomation.AddFile.Add( m.Groups[1].ToString() );
                            }
                        }

                        //削除するファイルを解析
                        {
                            var r = new System.Text.RegularExpressions.Regex( @"^del\s*(.+)",
                                                                              System.Text.RegularExpressions.RegexOptions.IgnoreCase );
                            System.Text.RegularExpressions.Match m = r.Match( line );
                            if ( m.Success )
                            {
                                modSetting.ModInfomation.RemoveFile.Add( m.Groups[1].ToString() );
                            }
                        }
                    }

                    //閉じる
                    sr.Close();

                    //追加していなければ追加する
                    if ( modSetting != null )
                    {
                        //リストに追加
                        this.Add( modSetting );
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
