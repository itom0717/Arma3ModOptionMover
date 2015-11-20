using System.Collections.Generic;

namespace Arma3ModOptionMover
{
    /// <summary>
    /// Mod情報
    /// </summary>
    public class ModInfomation
    {
        /// <summary>
        /// MOD名
        /// </summary>
        /// <returns></returns>
        public string ModName { get; set; } = "";

        /// <summary>
        /// optionalフォルダ名
        /// </summary>
        /// <returns></returns>
        public string OptionalsPath { get; set; } = "";

        /// <summary>
        /// 追加ファイル
        /// </summary>
        /// <returns></returns>
        public List<string> AddFile = new  List<string>();

        /// <summary>
        /// 削除ファイル
        /// </summary>
        /// <returns></returns>
        public List<string> RemoveFile = new  List<string>();



    }
}
