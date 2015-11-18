namespace Arma3ModOptionMover
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.ModTreeView = new System.Windows.Forms.TreeView();
            this.TreeViewImageList = new System.Windows.Forms.ImageList(this.components);
            this.LogListBox = new System.Windows.Forms.ListBox();
            this.CreateShortCutCheckBox = new System.Windows.Forms.CheckBox();
            this.OKButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.LoadingPictureBox = new System.Windows.Forms.PictureBox();
            this.GetModInfoButton = new System.Windows.Forms.Button();
            this.SetBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ServerListComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // ModTreeView
            // 
            resources.ApplyResources(this.ModTreeView, "ModTreeView");
            this.ModTreeView.ImageList = this.TreeViewImageList;
            this.ModTreeView.Name = "ModTreeView";
            // 
            // TreeViewImageList
            // 
            this.TreeViewImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("TreeViewImageList.ImageStream")));
            this.TreeViewImageList.TransparentColor = System.Drawing.Color.Fuchsia;
            this.TreeViewImageList.Images.SetKeyName(0, "OpenFolder.bmp");
            this.TreeViewImageList.Images.SetKeyName(1, "AddTable.bmp");
            this.TreeViewImageList.Images.SetKeyName(2, "DeleteTable.bmp");
            this.TreeViewImageList.Images.SetKeyName(3, "Task.bmp");
            this.TreeViewImageList.Images.SetKeyName(4, "NewDocument.bmp");
            this.TreeViewImageList.Images.SetKeyName(5, "Refresh_Cancel.bmp");
            this.TreeViewImageList.Images.SetKeyName(6, "DatabaseProject_7342.ico");
            // 
            // LogListBox
            // 
            resources.ApplyResources(this.LogListBox, "LogListBox");
            this.LogListBox.FormattingEnabled = true;
            this.LogListBox.Name = "LogListBox";
            // 
            // CreateShortCutCheckBox
            // 
            resources.ApplyResources(this.CreateShortCutCheckBox, "CreateShortCutCheckBox");
            this.CreateShortCutCheckBox.Name = "CreateShortCutCheckBox";
            this.CreateShortCutCheckBox.UseVisualStyleBackColor = true;
            // 
            // OKButton
            // 
            resources.ApplyResources(this.OKButton, "OKButton");
            this.OKButton.Name = "OKButton";
            this.OKButton.UseVisualStyleBackColor = true;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            // 
            // CloseButton
            // 
            resources.ApplyResources(this.CloseButton, "CloseButton");
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // LoadingPictureBox
            // 
            resources.ApplyResources(this.LoadingPictureBox, "LoadingPictureBox");
            this.LoadingPictureBox.BackColor = System.Drawing.Color.White;
            this.LoadingPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LoadingPictureBox.Name = "LoadingPictureBox";
            this.LoadingPictureBox.TabStop = false;
            // 
            // GetModInfoButton
            // 
            resources.ApplyResources(this.GetModInfoButton, "GetModInfoButton");
            this.GetModInfoButton.Name = "GetModInfoButton";
            this.GetModInfoButton.UseVisualStyleBackColor = true;
            this.GetModInfoButton.Click += new System.EventHandler(this.GetModInfoButton_Click);
            // 
            // SetBackgroundWorker
            // 
            this.SetBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.SetBackgroundWorker_DoWork);
            this.SetBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.SetBackgroundWorker_ProgressChanged);
            this.SetBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.SetBackgroundWorker_RunWorkerCompleted);
            // 
            // ServerListComboBox
            // 
            resources.ApplyResources(this.ServerListComboBox, "ServerListComboBox");
            this.ServerListComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ServerListComboBox.FormattingEnabled = true;
            this.ServerListComboBox.Name = "ServerListComboBox";
            this.ServerListComboBox.SelectedIndexChanged += new System.EventHandler(this.ServerListComboBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.LoadingPictureBox);
            this.Controls.Add(this.ModTreeView);
            this.Controls.Add(this.CreateShortCutCheckBox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.GetModInfoButton);
            this.Controls.Add(this.ServerListComboBox);
            this.Controls.Add(this.LogListBox);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.LoadingPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.TreeView ModTreeView;
        internal System.Windows.Forms.ImageList TreeViewImageList;
        internal System.Windows.Forms.ListBox LogListBox;
        internal System.Windows.Forms.CheckBox CreateShortCutCheckBox;
        internal System.Windows.Forms.Button OKButton;
        internal System.Windows.Forms.Button CloseButton;
        internal System.Windows.Forms.PictureBox LoadingPictureBox;
        internal System.Windows.Forms.Button GetModInfoButton;
        internal System.ComponentModel.BackgroundWorker SetBackgroundWorker;
        internal System.Windows.Forms.ComboBox ServerListComboBox;
    }
}

