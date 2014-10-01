using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StuInCourse
{
    class Permissions
    {
		public static bool 修課學生清單權限
        {
            get
            {
				return FISCA.Permission.UserAcl.Current[修課學生清單].Executable;
            }
        }

		public static string 修課學生清單 = "StuInCourse-{F9D339FE-63EA-47FB-A786-7DC975C5E44D}";
    }
}
