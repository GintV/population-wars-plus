namespace TowerDefense.UI
{
    partial class GameView
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
            Game.StopGame();
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
            this.SuspendLayout();
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1596, 896);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1612, 935);
            this.MinimumSize = new System.Drawing.Size(1612, 935);
            this.Name = "GameView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "GameView";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.GameViewPaint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.GameViewKeyPress);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GameViewMouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GameViewMouseClick);
            this.ResumeLayout(false);

        }

        #endregion
    }
}