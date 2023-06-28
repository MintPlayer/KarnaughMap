namespace MintPlayer.KarnaughMap.Demo
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
            karnaughMap1 = new KarnaughMap();
            SuspendLayout();
            // 
            // karnaughMap1
            // 
            karnaughMap1.Location = new Point(12, 12);
            karnaughMap1.Name = "karnaughMap1";
            karnaughMap1.PetricksMethod = null;
            karnaughMap1.QuineMcCluskey = null;
            karnaughMap1.Size = new Size(242, 236);
            karnaughMap1.TabIndex = 1;
            karnaughMap1.Text = "karnaughMap2";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 600);
            Controls.Add(karnaughMap1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
        }

        #endregion

        private KarnaughMap karnaughMap1;
    }
}