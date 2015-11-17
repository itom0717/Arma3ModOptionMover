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
        /// サーバー名
        /// </summary>
        public string ServerName { get; private set; } = "";

        /// <summary>
        /// サーバー設定情報取得
        /// </summary>
        public void GetServerSetting( string filename )
        {
            try
            {
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

                    //追加していなければついかする
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
