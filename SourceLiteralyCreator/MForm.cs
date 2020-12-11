using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.API.Native;
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

#if DEBUG
            BindCollection.Add(new LitSourceObject { SourceObject = "Hello" });
            BindCollection.Add(new LitSourceObject { SourceObject = "This" });
            BindCollection.Add(new LitSourceObject { SourceObject = "Simple" });
            BindCollection.Add(new LitSourceObject { SourceObject = "Object" });
            BindCollection.Add(new LitSourceObject { SourceObject = "Collection" });
#endif
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
            SetBookLabels();
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
            RichBuffer.ResetText();

            DocumentRange range = RichBuffer.Document.Selection;
            SubDocument doc = range.BeginUpdateDocument();

            ParagraphProperties pp = doc.BeginUpdateParagraphs(range);

            pp.Style.FontName = "Times New Roman";
            pp.Style.FontSize = 14;
            pp.Alignment = ParagraphAlignment.Justify;
            pp.SpacingAfter = 0;
            pp.SpacingBefore = 0;
            pp.FirstLineIndentType = ParagraphFirstLineIndent.Indented;
            pp.FirstLineIndent = 1.25f;
            pp.LineSpacingType = ParagraphLineSpacing.Single;
            pp.LineSpacingMultiplier = 1.5f;

            TabInfoCollection tbiColl = pp.BeginUpdateTabs(true);
            TabInfo tbi = new TabInfo
            {
                Alignment = TabAlignmentType.Left,
                Position = Units.DocumentsToCentimetersF(236.0f)
            };
            tbiColl.Add(tbi);
            pp.EndUpdateTabs(tbiColl);

            foreach (LitSourceObject item in BindCollection)
            {
                doc.AppendText(item.SourceObject + Environment.NewLine);
            }

            doc.EndUpdateParagraphs(pp);
            range.EndUpdateDocument(doc);

        }
        private void SetBookLabels()
        {
            DisplayNameHelper helper = new DisplayNameHelper();
            BookSource simpleBookSource = new BookSource();
            EthernetSource simpleEthernetSourcec = new EthernetSource();

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

            labelControl12.Text = helper.GetDisplayName(typeof(EthernetSource), nameof(simpleEthernetSourcec.SourceName));
            labelControl13.Text = helper.GetDisplayName(typeof(EthernetSource), nameof(simpleEthernetSourcec.SourceURL));
            labelControl14.Text = helper.GetDisplayName(typeof(EthernetSource), nameof(simpleEthernetSourcec.VisitDate));
        }

#endregion
#region add book

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
            simpleBookSource.PageCount = textEdit10.Text.IsNOE() ? 0 : Convert.ToInt32(textEdit10.Text);
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
            createBookPanel.HideSliding();
        }

#endregion
#region add ethernet

        private EthernetSource MapEthernetSourceFromUI()
        {
            EthernetSource simpleEthernetSource = new EthernetSource();
            simpleEthernetSource.SourceName = textEdit11.Text;
            simpleEthernetSource.SourceURL = textEdit12.Text;
            simpleEthernetSource.VisitDate = dateEdit2.DateTime.Date;
            return simpleEthernetSource;
        }
        private void MapEthernetSourceToUI(EthernetSource simpleEthernetSource)
        {
            textEdit11.Text = simpleEthernetSource.SourceName;
            textEdit12.Text = simpleEthernetSource.SourceURL;
            dateEdit2.DateTime = simpleEthernetSource.VisitDate;
        }
        private void AddEthernetSource_Click(object sender, EventArgs e)
        {
            BindCollection.Add(new LitSourceObject { SourceObject = MapEthernetSourceFromUI() });
            if (IsClearNewEthernetSource.Checked)
            {
                MapEthernetSourceToUI(new EthernetSource());
            }
            createEthernetSourcePanel.HideSliding();
        }

#endregion
    }
}
