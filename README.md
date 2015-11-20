Arma3 Mod Option Mover
====

このプログラムはArma3のMOD内のオプションファイルをAddonsフォルダへ移動させます。  

## 開発環境
 Microsoft Visual Studio Community 2015

## 必要ランタイム
 .NET Framework 4.5  

## 使い方


準備  (サーバー管理者向け)  
+ Play WithSixへ設定ファイルをアップロードします。    
    + フォルダ名を @ Arma3ModOptionMover_(ServerNamer) とする。  
　・(ServerNamer)には任意ですが、サーバー名を入れると良いでしょう。  
+ @Arma3ModOptionMover と @Arma3ModOptionMover_(ServerNamer)  をレポジトリに登録  
3.Play withSix にてダウンロード（以下のようになるはず）  

***
MODフォルダ  
　　+-- @ace3  
　　　　+-- addons  
　　　　+-- optionals  
　　+-- @sma  
　　　　+-- addons  
　　　　+-- optional  
　　+-- @Arma3ModOptionMover  
　　　　+-- Arma3ModOptionMover.exe       <---- このファイルを実行  
　　+-- @Arma3ModOptionMover_(ServerNamer)  
　　　　+-- ServerSetting.cfg  

***


## Licence
* MIT  
    * see LICENSE.txt

## Author

[itom0717](https://github.com/itom0717)
