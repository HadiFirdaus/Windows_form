﻿using System.Collections.Generic;
using System.Configuration;
using System.IO;
using TrackerLibrary.Models;
using System.Linq;
//Load the text file
//Convert the text to list<PrizeModel>
//Find the max ID
//Add the new record with the new ID (max + 1)
//Convert the prizes to list<string>
//Save the list<string> to the text file
namespace TrackerLibrary.DataAccess.TextHelpers
{
    public static class TextConnectorProcessor
    {
        public static string FullFilePath(this string fileName)
        /* PrizeModels.csv*/
        {
            //D: Documents\C++\TournamanetTracker
            return $"{ ConfigurationManager.AppSettings["filePath"]}\\{ fileName}";
        }
        public static List<string> LoadFile(this string file)
        {
            if (!File.Exists(file))
            {
                return new List<string>();
            }
            return File.ReadAllLines(file).ToList();
        }
        public static List<PrizeModel> ConvertToPrizeModels(this List<string> lines)
        {
            List<PrizeModel> output = new List<PrizeModel>();
            foreach(string line in lines)
            {
                string[] cols = line.Split(',');
                PrizeModel p = new PrizeModel();
                p.ID = int.Parse(cols[0]);
                p.PlaceNumber = int.Parse(cols[1]);
                p.PlaceName = cols[2];
                p.PlaceAmount = decimal.Parse(cols[3]);
                p.PlacePercentage = double.Parse(cols[4]);
                output.Add(p);
            }
            return output;
        }

        public static List<PersonModel> ConvertoPersonModels(this List<string> lines)
        {
            List<PersonModel> output = new List<PersonModel>();
            foreach(string line in lines)
            {
                string[] cols = line.Split(',');
                PersonModel p = new PersonModel();
                p.ID = int.Parse(cols[0]);
                p.FirstName = cols[1];
                p.LastName = cols[2];
                p.EmailAddress = cols[3];
                p.CellPhoneNumber = cols[4];
                output.Add(p);
            }
        }
        public static void SaveToPrizeFile(this List<PrizeModel> models, string fileName)
        {
            List<string> lines = new List<string>();
            foreach(PrizeModel p in models)
            {
                lines.Add($"{ p.ID },{ p.PlaceNumber }, { p.PlaceName }, { p.PlaceAmount }, { p.PlacePercentage }");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }

        public static void SaveToPeopleFile(this List<PersonModel> models, string fileName){
            List<string> lines = new List<string>();
            foreach(PersonModel p in models)
            {
                lines.Add($"{p.ID}, {p.FirstName}, {p.LastName}, {p.EmailAddress}, {p.CellPhoneNumber}");
            }
            File.WriteAllLines(fileName.FullFilePath(), lines);
        }
    }
}
