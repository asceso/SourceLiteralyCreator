using DevExpress.XtraEditors;
using SourceLiteralyCreator.Const;

namespace SourceLiteralyCreator
{
    internal static class ListBoxExtension
    {
        /// <summary>
        /// Установить выделение
        /// </summary>
        /// <param name="box">ListBox</param>
        /// <param name="selectedIndex">Индекс</param>
        internal static void ChangeSelect(this ListBoxControl box, int selectedIndex = ConstIntegers.NullIndex) => box.SelectedIndex = selectedIndex;
    }
}
