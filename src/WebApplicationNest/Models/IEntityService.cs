using System;
using System.Collections.Generic;

namespace WebApplicationNest.Models
{
    public interface IEntityService<TEntity, in TKey> where TEntity : class where TKey : IEquatable<TKey>
    {
        IEnumerable<TEntity> Find();

        IEnumerable<TEntity> Find(string searchterm);

        Ticket Post(TEntity ticket);

        Ticket Get(TKey ticketId);

        void Delete(TKey ticketId);
    }
}