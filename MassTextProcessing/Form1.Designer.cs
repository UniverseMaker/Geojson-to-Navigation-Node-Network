namespace MassTextProcessing
{
    partial class Form1
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
            this.txtSrc = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnKaistSafetyGeojsonPreprocessing = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.btnKSMG_BAP_Merge = new System.Windows.Forms.Button();
            this.btnGeojsonLineMerge = new System.Windows.Forms.Button();
            this.txtResult = new System.Windows.Forms.TextBox();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtSrc
            // 
            this.txtSrc.Location = new System.Drawing.Point(16, 30);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(500, 28);
            this.txtSrc.TabIndex = 0;
            this.txtSrc.Text = "C:\\";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Source";
            // 
            // btnKaistSafetyGeojsonPreprocessing
            // 
            this.btnKaistSafetyGeojsonPreprocessing.Location = new System.Drawing.Point(15, 64);
            this.btnKaistSafetyGeojsonPreprocessing.Name = "btnKaistSafetyGeojsonPreprocessing";
            this.btnKaistSafetyGeojsonPreprocessing.Size = new System.Drawing.Size(500, 32);
            this.btnKaistSafetyGeojsonPreprocessing.TabIndex = 2;
            this.btnKaistSafetyGeojsonPreprocessing.Text = "KAIST SAFETY MAP GEOJSON PREPROCESSING";
            this.btnKaistSafetyGeojsonPreprocessing.UseVisualStyleBackColor = true;
            this.btnKaistSafetyGeojsonPreprocessing.Click += new System.EventHandler(this.btnKaistSafetyGeojsonPreprocessing_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 955);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1666, 32);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(67, 25);
            this.lblStatus.Text = "READY";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 24);
            // 
            // btnKSMG_BAP_Merge
            // 
            this.btnKSMG_BAP_Merge.Location = new System.Drawing.Point(16, 102);
            this.btnKSMG_BAP_Merge.Name = "btnKSMG_BAP_Merge";
            this.btnKSMG_BAP_Merge.Size = new System.Drawing.Size(500, 32);
            this.btnKSMG_BAP_Merge.TabIndex = 4;
            this.btnKSMG_BAP_Merge.Text = "KAIST SAFETY MAP GEOJSON BUILDING AND POI MERGE";
            this.btnKSMG_BAP_Merge.UseVisualStyleBackColor = true;
            this.btnKSMG_BAP_Merge.Click += new System.EventHandler(this.btnKSMG_BAP_Merge_Click);
            // 
            // btnGeojsonLineMerge
            // 
            this.btnGeojsonLineMerge.Location = new System.Drawing.Point(16, 140);
            this.btnGeojsonLineMerge.Name = "btnGeojsonLineMerge";
            this.btnGeojsonLineMerge.Size = new System.Drawing.Size(500, 32);
            this.btnGeojsonLineMerge.TabIndex = 5;
            this.btnGeojsonLineMerge.Text = "GEOJSON LINE STRING MERGE";
            this.btnGeojsonLineMerge.UseVisualStyleBackColor = true;
            this.btnGeojsonLineMerge.Click += new System.EventHandler(this.btnGeojsonLineMerge_Click);
            // 
            // txtResult
            // 
            this.txtResult.Location = new System.Drawing.Point(522, 30);
            this.txtResult.Multiline = true;
            this.txtResult.Name = "txtResult";
            this.txtResult.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResult.Size = new System.Drawing.Size(1132, 913);
            this.txtResult.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1666, 987);
            this.Controls.Add(this.txtResult);
            this.Controls.Add(this.btnGeojsonLineMerge);
            this.Controls.Add(this.btnKSMG_BAP_Merge);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.btnKaistSafetyGeojsonPreprocessing);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSrc);
            this.Name = "Form1";
            this.Text = "Massive Text Processing";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSrc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnKaistSafetyGeojsonPreprocessing;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.Button btnKSMG_BAP_Merge;
        private System.Windows.Forms.Button btnGeojsonLineMerge;
        private System.Windows.Forms.TextBox txtResult;
    }
}

