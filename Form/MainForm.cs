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
        private  static string CreatedShortCutArguments  = "/s";

        /// <summary>
        /// サーバー設定情報リスト
        /// </summary>
        private ServerSettings ServerSettings = new ServerSettings();


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

            //フォームタイトル設定
            this.Text = String.Format( @"{0} - Ver.{1}" , asmprd.Title  , assmName.Version );

            //各コントロールの初期化
            this.LogListBox.Items.Clear();
            this.ModTreeView.Nodes.Clear();


            //とりあえずショートカット作成をチェック
            this.CreateShortCutCheckBox.Checked = true;
            //コマンドライン取得
            string[] cmds = System.Environment.GetCommandLineArgs();
            foreach( string cmd in cmds )
            {
                if( cmd .Equals( CreatedShortCutArguments , StringComparison.CurrentCultureIgnoreCase ) )
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
                ServerSettings.GetServerSettings();


                //    'サーバー設定データをコンボに表示
                //    Dim serverList As New ServerList
                //    serverList.GetServerList( Common.File.CombinePath(Common.File.GetApplicationDirectory,
                //                                                     ServerListConfigFilename))
                //    If serverList.Count = 0 Then
                //      MessageBox.Show(My.Resources.TextResource.ErrMsgNoServerList,
                //                        My.Application.Info.AssemblyName,
                //                        MessageBoxButtons.OK,
                //                        MessageBoxIcon.Information)
                //      Me.Close()
                //    End If

                //    'サーバーリストの内容をコンボボックスへセットする
                //    Me.ServerListComboBox.Items.Clear()
                //    For i As Integer = 0 To serverList.Count - 1
                //      Me.ServerListComboBox.Items.Add(serverList(i))
                //    Next
                //    Me.ServerListComboBox.SelectedIndex = 0
                //    Me.ServerListComboBox.DisplayMember = "ServerDisplayName"

                //    '初期値設定
                //    For i As Integer = 0 To serverList.Count - 1
                //      Dim serverInfo As ServerList.ServerInfo = Me.ServerListComboBox.Items( i )
                //      If Not My.Settings.SelectServer.Equals("") AndAlso serverInfo.SettingConfigUrl.Equals( My.Settings.SelectServer) Then
                //         Me.ServerListComboBox.SelectedIndex = i
                //         Exit For
                //       End If
                //       If Not My.Settings.SelectServer.Equals("") AndAlso serverInfo.SettingConfigFilename.Equals( My.Settings.SelectServer) Then
                //          Me.ServerListComboBox.SelectedIndex = i
                //          Exit For
                //        End If
                //      Next
                //    '





            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                this.Text,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
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

    }
}











#if false


