using InstagramApiSharp.Classes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Pixo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TestPage : Page
    {
        private StorageFile file;

        public TestPage()
        {
            this.InitializeComponent();
        }
        //readonly StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
        //IReadOnlyList<StorageFile> SelectedFiles = null;

        //private async void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    // nkon
        //    var selectedFile = SelectedFiles.FirstOrDefault();
        //    var fileb = (await FileIO.ReadBufferAsync(selectedFile)).ToArray();
        //    InstaImage image = new InstaImage
        //    {
        //        ImageBytes= fileb,
        //        Uri = selectedFile.Path
        //    };

        //    var up = await UserWorkation.InstaApi.MediaProcessor.UploadPhotoAsync(image, "It is My #Dream.");
        //    if(!up.Succeeded)
        //    tbriU.Text = up.Info.Message + "   " + up.Info.Exception.Message;
        //}

        //private  void tbriU_Tapped(object sender, TappedRoutedEventArgs e)
        //{
          
        //}

        //private async void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    var picker = new Windows.Storage.Pickers.FileOpenPicker();
        //    picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
        //    picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
        //    picker.FileTypeFilter.Add(".jpg");
        //    picker.FileTypeFilter.Add(".jpeg");
        //    // picker.FileTypeFilter.Add(".png");

        //    SelectedFiles = await picker.PickMultipleFilesAsync();
        //    if (file != null)
        //    {
        //        // Application now has read/write access to the picked file
        //        this.tbriU.Text = file.Path;
        //        var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
        //        string mruToken = mru.Add(file, "pic");
        //    }
        //    else
        //    {
        //        this.tbriU.Text = "Operation cancelled.";
        //    }
        //}
    }
}
