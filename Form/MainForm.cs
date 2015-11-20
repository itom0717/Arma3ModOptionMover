using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arma3ModOptionMover
{
    /// <summary>
    /// MainForm
    /// </summary>
    public partial class MainForm : Form
    {

        /// <summary>
        /// ショートカット作成済みオプション
        /// </summary>
        private const string CreatedShortCutArguments  = "/s";

        /// <summary>
        /// サーバー設定情報リスト
        /// </summary>
        private ServerSettings ServerSettings = new ServerSettings();

        /// <summary>
        /// アプリケーションタイトル
        /// </summary>
        private string ApplicationTitle = "";


        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Form Loadイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_Load( object sender , EventArgs e )
        {
            //EventHandler

            // 現在実行しているアセンブリ(.exeのアセンブリ)を取得する
            var assm = System.Reflection.Assembly.GetExecutingAssembly();
            // AssemblyNameを取得する
            var assmName = assm.GetName();
            //グローバル属性の情報からTitle取得
            var asmprd = (System.Reflection.AssemblyTitleAttribute )
                        Attribute.GetCustomAttribute(
                                 System.Reflection.Assembly.GetExecutingAssembly(),
                                 typeof(System.Reflection.AssemblyTitleAttribute ));

            //アプリケーションタイトル
            this.ApplicationTitle = asmprd.Title;

            //フォームタイトル設定
            this.Text = String.Format( @"{0} - Ver.{1}" , asmprd.Title , assmName.Version );

            //各コントロールの初期化
            this.LogListBox.Items.Clear();
            this.ModTreeView.Nodes.Clear();


            //とりあえずショートカット作成をチェック
            this.CreateShortCutCheckBox.Checked = true;
            //コマンドライン取得
            string[] cmds = System.Environment.GetCommandLineArgs();
            foreach ( string cmd in cmds )
            {
                if ( cmd.Equals( CreatedShortCutArguments , StringComparison.CurrentCultureIgnoreCase ) )
                {
                    //ショートカットから呼ばれたため、チェックを外す
                    this.CreateShortCutCheckBox.Checked = false;
                    break;
                }
            }

            this.LoadingPictureBox.Visible = false;
            this.LoadingPictureBox.Top = this.ModTreeView.Top;
            this.LoadingPictureBox.Left = this.ModTreeView.Left;
            this.LoadingPictureBox.Height = this.ModTreeView.Height;
            this.LoadingPictureBox.Width = this.ModTreeView.Width;
        }



        /// <summary>
        /// Form_Shown
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <remarks>
        /// 初回表示時に実行
        /// </remarks>
        private void Form_Shown( object sender , EventArgs e )
        {


            try
            {
                //サーバ設定情報取得
                this.ServerSettings.GetServerSettings();

                if ( this.ServerSettings.Count == 0 )
                {
                    throw new Exception( Resource.TextResource.ErrMsgNoServerList );
                }




                //イベント抑制
                this.DisableServerListComboBoxEvents = true;

                //サーバーリストの内容をコンボボックスへセットする
                this.ServerListComboBox.Items.Clear();
                for ( int i = 0; i < this.ServerSettings.Count; i++ )
                {
                    this.ServerListComboBox.Items.Add( this.ServerSettings[i] );
                }
                this.ServerListComboBox.SelectedIndex = 0;
                this.ServerListComboBox.DisplayMember = "ServerName";

                //前回設定していた値を復帰（サーバー名で判定）
                for ( int i = 0; i < this.ServerSettings.Count; i++ )
                {
                    var serverSetting = (ServerSetting) this.ServerListComboBox.Items[i];
                    if ( Properties.Settings.Default.SelectServerName.Equals( serverSetting.ServerName ) )
                    {
                        this.ServerListComboBox.SelectedIndex = i;
                        break;
                    }

                }

                //イベント抑制解除
                this.DisableServerListComboBoxEvents = false;

                // Mod情報更新
                this.UpdateModTreeView();

            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message ,
                                 this.ApplicationTitle ,
                                 MessageBoxButtons.OK ,
                                 MessageBoxIcon.Error );
                this.Close();
            }
        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click( object sender , EventArgs e )
        {
            this.Close();
        }

        /// <summary>
        /// サーバー選択コンボ変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ServerListComboBox_SelectedIndexChanged( object sender , EventArgs e )
        {
            if ( this.DisableServerListComboBoxEvents )
            {
                return;
            }
            // Mod情報更新
            this.UpdateModTreeView();
        }
        /// <summary>
        /// ServerListComboBoxのイベント抑制フラグ
        /// </summary>
        private bool DisableServerListComboBoxEvents = false;

        /// <summary>
        /// Mod情報更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetModInfoButton_Click( object sender , EventArgs e )
        {
            // Mod情報更新
            this.UpdateModTreeView();
        }

        /// <summary>
        /// MOD情報更新
        /// </summary>
        private void UpdateModTreeView()
        {
            try
            {
                //選択されたサーバー情報
                var selectedServerSetting = (ServerSetting) this.ServerListComboBox.SelectedItem;

                //ボタン類を無効にする
                this.EnableButtons( false );

                //TreeView初期化
                this.ModTreeView.Nodes.Clear();


                //サーバー名ノード
                var serverNode = new TreeNode();
                serverNode.Text = selectedServerSetting.ServerName;
                serverNode.ImageIndex = 6;
                serverNode.SelectedImageIndex = serverNode.ImageIndex;
                this.ModTreeView.Nodes.Add( serverNode );


                //MODの情報をTreeViewに表示
                foreach ( ModSetting modSetting in selectedServerSetting )
                {
                    //各MODの情報取得
                    modSetting.GetModInfo();

                    //Modノード
                    var modNode = new TreeNode();
                    modNode.Text = modSetting.ModInfomation.ModName;
                    modNode.ImageIndex = 0;
                    modNode.SelectedImageIndex = modNode.ImageIndex;

                    //追加する情報
                    var addNode = new TreeNode();
                    addNode.ImageIndex = 1;
                    addNode.SelectedImageIndex = addNode.ImageIndex;
                    addNode.Text = Resource.TextResource.AddOptionText;

                    //nodeに追加
                    foreach ( string pbo in modSetting.ModInfomation.AddFile )
                    {
                        var node = new TreeNode(pbo);
                        addNode.Nodes.Add( node );

                        if ( modSetting.AddonsPathInfo.ExistsPbo( pbo ) )
                        {
                            //存在する
                            node.ImageIndex = 3;
                            node.SelectedImageIndex = node.ImageIndex;
                        }
                        else
                        {
                            //存在しない
                            node.ImageIndex = 4;
                            node.SelectedImageIndex = node.ImageIndex;
                        }
                    }
                    //1件以上あれば追加する
                    if ( addNode.Nodes.Count >= 1 )
                    {
                        modNode.Nodes.Add( addNode );
                    }



                    //削除する情報
                    var removeNode = new TreeNode();
                    removeNode.ImageIndex = 2;
                    removeNode.SelectedImageIndex = removeNode.ImageIndex;
                    removeNode.Text = Resource.TextResource.RemoveOptionText;

                    //nodeに追加
                    foreach ( string pbo in modSetting.ModInfomation.RemoveFile )
                    {
                        var node = new TreeNode(pbo);
                        removeNode.Nodes.Add( node );

                        if ( modSetting.RemovePathInfo.ExistsPbo( pbo ) )
                        {
                            //存在する
                            node.ImageIndex = 5;
                            node.SelectedImageIndex = node.ImageIndex;
                        }
                        else
                        {
                            //存在しない
                            node.ImageIndex = 3;
                            node.SelectedImageIndex = node.ImageIndex;
                        }
                    }

                    //1件以上あれば追加する
                    if ( removeNode.Nodes.Count >= 1 )
                    {
                        modNode.Nodes.Add( removeNode );
                    }


                    //1件以上あれば追加する
                    if ( modNode.Nodes.Count >= 1 )
                    {
                        serverNode.Nodes.Add( modNode );
                    }
                }
                //ノードを全展開
                serverNode.ExpandAll();

                //サーバー名を選択状態にする
                this.ModTreeView.SelectedNode = serverNode;

            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message ,
                                this.ApplicationTitle ,
                                MessageBoxButtons.OK ,
                                MessageBoxIcon.Error );
                this.Close();
            }
            finally
            {
                //ボタン類を有効にする
                this.EnableButtons( true );
            }
        }


        /// <summary>
        /// ボタン類のDisable/Enable切り替え
        /// </summary>
        private void EnableButtons( bool isEnable )
        {
            //処理中表示
            this.LoadingPictureBox.Visible = !isEnable;

            //各ボタンの操作を無効にする
            this.ServerListComboBox.Enabled = isEnable;
            this.OKButton.Enabled = isEnable;
            this.GetModInfoButton.Enabled = isEnable;
            this.CreateShortCutCheckBox.Enabled = isEnable;
            this.CloseButton.Enabled = isEnable;

            if ( this.ModTreeView.Nodes.Count <= 0 )
            {
                //0件の場合は処理実行ボタンを無効にする
                this.OKButton.Enabled = false;
            }
        }



        /// <summary>
        /// 処理実行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click( object sender , EventArgs e )
        {

            // Mod情報更新
            this.UpdateModTreeView();

            //実行確認
            if ( MessageBox.Show( Resource.TextResource.ConfirmationMessageGo ,
                                  this.ApplicationTitle ,
                                  MessageBoxButtons.OKCancel ,
                                  MessageBoxIcon.Question ) != DialogResult.OK )
            {
                return;
            }

            //ボタン類を無効にする
            this.EnableButtons( false );

            //ログエリアクリア
            this.LogListBox.Items.Clear();

            //選択されたサーバー情報
            var selectedServerSetting = (ServerSetting) this.ServerListComboBox.SelectedItem;

            //今回選択したサーバー名を記憶
            Properties.Settings.Default.SelectServerName = selectedServerSetting.ServerName;
            Properties.Settings.Default.Save();

            //バックグラウンド処理を開始する
            this.SetBackgroundWorker.WorkerReportsProgress = true;
            this.SetBackgroundWorker.RunWorkerAsync( selectedServerSetting );
        }

        /// <summary>
        /// LogSeparator
        /// </summary>
        private const string LogSeparator = "--------------------------------------------";

        /// <summary>
        /// 追加ログのテキスト
        /// </summary>
        private const string LogTextAdd  = "ADD";

        /// <summary>
        /// 削除ログのテキスト
        /// </summary>
        private const string LogTextRemove  = "REMOVE";


        /// <summary>
        /// SetBackgroundWorker_DoWork
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBackgroundWorker_DoWork( object sender , DoWorkEventArgs e )
        {
            try
            {
                //BackgroundWorker
                BackgroundWorker bw = (BackgroundWorker)sender;

                //選択サーバー情報
                ServerSetting selectedServerSetting = (ServerSetting)e.Argument;

                //ProgressChangedイベントハンドラを呼び出し、
                //コントロールの表示を変更する
                bw.ReportProgress( 0 , LogSeparator );
                bw.ReportProgress( 0 , Resource.TextResource.LogTextStart );
                bw.ReportProgress( 0 , LogSeparator );
                bw.ReportProgress( 0 , "" );


                /// 前回処理した設定を元に戻す
                /// </summary>
                this.RestorePreviousSetting( bw );

                //今回処理したMod名記憶
                var modNameList   = new List<string>();
                var modoptionList = new List<string>();

                //Mod数分ループ
                foreach ( ModSetting modSetting in selectedServerSetting )
                {
                    //今回処理したmodを記憶
                    modNameList.Add( modSetting.ModInfomation.ModName );
                    modoptionList.Add( modSetting.ModInfomation.OptionalsPath  );

                    bw.ReportProgress( 0 , LogSeparator );
                    bw.ReportProgress( 0 , "[" + modSetting.ModInfomation.ModName + "]" );
                    bw.ReportProgress( 0 , "" );


                    //ログ格納フォルダを作成しておく
                    if ( !Common.File.ExistsDirectory( modSetting.ModLogPath ) )
                    {
                        //ない場合は作成しておく
                        Common.File.CreateDirectory( modSetting.ModLogPath );
                    }

                    //ログファイル書き込みオープン
                    using ( var sw = new System.IO.StreamWriter( modSetting.ModLogFilename, false,  System.Text.Encoding.UTF8 ) )
                    {
                        sw.AutoFlush = true;
                        sw.WriteLine( modSetting.ModInfomation.ModName  );//Mod名
                        sw.WriteLine( modSetting.AddonsPathInfo.FullPath  );//Addons Path
                        sw.WriteLine( modSetting.OptionalPathInfo.FullPath );//Optional Path
                        sw.WriteLine( modSetting.RemovePathInfo.FullPath  );//Remove　Path
                        sw.WriteLine( "" );

                        //追加ファイルの処理
                        sw.WriteLine( LogTextAdd );
                        foreach ( string tgtFile in modSetting.ModInfomation.AddFile )
                        {
                            //optionフォルダからAddonsフォルダへコピー
                            foreach ( string file in Common.File.GetFileList( modSetting.OptionalPathInfo.FullPath, tgtFile + "*", false ) )
                            {
                                bw.ReportProgress( 0, Resource.TextResource.LogTextAddFile + Common.File.GetFileName( file ) );

                                //Addonsフィルダへコピー処理
                                Common.File.CopyFile( file,
                                                      Common.File.CombinePath( modSetting.AddonsPathInfo.FullPath,
                                                                               Common.File.GetFileName( file ) ),
                                                      true );
                                sw.WriteLine( "" + Common.File.GetFileName( file ) );

                            }
                        }
                        bw.ReportProgress( 0, "" );
                        sw.WriteLine( "" );

                        //削除ファイルの処理
                        sw.WriteLine( LogTextRemove );
                        foreach ( string tgtFile in modSetting.ModInfomation.RemoveFile )
                        {
                            //Addonsフィルダから無効フォルダへ移動
                            foreach ( string file in Common.File.GetFileList( modSetting.AddonsPathInfo.FullPath, tgtFile + "*", false ) )
                            {
                                bw.ReportProgress( 0, Resource.TextResource.LogTextRemoveFile + Common.File.GetFileName( file ) );

                                if ( !Common.File.ExistsDirectory( modSetting.RemovePathInfo.FullPath ) )
                                {
                                    //ない場合は作成しておく
                                    Common.File.CreateDirectory( modSetting.RemovePathInfo.FullPath );
                                }


                                //Removeフィルダへコピー処理
                                Common.File.CopyFile( file,
                                                      Common.File.CombinePath( modSetting.RemovePathInfo.FullPath,
                                                                               Common.File.GetFileName( file ) ),
                                                      true );
                                //コピーできたら削除
                                Common.File.DeleteFile( file );

                                sw.WriteLine( "" + Common.File.GetFileName( file ) );
                            }
                        }
                        bw.ReportProgress( 0, "" );
                        bw.ReportProgress( 0, "" );

                     sw.Close();
                    }

                }


                //前回実施した移動等を元に戻す為にMod情報記憶(タブ区切り)
                Properties.Settings.Default.PreviousModName           = String.Join( "\t" , modNameList );
                Properties.Settings.Default.PreviousModOptionPathName = String.Join( "\t" , modoptionList );
                Properties.Settings.Default.Save();


                //ショートカット作成
                if ( this.CreateShortCutCheckBox.Checked )
                {
                    //ショートカット作成
                    bw.ReportProgress( 0 , Resource.TextResource.LogTextCreateShortCut );

                    //作成するショートカットのパス
                    string shortcutPath = Common.File.GetDesktopDirectory() +  this.ApplicationTitle + ".lnk";

                    //ショートカットのリンク先
                    string targetPath = Application.ExecutablePath;

                    //作成
                    Common.File.CreateShortcut( shortcutPath: shortcutPath ,
                                                targetLinkPath: targetPath ,
                                                arguments: CreatedShortCutArguments );

                    bw.ReportProgress( 0 , "" );
                }

                //処理が終了しました。
                bw.ReportProgress( 0 , Resource.TextResource.LogTextFinish );
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// SetBackgroundWorker_ProgressChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBackgroundWorker_ProgressChanged( object sender , ProgressChangedEventArgs e )
        {
            this.LogListBox.Items.Add( ( string ) e.UserState );
            this.LogListBox.SelectedIndex = this.LogListBox.Items.Count - 1;
        }

        /// <summary>
        /// SetBackgroundWorker_RunWorkerCompleted
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetBackgroundWorker_RunWorkerCompleted( object sender , RunWorkerCompletedEventArgs e )
        {
            if ( e.Error != null )
            {
                MessageBox.Show( e.Error.Message ,
                                 this.Text ,
                                 MessageBoxButtons.OK ,
                                 MessageBoxIcon.Error );
            }
            else if ( e.Cancelled )
            {
                //キャンセルされた（このプログラムでは存在しない）
            }
            else
            {
                //正常終了
                //MOD情報更新
                this.UpdateModTreeView();
            }

            //ボタン類を有効にする
            this.EnableButtons( true );
        }

        /// <summary>
        /// 前回処理した設定を元に戻す
        /// </summary>
        private void RestorePreviousSetting( BackgroundWorker bw )
        {

            //前回実施したMod名
            string previousModName           = Properties.Settings.Default.PreviousModName;
            //Option名
            string previousModOptionPathName = Properties.Settings.Default.PreviousModOptionPathName;

            if( previousModName .Equals("") || previousModOptionPathName .Equals(""))
            {
                return;
            }

            string[] modNames          = previousModName.Split('\t');
            string[] modOptionPathName = previousModOptionPathName.Split('\t');

            if ( modNames.Length == 0 || modNames.Length != modOptionPathName.Length )
            {
                //データ無いまたは数が合わないため、何もせず戻る
                return;
            }
            bw.ReportProgress( 0 , "" );


            for ( int i = 0; i < modNames.Length; i++ )
            {
                bw.ReportProgress( 0, Resource.TextResource.LogTextRestore + "[" + modNames[i] + "]" );

                var modSetting = new ModSetting();
                modSetting.ModInfomation.ModName = modNames[i];
                modSetting.ModInfomation.OptionalsPath = modOptionPathName[i];

                //データ取得
                modSetting.GetModInfo();
                if ( !Common.File.ExistsDirectory( modSetting.ModLogPath ) )
                {
                    //ログフォルダが存在しない場合は何もしない
                    continue;
                }
                if ( !Common.File.ExistsFile ( modSetting.ModLogFilename ) )
                {
                    //ログファイルが存在しない場合は何もしない
                    continue;
                }

                //ファイルを調査
                using ( var sr = new System.IO.StreamReader( modSetting.ModLogFilename, System.Text.Encoding.UTF8 ) )
                {
                    //内容を一行ずつ読み込む
                    bool isAdd    = false;
                    bool isRemove = false;
                    while ( sr.Peek() > -1 )
                    {
                        string line = sr.ReadLine();

                        //先頭と最後の空白を取り除く
                        line = line.Trim();

                        if ( line .Equals( LogTextAdd ) )
                        {
                            isAdd    = true;
                            isRemove = false;
                            continue;
                        }
                        if ( line.Equals( LogTextRemove ) )
                        {
                            isAdd    = false;
                            isRemove = true;
                            continue;
                        }

                        //追加ファイルの移動
                        if ( isAdd )
                        {
                        }

                        //削除ファイルの復帰
                        if ( isRemove )
                        {
                        }



                    }

                    sr.Close();
                }





                ////Optionに存在し、Addonsにある場合はAddonsから消す
                //foreach ( string addonPbo in modSetting.AddonsPathInfo.PboFiles )
                //{
                //    string baseFile =  Common.File.GetFileName(addonPbo );
                //    if ( modSetting.OptionalPathInfo.ExistsPbo( baseFile ) )
                //    {
                //        //optionフォルダに存在するので削除
                //        foreach ( string file in Common.File.GetFileList( modSetting.AddonsPathInfo.FullPath, baseFile + "*", false ) )
                //        {
                //            Common.File.DeleteFile( file );
                //        }

                //    }


                //}
                ////_RemoveFile_に存在し、Addonsにない場合は、Addonsに移動し、_RemoveFile_から消す
                //foreach ( string removePbo in modSetting.RemovePathInfo.PboFiles )
                //{
                //    string baseFile =  Common.File.GetFileName(removePbo );
                //    if ( !modSetting.AddonsPathInfo.ExistsPbo( baseFile ) )
                //    {
                //        foreach ( string file in Common.File.GetFileList( modSetting.RemovePathInfo.FullPath, baseFile + "*", false ) )
                //        {
                //            //Addonsフィルダへコピー処理
                //            Common.File.CopyFile( file,
                //                                  Common.File.CombinePath( modSetting.AddonsPathInfo.FullPath,
                //                                                           Common.File.GetFileName( file ) ),
                //                                 true );
                //            //コピーできたら削除
                //            Common.File.DeleteFile( file );
                //        }
                //    }
                //}

            }


        }

    }
}
