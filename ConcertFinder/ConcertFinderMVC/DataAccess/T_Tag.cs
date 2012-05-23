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
            return Id.Equals(tag.Id) && Name.Equals(tag.Name);
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public override int GetHashCode()
        {
            //Get hash code for the Name field if it is not null.
            int hashProductName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = Id.GetHashCode();

            //Calculate the hash code for the product.
            return hashProductName ^ hashProductCode;
        }
    }
}