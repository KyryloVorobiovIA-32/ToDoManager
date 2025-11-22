using System.ComponentModel;

namespace ToDo.UI;

partial class AddTaskForm
{
    private System.Windows.Forms.TextBox txtTaskTitle;
    private System.Windows.Forms.ComboBox cmbPriority;
    private System.Windows.Forms.NumericUpDown numPoints; // НОВИЙ КОНТРОЛ
    private System.Windows.Forms.Label lblPoints; // Підпис до нього
    private System.Windows.Forms.Button btnSave;
    private System.Windows.Forms.Button btnCancel;

    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    private void InitializeComponent()
    {
        this.txtTaskTitle = new System.Windows.Forms.TextBox();
        this.cmbPriority = new System.Windows.Forms.ComboBox();
        this.numPoints = new System.Windows.Forms.NumericUpDown();
        this.lblPoints = new System.Windows.Forms.Label();
        this.btnSave = new System.Windows.Forms.Button();
        this.btnCancel = new System.Windows.Forms.Button();
        ((System.ComponentModel.ISupportInitialize)(this.numPoints)).BeginInit();
        this.SuspendLayout();
        
        // txtTaskTitle
        this.txtTaskTitle.Location = new System.Drawing.Point(12, 35);
        this.txtTaskTitle.Name = "txtTaskTitle";
        this.txtTaskTitle.PlaceholderText = "Назва завдання";
        this.txtTaskTitle.Size = new System.Drawing.Size(260, 23);
        this.txtTaskTitle.TabIndex = 0;
        
        // cmbPriority
        this.cmbPriority.FormattingEnabled = true;
        this.cmbPriority.Location = new System.Drawing.Point(12, 70);
        this.cmbPriority.Name = "cmbPriority";
        this.cmbPriority.Size = new System.Drawing.Size(260, 23);
        this.cmbPriority.TabIndex = 1;
        this.cmbPriority.Text = "Виберіть пріоритет";

        // lblPoints
        this.lblPoints.Location = new System.Drawing.Point(12, 105);
        this.lblPoints.Name = "lblPoints";
        this.lblPoints.Size = new System.Drawing.Size(100, 23);
        this.lblPoints.Text = "Оцінка (бали):";
        
        // numPoints
        this.numPoints.Location = new System.Drawing.Point(118, 103);
        this.numPoints.Name = "numPoints";
        this.numPoints.Size = new System.Drawing.Size(154, 23);
        this.numPoints.TabIndex = 2;
        
        // btnSave
        this.btnSave.Location = new System.Drawing.Point(12, 140);
        this.btnSave.Name = "btnSave";
        this.btnSave.Size = new System.Drawing.Size(120, 30);
        this.btnSave.TabIndex = 3;
        this.btnSave.Text = "Зберегти";
        this.btnSave.UseVisualStyleBackColor = true;
        this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
        
        // btnCancel
        this.btnCancel.Location = new System.Drawing.Point(152, 140);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new System.Drawing.Size(120, 30);
        this.btnCancel.TabIndex = 4;
        this.btnCancel.Text = "Скасувати";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
        
        // AddTaskForm
        this.ClientSize = new System.Drawing.Size(284, 190);
        this.Controls.Add(this.lblPoints);
        this.Controls.Add(this.numPoints);
        this.Controls.Add(this.btnCancel);
        this.Controls.Add(this.btnSave);
        this.Controls.Add(this.cmbPriority);
        this.Controls.Add(this.txtTaskTitle);
        this.Name = "AddTaskForm";
        this.Text = "Додати Завдання";
        ((System.ComponentModel.ISupportInitialize)(this.numPoints)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion
}