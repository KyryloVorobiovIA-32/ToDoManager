namespace ToDo.UI;

partial class MainForm
{
    private System.Windows.Forms.ListBox tasksListBox;
    private System.Windows.Forms.Button btnAddTask;
    private System.Windows.Forms.ComboBox cmbSortStrategy;
    private System.Windows.Forms.Label lblSort;
    private System.Windows.Forms.Button btnExport;
    // НОВІ ЕЛЕМЕНТИ ДЛЯ ЛР 8
    private System.Windows.Forms.Button btnCalcStats;
    private System.Windows.Forms.Label lblStats;
    // -------------------------------
    
    private System.ComponentModel.IContainer components = null;

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
        this.tasksListBox = new System.Windows.Forms.ListBox();
        this.btnAddTask = new System.Windows.Forms.Button();
        this.cmbSortStrategy = new System.Windows.Forms.ComboBox();
        this.lblSort = new System.Windows.Forms.Label();
        this.btnExport = new System.Windows.Forms.Button();
        this.btnCalcStats = new System.Windows.Forms.Button();
        this.lblStats = new System.Windows.Forms.Label();
        this.SuspendLayout();
        
        // lblSort
        this.lblSort.Location = new System.Drawing.Point(12, 9);
        this.lblSort.Name = "lblSort";
        this.lblSort.Size = new System.Drawing.Size(100, 23);
        this.lblSort.Text = "Сортувати за:";
        this.lblSort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        // cmbSortStrategy
        this.cmbSortStrategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.cmbSortStrategy.FormattingEnabled = true;
        this.cmbSortStrategy.Location = new System.Drawing.Point(118, 9);
        this.cmbSortStrategy.Name = "cmbSortStrategy";
        this.cmbSortStrategy.Size = new System.Drawing.Size(294, 23);
        this.cmbSortStrategy.TabIndex = 0;
        
        // tasksListBox
        this.tasksListBox.Location = new System.Drawing.Point(12, 38);
        this.tasksListBox.Name = "tasksListBox";
        this.tasksListBox.Size = new System.Drawing.Size(400, 280);
        this.tasksListBox.TabIndex = 1;
        
        // btnAddTask
        this.btnAddTask.Location = new System.Drawing.Point(12, 324);
        this.btnAddTask.Name = "btnAddTask";
        this.btnAddTask.Size = new System.Drawing.Size(196, 30);
        this.btnAddTask.TabIndex = 2;
        this.btnAddTask.Text = "Додати завдання";
        this.btnAddTask.UseVisualStyleBackColor = true;
        this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click); 
        
        // btnExport
        this.btnExport.Location = new System.Drawing.Point(216, 324);
        this.btnExport.Name = "btnExport";
        this.btnExport.Size = new System.Drawing.Size(196, 30);
        this.btnExport.TabIndex = 3;
        this.btnExport.Text = "Експорт у файл";
        this.btnExport.UseVisualStyleBackColor = true;
        this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
        
        // btnCalcStats (ЛР 8: Нова кнопка знизу)
        this.btnCalcStats.Location = new System.Drawing.Point(12, 360);
        this.btnCalcStats.Name = "btnCalcStats";
        this.btnCalcStats.Size = new System.Drawing.Size(196, 30);
        this.btnCalcStats.TabIndex = 4;
        this.btnCalcStats.Text = "Оцінити Проєкт";
        this.btnCalcStats.UseVisualStyleBackColor = true;
        this.btnCalcStats.Click += new System.EventHandler(this.btnCalcStats_Click);

        // lblStats (ЛР 8: Лейбл для результату)
        this.lblStats.Location = new System.Drawing.Point(220, 360);
        this.lblStats.Name = "lblStats";
        this.lblStats.Size = new System.Drawing.Size(190, 30);
        this.lblStats.Text = "Загальна оцінка: 0";
        this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        
        // MainForm
        this.ClientSize = new System.Drawing.Size(424, 400);
        this.Controls.Add(this.lblStats);
        this.Controls.Add(this.btnCalcStats);
        this.Controls.Add(this.btnExport);
        this.Controls.Add(this.lblSort);
        this.Controls.Add(this.cmbSortStrategy);
        this.Controls.Add(this.btnAddTask);
        this.Controls.Add(this.tasksListBox);
        this.Text = "Менеджер Завдань (ToDo)";
        this.ResumeLayout(false);
    }

    #endregion
}