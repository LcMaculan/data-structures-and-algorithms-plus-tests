using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Heap {

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