using Cfdi.Domain;
using Cfdi.Domain.DTOs.Request;
using Cfdi.Domain.Entity;
using Cfdi.Domain.Models;
using Cfdi.Domain.Services;
using Cfdi.Worker.Exceptions;

namespace Cfdi.Worker.Services
{
    internal class ThreadManagerService : IThreadManagerService
    {
        private readonly ILogger<WorkerService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ICfdiHistoryService _cfdiHistoryService;
        private readonly IXmlReader _xmlReader;
        private readonly IPdfReader _pdfReader;
        private readonly IZipCreator _zipCreator;
        private readonly IFileManagerService _fileManager;

        public ThreadManagerService(
            ILogger<WorkerService> logger,
            IConfiguration configuration, 
            ICfdiHistoryService cfdiHistoryService,
            IXmlReader xmlReader,
            IPdfReader pdfReader,
            IZipCreator zipCreator,
            IFileManagerService fileManager)
        {
            _logger = logger;
            _configuration = configuration;
            _cfdiHistoryService = cfdiHistoryService;
            _xmlReader = xmlReader;
            _pdfReader = pdfReader;
            _zipCreator = zipCreator;
            _fileManager = fileManager;
        }

        public void CreateThread(CfdiRequest cfdiRequest)
        {
            //creamos solo un hilo o creamos 10, ...
            ThreadPool.QueueUserWorkItem(NewThreadFolio, cfdiRequest);
        }

        public bool CanCreateThread()
        {
            int max = 0, _ = 0;
            ThreadPool.GetAvailableThreads(out max, out _);
            return max > 0;
        }

        private async void NewThreadFolio(object data)
        {
            CfdiRequest request = (CfdiRequest)data;
            int threadId = Thread.CurrentThread.ManagedThreadId;
            string zipLocation = _configuration.GetValue<string>("ZipLocation");
            string location = zipLocation + "\\" + request.Usuario;

            //esto se quitara solo es para simular una petición
            #region RANDOM POSITIONS
            Random rand = new Random();
            int random = rand.Next(100, 50000);
            #endregion

            _logger.LogInformation("Hilo " + threadId + ": Consulta de " + random + " registros");
            //consulta de los folios (esta consulta puede cambiar por que va a ser a varios sistemas)
            ICollection<CfdiHistory> cfdis = await _cfdiHistoryService.AllQueryAsync(x => x.Id <= random);

            try
            {
                foreach (CfdiHistory cfdi in cfdis)
                {
                    //path del nuevo destino xml
                    string newCfdi = location + "\\invoice" + "\\cfdiDest" + cfdi.Poliza + ".xml";

                    //copiar xml (recordar que aquí el origen dependera ya que especifica que es de varios sistemas)
                    _fileManager.CopyFile(
                        "C:\\CFDIs\\invoice" + cfdi.Poliza + ".xml", 
                        location, 
                        "invoice", 
                        "cfdiDest" + cfdi.Poliza + ".xml");

                    //leer xml y subir a memoria, le pasamos el path archivo que copiamos mas no el original
                    CfdiEntity cfdiEntity = _xmlReader.GetCfdi(cfdi, newCfdi);

                    //convertir a PDF
                    string pathPdf = _pdfReader.CreateCfdi(cfdiEntity, location);

                    string[] files = new string[] { /*pathPdf,*/ newCfdi };

                    //comprimir xml y pdf por poliza (ciclo)
                    _zipCreator.AddInvoices(files, location + "\\invoice", "cfdiDestZip" + cfdi.Poliza + ".zip");

                    //eliminar archivos xml y pdf y dejar solo el zip
                    _fileManager.RemoveFiles(files);
                }

                //crear el zip padre
                _zipCreator.AddInvoices(location, zipLocation + "\\results", "cfdisZip_" + request.Usuario + "_" + Utl.GetActualDate() + ".zip");
                _logger.LogInformation("Hilo" + threadId + ": Finaliza");
            }
            catch (IOException iox)
            {
                //exception to copy or remove files (_fileManager)
                _logger.LogError(iox.Message);
            }
            catch (XmlReaderException xmlEx) 
            {
                _logger.LogError(xmlEx.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }
}
