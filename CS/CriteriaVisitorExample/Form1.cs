using System;
using System.Data;
using System.Windows.Forms;

namespace CriteriaVisitorExample {
    public partial class Form1 :Form {
        public Form1() {
            InitializeComponent();
            CreateData();
        }

        void CreateData() {
            DataTable data = new DataTable();
            data.Columns.Add("ColumnA");
            data.Columns.Add("ColumnB", typeof(int));
            data.Columns.Add("ColumnC", typeof(DateTime));
            filterControl1.SourceControl = data;
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            labelControl1.Text = CustomCriteriaToStringParser.Process(filterControl1.FilterCriteria);
        }
    }
}