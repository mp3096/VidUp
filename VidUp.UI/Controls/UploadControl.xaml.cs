﻿using Drexel.VidUp.Business;
using Drexel.VidUp.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Drexel.VidUp.UI.Controls
{
    /// <summary>
    /// Interaktionslogik für UploadControl.xaml
    /// </summary>
    public partial class UploadControl : UserControl
    {
        //todo: make thumbnail file path clearable
        public UploadControl()
        {
            InitializeComponent();
        }

        //FU MVVM, Combox or whatever! When usercontrol view unloads, ComboBox sets selectedItem to null
        //and so template is set to null on upload even if before a template was selected
        private void ComboBoxTemplateSelect_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            UploadViewModel uploadViewModel = (UploadViewModel)comboBox.DataContext;
            Template selectedTemplate = (Template)comboBox.SelectedItem;
            if (uploadViewModel.Template != selectedTemplate)
            {
                uploadViewModel.Template = selectedTemplate;
            }
        }

        private void ComboBoxQuarterHourSelect_DropDownClosed(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            UploadViewModel uploadViewModel = (UploadViewModel)comboBox.DataContext;
            QuarterHourViewModel selectedQuarterHour = (QuarterHourViewModel)comboBox.SelectedItem;
            if (uploadViewModel.Upload.PublishAtTime != selectedQuarterHour.QuarterHour)
            {
                uploadViewModel.SetPublishAtTime(selectedQuarterHour.QuarterHour);
            }
        }
        private void controlGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox control = (TextBox)sender;
            if(control.Name == "Description")
            {
                control.MinHeight = 200;
            }

            if (control.Name == "Tags")
            {
                control.MinHeight = 100;
            }

            UploadControl uploadControl = (UploadControl)((GroupBox)((Grid)((StackPanel)control.Parent).Parent).Parent).Parent;
            uploadControl.Minimize.Visibility = System.Windows.Visibility.Visible;
        }

        private void controlLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox control = (TextBox)sender;
            control.MinHeight = 0;

            UploadControl uploadControl = (UploadControl)((GroupBox)((Grid)((StackPanel)control.Parent).Parent).Parent).Parent;
            if(uploadControl.Description.MinHeight == 0 && uploadControl.Tags.MinHeight == 0)
            {
                uploadControl.Minimize.Visibility = System.Windows.Visibility.Collapsed;
            }

        }

        private void minimizeClick(object sender, RoutedEventArgs e)
        {
            UploadControl control = (UploadControl)((GroupBox)((StackPanel)((Button)sender).Parent).Parent).Parent;
            control.Description.MinHeight = 0;
            control.Tags.MinHeight = 0;
            control.Minimize.Visibility = System.Windows.Visibility.Collapsed;
        }
    }
}
