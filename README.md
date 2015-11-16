Arma3 Mod Option Mover
====

このプログラムはArma3のMOD内のオプションファイルをAddonsフォルダへ移動させます。  

## 開発環境
 Microsoft Visual Studio Community 2015

## 必要ランタイム
 .NET Framework 4.5  

## 使い方

1.modフォルダ内の @Arma3ModOptionMover_serverName(serverNameは変更必要)内の　ServerSetting.cfg　を使用







***
MODフォルダ  
　　+-- @ace3  
　　　　+-- addons  
　　　　+-- optionals  
　　+-- @sma  
　　　　+-- addons  
　　　　+-- optional  
　　+-- @Arma3ModOptionMover
　　　　+-- Arma3ModOptionMover.exe      <---- このファイルを実行  
　　　　+-- ServerList.cfg               <---- サーバー設定ファイル  
　　　　+-- ServerSetting.cfg            <---- 自動でダウンロードされます  
　　+-- @Arma3ModOptionMover_serverName  <---- サーバー管理者が各自PwSへアップしてください。
　　　　+-- ServerSetting.cfg            <---- サーバー管理者が各自PwSへアップしてください。

***

##リセット機能
リセット機能は、ありませんので もとに戻す場合は、Play withSixにて Diagnose してください。

## Licence
* MIT  
    * see LICENSE.txt

## Author

[itom0717](https://github.com/itom0717)
