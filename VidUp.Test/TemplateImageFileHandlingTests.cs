﻿#region

using System.IO;
using Drexel.VidUp.Business;
using Drexel.VidUp.UI.ViewModels;
using NUnit.Framework;

#endregion

namespace Drexel.VidUp.Test
{
    public class TemplateImageFileHandlingTests
    {
        private static TemplateViewModel templateViewModel;
        private static string t1RootFolder = Path.Combine(TestContext.CurrentContext.TestDirectory, "T1Root");
        private static string templateImage1SourceFilePath = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestAssets", "image1.png");
        private static string templateImage1TargetFilePath;
        private static string templateImageFileExistedImage12TargetFilePath;

        private static Template t1;
        private static Template t2;
        private static Template t3;

        private static TemplateList templateList;

        [OneTimeSetUp]
        public static void Initialize()
        {
            if (Directory.Exists(BaseSettings.StorageFolder))
            {
                Directory.Delete(BaseSettings.StorageFolder, true);
            }

            if (Directory.Exists(TemplateImageFileHandlingTests.t1RootFolder))
            {
                Directory.Delete(TemplateImageFileHandlingTests.t1RootFolder, true);
            }

            Directory.CreateDirectory(TemplateImageFileHandlingTests.t1RootFolder);
            Directory.CreateDirectory(Path.Combine(TemplateImageFileHandlingTests.t1RootFolder, "videos"));

            BaseSettings.SubFolder = string.Empty;
            TemplateImageFileHandlingTests.templateImage1TargetFilePath = Path.Combine(BaseSettings.TemplateImageFolder, "image1.png");
            TemplateImageFileHandlingTests.templateImageFileExistedImage12TargetFilePath = Path.Combine(BaseSettings.TemplateImageFolder, "image12.png");

            MainWindowViewModel mainWindowViewModel = new MainWindowViewModel(BaseSettings.UserSuffix, null, out _, out TemplateImageFileHandlingTests.templateList, out _);
            mainWindowViewModel.TabNo = 1;
            TemplateImageFileHandlingTests.templateViewModel = (TemplateViewModel) mainWindowViewModel.CurrentViewModel;

            TemplateImageFileHandlingTests.t1 = new Template("T1", null, TemplateMode.FolderBased, TemplateImageFileHandlingTests.t1RootFolder, null, TemplateImageFileHandlingTests.templateList);
            TemplateImageFileHandlingTests.t2 = new Template("T2", null, TemplateMode.FolderBased, null, null, TemplateImageFileHandlingTests.templateList);
            TemplateImageFileHandlingTests.t3 = new Template("T3", null, TemplateMode.FolderBased, null, null, TemplateImageFileHandlingTests.templateList);

            TemplateImageFileHandlingTests.templateViewModel.AddTemplate(t1);
            TemplateImageFileHandlingTests.templateViewModel.AddTemplate(t2);
            TemplateImageFileHandlingTests.templateViewModel.AddTemplate(t3);
        }

        [OneTimeTearDown]
        public static void CleanUp()
        {
            TemplateImageFileHandlingTests.templateViewModel = null;
            if (Directory.Exists(BaseSettings.StorageFolder))
            {
                Directory.Delete(BaseSettings.StorageFolder, true);
            }

            if (Directory.Exists(TemplateImageFileHandlingTests.t1RootFolder))
            {
                Directory.Delete(TemplateImageFileHandlingTests.t1RootFolder, true);
            }
        }

        [Test, Order(1)]
        public static void TestAddT1TemplateImage()
        {
            TemplateImageFileHandlingTests.t1.ImageFilePathForEditing = TemplateImageFileHandlingTests.templateImage1SourceFilePath;
            Assert.IsTrue(TemplateImageFileHandlingTests.t1.ImageFilePathForEditing == TemplateImageFileHandlingTests.templateImage1TargetFilePath);
            Assert.IsTrue(File.Exists(TemplateImageFileHandlingTests.templateImage1TargetFilePath));
        }

        [Test, Order(2)]
        public static void TestAddT2TemplateImageAgain()
        {
            TemplateImageFileHandlingTests.t2.ImageFilePathForEditing = TemplateImageFileHandlingTests.templateImage1SourceFilePath;
            Assert.IsTrue(File.Exists(TemplateImageFileHandlingTests.templateImageFileExistedImage12TargetFilePath));
        }

        [Test, Order(3)]
        public static void TestAddT2TemplateImageFromTemplateImageStorageFolder()
        {
            TemplateImageFileHandlingTests.t2.ImageFilePathForEditing = TemplateImageFileHandlingTests.templateImage1TargetFilePath;
            Assert.IsTrue(TemplateImageFileHandlingTests.t2.ImageFilePathForEditing == TemplateImageFileHandlingTests.templateImage1TargetFilePath);
            Assert.IsTrue(!File.Exists(Path.Combine(BaseSettings.TemplateImageFolder, "image13.png")));
        }

        [Test, Order(4)]
        public static void TestRemoveT1TemplateImage()
        {
            TemplateImageFileHandlingTests.t1.ImageFilePathForEditing = null;
            Assert.IsTrue(File.Exists(TemplateImageFileHandlingTests.templateImage1TargetFilePath));
        }

        [Test, Order(5)]
        public static void TestRemoveT2TemplateImage()
        {
            TemplateImageFileHandlingTests.t2.ImageFilePathForEditing = null;
            Assert.IsTrue(!File.Exists(TemplateImageFileHandlingTests.templateImage1TargetFilePath));
        }

        [Test, Order(6)]
        public static void TestAddT1T2TemplateImage()
        {
            TemplateImageFileHandlingTests.t1.ImageFilePathForEditing = TemplateImageFileHandlingTests.templateImage1SourceFilePath;
            Assert.IsTrue(TemplateImageFileHandlingTests.t1.ImageFilePathForEditing == TemplateImageFileHandlingTests.templateImage1TargetFilePath);
            Assert.IsTrue(File.Exists(TemplateImageFileHandlingTests.templateImage1TargetFilePath));

            TemplateImageFileHandlingTests.t2.ImageFilePathForEditing = TemplateImageFileHandlingTests.templateImage1TargetFilePath;
            Assert.IsTrue(TemplateImageFileHandlingTests.t2.ImageFilePathForEditing == TemplateImageFileHandlingTests.templateImage1TargetFilePath);
        }

        [Test, Order(7)]
        public static void TestRemoveT1()
        {
            TemplateImageFileHandlingTests.templateViewModel.DeleteTemplate(TemplateImageFileHandlingTests.t1.Guid.ToString());
            Assert.IsTrue(File.Exists(TemplateImageFileHandlingTests.templateImage1TargetFilePath));
        }

        [Test, Order(8)]
        public static void TestRemoveT2()
        {
            TemplateImageFileHandlingTests.templateViewModel.DeleteTemplate(TemplateImageFileHandlingTests.t2.Guid.ToString());
            Assert.IsTrue(!File.Exists(TemplateImageFileHandlingTests.templateImage1TargetFilePath));
        }
    }
}
