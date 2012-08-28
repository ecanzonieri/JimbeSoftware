using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace JimbeCore.Domain.Model
{


    public abstract class Entity<T> : IEntity<T>, IBusinessComparable
    {
        public virtual T Id { get; private set; }

        private int? _cachedHashCode;

        private const int HashMultiplier = 31;

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity<T>;

            if (compareTo == null || !GetType().Equals(compareTo.GetTypeUnproxied()))
            {
                return false;
            }


            if (ReferenceEquals(this, compareTo))
            {
                return true;
            }

            if (HasSameNonDefaultIdAs(compareTo))
            {
                return true;
            }

            // Since the Ids aren't the same, both of them must be transient to 
            // compare domain signatures; because if one is transient and the 
            // other is a persisted entity, then they cannot be the same object.

            return IsTransient() && compareTo.IsTransient() && EqualsBusiness(compareTo);

        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object"/>.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode()
        {
            if (!_cachedHashCode.HasValue)
            {
                if (IsTransient())
                {
                    _cachedHashCode = base.GetHashCode();
                }
                else
                {
                    unchecked
                    {
                        // It's possible for two objects to return the same hash code based on 
                        // identically valued properties, even if they're of two different types, 
                        // so we include the object's type in the hash calculation
                        var hashCode = GetType().GetHashCode();
                        _cachedHashCode = (hashCode*HashMultiplier) ^ Id.GetHashCode();
                    }
                }
            }
            return _cachedHashCode.Value;
        }

        public abstract bool EqualsBusiness(IBusinessComparable comparable);

        public virtual bool IsTransient()
        {
            return Equals(Id, default(T));
        }


        /// <summary>
        ///     Returns true if self and the provided entity have the same Id values 
        ///     and the Ids are not of the default Id value
        /// </summary>
        private bool HasSameNonDefaultIdAs(Entity<T> compareTo)
        {
            return !this.IsTransient() && !compareTo.IsTransient() && this.Id.Equals(compareTo.Id);
        }

        /// <summary>
        ///     When NHibernate proxies objects, it masks the type of the actual entity object.
        ///     This wrapper burrows into the proxied object to get its actual type.
        /// 
        ///     Although this assumes NHibernate is being used, it doesn't require any NHibernate
        ///     related dependencies and has no bad side effects if NHibernate isn't being used.
        /// 
        ///     Related discussion is at http://groups.google.com/group/sharp-architecture/browse_thread/thread/ddd05f9baede023a ...thanks Jay Oliver!
        /// </summary>
        protected virtual Type GetTypeUnproxied()
        {
            return this.GetType();
        }
    }
}