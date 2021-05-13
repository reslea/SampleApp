
namespace Di.Forms
{
    partial class MainForm
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
            this.BooksDataGridView = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.BooksDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // BooksDataGridView
            // 
            this.BooksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BooksDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BooksDataGridView.Location = new System.Drawing.Point(0, 0);
            this.BooksDataGridView.Name = "BooksDataGridView";
            this.BooksDataGridView.RowHeadersWidth = 51;
            this.BooksDataGridView.RowTemplate.Height = 29;
            this.BooksDataGridView.Size = new System.Drawing.Size(800, 450);
            this.BooksDataGridView.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.BooksDataGridView);
            this.Name = "MainForm";
            this.Text = "Main form";
            ((System.ComponentModel.ISupportInitialize)(this.BooksDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView BooksDataGridView;
    }
}

