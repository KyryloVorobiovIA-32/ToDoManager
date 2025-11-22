using System;
using System.Windows.Forms;
using ToDo.Enums;
using ToDo.Models; 

namespace ToDo.UI
{
    public partial class AddTaskForm : Form
    {
        public string TaskTitle { get; private set; }
        public PriorityEnum TaskPriority { get; private set; }
        
        // Нова властивість для балів
        public double TaskEstimatedPoints { get; private set; }
        
        public int ProjectId { get; private set; }

        public AddTaskForm(int projectId)
        {
            InitializeComponent();
            ProjectId = projectId;
            cmbPriority.DataSource = Enum.GetValues(typeof(PriorityEnum));
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTaskTitle.Text))
            {
                MessageBox.Show("Назва завдання не може бути порожньою.");
                return;
            }

            TaskTitle = txtTaskTitle.Text;
            TaskPriority = (PriorityEnum)cmbPriority.SelectedItem!;
            
            // Зчитуємо значення з NumericUpDown
            TaskEstimatedPoints = (double)numPoints.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}