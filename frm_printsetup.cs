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
        private List<string> list;
        private int columnNo = 0;

        public frm_printsetup(List<string> list)
        {
            InitializeComponent();
            this.list = list;
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
                columnNo = 1;
            }
            else if (checkBoxTwelve.Checked == true)
            {
                columnNo = 12;
            }
            Workbook wb = new Workbook();
            wb.Open(new MemoryStream(Properties.Resources.Template));
            Worksheet templateSheet = wb.Worksheets["Template"];

			#region 設定Style樣板
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

			//設定Style樣板：四邊框線 水平字中 垂直字中
			Style s3 = wb.Styles[wb.Styles.Add()];
			s3.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;
			s3.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;
			s3.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;
			s3.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;
			s3.HorizontalAlignment = TextAlignmentType.Center;
			s3.VerticalAlignment = TextAlignmentType.Center;
			#endregion
            
            List<CourseRecord> courseList = Course.SelectByIDs(K12.Presentation.NLDPanels.Course.SelectedSource);
			string schoolName = K12.Data.School.ChineseName;
            int sheetIndex = 1;
            int rowNumber = 0; //列編號
            int columnNumber; //行編號
            int left = 2; 
            int top = 4;  
            foreach (CourseRecord cr in courseList)
            {
                IEnumerable<SCAttendRecord> scr = SCAttend.SelectByCourseIDs(new string[] { cr.ID });
                IEnumerable<string> studentIds = from screc in scr select screc.RefStudentID;
                // 取得一般生
                List<StudentRecord> students = Student.SelectByIDs(studentIds).Where(x => x.Status == StudentRecord.StudentStatus.一般).ToList();
				
				// 取得所有班級
				List<ClassRecord> classList = K12.Data.Class.SelectAll();
				Dictionary<string, ClassRecord> classIdToRecord = new Dictionary<string, ClassRecord>();

				//取得班級名稱
				foreach (ClassRecord classr in classList)
				{
					if (!classIdToRecord.ContainsKey(classr.ID))
					{
						classIdToRecord.Add(classr.ID, classr);
					}
				}

				//將取回來的值(ClassRecord & StudentRecord)放入ObjList
				List<StuObj> ObjList = new List<StuObj>();
				foreach (StudentRecord sturecord in students)
				{
					StuObj obj = new StuObj(sturecord);
					obj.cr = classIdToRecord.ContainsKey(sturecord.RefClassID) ? classIdToRecord[sturecord.RefClassID] : new ClassRecord();
					ObjList.Add(obj);
				}

				//定義排序規則
				ObjList.Sort(delegate(StuObj x, StuObj y)
				{
					string xx = (x.cr.GradeYear + "").PadLeft(3, '0');
					xx += x.cr.DisplayOrder.PadLeft(5, '0');
					xx += x.cr.Name.PadLeft(20, '0');
					string seatNoX = x.sr.SeatNo + "";
					xx += seatNoX.PadLeft(20, '0');
					xx += x.sr.StudentNumber.PadLeft(20, '0');
						

					string yy = (y.cr.GradeYear + "").PadLeft(3, '0');
					yy += y.cr.DisplayOrder.PadLeft(5, '0');
					yy += y.cr.Name.PadLeft(20, '0');
					string seatNoY = y.sr.SeatNo + "";
					yy += seatNoY.PadLeft(20, '0');
					yy += y.sr.StudentNumber.PadLeft(20, '0');

					return xx.CompareTo(yy);
				});

                rowNumber = 1;
                sheetIndex = wb.Worksheets.AddCopy("Template");
                wb.Worksheets[sheetIndex].Name = cr.Name ;
				wb.Worksheets[sheetIndex].Cells[0, 0].PutValue(schoolName);
				wb.Worksheets[sheetIndex].Cells[1, 0].PutValue((cr.SchoolYear + 1911) + "~" + (cr.SchoolYear + 1912) + " 學年度第" + cr.Semester + "學期學生名單");
                wb.Worksheets[sheetIndex].Cells[2, 12].PutValue("Report Print：" + SelectTime());

                wb.Worksheets[sheetIndex].Cells[3, 0].Style = s3;
                wb.Worksheets[sheetIndex].Cells[3, 1].Style = s3;
				wb.Worksheets[sheetIndex].Cells[3, 2].Style = s3;

                wb.Worksheets[sheetIndex].Cells[3, 0].PutValue("No");
				wb.Worksheets[sheetIndex].Cells[3, 1].PutValue("Class");
                wb.Worksheets[sheetIndex].Cells[3, 2].PutValue("Name");

                int indexSub = 0;
                foreach (StuObj so in ObjList)
                {
                    wb.Worksheets[sheetIndex].Cells[top + indexSub, 0].PutValue(rowNumber);
                    wb.Worksheets[sheetIndex].Cells[top + indexSub, 1].PutValue(so.sr.Class.Name);
					wb.Worksheets[sheetIndex].Cells[top + indexSub, 2].PutValue(so.sr.Name + " " + so.sr.EnglishName);
                    indexSub++;
                    rowNumber++;
                }
				for (columnNumber = 1; columnNumber <= columnNo; columnNumber++)
				{
					wb.Worksheets[sheetIndex].Cells[3, columnNumber + left].Style = s2;
					wb.Worksheets[sheetIndex].Cells[3, columnNumber + left].PutValue(columnNumber);
				}
                wb.Worksheets[sheetIndex].Cells.Merge(0, 0, 1, columnNo + 2); 
                wb.Worksheets[sheetIndex].Cells.Merge(1, 0, 1, columnNo + 2);

                if (rowNumber > 1)
                {
                 Range allstyle = wb.Worksheets[sheetIndex].Cells.CreateRange(4, 0, rowNumber-1, columnNumber + left);
                 allstyle.Style = s;
                }
            }

            wb.Worksheets.RemoveAt(0);
            SaveFileDialog save = new SaveFileDialog();
            save.Title = "另存新檔";
			save.FileName = "學生修課清單.xls";
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
