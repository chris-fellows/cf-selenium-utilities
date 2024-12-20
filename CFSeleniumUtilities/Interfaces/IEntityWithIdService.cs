using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFSeleniumUtilities.Interfaces
{
    public interface IEntityWithIdService<TEntityType, TIdType>
    {
        TEntityType? GetById(TIdType id);

        List<TEntityType> GetAll();

        void Add(TEntityType entity);

        void Update(TEntityType entity);

        void Delete(TIdType id);
    }
}
