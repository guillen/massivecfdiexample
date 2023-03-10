using Cfdi.Domain.Common;
using Cfdi.Domain.Repository;
using Cfdi.Domain.Services;
using Cfdi.Worker;
using Cfdi.Worker.Context;
using Cfdi.Worker.Mapper;
using Cfdi.Worker.Repository;
using Cfdi.Worker.Services;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "CFDI Service";
    })
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        var optionsBuilder = new DbContextOptionsBuilder<WorkerDbContext>();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("SqlServer"));
        services.AddSingleton(s => new WorkerDbContext(optionsBuilder.Options));

        services.AddSingleton(typeof(IService<>), typeof(Service<>));
        services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        services.AddSingleton<IQueueRepository, QueueRepository>();
        services.AddSingleton<IQueueService, QueueService>();
        services.AddSingleton<ICfdiHistoryRepository, CfdiHistoryRepository>();
        services.AddSingleton<ICfdiHistoryService, CfdiHistoryService>();

        services.AddAutoMapper(typeof(WorkerProfile));

        services.AddSingleton<IMemoryMetricsService, MemoryMetricsService>();
        services.AddSingleton<IThreadManagerService, ThreadManagerService>();
        services.AddSingleton<IXmlReader, CfdiXmlReader>();
        services.AddSingleton<IPdfReader, CfdiPdfReader>();
        services.AddSingleton<IZipCreator, CfdiZipCreator>();
        services.AddSingleton<IFileManagerService, FileManagerService>();

        services.AddHostedService<WorkerService>();
    })
    .Build();

await host.RunAsync();
