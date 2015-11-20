Arma3 Mod Option Mover
====

このプログラムはArma3のMOD内のオプションファイルをAddonsフォルダへ移動させます。  
また、使わないオプションファイルを Addonsフォルダから除去します。

## 開発環境
 Microsoft Visual Studio Community 2015

## 必要ランタイム
 .NET Framework 4.5  

## 使い方

###サーバー管理者向け  
1. sample_ServerModSetting.cfg を参考に設定ファイルを作成します。
2. Play WithSixへ作成した設定ファイルをアップロード  
  ・フォルダ名を @ Arma3ModOptionMover_(ServerName) とする。  
　・(ServerNamer)には任意ですが、サーバー名を入れると良いでしょう。  
　・ファイル名は ServerModSetting.cfg とします。
3. @Arma3ModOptionMover と @Arma3ModOptionMover_(ServerName)  をレポジトリに登録  
4. Play withSix にてダウンロード（以下のようになるはず）  

MODフォルダ  
　　+-- @ace3  
　　　　+-- addons  
　　　　+-- optionals  
　　+-- @sma  
　　　　+-- addons  
　　　　+-- optional  
　　+-- @Arma3ModOptionMover  
　　　　+-- Arma3ModOptionMover.exe   
　　+-- @Arma3ModOptionMover_(ServerName)  
　　　　+-- ServerSetting.cfg  

5.@Arma3ModOptionMover 内の　Arma3ModOptionMover.exeを実行します。  
6.複数の設定ファイルが存在する場合は、コンボボックスで選択可能です。  

***

###個人で使用（サーバー管理者が本プログラムを導入していない場合）
1. sample_ServerModSetting.cfg を参考に設定ファイルを作成します。
2. Arma3Modフォルダに @ Arma3ModOptionMover_(ServerName) を作成する。
3. 作成したフォルダ内に設定ファイルを入れる
3. @Arma3ModOptionMover をPlay withSix または、GitHubよりダウンロード  
5. @Arma3ModOptionMover 内の　Arma3ModOptionMover.exeを実行します。





##リストア機能（テスト機能）
実行時に前回に移動した処理を一旦もとに戻してから新たにファイルを移動します。  
設定が異なるサーバーで交互にPlayする場合に Play withSixにて Diagnose　する必要がありません。  

※ファイルの状態がおかしくおかしくなった場合、Diagnoseを実施してください。



## Licence
* MIT  
    * see LICENSE.txt

## Author

[itom0717](https://github.com/itom0717)
