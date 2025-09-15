
namespace WindowsFormsClient
{
    partial class FormClient
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
            this.buttonConnect = new System.Windows.Forms.Button();
            this.listBoxChat = new System.Windows.Forms.ListBox();
            this.textChat = new System.Windows.Forms.TextBox();
            this.buttonChat = new System.Windows.Forms.Button();
            this.richTextBoxMessage = new System.Windows.Forms.RichTextBox();
            this.buttonMessage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(901, 36);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(366, 70);
            this.buttonConnect.TabIndex = 0;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // listBoxChat
            // 
            this.listBoxChat.FormattingEnabled = true;
            this.listBoxChat.HorizontalScrollbar = true;
            this.listBoxChat.ItemHeight = 18;
            this.listBoxChat.Location = new System.Drawing.Point(901, 127);
            this.listBoxChat.Name = "listBoxChat";
            this.listBoxChat.Size = new System.Drawing.Size(366, 904);
            this.listBoxChat.TabIndex = 1;
            // 
            // textChat
            // 
            this.textChat.Location = new System.Drawing.Point(901, 1048);
            this.textChat.Name = "textChat";
            this.textChat.Size = new System.Drawing.Size(224, 28);
            this.textChat.TabIndex = 2;
            this.textChat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textChat_KeyDown);
            // 
            // buttonChat
            // 
            this.buttonChat.Location = new System.Drawing.Point(1154, 1048);
            this.buttonChat.Name = "buttonChat";
            this.buttonChat.Size = new System.Drawing.Size(113, 28);
            this.buttonChat.TabIndex = 3;
            this.buttonChat.Text = "Send Chat";
            this.buttonChat.UseVisualStyleBackColor = true;
            this.buttonChat.Click += new System.EventHandler(this.buttonChat_Click);
            // 
            // richTextBoxMessage
            // 
            this.richTextBoxMessage.Location = new System.Drawing.Point(39, 36);
            this.richTextBoxMessage.Name = "richTextBoxMessage";
            this.richTextBoxMessage.Size = new System.Drawing.Size(819, 1141);
            this.richTextBoxMessage.TabIndex = 4;
            this.richTextBoxMessage.Text = "";
            // 
            // buttonMessage
            // 
            this.buttonMessage.Location = new System.Drawing.Point(901, 1100);
            this.buttonMessage.Name = "buttonMessage";
            this.buttonMessage.Size = new System.Drawing.Size(366, 77);
            this.buttonMessage.TabIndex = 5;
            this.buttonMessage.Text = "Send Message";
            this.buttonMessage.UseVisualStyleBackColor = true;
            this.buttonMessage.Click += new System.EventHandler(this.buttonMessage_Click);
            // 
            // FormClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1307, 1212);
            this.Controls.Add(this.buttonMessage);
            this.Controls.Add(this.richTextBoxMessage);
            this.Controls.Add(this.buttonChat);
            this.Controls.Add(this.textChat);
            this.Controls.Add(this.listBoxChat);
            this.Controls.Add(this.buttonConnect);
            this.Name = "FormClient";
            this.Text = "FormClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.ListBox listBoxChat;
        private System.Windows.Forms.TextBox textChat;
        private System.Windows.Forms.Button buttonChat;
        private System.Windows.Forms.RichTextBox richTextBoxMessage;
        private System.Windows.Forms.Button buttonMessage;
    }
}

