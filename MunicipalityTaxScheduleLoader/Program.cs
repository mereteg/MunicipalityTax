using BusinessLogic.Contract;
using BusinessLogic.Engines;
using MunicipalityTaxScheduleLoader.Model;
using ResourceAccess.Engines;
using ResourceAccess.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ResourceAccess.Context;
using Microsoft.EntityFrameworkCore;
using ServiceInterface.Model;
using System.Threading.Tasks;

namespace MunicipalityTaxScheduleLoader
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
                // TODO errorhandling
                // TODO get input from args
                string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MunicipalityTax;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False;";
                List<IMunicipalityTaxScheduleModel> taxSchedules = LoadTaxSchedules("TaxSchedules.json");
                LoadTaxSchedules(taxSchedules, connectionString).GetAwaiter().GetResult();
            }
			catch (Exception ex)
			{
                Console.WriteLine($"Exception: {ex}");
                Console.ReadLine();
			}
           

        }

        private static async Task LoadTaxSchedules(List<IMunicipalityTaxScheduleModel> taxSchedules, string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MunicipalityTaxScheduleDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            var context = new MunicipalityTaxScheduleDbContext(optionsBuilder.Options);
            var businessMunicipalityTaxScheduleUpdaterEngine = new BusinessMunicipalityTaxScheduleUpdaterEngine(
                    new ResourceMunicipalityTaxScheduleEngine(context)
                    );
            // TODO load in transaction
            // TODO create bulk-load method in ResourceAccess engine
			foreach (var taxSchedule in taxSchedules)
			{
                UpdateMunicipalityTaxStatus status = (await businessMunicipalityTaxScheduleUpdaterEngine.UpdateMunicipalityTax(
                    new UpdateMunicipalityTaxRequest
                    {
                        MunicipalityTaxSchedule = taxSchedule
                    })).Status;
                if (status != UpdateMunicipalityTaxStatus.OK)
                    throw new ApplicationException($"Illegal input {JsonSerializer.Serialize<IMunicipalityTaxScheduleModel>(taxSchedule)}");
			}
        }


		public static List<IMunicipalityTaxScheduleModel> LoadTaxSchedules(string path)
        {
            var result = new List<IMunicipalityTaxScheduleModel>();

            if (!File.Exists(path))
                throw new ApplicationException($"{path} does not exist!");

            var importText = File.ReadAllText(path);
            if (string.IsNullOrEmpty(importText))
                return result;

            MunicipalityTaxScheduleListModel taxSchedules =
                JsonSerializer.Deserialize<MunicipalityTaxScheduleListModel>(importText);

            foreach (var taxSchedule in taxSchedules.MunicipalityTaxScheduleModels)
                result.Add(taxSchedule);

            return result;
        }
    }
}
