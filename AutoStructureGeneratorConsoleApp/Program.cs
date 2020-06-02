using System;
using System.Collections.Generic;
using AutoStructureGeneratorConsoleApp.AutoGeneratorClasses;

namespace AutoStructureGeneratorConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Mustafa\\source\\repos\\Kafala\\Kafala";
            List<string> models=new List<string>();
            models.Add("Box");
            models.Add("Campaign");
            models.Add("CampaignMaterial");
            models.Add("CampaignType");
            models.Add("Debt");
            models.Add("Donor");
            models.Add("Family");
            models.Add("FamilyMember");
            models.Add("FamilyState");
            models.Add("FamilyTree");
            models.Add("Material");
            models.Add("Mediator");
            models.Add("Payment");
            models.Add("PaymentSponsorship");
            models.Add("ReceivedCampaign");
            models.Add("ReceivedCampaignMaterial");
            models.Add("ResidenceAreaName");
            models.Add("Sponsorship");

            //FoldersGenerator.FolderGenerator(path);
            ClassesGenerator classesGenerator=new ClassesGenerator(path, "Kafala", "Kafala", models);
            classesGenerator.CreateRepositories();
            classesGenerator.CreateRepositoriesInterfaces();
            classesGenerator.CreateRepositoryWrapper();
            classesGenerator.CreateRepositoryBase();
            Console.WriteLine("done");
            Console.ReadLine();
        }
    }
}
