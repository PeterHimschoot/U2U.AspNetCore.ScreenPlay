namespace U2U.AspNetCore.ScreenPlay {
  
  using System;
  using System.Collections.Generic;

  public class ArrayComparer<T> : IEqualityComparer<T[]> {
    private IEqualityComparer<T> innerComparer;
    
    public ArrayComparer(IEqualityComparer<T> comparer) {
      this.innerComparer = comparer ?? throw new ArgumentNullException(nameof(comparer));
    }

    public bool Equals(T[] x, T[] y)
    {
      if(object.ReferenceEquals(x,y)) {
        return true;
      }
      
      if( x.Length != y.Length) {
        return false;
      }
      
      for(int i = 0; i < x.Length; i+=1) {
        if( innerComparer.Equals( x[i], y[i]) == false ) {
          return false;
        }
      }
      return true;
    }

    public int GetHashCode(T[] obj)
    {
      throw new System.NotImplementedException();
    }
  }
}
