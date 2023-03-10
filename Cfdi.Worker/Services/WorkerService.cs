using AutoMapper;
using Cfdi.Domain;
using Cfdi.Domain.DTOs.Request;
using Cfdi.Domain.Entity;
using Cfdi.Domain.Models;
using Cfdi.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cfdi.Worker.Services
{
    internal class WorkerService : BackgroundService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IThreadManagerService _threadManagerService;
        private readonly IMemoryMetricsService _memoryMetricsService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IQueueService _queueService;
        private readonly ICfdiHistoryService _cfdiHistoryService;

        public WorkerService(
            ILogger<WorkerService> logger, 
            IThreadManagerService threadManagerService, 
            IMemoryMetricsService memoryMetricsService,
            IConfiguration configuration,
            IMapper mapper,
            IQueueService queueService,
            ICfdiHistoryService cfdiHistoryService)
        {
            _logger = logger;
            _threadManagerService = threadManagerService;
            _memoryMetricsService = memoryMetricsService;
            _configuration = configuration;
            _mapper = mapper;
            _queueService = queueService;
            _cfdiHistoryService = cfdiHistoryService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    await Main();
                    await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                }
            }
            catch (TaskCanceledException ex)
            {
                //expected exception, cancelled manually
                _logger.LogError(ex, "{Message}", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{Message}", ex.Message);
                Environment.Exit(1);
            }
        }

        private async Task Main() 
        {
            if (await _queueService.HasNext())
            {
                int avaibleRam = _configuration.GetValue<int>("Cost:RAM");//espacio en ram de olgura
                int avaibleDisk = _configuration.GetValue<int>("Cost:Disk");//espacio en disco de olgura
                string diskName = _configuration.GetValue<string>("Cost:DiskName");
                Metric ramMetric = _memoryMetricsService.GetRAMWindowsMetrics();//obtenemos la ram disponible
                Metric diskMetric = _memoryMetricsService.GetDiskWindowsMetrics(diskName);//obtenemos la memoria en disco disponible
                Queue queue = await _queueService.GetNext();//leemos de la cola de RabbitMq
                var request = _mapper.Map<CfdiRequest>(queue);
                double ramCost = Utl.OperationRAMCost(ramMetric, request);//validamos el costo de la operacion que impacta a la ram en MB
                double diskCost = Utl.OperationDiskCost(diskMetric, request);//validamos el costo de la operacion que impacta al disco en MB

                //_logger.LogInformation("Free RAM: " + ramMetric.Free + ", Free Disk: " + diskMetric.Free);

                //el costo de operacion mas el espacio disponible en ram no debe pasar de 512 MB y
                //el costo de operacion mas el espacio disponible en disco no debe pasar de 256 MB
                if (ramMetric.Free - ramCost >= avaibleRam && diskMetric.Free - diskCost >= avaibleDisk)
                {
                    //vemos si hay hilos disponibles
                    if (_threadManagerService.CanCreateThread())
                    {
                        await _queueService.PopQueue(queue);//sacamos de la cola de RabbitMq
                        _threadManagerService.CreateThread(request);//Creamos un hilo que procese la solicitud
                    }
                    else
                    {
                        //log de mensaje del por que no se ha podido ejecutar el proceso: No hay hilos
                    }
                }
                else
                {
                    //log de mensaje del por que no se ha podido ejecutar el proceso: espacio en disco o espacio en ram no disponible
                }
            }
        }
    }
}
