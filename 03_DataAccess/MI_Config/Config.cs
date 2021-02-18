using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_DataAccess.MI_Config
{
    public class Config
    {
        public static string connectionString { get; set; } = @"data source=DESKTOP-M6O799J\SQLEXPRESS;initial catalog=MACIN;user id=sa;password=sa;multipleactiveresultsets=true;";
    }
}
