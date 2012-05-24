using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConcertFinderMVC.DataAccess
{
    public partial class T_Tag : IEquatable<T_Tag>
    {
        public bool Equals(T_Tag tag)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(tag, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, tag)) return true;

            //Check whether the products' properties are equal.
            return Id.Equals(tag.Id);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            //Get hash code for the Id field.
            int hashIdCode = Id.GetHashCode();

            //Calculate the hash Id for the tag.
            return hashIdCode;
        }
    }
}