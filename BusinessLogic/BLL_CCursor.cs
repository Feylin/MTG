using System;
using System.Windows.Forms;

namespace BusinessLogic
{
    public class BLL_CCursor : IDisposable
    {
        private readonly Cursor _saved;

        public BLL_CCursor(Cursor newCursor)
        {
            _saved = Cursor.Current;

            Cursor.Current = newCursor;
        }

        public void Dispose()
        {
            Cursor.Current = _saved;
        }
    }
}
