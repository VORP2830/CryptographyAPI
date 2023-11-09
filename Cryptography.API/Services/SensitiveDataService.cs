using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Cryptography.API.DTOs;
using Cryptography.API.Models;
using Cryptography.API.Repositories.Interfaces;
using Cryptography.API.Services.Interfaces;

namespace Cryptography.API.Services
{
    public class SensitiveDataService : ISensitiveDataService
    {
        private readonly ISensitiveDataRepository _sensitiveDataRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public SensitiveDataService(ISensitiveDataRepository sensitiveDataRepository, IConfiguration configuration, IMapper mapper)
        {
            _sensitiveDataRepository = sensitiveDataRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<IEnumerable<SensitiveDataDTO>> GetAll()
        {
            List<SensitiveDataDTO> sensitiveDatas = new List<SensitiveDataDTO>();
            var sensitiveDatasDb = await _sensitiveDataRepository.GetAll();
            foreach(SensitiveData sensitiveData in sensitiveDatasDb)
            {
                var sensitiveDataDto = _mapper.Map<SensitiveDataDTO>(sensitiveData);
                sensitiveDataDto.CreditCardToken = Decrypt(sensitiveData.CreditCardToken);
                sensitiveDataDto.UserDocument = Decrypt(sensitiveData.UserDocument);
                sensitiveDatas.Add(sensitiveDataDto);
            }
            return sensitiveDatas;
        }
        public async Task<SensitiveDataDTO> GetById(long id)
        {
            var sensitiveData = await _sensitiveDataRepository.GetById(id);
            if(sensitiveData == null) throw new Exception("Não foi encontados dados com esses parametros");
            sensitiveData.SetUserDocument(Decrypt(sensitiveData.UserDocument));
            sensitiveData.SetCreditCardToken(Decrypt(sensitiveData.CreditCardToken));
            return _mapper.Map<SensitiveDataDTO>(sensitiveData);
        }
        public async Task<SensitiveDataDTO> Create(SensitiveDataDTO model)
        {
            SensitiveData sensitiveData = new SensitiveData(Encrypt(model.UserDocument), Encrypt(model.CreditCardToken), model.Value);
            _sensitiveDataRepository.Add(sensitiveData);
            sensitiveData.SetActive(true);
            await _sensitiveDataRepository.SaveChangesAsync();
            sensitiveData.SetUserDocument(Decrypt(sensitiveData.UserDocument));
            sensitiveData.SetCreditCardToken(Decrypt(sensitiveData.CreditCardToken));
            return _mapper.Map<SensitiveDataDTO>(sensitiveData);
        }
        public async Task<SensitiveDataDTO> Update(SensitiveDataDTO model)
        {
            SensitiveData sensitiveData = await _sensitiveDataRepository.GetById(model.Id);
            if(sensitiveData == null) throw new Exception("Não foi encontados dados com esses parametros");
            SensitiveData sensitiveDataModel = new SensitiveData(model.Id, Encrypt(model.UserDocument), Encrypt(model.CreditCardToken), model.Value);
            _sensitiveDataRepository.Update(sensitiveDataModel);
            await _sensitiveDataRepository.SaveChangesAsync();
            return _mapper.Map<SensitiveDataDTO>(sensitiveData);
        }
        public async Task<SensitiveDataDTO> Delete(long id)
        {
            SensitiveData sensitiveData = await _sensitiveDataRepository.GetById(id);
            if(sensitiveData == null) throw new Exception("Não foi encontados dados com esses parametros");
            sensitiveData.SetActive(false);
            _sensitiveDataRepository.Update(sensitiveData);
            await _sensitiveDataRepository.SaveChangesAsync();
            return _mapper.Map<SensitiveDataDTO>(sensitiveData);
        }
        private byte[] Key => Encoding.UTF8.GetBytes(_configuration["Encrypt:Key"]);
        private byte[] IV => Encoding.UTF8.GetBytes(_configuration["Encrypt:Iv"]);
        private string Encrypt(string value)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msEncrypt = new();
            using (CryptoStream csEncrypt = new(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (StreamWriter swEncrypt = new(csEncrypt))
            {
                swEncrypt.Write(value);
            }
            return Convert.ToBase64String(msEncrypt.ToArray());
        }
        private string Decrypt(string value)
        {
            using Aes aesAlg = Aes.Create();
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using MemoryStream msDecrypt = new(Convert.FromBase64String(value));
            using CryptoStream csDecrypt = new(msDecrypt, decryptor, CryptoStreamMode.Read);
            using StreamReader srDecrypt = new(csDecrypt);
            return srDecrypt.ReadToEnd();
        }
    }
}