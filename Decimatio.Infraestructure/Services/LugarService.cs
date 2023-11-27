namespace Decimatio.Infraestructure.Services
{
    internal sealed class LugarService : ILugarService
    {
        private readonly ILugarRepository _lugarRepository;
        private readonly IBlobFilesService _blobFilesService;
        private readonly BlobContainerConfig _containerConfig;

        public LugarService(ILugarRepository lugarRepository, IBlobFilesService blobFilesService, BlobContainerConfig containerConfig)
        {
            _lugarRepository = lugarRepository;
            _blobFilesService = blobFilesService;
            _containerConfig = containerConfig;
        }

        public async Task<IEnumerable<Lugar>> GetAllLugares()
        {
            try
            {
                var result = await _lugarRepository.GetAllLugares();
                var tasks = result.Select(async lugar =>
                {
                    lugar.NombreMapaReferencial = lugar.MapaReferencial;
                    string imageNamePath = _containerConfig.ReferencialMapName + lugar.MapaReferencial;
                    lugar.MapaReferencial = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                    return lugar;
                });

                return await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener la lista de lugares {ex.Message}");
            }
        }

        public async Task<Lugar> GetById(int idLugar)
        {
            try
            {
                var result = await _lugarRepository.GetById(idLugar);

                string imageNamePath = _containerConfig.ReferencialMapName + result.MapaReferencial;
                result.MapaReferencial = await _blobFilesService.GetURLImageFromBlobStorage(imageNamePath);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo obtener el lugar {ex.Message}");
            }
        }

        public async Task AddLugar(Lugar lugar)
        {
            try
            {
                if (lugar.MapaReferencial is not null || lugar.MapaReferencial != "")
                {
                    string imageNamePath = _containerConfig.ReferencialMapName + lugar.NombreMapaReferencial;
                    var flyerContent = Convert.FromBase64String(lugar.MapaReferencial);
                    await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
                }
                lugar.MapaReferencial = lugar.NombreMapaReferencial;
                await _lugarRepository.AddLugar(lugar);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo agregar el lugar {ex.Message}"); 
            }
        }

        public async Task<bool> UpdateLugar(Lugar lugar)
        {
            try
            {
                if (lugar.MapaReferencial is not null || lugar.MapaReferencial != "")
                {
                    string imageNamePath = _containerConfig.ReferencialMapName + lugar.NombreMapaReferencial;
                    var flyerContent = Convert.FromBase64String(lugar.MapaReferencial);
                    await _blobFilesService.AddFlyerBlobStorage(flyerContent, imageNamePath);
                }
                lugar.MapaReferencial = lugar.NombreMapaReferencial;
                return await _lugarRepository.UpdateLugar(lugar);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo editar el lugar {ex.Message}");
            }
        }

        public async Task<bool> DeleteLugar(int idLugar)
        {
            try
            {
                return await _lugarRepository.DeleteLugar(idLugar);
            }
            catch (Exception ex)
            {
                throw new Exception($"No se pudo eliminar el lugar {ex.Message}");
            }
        }
    }
}
