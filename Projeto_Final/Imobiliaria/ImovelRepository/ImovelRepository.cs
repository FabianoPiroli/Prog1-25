using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Modelo;


namespace Repository
{
    public class ImovelRepository
    {
        private readonly string _filePath;
        private List<Imovel> _imoveis;

        public ImovelRepository()
        {
            _filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "imoveis.json");
            _imoveis = LoadFromFile();
        }

        private List<Imovel> LoadFromFile()
        {
            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<Imovel>>(json) ?? new List<Imovel>();
            }
            return new List<Imovel>();
        }

        private void SaveToFile()
        {
            var json = JsonSerializer.Serialize(_imoveis, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public List<Imovel> RetrieveAll() => _imoveis;

        public Imovel Retrieve(int id) => _imoveis.FirstOrDefault(i => i.Id == id);

        public void Save(Imovel imovel)
        {
            imovel.Id = _imoveis.Any() ? _imoveis.Max(i => i.Id) + 1 : 1;
            _imoveis.Add(imovel);
            SaveToFile();
        }

        public void Update(Imovel updated)
        {
            var idx = _imoveis.FindIndex(i => i.Id == updated.Id);
            if (idx >= 0)
            {
                _imoveis[idx] = updated;
                SaveToFile();
            }
        }

        public bool Delete(Imovel imovel)
        {
            var removed = _imoveis.Remove(imovel);
            if (removed) SaveToFile();
            return removed;
        }

        public bool DeleteById(int id)
        {
            var imovel = Retrieve(id);
            return imovel != null && Delete(imovel);
        }
    }
}
