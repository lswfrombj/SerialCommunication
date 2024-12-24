
namespace tt
{
    partial class FrmInputDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnok = new System.Windows.Forms.Button();
            this.btncancel = new System.Windows.Forms.Button();
            this.label24 = new System.Windows.Forms.Label();
            this.txtbeginnum = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnok
            // 
            this.btnok.Location = new System.Drawing.Point(99, 109);
            this.btnok.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnok.Name = "btnok";
            this.btnok.Size = new System.Drawing.Size(99, 42);
            this.btnok.TabIndex = 0;
            this.btnok.Text = "确定";
            this.btnok.UseVisualStyleBackColor = true;
            this.btnok.Click += new System.EventHandler(this.btnok_Click);
            // 
            // btncancel
            // 
            this.btncancel.Location = new System.Drawing.Point(296, 109);
            this.btncancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btncancel.Name = "btncancel";
            this.btncancel.Size = new System.Drawing.Size(99, 42);
            this.btncancel.TabIndex = 1;
            this.btncancel.Text = "取消";
            this.btncancel.UseVisualStyleBackColor = true;
            this.btncancel.Click += new System.EventHandler(this.btncancel_Click);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(34, 35);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(166, 24);
            this.label24.TabIndex = 3;
            this.label24.Text = "加载数据范围:";
            // 
            // txtbeginnum
            // 
            this.txtbeginnum.Location = new System.Drawing.Point(231, 30);
            this.txtbeginnum.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtbeginnum.Name = "txtbeginnum";
            this.txtbeginnum.Size = new System.Drawing.Size(213, 35);
            this.txtbeginnum.TabIndex = 2;
            this.txtbeginnum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtString_KeyPress);
            // 
            // FrmInputDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 200);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtbeginnum);
            this.Controls.Add(this.btncancel);
            this.Controls.Add(this.btnok);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "FrmInputDialog";
            this.Text = "输入加载数据范围";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnok;
        private System.Windows.Forms.Button btncancel;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox txtbeginnum;
    }
}