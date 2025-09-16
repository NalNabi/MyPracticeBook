
namespace WindowsFormsDelegateServer
{
    partial class FormServer
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.listViewClient = new System.Windows.Forms.ListView();
            this.ColumnEP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnText = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ColumnUpdate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // listViewClient
            // 
            this.listViewClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ColumnEP,
            this.ColumnID,
            this.ColumnText,
            this.ColumnUpdate});
            this.listViewClient.HideSelection = false;
            this.listViewClient.Location = new System.Drawing.Point(46, 38);
            this.listViewClient.Name = "listViewClient";
            this.listViewClient.Size = new System.Drawing.Size(1267, 696);
            this.listViewClient.TabIndex = 0;
            this.listViewClient.UseCompatibleStateImageBehavior = false;
            this.listViewClient.View = System.Windows.Forms.View.Details;
            this.listViewClient.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewClient_MouseDoubleClick);
            // 
            // ColumnEP
            // 
            this.ColumnEP.Text = "EP";
            this.ColumnEP.Width = 173;
            // 
            // ColumnID
            // 
            this.ColumnID.Text = "ID";
            this.ColumnID.Width = 126;
            // 
            // ColumnText
            // 
            this.ColumnText.Text = "Text";
            this.ColumnText.Width = 284;
            // 
            // ColumnUpdate
            // 
            this.ColumnUpdate.Text = "Update";
            this.ColumnUpdate.Width = 228;
            // 
            // FormServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 787);
            this.Controls.Add(this.listViewClient);
            this.Name = "FormServer";
            this.Text = "Server";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listViewClient;
        private System.Windows.Forms.ColumnHeader ColumnEP;
        private System.Windows.Forms.ColumnHeader ColumnID;
        private System.Windows.Forms.ColumnHeader ColumnText;
        private System.Windows.Forms.ColumnHeader ColumnUpdate;
    }
}

