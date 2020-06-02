using System.IO;

namespace AutoStructureGeneratorConsoleApp.AutoGeneratorClasses
{
    public class FoldersGenerator
    {
        public static void FolderGenerator(string projectPath)
        {
            //Create Data Access Layer Folder
            CreateFolder($"{projectPath}/DAL");
            //Create Abstraction folder inside DAL
            CreateFolder($"{projectPath}/DAL/Abstraction");
            //Create Repositories folder inside DAL
            CreateFolder($"{projectPath}/DAL/Repositories");

            //Create Infrastructure Folder
            CreateFolder($"{projectPath}/Infrastructure");
            //Create Enums folder inside Infrastructure
            CreateFolder($"{projectPath}/Infrastructure/Enums");
            //Create Helpers folder inside Infrastructure
            CreateFolder($"{projectPath}/Infrastructure/Helpers");
            //Create Interfaces Folder inside Infrastructure
            CreateFolder($"{projectPath}/Infrastructure/Interfaces");
            //Create Repositories folder inside Interfaces
            CreateFolder($"{projectPath}/Infrastructure/Interfaces/Repositories");
            //Create Services folder inside Interfaces
            CreateFolder($"{projectPath}/Infrastructure/Interfaces/Services");

            //Create Models Folder
            CreateFolder($"{projectPath}/Models");
            //Create Db folder inside Models
            CreateFolder($"{projectPath}/Models/Db");
            //Create Dtos folder inside Models
            CreateFolder($"{projectPath}/Models/Dtos");
            //Create Forms folder inside Models
            CreateFolder($"{projectPath}/Models/Forms");

            //Create ServiceLayer Folder
            CreateFolder($"{projectPath}/ServiceLayer");


        }

        private  static void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}