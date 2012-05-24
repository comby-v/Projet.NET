using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class T_Event : IEquatable<T_Event>
    {
        public bool Equals(T_Event evnt)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(evnt, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, evnt)) return true;

            //Check whether the products' properties are equal.
            return Id.Equals(evnt.Id);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            //Get hash code for the Id field.
            int hashIdCode = Id.GetHashCode();

            //Calculate the hash code for the event.
            return hashIdCode;
        }
    }
}