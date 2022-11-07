namespace Test
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOpen = new System.Windows.Forms.Button();
            this.labStatus = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbStopBits = new System.Windows.Forms.ComboBox();
            this.cbDataBits = new System.Windows.Forms.ComboBox();
            this.cbParity = new System.Windows.Forms.ComboBox();
            this.cbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbPortName = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbDataType = new System.Windows.Forms.ComboBox();
            this.txbSendData = new System.Windows.Forms.TextBox();
            this.txbSlaveStation = new System.Windows.Forms.NumericUpDown();
            this.txbStartAddress = new System.Windows.Forms.NumericUpDown();
            this.txbLength = new System.Windows.Forms.NumericUpDown();
            this.cbMethod = new System.Windows.Forms.ComboBox();
            this.rtxbSendMsg = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txbReceiveData = new System.Windows.Forms.RichTextBox();
            this.rtxbReceiveMsg = new System.Windows.Forms.RichTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.cbByteOrder = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txbSlaveStation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStartAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbLength)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnOpen);
            this.groupBox1.Controls.Add(this.labStatus);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cbStopBits);
            this.groupBox1.Controls.Add(this.cbDataBits);
            this.groupBox1.Controls.Add(this.cbParity);
            this.groupBox1.Controls.Add(this.cbBaudRate);
            this.groupBox1.Controls.Add(this.cbPortName);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 422);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "控制面板";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnClose.Location = new System.Drawing.Point(6, 373);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 23);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "关闭串口";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnOpen.Location = new System.Drawing.Point(115, 373);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(86, 23);
            this.btnOpen.TabIndex = 4;
            this.btnOpen.Text = "打开串口";
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // labStatus
            // 
            this.labStatus.AutoSize = true;
            this.labStatus.Location = new System.Drawing.Point(80, 78);
            this.labStatus.Name = "labStatus";
            this.labStatus.Size = new System.Drawing.Size(44, 17);
            this.labStatus.TabIndex = 3;
            this.labStatus.Text = "未连接";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 253);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(56, 17);
            this.label7.TabIndex = 2;
            this.label7.Text = "停止位：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 17);
            this.label6.TabIndex = 2;
            this.label6.Text = "数据位：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 162);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(68, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "奇偶校验：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "波特率：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "连接状态：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "可用串口：";
            // 
            // cbStopBits
            // 
            this.cbStopBits.FormattingEnabled = true;
            this.cbStopBits.Location = new System.Drawing.Point(80, 250);
            this.cbStopBits.Name = "cbStopBits";
            this.cbStopBits.Size = new System.Drawing.Size(121, 25);
            this.cbStopBits.TabIndex = 0;
            // 
            // cbDataBits
            // 
            this.cbDataBits.FormattingEnabled = true;
            this.cbDataBits.Location = new System.Drawing.Point(80, 205);
            this.cbDataBits.Name = "cbDataBits";
            this.cbDataBits.Size = new System.Drawing.Size(121, 25);
            this.cbDataBits.TabIndex = 0;
            // 
            // cbParity
            // 
            this.cbParity.FormattingEnabled = true;
            this.cbParity.Location = new System.Drawing.Point(80, 159);
            this.cbParity.Name = "cbParity";
            this.cbParity.Size = new System.Drawing.Size(121, 25);
            this.cbParity.TabIndex = 0;
            // 
            // cbBaudRate
            // 
            this.cbBaudRate.FormattingEnabled = true;
            this.cbBaudRate.Location = new System.Drawing.Point(80, 116);
            this.cbBaudRate.Name = "cbBaudRate";
            this.cbBaudRate.Size = new System.Drawing.Size(121, 25);
            this.cbBaudRate.TabIndex = 0;
            // 
            // cbPortName
            // 
            this.cbPortName.FormattingEnabled = true;
            this.cbPortName.Location = new System.Drawing.Point(80, 37);
            this.cbPortName.Name = "cbPortName";
            this.cbPortName.Size = new System.Drawing.Size(121, 25);
            this.cbPortName.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbByteOrder);
            this.groupBox2.Controls.Add(this.cbDataType);
            this.groupBox2.Controls.Add(this.txbSendData);
            this.groupBox2.Controls.Add(this.txbSlaveStation);
            this.groupBox2.Controls.Add(this.txbStartAddress);
            this.groupBox2.Controls.Add(this.txbLength);
            this.groupBox2.Controls.Add(this.cbMethod);
            this.groupBox2.Controls.Add(this.rtxbSendMsg);
            this.groupBox2.Controls.Add(this.btnSend);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(248, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(673, 232);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "发送消息";
            // 
            // cbDataType
            // 
            this.cbDataType.FormattingEnabled = true;
            this.cbDataType.Location = new System.Drawing.Point(87, 76);
            this.cbDataType.Name = "cbDataType";
            this.cbDataType.Size = new System.Drawing.Size(75, 25);
            this.cbDataType.TabIndex = 9;
            // 
            // txbSendData
            // 
            this.txbSendData.Location = new System.Drawing.Point(87, 127);
            this.txbSendData.Name = "txbSendData";
            this.txbSendData.Size = new System.Drawing.Size(580, 23);
            this.txbSendData.TabIndex = 8;
            // 
            // txbSlaveStation
            // 
            this.txbSlaveStation.Location = new System.Drawing.Point(287, 33);
            this.txbSlaveStation.Name = "txbSlaveStation";
            this.txbSlaveStation.Size = new System.Drawing.Size(75, 23);
            this.txbSlaveStation.TabIndex = 7;
            // 
            // txbStartAddress
            // 
            this.txbStartAddress.Location = new System.Drawing.Point(442, 33);
            this.txbStartAddress.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txbStartAddress.Name = "txbStartAddress";
            this.txbStartAddress.Size = new System.Drawing.Size(75, 23);
            this.txbStartAddress.TabIndex = 7;
            // 
            // txbLength
            // 
            this.txbLength.Location = new System.Drawing.Point(592, 33);
            this.txbLength.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.txbLength.Name = "txbLength";
            this.txbLength.Size = new System.Drawing.Size(75, 23);
            this.txbLength.TabIndex = 7;
            // 
            // cbMethod
            // 
            this.cbMethod.FormattingEnabled = true;
            this.cbMethod.Location = new System.Drawing.Point(87, 32);
            this.cbMethod.Name = "cbMethod";
            this.cbMethod.Size = new System.Drawing.Size(113, 25);
            this.cbMethod.TabIndex = 4;
            this.cbMethod.SelectedIndexChanged += new System.EventHandler(this.cbMethod_SelectedIndexChanged);
            // 
            // rtxbSendMsg
            // 
            this.rtxbSendMsg.Location = new System.Drawing.Point(87, 169);
            this.rtxbSendMsg.Name = "rtxbSendMsg";
            this.rtxbSendMsg.Size = new System.Drawing.Size(580, 50);
            this.rtxbSendMsg.TabIndex = 3;
            this.rtxbSendMsg.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(592, 76);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(528, 38);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(68, 17);
            this.label11.TabIndex = 0;
            this.label11.Text = "读取长度：";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(13, 79);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(68, 17);
            this.label15.TabIndex = 0;
            this.label15.Text = "返回类型：";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(368, 37);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(68, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "起始地址：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(214, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(68, 17);
            this.label9.TabIndex = 0;
            this.label9.Text = "从站地址：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 38);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 17);
            this.label3.TabIndex = 0;
            this.label3.Text = "读写模式：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 172);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(68, 17);
            this.label12.TabIndex = 0;
            this.label12.Text = "请求报文：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(0, 130);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "值(逗号分割)：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txbReceiveData);
            this.groupBox3.Controls.Add(this.rtxbReceiveMsg);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Location = new System.Drawing.Point(248, 271);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(673, 163);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "接收消息";
            // 
            // txbReceiveData
            // 
            this.txbReceiveData.Location = new System.Drawing.Point(87, 92);
            this.txbReceiveData.Name = "txbReceiveData";
            this.txbReceiveData.Size = new System.Drawing.Size(580, 53);
            this.txbReceiveData.TabIndex = 3;
            this.txbReceiveData.Text = "";
            // 
            // rtxbReceiveMsg
            // 
            this.rtxbReceiveMsg.Location = new System.Drawing.Point(87, 22);
            this.rtxbReceiveMsg.Name = "rtxbReceiveMsg";
            this.rtxbReceiveMsg.Size = new System.Drawing.Size(580, 55);
            this.rtxbReceiveMsg.TabIndex = 3;
            this.rtxbReceiveMsg.Text = "";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(0, 95);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 17);
            this.label14.TabIndex = 0;
            this.label14.Text = "响应数据：";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 25);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 17);
            this.label13.TabIndex = 0;
            this.label13.Text = "响应报文：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(213, 79);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(68, 17);
            this.label16.TabIndex = 0;
            this.label16.Text = "字节顺序：";
            // 
            // cbByteOrder
            // 
            this.cbByteOrder.FormattingEnabled = true;
            this.cbByteOrder.Location = new System.Drawing.Point(287, 76);
            this.cbByteOrder.Name = "cbByteOrder";
            this.cbByteOrder.Size = new System.Drawing.Size(75, 25);
            this.cbByteOrder.TabIndex = 9;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(933, 446);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txbSlaveStation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbStartAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbLength)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label labStatus;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbPortName;
        private System.Windows.Forms.Button btnOpen;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbStopBits;
        private System.Windows.Forms.ComboBox cbDataBits;
        private System.Windows.Forms.ComboBox cbParity;
        private System.Windows.Forms.ComboBox cbBaudRate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox rtxbSendMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RichTextBox rtxbReceiveMsg;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbMethod;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txbSlaveStation;
        private System.Windows.Forms.NumericUpDown txbStartAddress;
        private System.Windows.Forms.NumericUpDown txbLength;
        private System.Windows.Forms.TextBox txbSendData;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RichTextBox txbReceiveData;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cbDataType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbByteOrder;
        private System.Windows.Forms.Label label16;
    }
}
