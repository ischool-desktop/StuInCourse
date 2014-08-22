using FISCA.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuInCourse
{
    public class Program
    {
        [FISCA.MainMethod]
        public static void Main()
        {
            FISCA.Presentation.RibbonBarItem item1 = FISCA.Presentation.MotherForm.RibbonBarItems["課程", "資料統計"];
            item1["報表"].Image = Properties.Resources.Report;
            item1["報表"].Size = FISCA.Presentation.RibbonBarButton.MenuButtonSize.Large;
			item1["報表"]["學生修課清單"].Enable = false;
            item1["報表"]["學生修課清單"].Click += delegate
            {
                frm_printsetup form = new frm_printsetup(K12.Presentation.NLDPanels.Class.SelectedSource);
                form.ShowDialog();
            };

			K12.Presentation.NLDPanels.Course.SelectedSourceChanged += delegate
			{
				if (K12.Presentation.NLDPanels.Course.SelectedSource.Count > 0 && Permissions.學生修課清單權限)
				{
					item1["報表"]["學生修課清單"].Enable = true;
				}
				else
					item1["報表"]["學生修課清單"].Enable = false;
			};

            //權限設定
            Catalog permission = RoleAclSource.Instance["班級"]["功能按鈕"];
            permission.Add(new RibbonFeature(Permissions.學生修課清單, "學生修課清單"));
        }
    }
}
