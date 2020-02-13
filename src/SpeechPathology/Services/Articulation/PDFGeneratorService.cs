using MigraDocCore.DocumentObjectModel;
using MigraDocCore.DocumentObjectModel.Tables;
using MigraDocCore.Rendering;
using PdfSharpCore.Fonts;
using SpeechPathology.Infrastructure.PDF;
using SpeechPathology.Models;
using SpeechPathology.Types;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SpeechPathology.Services.Articulation
{
    public class PDFGeneratorService : IPDFGeneratorService
    {
        private Document _document;
        private Section _section;

        public async Task<string> GeneratePDFForPositionTestResultsAsync(ArticulationTestExam articulationTestExam)
        {
            return await Task.Run(() => GeneratePDFForPositionTestResults(articulationTestExam));
        }

        public string GeneratePDFForPositionTestResults(ArticulationTestExam articulationTestExam)
        {
            // Set FontResolver
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new FontResolver();
            }

            // Set page format
            _document = new Document();
            _section = _document.Sections.AddSection();
            _section.PageSetup.PageFormat = PageFormat.A4;
            _section.PageSetup.Orientation = Orientation.Landscape;

            InitStyles();

            // Header

            // Logo
            //Image image = _section.Headers.Primary.AddImage(ImageSource.FromFile("logo_notext.png"));
            //image.Height = "2.5cm";
            //image.LockAspectRatio = true;
            //image.RelativeVertical = RelativeVertical.Line;
            //image.RelativeHorizontal = RelativeHorizontal.Margin;
            //image.Top = ShapePosition.Top;
            //image.Left = ShapePosition.Right;
            //image.WrapFormat.Style = WrapStyle.Through;

            // Render Results
            // Header title
            var par = _section.Headers.Primary.AddParagraph(Resources.AppResources.ProjectTitle);
            par.Format.Font.Size = 20;
            par.Format.Font.Bold = true;
            par.Format.Alignment = ParagraphAlignment.Center;
            par.Format.SpaceAfter = "1cm";


            // Title
            par = _section.AddParagraph(Resources.AppResources.PositionTestResults);
            par.Format.Font.Size = 16;
            par.Format.Font.Bold = true;
            par.Format.Alignment = ParagraphAlignment.Center;
            par.Format.SpaceAfter = "1cm";

            // Main content table
            Table table = _section.AddTable();
            table.Borders.Visible = true;
            // Create table columns
            var col = table.AddColumn();
            for (int i = 0; i <= articulationTestExam.Answers.Count; i++)
            {
                col = table.AddColumn();
                if (col.Index == 0)
                {
                    col.Width = 100;
                }
                col.Width = 20;
            }
            col.Width = 140;

            // Header row 1
            var row = table.AddRow();
            row.Height = 24;
            row.VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].Borders.Top.Visible = false;
            row.Cells[0].Borders.Left.Visible = false;
            row.Cells[0].Borders.Bottom.Visible = false;
            par = row.Cells[1].AddParagraph(Resources.AppResources.Letters);
            par.Format.Font.Bold = true;
            par.Format.Alignment = ParagraphAlignment.Center;
            row.Cells[1].MergeRight = articulationTestExam.Answers.Count - 1;
            row.Cells[table.Columns.Count - 1].Borders.Top.Visible = false;
            row.Cells[table.Columns.Count - 1].Borders.Right.Visible = false;

            // Header row 2
            row = table.AddRow();
            row.Height = 24;
            row.VerticalAlignment = VerticalAlignment.Center;
            row.Cells[0].Borders.Top.Visible = false;
            row.Cells[0].Borders.Left.Visible = false;
            int iCount = 1;
            foreach (ArticulationTestExamAnswer answer in articulationTestExam.Answers) {
                par = row.Cells[iCount].AddParagraph(answer.ArticulationTest.Sound);
                par.Format.Font.Bold = true;
                par.Format.Alignment = ParagraphAlignment.Center;
                iCount++;
            }
            par = row.Cells[iCount].AddParagraph(Resources.AppResources.Score);
            par.Format.Font.Bold = true;
            // Answers row
            row = table.AddRow();
            row.Height = 20;
            // Location column
            string soundPosition = articulationTestExam.SoundPosition != "" ? articulationTestExam.SoundPosition : 
                Resources.AppResources.All.ToUpper();
            row.Cells[0].AddParagraph(soundPosition);
            iCount = 1;
            foreach (ArticulationTestExamAnswer answer in articulationTestExam.Answers)
            {
                if (answer.IsCorrect.HasValue)
                {
                    string answerText = answer.IsCorrect.Value ? Resources.AppResources.CorrectAbbreviation : 
                        Resources.AppResources.NotCorrectAbbreviation;
                    par = row.Cells[iCount].AddParagraph(answerText);
                    par.Format.Alignment = ParagraphAlignment.Center;
                }
                iCount++;
            }
            // Score column
            row.Cells[iCount].AddParagraph($"{articulationTestExam.Score:0.0%}");

            // Footer
            par = _section.Footers.Primary.AddParagraph();
            par.AddText(DateTime.Now.ToString("dd/MM/yyyy"));
            par.Format.Font.Size = 9;
            par.Format.Alignment = ParagraphAlignment.Right;

            // Save PDF file to temp
            string fileName = Path.Combine(Path.GetTempPath(), "results.pdf");

            SavePDF(fileName);

            return fileName;
        }

        public async Task<string> GeneratePDFForSoundTestResultsAsync(ArticulationTestExam articulationTestExam, IEnumerable<Grouping<string, ArticulationTestExamAnswer>> articulationTestAnswersGrouping)
        {
            return await Task.Run(() => GeneratePDFForSoundTestResults(articulationTestExam, articulationTestAnswersGrouping));
        }

        public string GeneratePDFForSoundTestResults(ArticulationTestExam articulationTestExam, IEnumerable<Grouping<string, ArticulationTestExamAnswer>> articulationTestAnswersGrouping)
        {
            // Set FontResolver
            if (GlobalFontSettings.FontResolver == null)
            {
                GlobalFontSettings.FontResolver = new FontResolver();
            }

            // Set page format
            _document = new Document();
            _section = _document.Sections.AddSection();
            _section.PageSetup.PageFormat = PageFormat.A4;
            _section.PageSetup.Orientation = Orientation.Portrait;

            InitStyles();

            // Header

            // Logo
            //Image image = _section.Headers.Primary.AddImage(ImageSource.FromFile("logo_notext.png"));
            //image.Height = "2.5cm";
            //image.LockAspectRatio = true;
            //image.RelativeVertical = RelativeVertical.Line;
            //image.RelativeHorizontal = RelativeHorizontal.Margin;
            //image.Top = ShapePosition.Top;
            //image.Left = ShapePosition.Right;
            //image.WrapFormat.Style = WrapStyle.Through;

            // Render Results
            // Header title
            var par = _section.Headers.Primary.AddParagraph(Resources.AppResources.ProjectTitle);
            par.Format.Font.Size = 16;
            par.Format.Font.Bold = true;
            par.Format.Alignment = ParagraphAlignment.Center;
            par.Format.SpaceAfter = "1cm";

            // Title
            par = _section.AddParagraph(Resources.AppResources.SoundTestResults);
            par.Format.Font.Size = 14;
            par.Format.Font.Bold = true;
            par.Format.Alignment = ParagraphAlignment.Center;
            par.Format.SpaceAfter = "1cm";

            // Sub title 
            string soundPosition = articulationTestExam.SoundPosition != "" ? articulationTestExam.SoundPosition :
                Resources.AppResources.All.ToUpper();
        
            par = _section.AddParagraph(soundPosition);
            par.Format.Font.Size = 12;
            par.Format.Font.Bold = true;
            par.Format.Alignment = ParagraphAlignment.Center;
            par.Format.SpaceAfter = "1cm";

            // Main content table
            Table table = _section.AddTable();
            table.Borders.Visible = true;
            // Create table columns
            table.AddColumn(140);
            foreach (Grouping<string, ArticulationTestExamAnswer> grouping in articulationTestAnswersGrouping)
            {
                var row = table.AddRow();
                row.Height = 20;
                row.Shading.Color = Color.FromCmyk(0, 0, 0, 5); // #f1f1f1
                par = row.Cells[0].AddParagraph(grouping.Key);
                par.Format.Font.Size = 12;
                par.Format.Alignment = ParagraphAlignment.Center;
              
                foreach (ArticulationTestExamAnswer answer in grouping)
                {
                    row = table.AddRow();
                    row.Height = 20;

                    if (answer.IsCorrect.HasValue)
                    {
                        string answerText = answer.IsCorrect.Value ? Resources.AppResources.CorrectAbbreviation : 
                            Resources.AppResources.NotCorrectAbbreviation;
                        par = row.Cells[0].AddParagraph($"{answer.ArticulationTest.Text} - {answerText}");
                        par.Format.Font.Size = 12;
                        par.Format.Alignment = ParagraphAlignment.Center;
                    }
                }
            }          

            // Footer
            par = _section.Footers.Primary.AddParagraph();
            par.AddText(DateTime.Now.ToString("dd/MM/yyyy"));
            par.Format.Font.Size = 9;
            par.Format.Alignment = ParagraphAlignment.Right;

            // Save PDF file to temp
            string fileName = Path.Combine(Path.GetTempPath(), "results.pdf");

            SavePDF(fileName);

            return fileName;
        }

        private void InitStyles()
        {
            Style style = _document.Styles["Normal"];
            style.Font.Name = "OpenSans";
            style.Font.Size = 12;
        }

        private void SavePDF(string fileName)
        {
            PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true)
            {
                Document = _document
            };
            pdfRenderer.RenderDocument();
            pdfRenderer.PdfDocument.Save(fileName);
        }
    }
}
