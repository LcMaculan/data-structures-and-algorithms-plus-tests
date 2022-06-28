using System;
using System.Collections.Generic;

namespace DisjointSetList {

  public class DisjointSetList {
    public DisjointSetList next;
    public DisjointSetList rap;
    public DisjointSetList last;
    public int length;
    public int? value;

    public DisjointSetList() {
      this.next = null;
      this.rap = this;
      this.last = this;
 
      this.value = null;
      this.length = 0;
    }

    public DisjointSetList(int value) : this() {
      this.value = value;
      this.length = 1;
    }

    public DisjointSetList find() {
      return this.rap;
    }

    private static void link(DisjointSetList x, DisjointSetList y) {
      var j = x.last;
      j.next = y;
      x.last = y.last;
      x.length += y.length;
      while (y != null) {
        y.rap = x.rap;
        y = y.next;
      }
    }

    public DisjointSetList union(DisjointSetList disjointSetList) {
      if (this.find() != disjointSetList.find()) {
        if (this.length > disjointSetList.length) {
          DisjointSetList.link(this, disjointSetList);
          return this;
        }
        else {
          DisjointSetList.link(disjointSetList, this);
          return disjointSetList;
        }
      }
      return this;
    }

    public List<int?> getValues() {
      var result = new List<int?>();
      var currentNode = this.find();

      while (currentNode != null) {
        result.Add(currentNode.value);
        currentNode = currentNode.next;
      }

      return result;
    }

  }

}