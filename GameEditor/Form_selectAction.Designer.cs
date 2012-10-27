namespace GameEditor
{
    partial class Form_selectAction
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_editMap = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_editMap
            // 
            this.btn_editMap.Location = new System.Drawing.Point(59, 36);
            this.btn_editMap.Name = "btn_editMap";
            this.btn_editMap.Size = new System.Drawing.Size(167, 23);
            this.btn_editMap.TabIndex = 0;
            this.btn_editMap.Text = "Редактировать Карту";
            this.btn_editMap.UseVisualStyleBackColor = true;
            this.btn_editMap.Click += new System.EventHandler(this.btn_editMap_Click);
            // 
            // form_selectAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btn_editMap);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "form_selectAction";
            this.Text = "ForestDefense Editor";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_editMap;
    }
}

