
namespace DataRelations
{
    partial class MainForm
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
            this.BooksDataGrid = new System.Windows.Forms.DataGridView();
            this.BookPricesDataGrid = new System.Windows.Forms.DataGridView();
            this.DebugButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BooksDataGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookPricesDataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // BooksDataGrid
            // 
            this.BooksDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BooksDataGrid.Location = new System.Drawing.Point(13, 41);
            this.BooksDataGrid.Name = "BooksDataGrid";
            this.BooksDataGrid.RowHeadersWidth = 51;
            this.BooksDataGrid.Size = new System.Drawing.Size(594, 535);
            this.BooksDataGrid.TabIndex = 0;
            // 
            // BookPricesDataGrid
            // 
            this.BookPricesDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BookPricesDataGrid.Location = new System.Drawing.Point(613, 41);
            this.BookPricesDataGrid.Name = "BookPricesDataGrid";
            this.BookPricesDataGrid.RowHeadersWidth = 51;
            this.BookPricesDataGrid.Size = new System.Drawing.Size(567, 535);
            this.BookPricesDataGrid.TabIndex = 1;
            // 
            // DebugButton
            // 
            this.DebugButton.Location = new System.Drawing.Point(1105, 12);
            this.DebugButton.Name = "DebugButton";
            this.DebugButton.Size = new System.Drawing.Size(75, 23);
            this.DebugButton.TabIndex = 2;
            this.DebugButton.Text = "Debug";
            this.DebugButton.UseVisualStyleBackColor = true;
            this.DebugButton.Click += new System.EventHandler(this.DebugButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1192, 588);
            this.Controls.Add(this.DebugButton);
            this.Controls.Add(this.BookPricesDataGrid);
            this.Controls.Add(this.BooksDataGrid);
            this.Name = "MainForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.BooksDataGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BookPricesDataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView BooksDataGrid;
        private System.Windows.Forms.DataGridView BookPricesDataGrid;
        private System.Windows.Forms.Button DebugButton;
    }
}

