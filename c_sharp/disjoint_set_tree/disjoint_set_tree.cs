using System;
using System.Collections.Generic;

namespace DisjointSetTree {

  public class DisjointSetTree {
    public DisjointSetTree p;
    public int? value;
    public int rank;

    public DisjointSetTree() {
      this.p = this;
      this.value = null;
      this.rank = 0;
    }

    public DisjointSetTree(int value) : this() {
      this.value = value;
    }

    public DisjointSetTree find() {
      DisjointSetTree currentTree = this;
      while (currentTree != currentTree.p) {
        currentTree = currentTree.p;
      }
      return currentTree;
    }

    public DisjointSetTree union(DisjointSetTree disjointSetTree) {
      if (this.find() != disjointSetTree.find()) {
        var x = this.find();
        var y = disjointSetTree.find();
        if (x.rank > y.rank) {
          y.p = x;
          return x;
        }
        else {
          x.p = y;
          if (x.rank == y.rank) {
            y.rank += 1;
          }
          return y;
        }
      }
      return this;
    }

  }

}