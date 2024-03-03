using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BusinessLogic
{
    public class BLL_CWaitCursor : BLL_CCursor
    {
        public BLL_CWaitCursor() : base(Cursors.WaitCursor) { }
    }
}
