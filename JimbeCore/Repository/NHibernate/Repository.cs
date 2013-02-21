using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JimbeCore.Repository.Interfaces;
using FluentNHibernate;
using NHi = NHibernate;
using NHibernate.Linq;

namespace JimbeCore.Repository.NHibernate
{
    public class Repository<TKey, T> : IRepository<TKey,T> where T : class
    {
        private NHi.ISession _session;

        public Repository(NHi.ISession session)
        {
            _session = session;
        }

        #region IReadOnlyRepository<Tkey,T> Members

        public IQueryable<T> All()
        {
            return _session.Query<T>();
        }

        public T FindBy(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return FilterBy(expression).SingleOrDefault();
        }

        public IQueryable<T> FilterBy(System.Linq.Expressions.Expression<Func<T, bool>> expression)
        {
            return All().Where(expression).AsQueryable();
        }

        public T FindBy(TKey id)
        {
            return _session.Get<T>(id);
        }

        #endregion

        #region IPersistRepository<T> Members

        public bool Add(T entity)
        {
            try
            {
                _session.Transaction.Begin();
                _session.Save(entity);
                _session.Transaction.Commit();
                return true;
            } catch  (NHi.Exceptions.GenericADOException e) {
                _session.Transaction.Rollback();
                return false;
            }
        }

        public bool Add(IEnumerable<T> items)
        {
            try
            {
                _session.Transaction.Begin();
                foreach (T item in items)
                    _session.Save(item);
                _session.Transaction.Commit();
                return true;
            }
            catch (NHi.Exceptions.GenericADOException e)
            {
                _session.Transaction.Rollback();
                return false;
            }
        }

        public bool Update(T entity)
        {
            try
            {
                _session.Transaction.Begin();
                _session.Update(entity);
                _session.Transaction.Commit();
                return true;
            }
            catch (NHi.Exceptions.GenericADOException e)
            {
                _session.Transaction.Rollback();
                return false;
            }
        }

        public bool Delete(T entity)
        {
            try
            {
                _session.Transaction.Begin();
                _session.Delete(entity);
                _session.Transaction.Commit();
                return true;
            }
            catch (NHi.Exceptions.GenericADOException e)
            {
                _session.Transaction.Rollback();
                return false;
            }
        }

        public bool Delete(IEnumerable<T> entities)
        {
            try
            {
                _session.Transaction.Begin();
                foreach (T entity in entities)
                {
                    _session.Delete(entity);
                }
                _session.Transaction.Commit();
                return true;
            }
            catch (NHi.Exceptions.GenericADOException e)
            {
                _session.Transaction.Rollback();
                return false;
            }
        }

        #endregion

        #region Implementation of IDisposable

        public void Dispose()
        {
            _session.Dispose();
            _session = null;
        }

        #endregion
    }
}
