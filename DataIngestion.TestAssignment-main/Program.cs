using DataIngestion.TestAssignment.DependencyInjection;
using DataIngestion.TestAssignment.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace DataIngestion.TestAssignment
{
	class Program
	{
		public static void Main(string[] args)
		{
			var dependencyCollection = new ServiceCollection();
			DiContainer.RegisterServices(dependencyCollection);

			Task.Run(async () => await Start());

			Console.ReadKey();
		}

		public static async Task Start()
        {
			var albumWorkflow = DiContainer.ServiceProvider.GetRequiredService<IAlbumWorkflow>();

			var isSuccess = false;

			try
			{
				isSuccess = await albumWorkflow.Run();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			var taskStatus = isSuccess ? "Success" : "Fail";
			Console.WriteLine($"Task result: {taskStatus}");
		}
	}
}
