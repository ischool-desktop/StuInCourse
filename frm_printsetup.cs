using Aspose.Cells;
using FISCA.Presentation.Controls;
using K12.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JHSchool.Data;
using FISCA.Data;
namespace StuInCourse
{
    public partial class frm_printsetup : BaseForm
    {
        private List<string> _list;
        private int ColumnNo = 0;

        public frm_printsetup(List<string> list)
        {
            InitializeComponent();
            this._list = list;
            checkBoxOne.Checked = true;
            checkBoxTwelve.Checked = false;
            checkBoxOne.CheckedChanged += checkBoxOne_CheckedChanged;
            checkBoxTwelve.CheckedChanged += checkBoxTwelve_CheckedChanged;
        }
        void checkBoxOne_CheckedChanged(object sender, EventArgs e)
        {
            if ( checkBoxOne.Checked == true )
                checkBoxTwelve.Checked = false; 

        }
        void checkBoxTwelve_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxTwelve.Checked == true)
                checkBoxOne.Checked = false; 
        }

        private string SelectTime() //取得Server的時間
        {
            QueryHelper Sql = new QueryHelper();
            DataTable dtable = Sql.Select("select now()"); //取得時間
            DateTime dt = DateTime.Now;
            DateTime.TryParse("" + dtable.Rows[0][0], out dt); //Parse資料
            string ComputerSendTime = dt.ToString("yyyy/MM/dd"); //最後時間

            return ComputerSendTime;
        }


        private void btn_Yes_Click(object sender, EventArgs e)
        {
            if (checkBoxOne.Checked == true)
            {
                ColumnNo = 1;
            }
            else if (checkBoxTwelve.Checked == true)
            {
                ColumnNo = 12;
            }
            /*
            //GetData
            List<SCAttendRecord> sc_attends = K12.Data.SCAttend.SelectByCourseIDs(_list);

            Dictionary<string, List<string>> course_students = new Dictionary<string, List<string>>();
            foreach(SCAttendRecord record in sc_attends)
            {
                if (!course_students.ContainsKey(record.ID))
                {
                    string ref_course = record.RefCourseID;
                    string ref_student = record.RefStudentID;
                }
            }
            // List<StudentRecord> students = K12.Data.Student.SelectByIDs();
            */
            Workbook wb = new Workbook();
            wb.Open(new MemoryStream(Properties.Resources.Template));
            Worksheet template_sheet = wb.Worksheets["Template"];
            

            //設定Style樣板：四邊框線 水平字左 垂直字中 
            Style s = wb.Styles[wb.Styles.Add()];
            s.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            s.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            s.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            s.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            s.HorizontalAlignment = TextAlignmentType.Left;
            s.VerticalAlignment = TextAlignmentType.Center;

            //設定Style樣板：四邊框線 水平字中 垂直字頂 字底線
            Style s2 = wb.Styles[wb.Styles.Add()];
            s2.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            s2.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            s2.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            s2.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            s2.Font.Underline = FontUnderlineType.Single;
            s2.HorizontalAlignment = TextAlignmentType.Center;
            s2.VerticalAlignment = TextAlignmentType.Top;

            Style s3 = wb.Styles[wb.Styles.Add()];
            s3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
            s3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
            s3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
            s3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
            s3.HorizontalAlignment = TextAlignmentType.Center;
            s3.VerticalAlignment = TextAlignmentType.Center;


            List<CourseRecord> _CourseList = Course.SelectByIDs(K12.Presentation.NLDPanels.Course.SelectedSource);
            _CourseList.Sort();

            int sheet_index = 1;
            int row_number = 0; //列編號
            int column_number; //行編號
            int left = 1; //1
            int top = 4;  //4
            foreach (CourseRecord cr in _CourseList)
            {

                IEnumerable<SCAttendRecord> scr = SCAttend.SelectByCourseIDs(new string[] { cr.ID });
                IEnumerable<string> studentids = from screc in scr select screc.RefStudentID;
                // 取得一般生
                List<StudentRecord> students = Student.SelectByIDs(studentids).Where(x => x.Status == StudentRecord.StudentStatus.一般).ToList();


                row_number = 1;
                sheet_index = wb.Worksheets.AddCopy("Template");
                wb.Worksheets[sheet_index].Name = cr.Name ;
                wb.Worksheets[sheet_index].Cells[0, 0].PutValue("國立科學工業園區實驗高級中學雙語部");
                wb.Worksheets[sheet_index].Cells[1, 0].PutValue(cr.Class.Name +"班  " + cr.SchoolYear + " 學年度第" + cr.Semester + "學期學生名單");
                wb.Worksheets[sheet_index].Cells[2, 11].PutValue("Report Print：" + SelectTime());

                wb.Worksheets[sheet_index].Cells[3, 0].Style = s3;
                wb.Worksheets[sheet_index].Cells[3, 1].Style = s3;

                wb.Worksheets[sheet_index].Cells[3, 0].PutValue("No");
                wb.Worksheets[sheet_index].Cells[3, 1].PutValue("Name");

                int indexSub = 0;
                foreach (StudentRecord student in students)
                {
                    wb.Worksheets[sheet_index].Cells[top + indexSub, 0].PutValue(row_number);
                    wb.Worksheets[sheet_index].Cells[top + indexSub, 1].PutValue(student.Name + " " + student.EnglishName);
                    indexSub++;
                    row_number++;
                }
                    for (column_number = 1; column_number <= ColumnNo; column_number++)
                    {
                        wb.Worksheets[sheet_index].Cells[3, column_number +left ].Style = s2;
                        wb.Worksheets[sheet_index].Cells[3, column_number +left].PutValue(column_number); 
                    }
                wb.Worksheets[sheet_index].Cells.Merge(0, 0, 1, ColumnNo + 2); 
                wb.Worksheets[sheet_index].Cells.Merge(1, 0, 1, ColumnNo + 2);

                if (row_number > 1)
                {
                 Range allstyle = wb.Worksheets[sheet_index].Cells.CreateRange(4, 0, row_number-1, column_number + left);
                 allstyle.Style = s;
                }
               
                /*StyleFlag sf = new StyleFlag();
                sf.Borders = true;
                allstyle.ApplyStyle(s, sf);*/
            }

            wb.Worksheets.RemoveAt(0);
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "另存新檔";
            save.FileName = "StudentInCourse.xls";
            save.Filter = "Excel檔案 (*.xls)|*.xls|所有檔案 (*.*)|*.*";
            if (save.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    wb.Save(save.FileName, Aspose.Cells.FileFormatType.Excel2003);
                    System.Diagnostics.Process.Start(save.FileName);
                }
                catch
                {
                    MessageBox.Show("檔案儲存失敗");
                }
            }
        }

        private void btn_No_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
