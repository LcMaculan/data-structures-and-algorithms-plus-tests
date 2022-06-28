using System;
using System.Collections.Generic;
using System.Linq;

namespace Heap {
  public class Heap {
    public List<int> values;
    public int size;

    public Heap(List<int> values) {
      this.values = values;
      this.size = values.Count;
    }

    public void sort() {
      this.build();
      var realSize = this.size;
      for (int i = this.size - 1; i >= 0; i--) {
        this.swap(0, i);
        this.size -= 1;
        this.heapify(0);
      }
      this.size = realSize;
    }

    public void build() {
      var i = this.values.Count / 2;
      while (i >= 0) {
        this.heapify(i);
        i--;
      }
    }

    public void heapify(int i) {
      var l = this.left(i);
      var r = this.right(i);

      var largest = i;

      if (l < this.size && this.values[l] > this.values[largest]) {
        largest = l;
      }

      if (r < this.size && this.values[r] > this.values[largest]) {
        largest = r;
      }

      if (i != largest) {
        this.swap(i, largest);
        this.heapify(largest);
      }
    }

    private static int _left(int i) {
      return (i + 1) * 2 - 1;
    }

    private static int _right(int i) {
      return (i + 1) * 2 + 1 - 1;
    }

    public int left(int i) {
      return Heap._left(i);
    }

    public int right(int i) {
      return Heap._right(i);
    }

    private void swap(int i, int j) {
      var t = this.values[i];
      this.values[i] = this.values[j];
      this.values[j] = t;
    }
  }

}