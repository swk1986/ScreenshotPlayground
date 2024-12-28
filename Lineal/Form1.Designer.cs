namespace Lineal;

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
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.ClientRectLabel = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.SuspendLayout();
        // 
        // ClientRectLabel
        // 
        this.ClientRectLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
        this.ClientRectLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
        this.ClientRectLabel.Location = new System.Drawing.Point(145, 85);
        this.ClientRectLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.ClientRectLabel.Name = "ClientRectLabel";
        this.ClientRectLabel.Size = new System.Drawing.Size(914, 508);
        this.ClientRectLabel.TabIndex = 5;
        this.ClientRectLabel.Text = "label1";
        this.ClientRectLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        // 
        // label1
        // 
        this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        this.label1.Dock = System.Windows.Forms.DockStyle.Left;
        this.label1.Location = new System.Drawing.Point(0, 0);
        this.label1.Name = "label1";
        this.label1.Size = new System.Drawing.Size(138, 692);
        this.label1.TabIndex = 1;
        this.label1.Text = "label1";
        // 
        // label2
        // 
        this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
        this.label2.Dock = System.Windows.Forms.DockStyle.Right;
        this.label2.Location = new System.Drawing.Point(1066, 0);
        this.label2.Name = "label2";
        this.label2.Size = new System.Drawing.Size(134, 692);
        this.label2.TabIndex = 2;
        this.label2.Text = "label2";
        // 
        // label3
        // 
        this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
        this.label3.Dock = System.Windows.Forms.DockStyle.Top;
        this.label3.Location = new System.Drawing.Point(138, 0);
        this.label3.Name = "label3";
        this.label3.Size = new System.Drawing.Size(928, 75);
        this.label3.TabIndex = 3;
        this.label3.Text = "label3";
        // 
        // label4
        // 
        this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
        this.label4.Dock = System.Windows.Forms.DockStyle.Bottom;
        this.label4.Location = new System.Drawing.Point(138, 606);
        this.label4.Name = "label4";
        this.label4.Size = new System.Drawing.Size(928, 86);
        this.label4.TabIndex = 4;
        this.label4.Text = "label4";
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1200, 692);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.ClientRectLabel);
        this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
        this.Name = "Form1";
        this.Text = "Form1";
        this.ResumeLayout(false);
    }

    private System.Windows.Forms.Label label4;

    private System.Windows.Forms.Label label3;

    private System.Windows.Forms.Label label2;

    private System.Windows.Forms.Label label1;

    private System.Windows.Forms.Label ClientRectLabel;

    #endregion
}