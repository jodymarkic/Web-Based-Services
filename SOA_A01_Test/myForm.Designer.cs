namespace SOA_A01_Test
{
    partial class myForm
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
            this.ServicesCombobox = new System.Windows.Forms.ComboBox();
            this.MethodCombobox = new System.Windows.Forms.ComboBox();
            this.ParametersDataGridView = new System.Windows.Forms.DataGridView();
            this.requestButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ParametersDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // ServicesCombobox
            // 
            this.ServicesCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServicesCombobox.FormattingEnabled = true;
            this.ServicesCombobox.Location = new System.Drawing.Point(16, 15);
            this.ServicesCombobox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ServicesCombobox.Name = "ServicesCombobox";
            this.ServicesCombobox.Size = new System.Drawing.Size(356, 47);
            this.ServicesCombobox.TabIndex = 0;
            this.ServicesCombobox.Text = "Select a service...";
            this.ServicesCombobox.SelectedIndexChanged += new System.EventHandler(this.ServicesCombobox_SelectedIndexChanged);
            // 
            // MethodCombobox
            // 
            this.MethodCombobox.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MethodCombobox.FormattingEnabled = true;
            this.MethodCombobox.Location = new System.Drawing.Point(381, 15);
            this.MethodCombobox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MethodCombobox.Name = "MethodCombobox";
            this.MethodCombobox.Size = new System.Drawing.Size(639, 47);
            this.MethodCombobox.TabIndex = 1;
            this.MethodCombobox.Text = "Select a method...";
            this.MethodCombobox.SelectedIndexChanged += new System.EventHandler(this.MethodCombobox_SelectedIndexChanged);
            // 
            // ParametersDataGridView
            // 
            this.ParametersDataGridView.AllowUserToAddRows = false;
            this.ParametersDataGridView.AllowUserToDeleteRows = false;
            this.ParametersDataGridView.AllowUserToResizeColumns = false;
            this.ParametersDataGridView.AllowUserToResizeRows = false;
            this.ParametersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ParametersDataGridView.Location = new System.Drawing.Point(16, 70);
            this.ParametersDataGridView.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ParametersDataGridView.MultiSelect = false;
            this.ParametersDataGridView.Name = "ParametersDataGridView";
            this.ParametersDataGridView.Size = new System.Drawing.Size(541, 539);
            this.ParametersDataGridView.StandardTab = true;
            this.ParametersDataGridView.TabIndex = 2;
            // 
            // requestButton
            // 
            this.requestButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.requestButton.Location = new System.Drawing.Point(565, 481);
            this.requestButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.requestButton.Name = "requestButton";
            this.requestButton.Size = new System.Drawing.Size(456, 128);
            this.requestButton.TabIndex = 3;
            this.requestButton.Text = "SUBMIT REQUEST";
            this.requestButton.UseVisualStyleBackColor = true;
            this.requestButton.Click += new System.EventHandler(this.requestButton_Click);
            // 
            // myForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1037, 624);
            this.Controls.Add(this.requestButton);
            this.Controls.Add(this.ParametersDataGridView);
            this.Controls.Add(this.MethodCombobox);
            this.Controls.Add(this.ServicesCombobox);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "myForm";
            this.Text = "NAD A02 - Web Services";
            ((System.ComponentModel.ISupportInitialize)(this.ParametersDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox ServicesCombobox;
        private System.Windows.Forms.ComboBox MethodCombobox;
        private System.Windows.Forms.DataGridView ParametersDataGridView;
        private System.Windows.Forms.Button requestButton;
    }
}

