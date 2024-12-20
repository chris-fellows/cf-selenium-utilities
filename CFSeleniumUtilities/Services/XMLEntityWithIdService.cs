using CFSeleniumUtilities.Utilities;

namespace CFSeleniumUtilities.Services
{
    public abstract class XMLEntityWithIdService<TEntityType, TIdType>
    {
        protected readonly string _folder;
        protected readonly string _getAllFilePattern;                                       // E.g. "Browser.*.xml"
        protected readonly Func<TEntityType, string> _getEntityFileNameByEntityFunction;    // Returns file name from entity
        protected readonly Func<TIdType, string> _getEntityFileNameByIdFunction;            // Returns file name from id

        public XMLEntityWithIdService(string folder,
                                    string getAllFilePattern,
                                    Func<TEntityType, string> getEntityFileNameByEntityFunction,
                                    Func<TIdType, string> getEntityFileNameByIdFunction)

        {
            _folder = folder;
            _getAllFilePattern = getAllFilePattern;
            _getEntityFileNameByEntityFunction = getEntityFileNameByEntityFunction;
            _getEntityFileNameByIdFunction = getEntityFileNameByIdFunction;
        }

        public List<TEntityType> GetAll()
        {
            var items = new List<TEntityType>();
            foreach (var file in Directory.GetFiles(_folder, _getAllFilePattern))
            {
                items.Add(XMLUtilities.DeserializeFromString<TEntityType>(File.ReadAllText(file)));
            }
            return items;
        }

        public TEntityType? GetById(TIdType id)
        {
            var file = Path.Combine(_folder, _getEntityFileNameByIdFunction(id));
            return File.Exists(file) ?
                    XMLUtilities.DeserializeFromString<TEntityType>(File.ReadAllText(file)) : default(TEntityType);
        }

        public void Add(TEntityType entity)
        {
            Update(entity);
        }

        public void Update(TEntityType entity)
        {
            var file = Path.Combine(_folder, _getEntityFileNameByEntityFunction(entity));
            File.WriteAllText(file, XMLUtilities.SerializeToString(entity));
        }

        public void Delete(TIdType id)
        {
            var file = Path.Combine(_folder, _getEntityFileNameByIdFunction(id));
            if (File.Exists(file)) File.Delete(file);
        }
    }
}
