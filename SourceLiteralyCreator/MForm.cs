using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using SourceLiteralyCreator.Const;
using SourceLiteralyCreator.Models;

namespace SourceLiteralyCreator
{
    public partial class MForm : Form
    {
        #region fields

        private int GetListIndex => SourcesList.SelectedIndex;
        private int[] GetListIndexes => SourcesList.SelectedIndices.ToArray();

        private List<LitSourceObject> listBoxSource;
        internal List<LitSourceObject> ListBoxSource
        {
            get => listBoxSource ??= new List<LitSourceObject>();
            set => listBoxSource = value;
        }

        #endregion
        #region ctor

        public MForm()
        {
            InitializeComponent();

            BindCollection.DataSource = ListBoxSource;
            BindCollection.ListChanged += BindCollectionChanged;
            hideContainerRight.MouseHover += HideContainerRightMouseHover;

            //BindCollection.Add(new LitSourceObject { SourceObject = "Hello" });
            //BindCollection.Add(new LitSourceObject { SourceObject = "This" });
            //BindCollection.Add(new LitSourceObject { SourceObject = "Simple" });
            //BindCollection.Add(new LitSourceObject { SourceObject = "Object" });
            //BindCollection.Add(new LitSourceObject { SourceObject = "Collection" });
        }
        private void BindCollectionChanged(object sender, ListChangedEventArgs e)
        {
            CountLabel.Text = $"Количество элементов: {BindCollection.Count}";
            DeleteAllButton.Enabled = BindCollection.Count != ConstIntegers.ZeroCount;
            DeleteAllContextButton.Enabled = BindCollection.Count != ConstIntegers.ZeroCount;
            SelectAllContextButton.Enabled = BindCollection.Count != ConstIntegers.ZeroCount;
            DeleteContextGroup.Enabled = DeleteSelectedContextButton.Enabled || DeleteAllContextButton.Enabled;
        }
        private void MFormLoad(object sender, EventArgs e)
        {
            SourcesList.ChangeSelect();
            ResumeLayout(true);
            SetLabels();
            ResumeLayout(false);
        }

        #endregion
        #region clicks

        private void DeleteAllButtonClick(object sender, EventArgs e) => BindCollection.Clear();

        private void RemoveSelectedButton_Click(object sender, EventArgs e)
        {
            if (GetListIndex != ConstIntegers.NullIndex)
            {
                if (GetListIndexes.Count() > 1)
                {
                    foreach (int index in GetListIndexes.Reverse())
                    {
                        BindCollection.RemoveAt(index);
                    }
                }
                else
                {
                    BindCollection.RemoveAt(GetListIndex);
                }
                SourcesList.ChangeSelect();
            }
        }

        #endregion
        #region list events

        private void SourcesListSelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteSelectedButton.Enabled = GetListIndex != ConstIntegers.NullIndex;
            DeleteSelectedContextButton.Enabled = GetListIndex != ConstIntegers.NullIndex;
            ResetSelectionContextButton.Enabled = GetListIndex != ConstIntegers.NullIndex;
        }

        private void SourcesListKeyPress(object sender, KeyPressEventArgs e)
            => SourcesList.ChangeSelect(e.KeyChar == (char)Keys.Escape ? ConstIntegers.NullIndex : GetListIndex);

        #endregion
        #region context menu

        private void SelectAllContextButton_Click(object sender, EventArgs e) => SourcesList.SelectAll();
        private void ResetSelectingButtonClick(object sender, EventArgs e) => SourcesList.ChangeSelect();
        private void DeleteAllContextButton_Click(object sender, EventArgs e) => DeleteAllButton.PerformClick();
        private void DeleteSelectedContextButton_Click(object sender, EventArgs e) => DeleteSelectedButton.PerformClick();

        #endregion

        #region buffer dock

        private void HideContainerRightMouseHover(object sender, EventArgs e)
        {
            //var par = RichBuffer.Document.Paragraphs.FirstOrDefault();
            //var parStyle = par.Style;
            //parStyle.Alignment = DevExpress.XtraRichEdit.API.Native.ParagraphAlignment.Justify;
            //parStyle.FirstLineIndent = 120;
            //parStyle.LineSpacing = 1.5f;
            //parStyle.Spacing = 15;
            //RichBuffer.WordMLText = null;

            //RichBuffer.Document.AppendText("First paragraph\r\n");
            //RichBuffer.Document.AppendText("Second paragraph\r\n");
            //RichBuffer.Document.AppendText("Third paragraph\r\n");

            var parag = RichBuffer.Document.Paragraphs.FirstOrDefault();
            parag.Alignment = DevExpress.XtraRichEdit.API.Native.ParagraphAlignment.Justify;
            parag.FirstLineIndent = 120;
            parag.LineSpacing = 1.5f;

        }

        #endregion
        #region add book
        private void SetLabels()
        {
            DisplayNameHelper helper = new DisplayNameHelper();
            BookSource simpleBookSource = new BookSource();

            labelControl1.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.FirstAuthor));
            labelControl2.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.SecondAuthor));
            labelControl3.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.MainTitle));
            labelControl4.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.SubTitle));
            labelControl5.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.AdditionalTitleInfo));
            labelControl6.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.ResponsibilityInfo));
            labelControl7.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.PublicationInfo));
            labelControl8.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.PublicationPlace));
            labelControl9.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.PublicationCompany));
            labelControl10.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.PublicationDate));
            labelControl11.Text = helper.GetDisplayName(typeof(BookSource), nameof(simpleBookSource.PageCount));
        }
        private BookSource MapBookFromUI()
        {
            BookSource simpleBookSource = new BookSource();
            simpleBookSource.FirstAuthor = textEdit1.Text;
            simpleBookSource.SecondAuthor = textEdit2.Text;
            simpleBookSource.MainTitle = textEdit3.Text;
            simpleBookSource.SubTitle = textEdit4.Text;
            simpleBookSource.AdditionalTitleInfo = textEdit5.Text;
            simpleBookSource.ResponsibilityInfo = textEdit6.Text;
            simpleBookSource.PublicationInfo = textEdit7.Text;
            simpleBookSource.PublicationPlace = textEdit8.Text;
            simpleBookSource.PublicationCompany = textEdit9.Text;
            simpleBookSource.PublicationDate = dateEdit1.DateTime;
            simpleBookSource.PageCount = Convert.ToInt32(textEdit10.Text);
            return simpleBookSource;
        }
        private void MapBookToUI(BookSource simpleBookSource)
        {
            textEdit1.Text = simpleBookSource.FirstAuthor;
            textEdit2.Text = simpleBookSource.SecondAuthor;
            textEdit3.Text = simpleBookSource.MainTitle;
            textEdit4.Text = simpleBookSource.SubTitle;
            textEdit5.Text = simpleBookSource.AdditionalTitleInfo;
            textEdit6.Text = simpleBookSource.ResponsibilityInfo;
            textEdit7.Text = simpleBookSource.PublicationInfo;
            textEdit8.Text = simpleBookSource.PublicationPlace;
            textEdit9.Text = simpleBookSource.PublicationCompany;
            dateEdit1.DateTime = simpleBookSource.PublicationDate;
            textEdit10.Text = simpleBookSource.PageCount.ToString();
        }
        private void AddBookSource_Click(object sender, EventArgs e)
        {
            BindCollection.Add(new LitSourceObject { SourceObject = MapBookFromUI() });
            if (IsClearNewBookSource.Checked)
            {
                MapBookToUI(new BookSource());
            }
        }
        #endregion
    }
}
