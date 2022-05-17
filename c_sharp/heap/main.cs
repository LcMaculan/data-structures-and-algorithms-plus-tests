using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

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

  [TestFixture]
  public class HeapSpec {

    [Test]
    public void Heap() {
      var list = new List<int>();
      list.Add(10);
      list.Add(12);
      list.Add(14);
      list.Add(20);
      list.Add(30);
      var heap = new Heap(list);

      Assert.IsTrue(heap.values.All(list.Contains));
    }

    [Test]
    public void CheckIndexHeap() {
      var list = new List<int>();
      var heap = new Heap(list);

      Assert.AreEqual(heap.left(0), 1);
      Assert.AreEqual(heap.right(0), 2);
      //
      Assert.AreEqual(heap.left(1), 3);
      Assert.AreEqual(heap.right(1), 4);
      //
      Assert.AreEqual(heap.left(2), 5);
      Assert.AreEqual(heap.right(2), 6);
    }

    [Test]
    public void HeapifyHeap() {
      var list = new List<int>();
      list.Add(10);
      list.Add(30);
      list.Add(20);
      list.Add(14);
      list.Add(12);
      list.Add(40);
      list.Add(50);
      var heap = new Heap(list);

      heap.heapify(0);
      Assert.IsTrue(heap.values[heap.left(0)] <= heap.values[0]);
      Assert.IsTrue(heap.values[heap.right(0)] <= heap.values[0]);
    }

    [Test]
    public void CompleteHeapifyHeap() {
      var list = new List<int>();
      list.Add(10);
      list.Add(30);
      list.Add(20);
      list.Add(14);
      list.Add(12);
      list.Add(40);
      list.Add(50);
      var heap = new Heap(list);

      heap.build();
      for (int i = heap.values.Count - 1; i >= 1; i--) {
        if (heap.left(i) < heap.values.Count) {
          Assert.IsTrue(heap.values[heap.left(i)] <= heap.values[i]);
        }
        if (heap.right(i) < heap.values.Count) {
          Assert.IsTrue(heap.values[heap.right(i)] <= heap.values[i]);
        }
      }
    }

    [Test]
    public void SortHeap() {
      var list = new List<int>();
      list.Add(10);
      list.Add(30);
      list.Add(20);
      list.Add(14);
      list.Add(12);
      list.Add(40);
      list.Add(50);
      var heap = new Heap(list);

      heap.build();
      heap.sort();

      for (int i = heap.values.Count - 1; i > 0; i--) {
        Assert.IsTrue(heap.values[i-1] <= heap.values[i]);
      }
      Assert.IsTrue(heap.values.Count <= heap.size);
    }
  }

}