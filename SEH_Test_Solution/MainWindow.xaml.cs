using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Syncfusion.Presentation;


namespace SEH_Test_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IPresentation PowerPointDoc;

        public List<ImageContainer> images = new List<ImageContainer>(6);

        public MainWindow()
        {
            InitializeComponent();

            PowerPointDoc = Presentation.Create();

            images.Add(new ImageContainer());
            images.Add(new ImageContainer());
            images.Add(new ImageContainer());
            images.Add(new ImageContainer());
            images.Add(new ImageContainer());
            images.Add(new ImageContainer());

            //Added for future proofing as it currently doesn't negatively affect teh program
            (Image1.Parent as Grid).DataContext = images[0];
            (Image2.Parent as Grid).DataContext = images[1];
            (Image3.Parent as Grid).DataContext = images[2];
            (Image4.Parent as Grid).DataContext = images[3];
            (Image5.Parent as Grid).DataContext = images[4];
            (Image6.Parent as Grid).DataContext = images[5];
        }

        /// <summary>
        /// When an image is clicked this function will check or uncheck corresponding checkbox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ImageClicked(object sender, MouseButtonEventArgs e)
        {
            Image NewImage = (sender as Image);

            string name = NewImage.Name;
            char value = name.LastOrDefault();

            switch (value)
            {
                case '1': CheckBox1.IsChecked = CheckBox1.IsChecked == false; break;
                case '2': CheckBox2.IsChecked = CheckBox2.IsChecked == false; break;
                case '3': CheckBox3.IsChecked = CheckBox3.IsChecked == false; break;
                case '4': CheckBox4.IsChecked = CheckBox4.IsChecked == false; break;
                case '5': CheckBox5.IsChecked = CheckBox5.IsChecked == false; break;
                case '6': CheckBox6.IsChecked = CheckBox6.IsChecked == false; break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Grabs images and requires text to be in the titleTextBox, bodytextBox or Both.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RetrieveImages(object sender, RoutedEventArgs e)
        {
            string SearchQuery = new TextRange(TitleTextBox.Document.ContentStart, TitleTextBox.Document.ContentEnd).Text;

            foreach (Paragraph p in BodyTextBox.Document.Blocks)
            {
                foreach (Inline inline in p.Inlines)
                {
                    if (inline.FontWeight == FontWeights.Bold)
                    {
                        SearchQuery = SearchQuery + " " + new TextRange(inline.ContentStart, inline.ContentEnd).Text;
                    }
                }
            }

            //Sends initial query to get image results
            ImageCollector.SendQuery(SearchQuery);

            //gets an image from google for each image UIElement
            ImageCollector.SetNextImage(images[0]);
            Image1.Source = images[0].Image;
            ImageCollector.SetNextImage(images[1]);
            Image2.Source = images[1].Image;
            ImageCollector.SetNextImage(images[2]);
            Image3.Source = images[2].Image;
            ImageCollector.SetNextImage(images[3]);
            Image4.Source = images[3].Image;
            ImageCollector.SetNextImage(images[4]);
            Image5.Source = images[4].Image;
            ImageCollector.SetNextImage(images[5]);
            Image6.Source = images[5].Image;
        }

        /// <summary>
        /// Creates a MS Power Point slide from the text inputs provided and adds it to the presentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CreateSlide(object sender, RoutedEventArgs e)
        {
            ISlide slide = PowerPointDoc.Slides.Add(SlideLayoutType.ContentWithCaption);

            IShape shape = slide.Shapes[0] as IShape;
            shape.TextBody.Text = new TextRange(TitleTextBox.Document.ContentStart, TitleTextBox.Document.ContentEnd).Text;

            shape = slide.Shapes[2] as IShape;
            shape.TextBody.Text = new TextRange(BodyTextBox.Document.ContentStart, BodyTextBox.Document.ContentEnd).Text;

            double offset = 75;
            double HorizontalOffset = 500;
            double VerticalOffset = 100;
            int sway = 1;

            //Most of the work being done is to make the pictures fit better and too look a bit more pretty
            if ((bool)CheckBox1.IsChecked)
            {
                using (var ms = new MemoryStream(images[0].ImageMap))
                {
                    double width = 250;
                    double height = images[0].Image.Height * (250 / images[0].Image.Width);
                    slide.Shapes.AddPicture(ms, HorizontalOffset, VerticalOffset, width, height);
                    HorizontalOffset = HorizontalOffset + (offset * sway);
                    VerticalOffset = VerticalOffset + (height * .75);
                    sway = sway * -1;
                }
            }
            if ((bool)CheckBox2.IsChecked)
            {
                using (var ms = new MemoryStream(images[1].ImageMap))
                {
                    double width = 250;
                    double height = images[1].Image.Height * (250 / images[1].Image.Width);
                    slide.Shapes.AddPicture(ms, HorizontalOffset, VerticalOffset, width, height);
                    HorizontalOffset = HorizontalOffset + (offset * sway);
                    VerticalOffset = VerticalOffset + (height * .75);
                    sway = sway * -1;
                }
            }
            if ((bool)CheckBox3.IsChecked)
            {
                using (var ms = new MemoryStream(images[2].ImageMap))
                {
                    double width = 250;
                    double height = images[2].Image.Height * (250 / images[2].Image.Width);
                    slide.Shapes.AddPicture(ms, HorizontalOffset, VerticalOffset, width, height);
                    HorizontalOffset = HorizontalOffset + (offset * sway);
                    VerticalOffset = VerticalOffset + (height * .75);
                    sway = sway * -1;
                }
            }
            if ((bool)CheckBox4.IsChecked)
            {
                using (var ms = new MemoryStream(images[3].ImageMap))
                {
                    double width = 250;
                    double height = images[3].Image.Height * (250 / images[3].Image.Width);
                    slide.Shapes.AddPicture(ms, HorizontalOffset, VerticalOffset, width, height);
                    HorizontalOffset = HorizontalOffset + (offset * sway);
                    VerticalOffset = VerticalOffset + (height * .75);
                    sway = sway * -1;
                }
            }
            if ((bool)CheckBox5.IsChecked)
            {
                using (var ms = new MemoryStream(images[4].ImageMap))
                {
                    double width = 250;
                    double height = images[4].Image.Height * (250 / images[4].Image.Width);
                    slide.Shapes.AddPicture(ms, HorizontalOffset, VerticalOffset, width, height);
                    HorizontalOffset = HorizontalOffset + (offset * sway);
                    VerticalOffset = VerticalOffset + (height * .75);
                    sway = sway * -1;
                }
            }
            if ((bool)CheckBox6.IsChecked)
            {
                using (var ms = new MemoryStream(images[5].ImageMap))
                {
                    double width = 250;
                    double height = images[5].Image.Height * (250 / images[5].Image.Width);
                    slide.Shapes.AddPicture(ms, HorizontalOffset, VerticalOffset, width, height);
                    HorizontalOffset = HorizontalOffset + (offset * sway);
                    VerticalOffset = VerticalOffset + (height * .75);
                    sway = sway * -1;
                }
            }

            MessageBox.Show("Your slide was created. you can either add another using the same procedure or finalize and create the presentation.");
        }

        /// <summary>
        /// Saves and closes the MS Power Point Presentation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FinishPresentation(object sender, RoutedEventArgs e)
        {
            PowerPointDoc.Save("Output.pptx");
            PowerPointDoc.Close();
        }

        /// <summary>
        /// Will Copy the pictures to the Clipboard however it is currently unavailable
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyToClipboard(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Copying pictures directly is currently unavailable.");

            /*
            if ((bool)CheckBox1.IsChecked)
            {
                using (var ms = new MemoryStream(images[0].ImageMap))
                {
                }
            }
            if ((bool)CheckBox2.IsChecked)
            {
                using (var ms = new MemoryStream(images[1].ImageMap))
                {
                }
            }
            if ((bool)CheckBox3.IsChecked)
            {
                using (var ms = new MemoryStream(images[2].ImageMap))
                {
                }
            }
            if ((bool)CheckBox4.IsChecked)
            {
                using (var ms = new MemoryStream(images[3].ImageMap))
                {
                }
            }
            if ((bool)CheckBox5.IsChecked)
            {
                using (var ms = new MemoryStream(images[4].ImageMap))
                {
                }
            }
            if ((bool)CheckBox6.IsChecked)
            {
                using (var ms = new MemoryStream(images[5].ImageMap))
                {
                }
            }
            */
        }


    }
}
