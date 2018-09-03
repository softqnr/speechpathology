﻿using System;
using System.IO;
using System.Threading.Tasks;
using Foundation;
using SpeechPathology.Interfaces;
using SpeechPathology.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(FileAccessHelper))]
namespace SpeechPathology.iOS
{
    public class FileAccessHelper : IFileAccessHelper
    {
        public async Task<String> GetDBPathAndCreateIfNotExists(string databaseFilename)
        {
            //String databaseName = "MyLite.db";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, databaseFilename);
            if (!File.Exists(path))
            {

                var existingDb = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(databaseFilename)
                    , Path.GetExtension(databaseFilename));
                //File.Copy(existingDb, path);
                await CopyFileAsync(existingDb, path);
            }
            return path;
        }

        public async Task CopyFileAsync(string sourcePath, string destinationPath)
        {
            using (Stream source = File.OpenRead(sourcePath))
            {
                using (Stream destination = File.Create(destinationPath))
                {
                    await source.CopyToAsync(destination);
                }
            }
        }
    } 
}