Imports System.ComponentModel
''' <summary>
''' メインフォーム
''' </summary>
Public Class MainForm


  ''' <summary>
  ''' LogSeparator
  ''' </summary>
  Private Const LogSeparator As String = "--------------------------------------------"

  ''' <summary>
  ''' 設定情報
  ''' </summary>
  Private SettingData As New SettingData


  ''' <summary>
  ''' MOd情報
  ''' </summary>
  Private ModsInfoList As List(Of ModInfo)






  ''' <summary>
  ''' サーバーリストURL設定ファイルのファイル名
  ''' </summary>
  Private Const ServerListUrlConfigFilename As String = "ServerListUrl.cfg"

  ''' <summary>
  ''' サーバーリスト設定ファイルのファイル名(DLするときに設定するファイル名)
  ''' </summary>
  Private Const ServerListConfigFilename As String = "ServerList.cfg"

  ''' <summary>
  ''' 設定ファイルのファイル名(DLするときに設定するファイル名)
  ''' </summary>
  Private Const ServerSettingConfigFilename As String = "ServerSetting.cfg"




  ''' <summary>
  ''' Cponfigファイルのダウンロード＆読み込み
  ''' </summary>
  Private Sub DownloadConfigFile()

    Dim settingConfigFileName As String = Common.File.CombinePath(Common.File.GetApplicationDirectory,
                                                                   ServerSettingConfigFilename)

    Try
      Dim serverInfo As ServerList.ServerInfo
      serverInfo = Me.ServerListComboBox.SelectedItem



      If Not serverInfo.SettingConfigUrl.Equals("") Then
        'URLからDL

        Me.AddLogText(My.Resources.TextResource.SettingFileDownloading)

        If Common.File.ExistsFile(settingConfigFileName) Then
          Common.File.DeleteFile(settingConfigFileName)
        End If

        'ファイルダウンロード
        Dim wc As New System.Net.WebClient()
        wc.DownloadFile(serverInfo.SettingConfigUrl, Common.File.CombinePath(Common.File.GetApplicationDirectory,
                                                                ServerSettingConfigFilename))
        wc.Dispose()

        If Not Common.File.ExistsFile(settingConfigFileName) Then
          Throw New Exception(My.Resources.TextResource.ErrMsgSettingFilesNotDownload)
        End If

        Me.AddLogText(My.Resources.TextResource.SettingFileDownloadComplate)
        Me.AddLogText("")

        '選択URLを記憶
        My.Settings.SelectServer = serverInfo.SettingConfigUrl
        My.Settings.Save()
      Else

        settingConfigFileName = Common.File.CombinePath(Common.File.GetApplicationDirectory,
                                                           serverInfo.SettingConfigFilename)


        If Not Common.File.ExistsFile(settingConfigFileName) Then
          Throw New Exception(My.Resources.TextResource.ErrMsgSettingFilesNotFound)
        End If

        '選択fファイルを記憶
        My.Settings.SelectServer = Common.File.GetFileName(settingConfigFileName)
        My.Settings.Save()

      End If

    Catch ex As Exception
      MessageBox.Show(ex.Message,
              My.Application.Info.AssemblyName,
              MessageBoxButtons.OK,
              MessageBoxIcon.Error)
      Me.Close()
    End Try

    '設定データ取得
    Me.ModsInfoList = Me.SettingData.GetSetting(settingConfigFileName)

  End Sub





  ''' <summary>
  ''' MOD情報更新
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub GetModInfoButton_Click(sender As Object, e As EventArgs) Handles GetModInfoButton.Click

    ' Cponfigファイルのダウンロード＆読み込み
    Me.DownloadConfigFile()

    'MOD情報更新
    Me.UpdateModTreeView()
  End Sub



  ''' <summary>
  ''' OK Button
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub OKButton_Click(sender As Object, e As EventArgs) Handles OKButton.Click

    ' Cponfigファイルのダウンロード＆読み込み
    Me.DownloadConfigFile()

    'MOD情報更新
    Me.UpdateModTreeView()

    '実行確認
    If MessageBox.Show(My.Resources.TextResource.ConfirmationMessageGo,
                           My.Application.Info.AssemblyName,
                           MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) <> DialogResult.OK Then
      Return
    End If

    'ボタン類を無効にする
    Me.EnableButtons(False)

    'ログエリアクリア
    Me.LogListBox.Items.Clear()




    'バックグラウンド処理を開始する
    Me.SetBackgroundWorker.RunWorkerAsync()

  End Sub

  ''' <summary>
  ''' Reset
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ResetButton_Click(sender As Object, e As EventArgs) Handles ResetButton.Click

    '実行確認
    If MessageBox.Show(My.Resources.TextResource.ConfirmationMessageReset,
                           My.Application.Info.AssemblyName,
                           MessageBoxButtons.OKCancel,
                           MessageBoxIcon.Question) <> DialogResult.OK Then
      Return
    End If

    'ボタン類を無効にする
    Me.EnableButtons(False)

    'ログエリアクリア
    Me.LogListBox.Items.Clear()


    'バックグラウンド処理を開始する
    Me.ResetBackgroundWorker.RunWorkerAsync()
  End Sub



  ''' <summary>
  ''' MOD情報更新
  ''' </summary>
  Private Sub UpdateModTreeView()

    Try
      'ボタン類を無効にする
      Me.EnableButtons(False)

      Me.ModTreeView.Nodes.Clear()


      Dim serverNode As New TreeNode
      serverNode.Text = Me.SettingData.ServerInfo
      serverNode.ImageIndex = 6
      serverNode.SelectedImageIndex = serverNode.ImageIndex
      Me.ModTreeView.Nodes.Add(serverNode)


      'MODの情報をTreeViewに表示
      For Each modInfo As ModInfo In Me.ModsInfoList

        '各MODの情報取得
        modInfo.GetModInfo()

        Dim modNode As New TreeNode
        modNode.Text = modInfo.ModSetting.ModName
        modNode.ImageIndex = 0
        modNode.SelectedImageIndex = modNode.ImageIndex

        '追加情報
        Dim addNode As New TreeNode
        addNode.ImageIndex = 1
        addNode.SelectedImageIndex = addNode.ImageIndex
        addNode.Text = My.Resources.TextResource.AddText

        'nodeに追加
        For Each pbo As String In modInfo.ModSetting.AddFile
          Dim node As New TreeNode(pbo)
          addNode.Nodes.Add(node)

          If modInfo.AddonsPathInfo.ExistsPbo(pbo) Then
            '存在する
            node.ImageIndex = 3
            node.SelectedImageIndex = node.ImageIndex
          Else
            '存在しない
            node.ImageIndex = 4
            node.SelectedImageIndex = node.ImageIndex
          End If
        Next

        '1件以上あれば追加する
        If addNode.Nodes.Count >= 1 Then
          modNode.Nodes.Add(addNode)
        End If


        '削除情報
        Dim disableNode As New TreeNode
        disableNode.ImageIndex = 2
        disableNode.SelectedImageIndex = disableNode.ImageIndex
        disableNode.Text = My.Resources.TextResource.DelText

        'nodeに追加
        For Each pbo As String In modInfo.ModSetting.DelFile
          Dim node As New TreeNode(pbo)
          disableNode.Nodes.Add(node)

          If modInfo.AddonsPathInfo.ExistsPbo(pbo) Then
            '存在する
            node.ImageIndex = 5
            node.SelectedImageIndex = node.ImageIndex
          Else
            '存在しない
            node.ImageIndex = 3
            node.SelectedImageIndex = node.ImageIndex
          End If

        Next

        '1件以上あれば追加する
        If disableNode.Nodes.Count >= 1 Then
          modNode.Nodes.Add(disableNode)
        End If


        '1件以上あれば追加する
        If modNode.Nodes.Count >= 1 Then
          serverNode.Nodes.Add(modNode)
        End If
      Next


      serverNode.ExpandAll()


    Catch ex As Exception

      MessageBox.Show(ex.Message,
                          My.Application.Info.AssemblyName,
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
      Me.Close()

    Finally
      'ボタン類を有効にする
      Me.EnableButtons(True)
    End Try

  End Sub

  ''' <summary>
  ''' ログテキスト表示
  ''' </summary>
  ''' <param name="text"></param>
  Private Sub AddLogText(Optional text As String = "")
    Me.LogListBox.Items.Add(text)
    Me.LogListBox.SelectedIndex = Me.LogListBox.Items.Count - 1
  End Sub


  ''' <summary>
  ''' ボタン類のDisable/Enable切り替え
  ''' </summary>
  Private Sub EnableButtons(isEnable As Boolean)

    '処理中表示
    Me.LoadingPictureBox.Visible = Not isEnable

    '各ボタンの操作を無効にする
    Me.OKButton.Enabled = isEnable
    Me.GetModInfoButton.Enabled = isEnable
    Me.CreateShortCutCheckBox.Enabled = isEnable
    Me.CloseButton.Enabled = isEnable

    If Me.ModTreeView.Nodes.Count <= 0 Then
      '0件の場合は処理実行ボタンを無効にする
      Me.OKButton.Enabled = False
    End If
  End Sub




  ''' <summary>
  ''' バックグラウンド処理完了時の処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SetBackgroundWorker_RunWorkerCompleted(sender As Object, e As RunWorkerCompletedEventArgs) Handles SetBackgroundWorker.RunWorkerCompleted, ResetBackgroundWorker.RunWorkerCompleted

    If Not (e.Error Is Nothing) Then
      MessageBox.Show(e.Error.Message,
                        My.Application.Info.AssemblyName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error)
    ElseIf e.Cancelled Then
      'キャンセルされた（このプログラムでは存在しない）
    Else
      '正常終了
      'MOD情報更新
      Me.UpdateModTreeView()
    End If

    'ボタン類を有効にする
    Me.EnableButtons(True)
  End Sub

  ''' <summary>
  ''' 設定処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub SetBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles SetBackgroundWorker.DoWork

    Try
      Dim addLog As New Action(Of String)(AddressOf AddLogText)

      Me.Invoke(addLog, LogSeparator)
      Me.Invoke(addLog, My.Resources.TextResource.LogTextStart) '処理を開始
      Me.Invoke(addLog, LogSeparator)
      Me.Invoke(addLog, "")

      'mod数分ループ
      For Each modinfo As ModInfo In Me.ModsInfoList

        Me.Invoke(addLog, LogSeparator)
        Me.Invoke(addLog, "[" & modinfo.ModSetting.ModName & "]")
        Me.Invoke(addLog, "")

        'addファイルを処理
        For Each tgtFile As String In modinfo.ModSetting.AddFile
          'ファイルをコピー
          For Each file As String In Common.File.GetFileList(modinfo.OptionalPathInfo.Path, tgtFile & "*", False)

            Me.Invoke(addLog, My.Resources.TextResource.LogTextAddFile & My.Computer.FileSystem.GetName(file))

            'コピー処理
            Common.File.CopyFile(file, Common.File.CombinePath(modinfo.AddonsPathInfo.Path, Common.File.GetFileName(file)), True)
          Next
        Next

        Me.Invoke(addLog, "")

        'delファイルを処理
        For Each tgtFile As String In modinfo.ModSetting.DelFile

          For Each file As String In Common.File.GetFileList(modinfo.AddonsPathInfo.Path, tgtFile & "*", False)

            Me.Invoke(addLog, My.Resources.TextResource.LogTextDelFile & My.Computer.FileSystem.GetName(file))

            'disableへコピーして削除
            Common.File.CopyFile(file, Common.File.CombinePath(modinfo.DisablePathInfo.Path, Common.File.GetFileName(file)), True)

            '削除
            Common.File.DeleteFile(file)
          Next
        Next

        Me.Invoke(addLog, "")
        Me.Invoke(addLog, "")
      Next

      'ショートカット作成
      If Me.CreateShortCutCheckBox.Checked Then
        Me.Invoke(addLog, My.Resources.TextResource.LogTextCreateShortCut) 'ショートカット作成

        '作成するショートカットのパス
        Dim shortcutPath As String = Common.File.GetDesktopDirectory & My.Application.Info.Title & ".lnk"

        'ショートカットのリンク先
        Dim targetPath As String = Application.ExecutablePath

        '作成
        Common.File.CreateShortcut(shortcutPath, targetPath,,,, CreatedShortCutArguments)

        Me.Invoke(addLog, "")
      End If

      Me.Invoke(addLog, My.Resources.TextResource.LogTextFinish) '処理が終了しました。


    Catch ex As Exception
      Throw
    End Try

  End Sub

  ''' <summary>
  ''' リセット処理
  ''' </summary>
  ''' <param name="sender"></param>
  ''' <param name="e"></param>
  Private Sub ResetBackgroundWorker_DoWork(sender As Object, e As DoWorkEventArgs) Handles ResetBackgroundWorker.DoWork


    Try
      Dim addLog As New Action(Of String)(AddressOf AddLogText)

      Me.Invoke(addLog, LogSeparator)
      Me.Invoke(addLog, My.Resources.TextResource.LogTextStart) '処理を開始
      Me.Invoke(addLog, LogSeparator)
      Me.Invoke(addLog, "")

      'mod数分ループ
      For Each modinfo As ModInfo In Me.ModsInfoList

        Me.Invoke(addLog, LogSeparator)
        Me.Invoke(addLog, "[" & modinfo.ModSetting.ModName & "]")
        Me.Invoke(addLog, "")

        'disable からaddon へ移動
        For Each file As String In Common.File.GetFileList(modinfo.DisablePathInfo.Path, "*.*", False)
          Me.Invoke(addLog, My.Resources.TextResource.LogTextResetFile & My.Computer.FileSystem.GetName(file))

          'コピー処理
          Common.File.CopyFile(file, Common.File.CombinePath(modinfo.AddonsPathInfo.Path, Common.File.GetFileName(file)), True)

          '削除
          Common.File.DeleteFile(file)
        Next

        'Ontionに存在したら削除
        For Each optFile As String In Common.File.GetFileList(modinfo.OptionalPathInfo.Path, "*.pbo", False)
          For Each file As String In Common.File.GetFileList(modinfo.AddonsPathInfo.Path, Common.File.GetFileName(optFile) & "*", False)

            Me.Invoke(addLog, My.Resources.TextResource.LogTextResetFile & My.Computer.FileSystem.GetName(file))

            '削除
            Common.File.DeleteFile(file)
          Next
        Next


        Me.Invoke(addLog, "")
        Me.Invoke(addLog, "")
      Next

      Me.Invoke(addLog, My.Resources.TextResource.LogTextFinish) '処理が終了しました。

    Catch ex As Exception
      Throw
    End Try

  End Sub


End Class


#endif