namespace ToDo.UI;

partial class MainForm
{
    private System.Windows.Forms.ListBox tasksListBox;
    private System.Windows.Forms.Button btnAddTask;
    private System.Windows.Forms.ComboBox cmbSortStrategy;
    private System.Windows.Forms.Label lblSort;
    private System.Windows.Forms.Button btnExport;
    private System.Windows.Forms.Button btnCalcStats;
    private System.Windows.Forms.Label lblStats;
    private System.Windows.Forms.Button btnStart;
    private System.Windows.Forms.Button btnComplete;
    
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
        this.btnStart = new System.Windows.Forms.Button();
        this.btnComplete = new System.Windows.Forms.Button();
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
        this.cmbSortStrategy.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
        
        // tasksListBox
        this.tasksListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed; 
        this.tasksListBox.Location = new System.Drawing.Point(12, 38);
        this.tasksListBox.Name = "tasksListBox";
        this.tasksListBox.Size = new System.Drawing.Size(400, 230);
        this.tasksListBox.TabIndex = 1;
        this.tasksListBox.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
        this.tasksListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tasksListBox_DrawItem); 
        
        // btnStart
        this.btnStart.Location = new System.Drawing.Point(12, 280);
        this.btnStart.Name = "btnStart";
        this.btnStart.Size = new System.Drawing.Size(196, 30);
        this.btnStart.TabIndex = 5;
        this.btnStart.Text = "Взяти в роботу";
        this.btnStart.UseVisualStyleBackColor = true;
        this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
        this.btnStart.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);

        // btnComplete
        this.btnComplete.Location = new System.Drawing.Point(216, 280);
        this.btnComplete.Name = "btnComplete";
        this.btnComplete.Size = new System.Drawing.Size(196, 30);
        this.btnComplete.TabIndex = 6;
        this.btnComplete.Text = "Завершити";
        this.btnComplete.UseVisualStyleBackColor = true;
        this.btnComplete.Click += new System.EventHandler(this.btnComplete_Click);
        this.btnComplete.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);

        // btnAddTask
        this.btnAddTask.Location = new System.Drawing.Point(12, 320);
        this.btnAddTask.Name = "btnAddTask";
        this.btnAddTask.Size = new System.Drawing.Size(196, 30);
        this.btnAddTask.TabIndex = 2;
        this.btnAddTask.Text = "Додати завдання";
        this.btnAddTask.UseVisualStyleBackColor = true;
        this.btnAddTask.Click += new System.EventHandler(this.btnAddTask_Click); 
        this.btnAddTask.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
        
        // btnExport
        this.btnExport.Location = new System.Drawing.Point(216, 320);
        this.btnExport.Name = "btnExport";
        this.btnExport.Size = new System.Drawing.Size(196, 30);
        this.btnExport.TabIndex = 3;
        this.btnExport.Text = "Експорт у файл";
        this.btnExport.UseVisualStyleBackColor = true;
        this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
        this.btnExport.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
        
        // btnCalcStats
        this.btnCalcStats.Location = new System.Drawing.Point(12, 360);
        this.btnCalcStats.Name = "btnCalcStats";
        this.btnCalcStats.Size = new System.Drawing.Size(196, 30);
        this.btnCalcStats.TabIndex = 4;
        this.btnCalcStats.Text = "Оцінити Проєкт";
        this.btnCalcStats.UseVisualStyleBackColor = true;
        this.btnCalcStats.Click += new System.EventHandler(this.btnCalcStats_Click);
        this.btnCalcStats.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);

        // lblStats
        this.lblStats.Location = new System.Drawing.Point(220, 360);
        this.lblStats.Name = "lblStats";
        this.lblStats.Size = new System.Drawing.Size(190, 30);
        this.lblStats.Text = "Загальна оцінка: 0";
        this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
        this.lblStats.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
        
        // MainForm
        this.ClientSize = new System.Drawing.Size(424, 410);
        this.Controls.Add(this.btnStart);
        this.Controls.Add(this.btnComplete);
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