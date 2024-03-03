using System.Windows.Forms;

namespace CardsApp
{
    public partial class LoadingBox : Form
    {
        public LoadingBox()
        {
            InitializeComponent();

            ControlBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
        }
    }
}
